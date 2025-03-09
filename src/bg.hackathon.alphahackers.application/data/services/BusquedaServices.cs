using bg.hackathon.alphahackers.application.constants;
using bg.hackathon.alphahackers.application.data.interfaces.repositories;
using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.application.exceptions;
using bg.hackathon.alphahackers.application.models.dtos;
using bg.hackathon.alphahackers.domain.entities.pyme;

namespace bg.hackathon.alphahackers.application.data.services
{
    public class BusquedaServices : IBusquedaServices
    {
        private readonly IBusquedaRepository _busquedaRepository;

        public BusquedaServices(IBusquedaRepository busquedaRepository)
        {
            _busquedaRepository = busquedaRepository;
        }

        public async Task<List<ClienteDTo>> ObtenerBusqueda(string query, string? ciudad, string? pais, string? provincia)
        {
            var result = await _busquedaRepository.ObtenerBusqueda(query, ciudad, pais, provincia);

            return result == null ? throw new NotFoundException(GlobalConstant.MSG_NOT_FOUND) : result;
        }
    }
}
