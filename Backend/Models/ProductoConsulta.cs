namespace descontinuarProductosBackend.Models
{
    public class ProductoConsulta
    {
        public int IdProducto { get; set; }
        public string CodigoBarras { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Existencia { get; set; }
    }
}
