using Newtonsoft.Json;
using CollabDo.Infrastructure.Configuration;
using CollabDo.Application.Exceptions;

namespace CollabDo.Infrastructure
{
    public class KeycloakToken
    {
        public static async Task<KeycloakTokenData> GetToken(HttpClient httpClient, AuthConfiguration configuration)
        {
            string keycloakBaseUrl = configuration.ServerAddress;
            string realm = configuration.Realm;
            string clientId = configuration.ClientId;
            string clientSecret = configuration.ClientSecret;

            string tokenEndpoint = $"{keycloakBaseUrl}/auth/realms/{realm}/protocol/openid-connect/token";

            HttpRequestMessage tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
            tokenRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
            });

            HttpResponseMessage tokenResponse = await httpClient.SendAsync(tokenRequest);
            string tokenResponseContent = await tokenResponse.Content.ReadAsStringAsync();
            Dictionary<string, string> tokenData = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenResponseContent);

            if (!tokenResponse.IsSuccessStatusCode || !tokenData.ContainsKey("access_token"))
            {
                throw new KeycloakTokenException("Failed to obtain access token from Keycloak.");
            }

            return new KeycloakTokenData(tokenData["access_token"], tokenData.ContainsKey("refresh_token") ? tokenData["refresh_token"] : null);
        }
        
    }
}
