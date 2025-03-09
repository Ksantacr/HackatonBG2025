using bg.hackathon.alphahackers.application.data.interfaces.repositories;
using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.domain.entities.pyme;

namespace bg.hackathon.alphahackers.application.data.services
{
    public class LineaCreditoServices : ILineaCreditoServices
    {
        private readonly ILineaCreditoRepository _lineaCreditoRepository;

        public LineaCreditoServices(ILineaCreditoRepository lineaCreditoRepository)
        {
            _lineaCreditoRepository = lineaCreditoRepository;
        }

        public async Task<LineaCredito> ObtenerLineaCredito(int codigo_cliente)
        {

            var result = await _lineaCreditoRepository.ObtenerLineaCredito(codigo_cliente);
            return result;

        }
    }
}
