using System.Text.Json.Serialization;

namespace ProductosDescontinuados.Models
{
    public class ProductoConsulta
    {
        [JsonPropertyName("idProducto")]
        public int IdProducto { get; set; }
        
        [JsonPropertyName("codigoBarras")]
        public string CodigoBarras { get; set; } = string.Empty;
        
        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;
        
        [JsonPropertyName("descripcion")]
        public string? Descripcion { get; set; }
        
        [JsonPropertyName("precioVenta")]
        public decimal PrecioVenta { get; set; }
        
        [JsonPropertyName("existencia")]
        public int Existencia { get; set; }
    }
}
