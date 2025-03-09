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

        /// <summary>
        /// Retorna una lista de empresas que coinciden con el nombre o categoría proporcionados. Se pueden aplicar filtros opcionales de ubicación (país, ciudad, provincia).
        /// </summary>
        /// <param name="query">Filtro Opcional término de búsqueda para nombre o categoría. Ejemplo: Si se busca "Contoso" o "Medicina", se devolverán empresas que coincidan con cualquiera de los dos.</param>
        /// <param name="pais">Filtro opcional por país.</param>
        /// <param name="ciudad">Filtro opcional por ciudad.</param>
        /// <param name="provincia">Filtro opcional por provincia.</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [Route("buscar-empresas")]
        [ProducesResponseType(typeof(MsDtoResponse<List<ClienteDTo>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObtenerPerfil([FromQuery][Optional]string? query, [FromQuery][Optional]string? pais, [FromQuery][Optional]string? ciudad, [FromQuery][Optional] string? provincia)
        {
            var response = await _busquedaServices.ObtenerBusqueda(query,ciudad,pais,provincia);
            return Ok(new MsDtoResponse<List<ClienteDTo>>(HttpContext.TraceIdentifier, response));
        }
    }
}
