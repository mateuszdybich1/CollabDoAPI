using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CollabDo.Infrastructure.Configuration;

namespace CollabDo.Infrastructure
{
    public class KeycloakToken
    {
        
        public static async Task<string> GetToken(HttpClient _httpClient, AuthConfiguration _configuration)
        {
            var keycloakBaseUrl = _configuration.ServerAddress;
            var realm = _configuration.Realm;
            var clientId = _configuration.ClientId;
            var clientSecret = _configuration.ClientSecret;


            var tokenEndpoint = $"{keycloakBaseUrl}/auth/realms/{realm}/protocol/openid-connect/token";

            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
            tokenRequest.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
            });

            var tokenResponse = await _httpClient.SendAsync(tokenRequest);
            var tokenResponseContent = await tokenResponse.Content.ReadAsStringAsync();
            var tokenData = JsonConvert.DeserializeObject<Dictionary<string, string>>(tokenResponseContent);

            if (!tokenResponse.IsSuccessStatusCode || !tokenData.ContainsKey("access_token"))
            {

                throw new Exception("Failed to obtain access token from Keycloak.");
            }


            return tokenData["access_token"];
        }
    }
}
