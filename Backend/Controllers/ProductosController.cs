using Microsoft.AspNetCore.Mvc;
using descontinuarProductosBackend.Models;
using descontinuarProductosBackend.Services;

namespace descontinuarProductosBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;
        private readonly ILogger<ProductosController> _logger;

        public ProductosController(IProductoService productoService, ILogger<ProductosController> logger)
        {
            _productoService = productoService;
            _logger = logger;
        }

        [HttpGet("consultar/{codigoBarras}")]
        [ProducesResponseType(typeof(ApiResponse<ProductoConsulta>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<ProductoConsulta>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<ProductoConsulta>>> ConsultarProducto(string codigoBarras)
        {
            try
            {
                _logger.LogInformation("Consultando producto con código de barras: {CodigoBarras}", codigoBarras);

                var producto = await _productoService.ConsultarProductoPorCodigoBarrasAsync(codigoBarras);

                if (producto == null)
                {
                    return NotFound(new ApiResponse<ProductoConsulta>
                    {
                        Success = false,
                        Message = $"No se encontró el producto con código de barras: {codigoBarras}",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<ProductoConsulta>
                {
                    Success = true,
                    Message = "Producto encontrado exitosamente",
                    Data = producto
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al consultar producto con código de barras: {CodigoBarras}", codigoBarras);
                
                return StatusCode(500, new ApiResponse<ProductoConsulta>
                {
                    Success = false,
                    Message = "Error interno del servidor al consultar el producto",
                    Data = null
                });
            }
        }

        [HttpPost("descontinuar")]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiResponse<object>>> DescontinuarProducto([FromBody] DescontinuarRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.CodigoBarras))
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "El código de barras es requerido",
                        Data = null
                    });
                }

                _logger.LogInformation("Descontinuando producto con código de barras: {CodigoBarras}", request.CodigoBarras);

                bool resultado = await _productoService.DescontinuarProductoAsync(request.CodigoBarras);

                if (!resultado)
                {
                    return BadRequest(new ApiResponse<object>
                    {
                        Success = false,
                        Message = "No se pudo descontinuar el producto. Verifique que el código de barras sea válido.",
                        Data = null
                    });
                }

                return Ok(new ApiResponse<object>
                {
                    Success = true,
                    Message = "Producto descontinuado exitosamente",
                    Data = new { CodigoBarras = request.CodigoBarras }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al descontinuar producto con código de barras: {CodigoBarras}", request.CodigoBarras);
                
                return StatusCode(500, new ApiResponse<object>
                {
                    Success = false,
                    Message = "Error interno del servidor al descontinuar el producto",
                    Data = null
                });
            }
        }

        [HttpGet("health")]
        public IActionResult Health()
        {
            return Ok(new
            {
                Status = "Healthy",
                Message = "API de Productos funcionando correctamente",
                Timestamp = DateTime.Now
            });
        }
    }
}
