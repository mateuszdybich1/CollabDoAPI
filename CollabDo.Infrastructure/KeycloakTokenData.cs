namespace CollabDo.Infrastructure
{
    public class KeycloakTokenData
    {
        public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }

        public KeycloakTokenData(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
