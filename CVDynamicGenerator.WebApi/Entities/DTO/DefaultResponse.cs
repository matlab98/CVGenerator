using System.Text.Json.Serialization;

namespace CVDynamicGenerator.WebApi.Entities.DTO
{
    public class DefaultResponse
    {
    public int statusCode { get; set; }

        public string statusMessage { get; set; }

        public Result result { get; set; }
    }

    public class Result
    {
        public string base64 { get; set; }
    }
}
