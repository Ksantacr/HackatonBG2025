﻿using bg.hackathon.alphahackers.application.constants;
using bg.hackathon.alphahackers.application.data.interfaces.repositories;
using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.application.exceptions;
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
                    HttpMethod.Post,
                    content : body
                );

                return result;
            }
            catch (Exception ex)
            {
                throw new InternalException(GlobalConstant.MSG_ERROR, ex);
            }
        }

        public async Task<Cliente> ObtenerPerfil(int codigo_cliente)
        {
            var url = _configuration["Aws:ApiGateway:InfoPerfil:url"];

            var body = new
            {
                id_cliente = codigo_cliente.ToString()
            };

            try
            {
                var result = await _httpRequestService.ExecuteRestRequestAsync<Cliente, Cliente>(
                    url,
                    HttpMethod.Post,
                    content: body
                );

                return result;
            }catch(Exception ex)
            {
                throw new InternalException(GlobalConstant.MSG_ERROR,ex);
            }
        }
    }
}
