using CollabDo.Application.Entities;
using CollabDo.Application.Exceptions;
using CollabDo.Application.IRepositories;
using CollabDo.Web.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CollabDo.Web.Repositories
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

            HttpRequestMessage createUserRequest = new HttpRequestMessage(HttpMethod.Post, $"{_httpClient.BaseAddress}/users");

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

            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/users?email={Uri.EscapeDataString(email)}");
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                List<UserEntity> users = JsonConvert.DeserializeObject<List<UserEntity>>(responseContent);
                if (users.Count > 0)
                {
                    return users.Any(x => x.Email.ToLower() == email.ToLower());
                }
                return false;
            }
            else
            {
                throw new EntityNotFoundException($"Failed to check user existence in Keycloak. Status code: {response.StatusCode}");
            }
        }

        public async Task<bool> UsernameExists(string username)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await KeycloakToken.GetToken(_httpClient, _configuration)).AccessToken);

            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/users?username={Uri.EscapeDataString(username)}");
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                List<UserEntity> users = JsonConvert.DeserializeObject<List<UserEntity>>(responseContent);
                if(users.Count > 0)
                {
                   return users.Any(x => x.Username.ToLower() == username.ToLower());
                }
                return false;
            }
            else
            {
                throw new EntityNotFoundException($"Failed to check user existence in Keycloak. Status code: {response.StatusCode}");
            }

        }

        public async Task<Guid> GetUserIdByEmail(string email)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await KeycloakToken.GetToken(_httpClient, _configuration)).AccessToken);

            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/users?email={Uri.EscapeDataString(email)}");
            
     
            string responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                List<KeycloakUserRequestModel> users = JsonConvert.DeserializeObject<List<KeycloakUserRequestModel>>(responseContent);
                var user = users.SingleOrDefault(u => u.Email == email);
                if (user != null)
                {
                    return user.Id;
                }
                throw new EntityNotFoundException($"Email does not exist");
            }
            else
            {
                throw new EntityNotFoundException($"Failed to check user existence in Keycloak. Status code: {response.StatusCode}");
            }
            
        }

        public async Task<KeycloakUserRequestModel> GetUser(Guid userId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await KeycloakToken.GetToken(_httpClient, _configuration)).AccessToken);

            HttpResponseMessage response = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/users/{Uri.EscapeDataString(userId.ToString())}");


            if (!response.IsSuccessStatusCode)
            {
                throw new EntityNotFoundException($"ID: {userId} does not exists");

            }

            string responseContent = await response.Content.ReadAsStringAsync();
            KeycloakUserRequestModel user = JsonConvert.DeserializeObject<KeycloakUserRequestModel>(responseContent);

            if (user == null)
            {
                throw new EntityNotFoundException($"ID: {userId} does not exist");
            }

            return user;
        }

        
        public async Task VerifyEmail(Guid userId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await KeycloakToken.GetToken(_httpClient, _configuration)).AccessToken);

            var actionContent = new StringContent("[\"VERIFY_EMAIL\"]", Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/users/{Uri.EscapeDataString(userId.ToString())}/execute-actions-email", actionContent);


            if (!response.IsSuccessStatusCode)
            {
                throw new ValidationException("Error");
            }
            
        }

        public async Task ResetPassword(Guid userId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (await KeycloakToken.GetToken(_httpClient, _configuration)).AccessToken);

            var actionContent = new StringContent("[\"UPDATE_PASSWORD\"]", Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _httpClient.PutAsync($"{_httpClient.BaseAddress}/users/{Uri.EscapeDataString(userId.ToString())}/execute-actions-email", actionContent);


            if (!response.IsSuccessStatusCode)
            {
                throw new ValidationException("Error");
            }

        }
    }

}
