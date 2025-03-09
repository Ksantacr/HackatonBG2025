using Moq;
using Microsoft.AspNetCore.Mvc;
using bg.hackathon.alphahackers.api.controller;
using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.application.models.dtos;
using bg.hackathon.alphahackers.domain.models;

namespace bg.hackathon.alphahacker.test
{
    public class BusquedaControllerTests
    {
        private readonly Mock<IBusquedaServices> _busquedaServicesMock;
        private readonly BusquedaController _controller;

        public BusquedaControllerTests()
        {
            _busquedaServicesMock = new Mock<IBusquedaServices>();
            _controller = new BusquedaController(_busquedaServicesMock.Object);
        }

        [Fact]
        public async Task ObtenerPerfil_DeberiaRetornarOkConDatos()
        {
            // Arrange
            string query = "Medicina";
            string pais = "Ecuador";
            string ciudad = "Quito";
            string provincia = "Pichincha";

            var empresas = new List<ClienteDTo>
        {
            new ClienteDTo { Id_Cliente = 1, Nombre = "Clinica Quito", Categoria = "Medicina" },
            new ClienteDTo { Id_Cliente = 2, Nombre = "Farmacia Salud", Categoria = "Medicina" }
        };

            _busquedaServicesMock.Setup(s => s.ObtenerBusqueda(query, ciudad, pais, provincia))
                                 .ReturnsAsync(empresas);

            // Act
            var result = await _controller.ObtenerPerfil(query, pais, ciudad, provincia);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<MsDtoResponse<List<ClienteDTo>>>(okResult.Value);
            Assert.Equal(empresas, response.data);
        }

        [Fact]
        public async Task ObtenerPerfil_DeberiaRetornarBadRequestSiFalla()
        {
            // Arrange
            string query = "Tecnología";
            _busquedaServicesMock.Setup(s => s.ObtenerBusqueda(query, null, null, null))
                                 .ThrowsAsync(new Exception("Error en el servicio"));

            // Act
            var result = await _controller.ObtenerPerfil(query, null, null, null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}