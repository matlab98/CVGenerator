using CVDynamicGenerator.WebApi.Entities.DTO;
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CVDynamicGenerator.WebApi.Entities.Exceptions
{
    public class LoginException : Exception
    {
        public DefaultResponse loginResponse { get; set; }
        public LoginException()
        {
        }

        public LoginException(string message) : base(message)
        {
            loginResponse = new DefaultResponse()
            {
                statusCode = StatusCodes.Status401Unauthorized,
                statusMessage = message
            };
        }

        public LoginException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected LoginException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
