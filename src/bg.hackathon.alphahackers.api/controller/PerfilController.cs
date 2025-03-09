using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.domain.entities.pyme;
using bg.hackathon.alphahackers.domain.models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace bg.hackathon.alphahackers.api.controller
{
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly IPerfilServices _perfilServices;

        public PerfilController(IPerfilServices perfilServices)
        {
            _perfilServices = perfilServices;
        }

        /// <summary>
        /// Retorna los detalles de la línea de crédito asociada a un cliente, incluyendo total, utilizado, disponible y estado.
        /// </summary>
        /// <param name="codigo_cliente" example="1123">ID del cliente para consultar su línea de crédito.</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [Route("linea-credito")]
        [ProducesResponseType(typeof(MsDtoResponse<LineaCredito>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerLineaCredito([FromHeader][Required] int codigo_cliente)
        {
            var response = await _perfilServices.ObtenerLineaCredito(codigo_cliente);
            return Ok(new MsDtoResponse<LineaCredito>(HttpContext.TraceIdentifier, response));
        }

        /// <summary>
        /// Retorna la información del perfil del cliente y la lista de productos asociados.
        /// </summary>
        /// <param name="codigo_cliente" example="1123">ID del cliente</param>
        /// <returns></returns>
        [HttpGet]
        [Route("info-perfil")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MsDtoResponse<Cliente>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerPerfil([FromHeader][Required] int codigo_cliente)
        {
            var response = await _perfilServices.ObtenerPerfil(codigo_cliente);
            return Ok(new MsDtoResponse<Cliente>(HttpContext.TraceIdentifier, response));
        }
    }
}
