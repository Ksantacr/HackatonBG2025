using bg.hackathon.alphahackers.application.data.interfaces.repositories;
using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.application.models.dtos;
using bg.hackathon.alphahackers.domain.entities.pyme;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using bg.hackathon.alphahackers.application.constants;
using bg.hackathon.alphahackers.application.exceptions;

namespace bg.hackathon.alphahackers.infrastructure.data.repositories
{
    public class BusquedaRepository : IBusquedaRepository
    {
        private readonly IHttpRequestService _httpRequestService;
        private readonly IConfiguration _configuration;

        public BusquedaRepository(IHttpRequestService httpRequestService, IConfiguration configuration)
        {
            _httpRequestService = httpRequestService;
            _configuration = configuration;
        }

        public async Task<List<ClienteDTo>> ObtenerBusqueda(string query, string? ciudad, string? pais, string? provincia)
        {
            var url = _configuration["Aws:ApiGateway:BuscarEmpresa:url"];


            var body = new
            {
                query = query, 
                ciudad = ciudad != null ? ciudad : null,    
                pais = pais != null ? pais : null,       
                provincia = provincia != null ? provincia : null 
            };

            try
            {
                var result = await _httpRequestService.ExecuteRestRequestAsync<List<ClienteDTo>, List<ClienteDTo>>(
                    url,
                    HttpMethod.Post,
                    content: body
                );

                return result;
            }
            catch (Exception ex)
            {
                throw new InternalException(GlobalConstant.MSG_ERROR, ex);
            }
        }
    }
}
