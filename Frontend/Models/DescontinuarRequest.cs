using System.Text.Json.Serialization;

namespace ProductosDescontinuados.Models
{
    public class DescontinuarRequest
    {
        [JsonPropertyName("codigoBarras")]
        public string CodigoBarras { get; set; } = string.Empty;
    }
}
