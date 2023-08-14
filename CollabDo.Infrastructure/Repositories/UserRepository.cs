using CollabDo.Application.Entities;
using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;
using CollabDo.Infrastructure.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CollabDo.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _httpClient;
        private readonly AuthConfiguration _configuration;

        public UserRepository(HttpClient httpClient, AuthConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Guid> AddUser(UserEntity user)
        {
            string token = await KeycloakToken.GetToken(_httpClient, _configuration);

            string keycloakBaseUrl = _configuration.ServerAddress;
            string realm = _configuration.Realm;


            string createUserUrl = $"{keycloakBaseUrl}/auth/admin/realms/{realm}/users";
            HttpRequestMessage createUserRequest = new HttpRequestMessage(HttpMethod.Post, createUserUrl);

            createUserRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var content = new
            {
                username = user.Username,
                email = user.Email,
                credentials = new[]
                {
                    new
                    {
                        type = "password",
                        value = user.Password,
                        temporary = false
                    }
                },
                enabled = true
            };
            createUserRequest.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.SendAsync(createUserRequest);

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new ValidationException($"Failed to create user in Keycloak.");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                throw new ValidationException($"User exists.");
            }

            Uri location = response.Headers.Location;

            HttpRequestMessage getUserInformationRequest = new HttpRequestMessage(HttpMethod.Get, location);
            getUserInformationRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            response = await _httpClient.SendAsync(getUserInformationRequest);

            string userInformation = await response.Content.ReadAsStringAsync();

            KeycloakUserRequestModel responseUser = JsonConvert.DeserializeObject<KeycloakUserRequestModel>(userInformation);


            string userId = responseUser.Id.ToString();
            string assignRoleUrl = $"{keycloakBaseUrl}/auth/admin/realms/{realm}/users/{userId}/role-mappings/realm";
            var roleAssignment = new
            {
                roles = new[] { new { name = user.Role.ToString() } }
            };

            HttpRequestMessage assignRoleRequest = new HttpRequestMessage(HttpMethod.Post, assignRoleUrl);
            assignRoleRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            assignRoleRequest.Content = new StringContent(JsonConvert.SerializeObject(roleAssignment), Encoding.UTF8, "application/json");

            response = await _httpClient.SendAsync(assignRoleRequest);

            return responseUser.Id;
        }

        public async Task<bool> EmailExists(string email)
        {
            string keycloakBaseUrl = _configuration.ServerAddress;
            string realm = _configuration.Realm;


            string usersUrl = $"{keycloakBaseUrl}/auth/admin/realms/{realm}/users?email={Uri.EscapeDataString(email)}";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await KeycloakToken.GetToken(_httpClient, _configuration));

                HttpResponseMessage response = await httpClient.GetAsync(usersUrl);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<UserEntity> users = JsonConvert.DeserializeObject<List<UserEntity>>(responseContent);
                    return users.Count > 0;
                }
                else
                {

                    throw new Exception($"Failed to check user existence in Keycloak. Status code: {response.StatusCode}");
                }
            }
        }

        public async Task<bool> UsernameExists(string username)
        {
            string keycloakBaseUrl = _configuration.ServerAddress;
            string realm = _configuration.Realm;


            string usersUrl = $"{keycloakBaseUrl}/auth/admin/realms/{realm}/users?username={Uri.EscapeDataString(username)}";

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await KeycloakToken.GetToken(_httpClient, _configuration));

                HttpResponseMessage response = await httpClient.GetAsync(usersUrl);
                string responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    List<UserEntity> users = JsonConvert.DeserializeObject<List<UserEntity>>(responseContent);
                    return users.Count > 0;
                }
                else
                {

                    throw new Exception($"Failed to check user existence in Keycloak. Status code: {response.StatusCode}");
                }
            }
        }
    }
}
