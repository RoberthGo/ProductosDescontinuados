using MySql.Data.MySqlClient;
using descontinuarProductosBackend.Models;
using System.Data;

namespace descontinuarProductosBackend.Services
{
    public interface IProductoService
    {
        Task<ProductoConsulta?> ConsultarProductoPorCodigoBarrasAsync(string codigoBarras);
        Task<bool> DescontinuarProductoAsync(string codigoBarras);
    }

    public class ProductoService : IProductoService
    {
        private readonly string _connectionString;
        private readonly ILogger<ProductoService> _logger;

        public ProductoService(IConfiguration configuration, ILogger<ProductoService> logger)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            _logger = logger;
        }

        public async Task<ProductoConsulta?> ConsultarProductoPorCodigoBarrasAsync(string codigoBarras)
        {
            ProductoConsulta? producto = null;

            try
            {
                using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();

                using var command = new MySqlCommand("sp_consultar_productos", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@p_codigo_barras", codigoBarras);

                using var reader = await command.ExecuteReaderAsync();
                
                if (await reader.ReadAsync())
                {
                    producto = new ProductoConsulta
                    {
                        IdProducto = reader.GetInt32("id_producto"),
                        CodigoBarras = reader.GetString("codigo_barras"),
                        Nombre = reader.GetString("nombre"),
                        Descripcion = reader.IsDBNull(reader.GetOrdinal("descripcion")) 
                            ? null 
                            : reader.GetString("descripcion"),
                        PrecioVenta = reader.GetDecimal("precio_venta"),
                        Existencia = reader.GetInt32("existencia")
                    };
                }
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "Error de MySQL al consultar producto con código de barras: {CodigoBarras}", codigoBarras);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar producto con código de barras: {CodigoBarras}", codigoBarras);
                throw;
            }

            return producto;
        }

        public async Task<bool> DescontinuarProductoAsync(string codigoBarras)
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();

                // Iniciar transacción
                using var transaction = await connection.BeginTransactionAsync();

                try
                {
                    using var command = new MySqlCommand(
                        "UPDATE productos SET descontinuado = TRUE WHERE codigo_barras = @codigo_barras",
                        connection,
                        (MySqlTransaction)transaction
                    );

                    command.Parameters.AddWithValue("@codigo_barras", codigoBarras);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    // Confirmar transacción
                    await transaction.CommitAsync();

                    return rowsAffected > 0;
                }
                catch
                {
                    // Revertir transacción en caso de error
                    await transaction.RollbackAsync();
                    throw;
                }
            }
            catch (MySqlException ex)
            {
                _logger.LogError(ex, "Error de MySQL al descontinuar producto con código de barras: {CodigoBarras}", codigoBarras);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al descontinuar producto con código de barras: {CodigoBarras}", codigoBarras);
                throw;
            }
        }
    }
}
