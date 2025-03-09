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
