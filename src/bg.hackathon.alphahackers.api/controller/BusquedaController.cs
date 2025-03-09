using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.application.models.dtos;
using bg.hackathon.alphahackers.domain.entities.pyme;
using bg.hackathon.alphahackers.domain.models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace bg.hackathon.alphahackers.api.controller
{
    
    [ApiController]
    public class BusquedaController : ControllerBase
    {
        private readonly IBusquedaServices _busquedaServices;

        public BusquedaController(IBusquedaServices busquedaServices)
        {
            _busquedaServices = busquedaServices;
        }
        [HttpGet]
        [Produces("application/json")]
        [Route("buscar-empresas")]
        [ProducesResponseType(typeof(MsDtoResponse<List<ClienteDTo>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerPerfil([FromQuery][Optional] string? query, [FromQuery][Optional]string? pais, [FromQuery][Optional]string? ciudad, [FromQuery][Optional] string? provincia)
        {
            var response = await _busquedaServices.ObtenerBusqueda(query,ciudad,pais,provincia);
            return Ok(new MsDtoResponse<List<ClienteDTo>>(HttpContext.TraceIdentifier, response));
        }
    }
}
