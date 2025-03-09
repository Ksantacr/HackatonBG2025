using bg.hackathon.alphahackers.domain.entities.pyme;

namespace bg.hackathon.alphahackers.application.data.interfaces.repositories
{
    public interface ILineaCreditoRepository
    {
        Task<LineaCredito> ObtenerLineaCredito(int codigo_cliente);
    }
}
