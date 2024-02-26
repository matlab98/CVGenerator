using CVDynamicGenerator.WebApi.Entities.DTO;
using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GseControl.Authentication.WebApi.Entities.Exceptions
{
    public class XException : Exception
    {
        public DefaultResponse defaultResponse { get; set; }
        public XException()
        {
        }

        public XException(string message) : base(message)
        {
            defaultResponse = new DefaultResponse()
            {
                statusCode = StatusCodes.Status500InternalServerError,
                statusMessage = $"Error interno {message}"
            };
        }
    }
}
