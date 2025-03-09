using bg.hackathon.alphahackers.application.models.dtos;
using bg.hackathon.alphahackers.domain.entities.pyme;

namespace bg.hackathon.alphahackers.application.data.interfaces.services
{
    public interface IBusquedaServices
    {
        Task<List<ClienteDTo>> ObtenerBusqueda(string query, string? ciudad, string? pais, string? provincia);
    }
}
