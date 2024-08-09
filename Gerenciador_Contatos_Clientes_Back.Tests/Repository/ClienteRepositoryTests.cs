using Microsoft.EntityFrameworkCore;
using Gerenciador_Contatos_Clientes_Back.Models;
using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Data;
using Moq;

namespace Gerenciador_Contatos_Clientes_Back.Tests.Repository
{
    public class ClienteRepositoryTests
    {
        private readonly Mock<DbSet<Cliente>> _mockSet;
        private readonly Mock<GerenciadorContatosContext> _mockContext;
        private readonly ClienteRepository _repository;

        public ClienteRepositoryTests()
        {
            _mockSet = new Mock<DbSet<Cliente>>();
            _mockContext = new Mock<GerenciadorContatosContext>();
            _mockContext.Setup(m => m.Set<Cliente>()).Returns(_mockSet.Object);

            _repository = new ClienteRepository();
        }

        [Fact]
        public async Task GetAllAsNoTracking_ReturnsListOfClientes()
        {
            // Arrange
            var data = new List<Cliente>
            {
                new Cliente { Id = 1, Nome = "Cliente 1" },
                new Cliente { Id = 2, Nome = "Cliente 2" }
            }.AsQueryable();

            _mockSet.As<IQueryable<Cliente>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<Cliente>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<Cliente>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<Cliente>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            // Act
            var result = await _repository.GetAllAsNoTracking().ToListAsync();

            // Assert
            Assert.Equal(2, result.Count);
        }

        // Similar test cases for other methods like AddAsync, Remove, SaveChangesAsync, etc.
    }

}
