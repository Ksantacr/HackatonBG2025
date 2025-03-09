using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.domain.entities.pyme;
using bg.hackathon.alphahackers.domain.models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;

namespace bg.hackathon.alphahackers.api.controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerfilController : ControllerBase
    {
        private readonly ILineaCreditoServices _lineaCreditoServices;


        [HttpGet]
        [Route("linea-credito")]
        [ProducesResponseType(typeof(MsDtoResponse<LineaCredito>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerLineaCredito(int codigo_cliente)
        {
            var response = await _lineaCreditoServices.ObtenerLineaCredito(codigo_cliente);
            return Ok(new MsDtoResponse<LineaCredito>(HttpContext.TraceIdentifier, response));
        }
    }
}
