using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gerenciador_Contatos_Clientes_Back.Controllers;
using Gerenciador_Contatos_Clientes_Back.Data;
using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Data.Repository.Interfaces;
using Gerenciador_Contatos_Clientes_Back.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

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

        //TODO: Corrigir integracao
        //[Fact]
        //public async Task GetClientes_ReturnsOk_WithListOfClientes()
        //{
        //    // Arrange
        //    var clientes = new List<Cliente> { new Cliente { Id = 1 }, new Cliente { Id = 2 } };
        //    _mockClienteRepo.Setup(repo => repo.GetAllAsNoTracking().OrderBy(o => o.Id).ToListAsync())
        //                    .ReturnsAsync(clientes);

        //    // Act
        //    var result = await _controller.GetClientes();

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //    Assert.Equal(clientes, okResult.Value);
        //}

        [Fact]
        public async Task GetCliente_ReturnsOk_WithValidCliente()
        {
            // Arrange
            var clienteId = 7;
            var cliente = new Cliente { Id = clienteId };
            _mockClienteRepo.Setup(repo => repo.GetDetalheClienteAsync(clienteId))
                            .ReturnsAsync(cliente);

            // Act
            var result = await _controller.GetCliente(clienteId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(cliente, okResult.Value);
        }

        [Fact]
        public async Task GetCliente_ReturnsNotFound_WhenClienteIsNull()
        {
            // Arrange
            var clienteId = 7;
            _mockClienteRepo.Setup(repo => repo.GetDetalheClienteAsync(clienteId))
                            .ReturnsAsync((Cliente)null);

            // Act
            var result = await _controller.GetCliente(clienteId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PostCliente_ReturnsCreatedAtAction_WhenClienteIsCreated()
        {
            // Arrange
            var clienteViewModel = new ClienteViewModel { Cnpj = "12345678901234" };
            var cliente = new Cliente { Id = 7, Cnpj = clienteViewModel.Cnpj };
            _mockClienteRepo.Setup(repo => repo.Where(e => e.Cnpj == clienteViewModel.Cnpj))
                            .Returns(Enumerable.Empty<Cliente>().AsQueryable());
            _mockClienteRepo.Setup(repo => repo.AddAsync(It.IsAny<Cliente>()))
                            .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PostCliente(clienteViewModel);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(clienteViewModel, createdAtActionResult.Value);
        }

        [Fact]
        public async Task PostCliente_ReturnsBadRequest_WhenCNPJExists()
        {
            // Arrange
            var clienteViewModel = new ClienteViewModel { Cnpj = "12345678901234" };
            _mockClienteRepo.Setup(repo => repo.Where(e => e.Cnpj == clienteViewModel.Cnpj))
                            .Returns(new List<Cliente> { new Cliente { Cnpj = clienteViewModel.Cnpj } }.AsQueryable());

            // Act
            var result = await _controller.PostCliente(clienteViewModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("CNPJ Já Cadastrado", badRequestResult.Value);
        }

        [Fact]
        public async Task PutCliente_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var clienteId = 7;
            var clienteViewModel = new ClienteViewModel(); // Populate with necessary data
            var cliente = new Cliente { Id = clienteId };
            _mockClienteRepo.Setup(repo => repo.GetById(clienteId))
                            .Returns(cliente);
            _mockClienteRepo.Setup(repo => repo.ExistsAsync(e => e.Id == clienteId))
                            .ReturnsAsync(true);

            // Act
            var result = await _controller.PutCliente(clienteId, clienteViewModel);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockClienteRepo.Verify(repo => repo.Update(It.IsAny<Cliente>()), Times.Once);
            _mockClienteRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteCliente_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            var clienteId = 7;
            var cliente = new Cliente { Id = clienteId };
            _mockClienteRepo.Setup(repo => repo.GetByIdAsync(clienteId))
                            .ReturnsAsync(cliente);

            // Act
            var result = await _controller.DeleteCliente(clienteId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockClienteRepo.Verify(repo => repo.RemoverCascata(It.IsAny<Cliente>()), Times.Once);
            _mockClienteRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteCliente_ReturnsNotFound_WhenClienteIsNull()
        {
            // Arrange
            var clienteId = 7;
            _mockClienteRepo.Setup(repo => repo.GetByIdAsync(clienteId))
                            .ReturnsAsync((Cliente)null);

            // Act
            var result = await _controller.DeleteCliente(clienteId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //TODO: Corrigir integracao
        //[Fact]
        //public async Task GetContatosCliente_ReturnsOk_WithListOfContatos()
        //{
        //    // Arrange
        //    var clienteId = 7;
        //    var contatos = new List<Contato> { new Contato { ClienteId = clienteId }, new Contato { ClienteId = clienteId } };
        //    _mockContatoRepo.Setup(repo => repo.Where(w => w.ClienteId == clienteId).AsNoTracking().ToListAsync())
        //                    .ReturnsAsync(contatos);

        //    // Act
        //    var result = await _controller.GetContatosCliente(clienteId);

        //    // Assert
        //    var okResult = Assert.IsType<OkObjectResult>(result.Result);
        //    Assert.Equal(contatos, okResult.Value);
        //}

        //TODO: Corrigir integracao
        //[Fact]
        //public async Task PostContatoCliente_ReturnsCreatedAtAction_WhenContatoIsCreated()
        //{
        //    // Arrange
        //    var clienteId = 7;
        //    var contatoViewModel = new ContatoViewModel(); // Populate with necessary data
        //    var contato = new Contato { Id = 1, ClienteId = clienteId };
        //    _mockContatoRepo.Setup(repo => repo.AddAsync(It.IsAny<Contato>()))
        //                    .Returns(Task.CompletedTask);

        //    // Act
        //    var result = await _controller.PostContatoCliente(clienteId, contatoViewModel);

        //    // Assert
        //    var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        //    Assert.Equal(contato, createdAtActionResult.Value);
        //}
    }
}