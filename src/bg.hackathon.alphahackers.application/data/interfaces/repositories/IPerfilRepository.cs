﻿using bg.hackathon.alphahackers.domain.entities.pyme;

namespace bg.hackathon.alphahackers.application.data.interfaces.repositories
{
    public interface IPerfilRepository
    {
        Task<LineaCredito> ObtenerLineaCredito(int codigo_cliente);
        Task<Cliente> ObtenerPerfil(int codigo_cliente);
    }
}
