using bg.hackathon.alphahackers.application.data.interfaces.repositories;
using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.domain.entities.pyme;
using Microsoft.Extensions.Configuration;

namespace bg.hackathon.alphahackers.infrastructure.data.repositories
{
    public class PerfilRepository : IPerfilRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpRequestService _httpRequestService;

        public PerfilRepository(
            IConfiguration configuration,
            IHttpRequestService httpRequestService
        )
        {
            _configuration = configuration;
            _httpRequestService = httpRequestService;
        }

        public async Task<LineaCredito> ObtenerLineaCredito(int codigo_cliente)
        {
            var url = _configuration["Aws:ApiGateway:LineaCredito:url"];

            var body = new
            {
                id_cliente = codigo_cliente.ToString()
            };

            try
            {
                var result = await _httpRequestService.ExecuteRestRequestAsync<LineaCredito, LineaCredito>(
                    url,
                    HttpMethod.Get,
                    content : body
                );

                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<Cliente> ObtenerPerfil(int codigo_cliente)
        {
            throw new NotImplementedException();
        }
    }
}
