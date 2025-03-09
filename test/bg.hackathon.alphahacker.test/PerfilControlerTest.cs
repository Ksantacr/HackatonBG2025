using Moq;
using Microsoft.AspNetCore.Mvc;
using bg.hackathon.alphahackers.api.controller;
using bg.hackathon.alphahackers.application.data.interfaces.services;
using bg.hackathon.alphahackers.domain.entities.pyme;
using bg.hackathon.alphahackers.domain.models;
using bg.hackathon.alphahackers.domain.entities.enums;

namespace bg.hackathon.alphahacker.test
{
    public class PerfilControllerTests
    {
        private readonly Mock<IPerfilServices> _perfilServicesMock;
        private readonly PerfilController _controller;

        public PerfilControllerTests()
        {
            _perfilServicesMock = new Mock<IPerfilServices>();
            _controller = new PerfilController(_perfilServicesMock.Object);
        }

        [Fact]
        public async Task ObtenerLineaCredito_DeberiaRetornarOkConDatos()
        {
            // Arrange
            int codigoCliente = 1123;
            var lineaCredito = new LineaCredito { Total_Credito = 10000, Utilizado = 5000, Estado = EstadoLineaCredito.ACTIVO };
            _perfilServicesMock.Setup(s => s.ObtenerLineaCredito(codigoCliente))
                               .ReturnsAsync(lineaCredito);

            // Act
            var result = await _controller.ObtenerLineaCredito(codigoCliente);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<MsDtoResponse<LineaCredito>>(okResult.Value);
            Assert.Equal(lineaCredito, response.data);
        }

        [Fact]
        public async Task ObtenerLineaCredito_DeberiaRetornarBadRequestSiFalla()
        {
            // Arrange
            int codigoCliente = 1123;
            _perfilServicesMock.Setup(s => s.ObtenerLineaCredito(codigoCliente))
                               .ThrowsAsync(new Exception("Error en el servicio"));

            // Act
            var result = await _controller.ObtenerLineaCredito(codigoCliente);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task ObtenerPerfil_DeberiaRetornarOkConDatos()
        {
            // Arrange
            int codigoCliente = 1123;
            var cliente = new Cliente();
            _perfilServicesMock.Setup(s => s.ObtenerPerfil(codigoCliente))
                               .ReturnsAsync(cliente);

            // Act
            var result = await _controller.ObtenerPerfil(codigoCliente);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<MsDtoResponse<Cliente>>(okResult.Value);
            Assert.Equal(cliente, response.data);
        }

        [Fact]
        public async Task ObtenerPerfil_DeberiaRetornarBadRequestSiFalla()
        {
            // Arrange
            int codigoCliente = 1123;
            _perfilServicesMock.Setup(s => s.ObtenerPerfil(codigoCliente))
                               .ThrowsAsync(new Exception("Error en el servicio"));

            // Act
            var result = await _controller.ObtenerPerfil(codigoCliente);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }

}