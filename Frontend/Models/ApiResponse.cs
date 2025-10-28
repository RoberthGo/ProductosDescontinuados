using System.Text.Json.Serialization;

namespace ProductosDescontinuados.Models
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }
        
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        
        [JsonPropertyName("data")]
        public T? Data { get; set; }
    }
}
