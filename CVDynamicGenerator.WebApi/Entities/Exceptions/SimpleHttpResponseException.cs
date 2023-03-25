using System;
using System.Net;

namespace GseControl.Authentication.WebApi.Entities.Exceptions
{
    public class SimpleHttpResponseException : Exception
    {
        public int StatusCode { get; private set; }

        public SimpleHttpResponseException(int statusCode, string content) : base(content)
        {
            StatusCode = statusCode;
        }
    }
}
