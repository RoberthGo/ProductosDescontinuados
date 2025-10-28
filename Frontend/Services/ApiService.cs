using System.Net.Http;
using System.Text;
using System.Text.Json;
using ProductosDescontinuados.Models;

namespace ProductosDescontinuados.Services
{
    public class ApiService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ApiService(string baseUrl = "http://localhost:5053")
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        public async Task<ProductoConsulta?> ConsultarProductoAsync(string codigoBarras)
        {
            try
            {
                var url = $"/api/productos/consultar/{codigoBarras}";
                var response = await _httpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                System.IO.File.WriteAllText("debug_response.json", json);

                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }

                var options = new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var apiResponse = JsonSerializer.Deserialize<ApiResponse<ProductoConsulta>>(json, options);
                return apiResponse?.Data;
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error al deserializar JSON: {ex.Message}", ex);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión con el servidor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al consultar producto: {ex.Message}", ex);
            }
        }

        public async Task<bool> DescontinuarProductoAsync(string codigoBarras)
        {
            try
            {
                var request = new DescontinuarRequest { CodigoBarras = codigoBarras };
                var options = new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                
                var json = JsonSerializer.Serialize(request, options);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/api/productos/descontinuar", content);

                if (!response.IsSuccessStatusCode)
                {
                    var errorJson = await response.Content.ReadAsStringAsync();
                    var errorResponse = JsonSerializer.Deserialize<ApiResponse<object>>(errorJson, options);
                    throw new Exception(errorResponse?.Message ?? "Error desconocido al descontinuar producto");
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error de conexión con el servidor: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al descontinuar producto: {ex.Message}", ex);
            }
        }

        public async Task<bool> VerificarConexionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("/api/productos/health");
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}
