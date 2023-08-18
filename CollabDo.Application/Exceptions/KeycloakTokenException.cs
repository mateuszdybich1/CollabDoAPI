using System.Runtime.Serialization;

namespace CollabDo.Application.Exceptions
{
    [Serializable]
    public class KeycloakTokenException : Exception
    {
        public KeycloakTokenException(string message) : base(message)
        {
        }

        public KeycloakTokenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KeycloakTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
