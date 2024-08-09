using System.Threading.Tasks;
using Gerenciador_Contatos_Clientes_Back.Controllers;
using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Data.Repository.Interfaces;
using Gerenciador_Contatos_Clientes_Back.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Gerenciador_Contatos_Clientes_Back.Tests.Controllers
{
    public class ContatosControllerTests
    {
        private readonly Mock<IRepositoryBase<Contato>> _mockRepo;
        private readonly ContatosController _controller;

        public ContatosControllerTests()
        {
            _mockRepo = new Mock<IRepositoryBase<Contato>>();
            _controller = new ContatosController(_mockRepo.Object);
        }

        [Fact]
        public async Task GetContato_ReturnsOk_WithValidContato()
        {
            // Arrange
            var contatoId = 7;
            var contato = new Contato { Id = contatoId };
            _mockRepo.Setup(repo => repo.GetByIdAsync(contatoId)).ReturnsAsync(contato);

            // Act
            var result = await _controller.GetContato(contatoId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(contato, okResult.Value);
        }

        [Fact]
        public async Task GetContato_ReturnsNotFound_WhenContatoIsNull()
        {
            // Arrange
            var contatoId = 7;
            _mockRepo.Setup(repo => repo.GetByIdAsync(contatoId)).ReturnsAsync((Contato)null);

            // Act
            var result = await _controller.GetContato(contatoId);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutContato_ReturnsNoContent_WhenUpdateIsSuccessful()
        {
            // Arrange
            var contatoId = 5;
            var contatoViewModel = new ContatoViewModel(); // Populate with necessary data
            var contato = new Contato { Id = contatoId };
            _mockRepo.Setup(repo => repo.GetByIdAsync(contatoId)).ReturnsAsync(contato);
            _mockRepo.Setup(repo => repo.ExistsAsync(a => a.Id == contatoId)).ReturnsAsync(true);

            // Act
            var result = await _controller.PutContato(contatoId, contatoViewModel);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockRepo.Verify(repo => repo.Update(It.IsAny<Contato>()), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteContato_ReturnsNoContent_WhenDeleteIsSuccessful()
        {
            // Arrange
            var contatoId = 7;
            var contato = new Contato { Id = contatoId };
            _mockRepo.Setup(repo => repo.GetByIdAsync(contatoId)).ReturnsAsync(contato);

            // Act
            var result = await _controller.DeleteContato(contatoId);

            // Assert
            Assert.IsType<NoContentResult>(result);
            _mockRepo.Verify(repo => repo.Remove(It.IsAny<Contato>()), Times.Once);
            _mockRepo.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteContato_ReturnsNotFound_WhenContatoIsNull()
        {
            // Arrange
            var contatoId = 7;
            _mockRepo.Setup(repo => repo.GetByIdAsync(contatoId)).ReturnsAsync((Contato)null);

            // Act
            var result = await _controller.DeleteContato(contatoId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}