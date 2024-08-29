using System.Text.Json;

namespace webApi.Errors
{
    public class ApiError(int errorCode, string errorMessage, string? errorDetails = null)
    {
        public int ErrorCode { get; set; } = errorCode;
        public string ErrorMessage { get; set; } = errorMessage;
        public string? ErrorDetails { get; set; } = errorDetails;

        public override string ToString()
        {
            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return JsonSerializer.Serialize(this,options);
        }
    }
}
