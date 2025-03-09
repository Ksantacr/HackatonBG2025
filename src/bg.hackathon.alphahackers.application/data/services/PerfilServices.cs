using bg.hackathon.alphahackers.application.constants;
using bg.hackathon.alphahackers.application.data.interfaces.repositories;
using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.application.exceptions;
using bg.hackathon.alphahackers.domain.entities.pyme;

namespace bg.hackathon.alphahackers.application.data.services
{
    public class PerfilServices : IPerfilServices
    {
        private readonly IPerfilRepository _perfilRepository;

        public PerfilServices(IPerfilRepository perfilRepository)
        {
            _perfilRepository = perfilRepository;
        }

        public async Task<LineaCredito> ObtenerLineaCredito(int codigo_cliente)
        {

            var result = await _perfilRepository.ObtenerLineaCredito(codigo_cliente);

            return result == null ? throw new NotFoundException(GlobalConstant.MSG_NOT_FOUND) : result;
        }

        public async Task<Cliente> ObtenerPerfil(int codigo_cliente)
        {

            var result = await _perfilRepository.ObtenerPerfil(codigo_cliente);

            return result == null ? throw new NotFoundException(GlobalConstant.MSG_NOT_FOUND) : result;
        }
    }
}
