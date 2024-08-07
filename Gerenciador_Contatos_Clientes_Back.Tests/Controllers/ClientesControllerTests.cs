using Microsoft.AspNetCore.Mvc;
using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Data.Repository.Interfaces;
using Gerenciador_Contatos_Clientes_Back.Controllers;
using Gerenciador_Contatos_Clientes_Back.ViewModel;
using Moq;
using Gerenciador_Contatos_Clientes_Back.Data;
using Microsoft.EntityFrameworkCore;
using Gerenciador_Contatos_Clientes_Back.Models;

namespace Gerenciador_Contatos_Clientes_Back.Tests.Controllers
{
    public class ClientesControllerTests
    {
        private readonly Mock<IClienteRepository> _mockClienteRepo;
        private readonly Mock<IRepositoryBase<Contato>> _mockContatoRepo;
        private readonly ClientesController _controller;

        public ClientesControllerTests()
        {
            _mockClienteRepo = new Mock<IClienteRepository>();
            _mockContatoRepo = new Mock<IRepositoryBase<Contato>>();
            _controller = new ClientesController(_mockClienteRepo.Object, _mockContatoRepo.Object);
        }

        [Fact]
        public async Task GetCliente_ReturnsCliente()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Nome = "Cliente 1", Cnpj = "54048038000163" };
            _mockClienteRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(cliente);

            // Act
            var result = await _controller.GetCliente(1);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Cliente>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var returnValue = Assert.IsType<Cliente>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task PutCliente_ValidModel_ReturnsNoContent()
        {
            // Arrange
            var clienteViewModel = new ClienteViewModel { Nome = "Cliente Atualizado", Cnpj = "54048038000163" };
            var cliente = new Cliente { Id = 1, Nome = "Cliente 1", Cnpj = "54048038000163" };

            _mockClienteRepo.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(cliente);
            _mockClienteRepo.Setup(repo => repo.Update(It.IsAny<Cliente>()));
            _mockClienteRepo.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutCliente(1, clienteViewModel);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }

        [Fact]
        public async Task DeleteCliente_ExistingId_ReturnsNoContent()
        {
            // Arrange
            var cliente = new Cliente { Id = 1, Nome = "Cliente 1", Cnpj = "54048038000163" };
            _mockClienteRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(cliente);
            _mockClienteRepo.Setup(repo => repo.Remove(cliente));
            _mockClienteRepo.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCliente(1);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(204, noContentResult.StatusCode);
        }
    }

    public static class MockExtensions
    {
        public static void SetupIQueryable<T>(this Mock<T> mock, IQueryable queryable)
            where T : class, IQueryable
        {
            mock.Setup(r => r.GetEnumerator()).Returns(queryable.GetEnumerator());
            mock.Setup(r => r.Provider).Returns(queryable.Provider);
            mock.Setup(r => r.ElementType).Returns(queryable.ElementType);
            mock.Setup(r => r.Expression).Returns(queryable.Expression);
        }
    }
}
