using CollabDo.Application.Entities;
using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;
using CollabDo.Infrastructure.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            KeycloakTokenData token = await KeycloakToken.GetToken(_httpClient, _configuration);

            HttpRequestMessage createUserRequest = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress.ToString());

            createUserRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);

            var content = new
            {
                username = user.Username,
                email = user.Email,
                enabled = true,
                credentials = new[]
                {
                    new
                    {
                        type = "password",
                        value = user.Password,
                        temporary = false
                    }
                }
            };
            createUserRequest.Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.SendAsync(createUserRequest);

            KeycloakValidation.CreateUserValidation(response);

            Uri location = response.Headers.Location;

            HttpRequestMessage getUserInformationRequest = new HttpRequestMessage(HttpMethod.Get, location);
            getUserInformationRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            response = await _httpClient.SendAsync(getUserInformationRequest);

            string userInformation = await response.Content.ReadAsStringAsync();

            KeycloakUserRequestModel responseUser = JsonConvert.DeserializeObject<KeycloakUserRequestModel>(userInformation);

            return responseUser.Id;
        }

        public async Task<bool> EmailExists(string email)
        {
            
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await KeycloakToken.GetToken(_httpClient, _configuration)).AccessToken); ;

            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress.ToString() + $"?email={Uri.EscapeDataString(email)}");
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                List<UserEntity> users = JsonConvert.DeserializeObject<List<UserEntity>>(responseContent);
                return users.Count > 0;
            }
            else
            {
                throw new EntityNotFoundException($"Failed to check user existence in Keycloak. Status code: {response.StatusCode}");
            }
        }

        public async Task<bool> UsernameExists(string username)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await KeycloakToken.GetToken(_httpClient, _configuration)).AccessToken);

            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress.ToString() + $"?username={Uri.EscapeDataString(username)}");
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                List<UserEntity> users = JsonConvert.DeserializeObject<List<UserEntity>>(responseContent);
                return users.Count > 0;
            }
            else
            {
                throw new EntityNotFoundException($"Failed to check user existence in Keycloak. Status code: {response.StatusCode}");
            }

        }

        public async Task<Guid> GetUserIdByEmail(string email)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await KeycloakToken.GetToken(_httpClient, _configuration)).AccessToken);

            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress.ToString() + $"?email={Uri.EscapeDataString(email)}");
            


            if (!response.IsSuccessStatusCode)
            {
                throw new EntityNotFoundException($"Email: {email} does not exists");

            }

            string responseContent = await response.Content.ReadAsStringAsync();
            List<KeycloakUserRequestModel> users = JsonConvert.DeserializeObject<List<KeycloakUserRequestModel>>(responseContent);

            var user = users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                throw new EntityNotFoundException($"Email: {email} does not exists");
            }
            return user.Id;
        }   
    }
}
