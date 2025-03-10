﻿using bg.hackathon.alphahackers.application.models.dtos;

namespace bg.hackathon.alphahackers.application.data.interfaces.repositories
{
    public interface IBusquedaRepository
    {
        Task<List<ClienteDTo>> ObtenerBusqueda(string query, string? ciudad, string? pais, string? provincia);
    }
}
