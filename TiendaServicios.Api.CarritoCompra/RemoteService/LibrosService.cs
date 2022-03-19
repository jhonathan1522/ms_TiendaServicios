using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TiendaServicios.Api.CarritoCompra.RemoteInterface;
using TiendaServicios.Api.CarritoCompra.RemoteModel;

namespace TiendaServicios.Api.CarritoCompra.RemoteService
{
    public class LibrosService : ILibroService
    {
        private readonly IHttpClientFactory _httpClient;

        // cada vez que se trabaje con ILogger se le debe indicar la clase donde va trabajar
        private readonly ILogger<LibrosService> _logger;

        public LibrosService(IHttpClientFactory httpClient, ILogger<LibrosService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<(bool resultado, LibroRemote Libro, string ErrorMessage)> GetLibro(Guid LibroId)
        {
            try
            {
                // 
                var cliente = _httpClient.CreateClient("Libros");
                // el get async lo que hace es invocar al endPoint que yo quiero. en este caso devuelve los parametros del libro
                // el endpoint es el nombre del controllador y recibe un parametro.
                var response = await cliente.GetAsync($"api/LibroMaterial/{LibroId}");
                if (response.IsSuccessStatusCode) {
                    var contenido = await response.Content.ReadAsStringAsync();
                    // ahora le colocamos una propiedad para que no de problema de mayusculas y minusculas dentro del json devuelto.
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

                    var resultado = JsonSerializer.Deserialize<LibroRemote>(contenido, options);

                    return (true, resultado, null);
                }
                // si es incorrecta
                return (false, null, response.ReasonPhrase);
            }
            catch (Exception e)
            {
                _logger?.LogError(e.ToString());
                return (false,null,e.Message);
            }
        }
    }
}
