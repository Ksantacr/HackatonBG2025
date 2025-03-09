using bg.hackathon.alphahackers.domain.entities.pyme;

namespace bg.hackathon.alphahackers.application.data.interfaces.services
{
    public interface ILineaCreditoServices
    {
        Task<LineaCredito> ObtenerLineaCredito(int codigo_cliente);
    }
}
