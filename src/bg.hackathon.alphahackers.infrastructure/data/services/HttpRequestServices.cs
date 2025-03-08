using bg.hackathon.alphahackers.application.data.interfaces.services;
using Newtonsoft.Json;
using Serilog;
using System.Runtime.CompilerServices;
using System.Text;

namespace bg.hackathon.alphahackers.infrastructure.data.services
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly IHttpClientFactory _httpClient;
        public HttpRequestService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TDestination> ExecuteRestRequestAsync<TSource, TDestination>(
        string url,
        HttpMethod method,
        Dictionary<string, string> headers = null,
        Dictionary<string, string> queryParams = null,
        object content = null,
        int timeoutMilliseconds = 10000,
        Func<TSource, TDestination> mapFunction = null,
        JsonSerializerSettings jsonSettings = null,
        [CallerMemberName] string callerName = null)
        {
            ValidateParameters<TSource, TDestination>(url, method, timeoutMilliseconds, content, mapFunction);
            Log.Information("[{method}] {Caller} IN", method, callerName);
            var client = _httpClient.CreateClient();
            client.Timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
            var requestMessage = BuildHttpRequestMessage(url, method, headers, queryParams, content);

            try
            {
                var responseMessage = await client.SendAsync(requestMessage).ConfigureAwait(false);
                var bodyResponse = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                Log.Information("[{method}] - {StatusCode} {Caller} ", method, responseMessage.StatusCode, callerName);

                responseMessage.EnsureSuccessStatusCode();

                var responseData = JsonConvert.DeserializeObject<TSource>(bodyResponse, jsonSettings);

                var result = mapFunction != null ? mapFunction(responseData) : (TDestination)(object)responseData;

                return result;
            }
            catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
            {
                Log.Error(ex, "La solicitud [{method}] desde {Caller} ha excedido el tiempo de espera.", method, callerName);
                throw new TimeoutException("La solicitud ha excedido el tiempo de espera establecido.", ex);
            }
            catch (HttpRequestException ex)
            {
                Log.Error(ex, "Error en la solicitud [{method}] desde {Caller}: {Message}", method, callerName, ex.Message);
                throw new HttpRequestException($"Error al realizar la solicitud HTTP: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error inesperado en la solicitud {method} desde {Caller}: {Message}", method, callerName, ex.Message);
                throw;
            }
            finally
            {
                Log.Information("[{method}] {Caller} OUT", method, callerName);
            }
        }

        /// <summary>
        /// Valida los parámetros de entrada para garantizar que los valores requeridos sean correctos y no nulos.
        /// 
        /// Las validaciones incluyen:
        /// - Verificar que la URL no sea nula o vacía.
        /// - Verificar que el método HTTP no sea nulo.
        /// - Verificar que el tiempo de espera sea un valor positivo.
        /// - Asegurarse de que el contenido no sea nulo en solicitudes POST o PUT.
        /// - Asegurarse de que haya una función de mapeo si los tipos TSource y TDestination son diferentes.
        /// </summary>
        /// <typeparam name="TSource">El tipo de los datos de respuesta que se deserializan.</typeparam>
        /// <typeparam name="TDestination">El tipo de los datos mapeados que se devuelven.</typeparam>
        /// <param name="url">La URL a la que se va a realizar la solicitud.</param>
        /// <param name="method">El método HTTP (GET, POST, PUT, DELETE) que se va a utilizar en la solicitud.</param>
        /// <param name="timeoutMilliseconds">El tiempo máximo de espera para la solicitud en milisegundos.</param>
        /// <param name="content">El contenido del cuerpo de la solicitud (solo para POST o PUT).</param>
        /// <param name="mapFunction">Función opcional para mapear la respuesta de TSource a TDestination.</param>
        /// <exception cref="ArgumentException">Lanzada si la URL es nula o vacía.</exception>
        /// <exception cref="ArgumentNullException">Lanzada si el método HTTP o el contenido (en POST o PUT) es nulo.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Lanzada si el tiempo de espera es menor o igual a cero.</exception>
        /// <exception cref="InvalidOperationException">Lanzada si los tipos TSource y TDestination son diferentes y no se proporciona una función de mapeo.</exception>
        private void ValidateParameters<TSource, TDestination>(
        string url,
        HttpMethod method,
        int timeoutMilliseconds,
        object content,
        Func<TSource, TDestination> mapFunction)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new ArgumentException("La URL no puede ser nula o vacía.", nameof(url));
            }

            if (method == null)
            {
                throw new ArgumentNullException(nameof(method), "El método HTTP no puede ser nulo.");
            }

            if (timeoutMilliseconds <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(timeoutMilliseconds), "El tiempo de espera debe ser un valor positivo.");
            }

            if ((method == HttpMethod.Post || method == HttpMethod.Put) && content == null)
            {
                throw new ArgumentNullException(nameof(content), "El contenido no puede ser nulo para solicitudes POST o PUT.");
            }

            if (typeof(TSource) != typeof(TDestination) && mapFunction == null)
            {
                throw new InvalidOperationException("Se requiere una función de mapeo para mapear entre tipos diferentes.");
            }
        }
        /// <summary>
        /// Construye un objeto HttpRequestMessage con los parámetros proporcionados, incluyendo la URL, el método HTTP,
        /// los encabezados personalizados, los parámetros de consulta (query params) y el contenido del cuerpo de la solicitud (si aplica).
        /// 
        /// El método agrega encabezados personalizados y parámetros de consulta a la URL cuando se especifican,
        /// y serializa el contenido en JSON si se trata de una solicitud POST o PUT.
        /// </summary>
        /// <param name="url">La URL del servicio al que se enviará la solicitud.</param>
        /// <param name="method">El método HTTP a utilizar (GET, POST, PUT, DELETE, etc.).</param>
        /// <param name="headers">Diccionario de encabezados personalizados a incluir en la solicitud. Puede ser null.</param>
        /// <param name="queryParams">Diccionario de parámetros de consulta (query params) para agregar a la URL. Puede ser null.</param>
        /// <param name="content">El contenido del cuerpo de la solicitud, que será serializado en JSON. Solo aplicable para métodos POST o PUT. Puede ser null.</param>
        /// <returns>Un objeto HttpRequestMessage configurado con la URL, método, encabezados, parámetros de consulta y contenido proporcionados.</returns>
        private static HttpRequestMessage BuildHttpRequestMessage(
            string url,
            HttpMethod method,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> queryParams = null,
            object content = null)
        {
            if (queryParams != null && queryParams.Any())
            {
                var queryString = string.Join("&", queryParams.Select(qp => $"{Uri.EscapeDataString(qp.Key)}={Uri.EscapeDataString(qp.Value)}"));
                url = url.Contains("?") ? $"{url}&{queryString}" : $"{url}?{queryString}";
            }

            var requestMessage = new HttpRequestMessage(method, url);

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            if (content != null && (method == HttpMethod.Post || method == HttpMethod.Put))
            {
                var jsonContent = JsonConvert.SerializeObject(content);
                requestMessage.Content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            return requestMessage;
        }
    }
}
}
