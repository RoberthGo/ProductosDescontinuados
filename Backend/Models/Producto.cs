namespace descontinuarProductosBackend.Models
{
    public class Producto
    {
        public int IdProducto { get; set; }
        public string CodigoBarras { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal? PrecioCompra { get; set; }
        public int Existencia { get; set; }
        public bool Descontinuado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
