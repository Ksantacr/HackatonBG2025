using bg.hackathon.alphahackers.domain.entities.pyme;

namespace bg.hackathon.alphahackers.application.data.interfaces.services
{
    public interface IBusquedaServices
    {
        Task<Cliente> ObtenerBusqueda(string query, string? ciudad, string? pais, string? provincia);
    }
}
