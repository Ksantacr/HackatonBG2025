using bg.hackathon.alphahackers.domain.entities.pyme;

namespace bg.hackathon.alphahackers.application.data.interfaces.repositories
{
    public interface IBusquedaRepository
    {
        Task<Cliente> ObtenerBusqueda(string query, string? ciudad, string? pais, string? provincia);
    }
}
