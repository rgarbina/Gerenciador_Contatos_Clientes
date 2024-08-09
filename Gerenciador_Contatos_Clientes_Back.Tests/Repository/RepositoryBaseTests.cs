using Microsoft.EntityFrameworkCore;
using Gerenciador_Contatos_Clientes_Back.Models;
using Gerenciador_Contatos_Clientes_Back.Data.Repository.Interfaces;
using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Data.Repository;

namespace Gerenciador_Contatos_Clientes_Back.Tests.Repository
{
    public class RepositoryBaseTests
    {
        private readonly GerenciadorContatosContext _context;
        private readonly RepositoryBase<Cliente> _clienteRepository;

        public RepositoryBaseTests()
        {
            var options = new DbContextOptionsBuilder<GerenciadorContatosContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new GerenciadorContatosContext(options);
            _clienteRepository = new RepositoryBase<Cliente>();
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            // Arrange
            var cliente = new Cliente { Nome = "Cliente 1", Cnpj = "54048038000163" };

            // Act
            await _clienteRepository.AddAsync(cliente);
            var addedCliente = await _context.Clientes.FindAsync(cliente.Id);

            // Assert
            Assert.NotNull(addedCliente);
            Assert.Equal(cliente.Nome, addedCliente.Nome);
        }

        [Fact]
        public void GetAllAsync_ShouldReturnAllEntities()
        {
            // Arrange
            _context.Clientes.Add(new Cliente { Nome = "Cliente 1", Cnpj = "54048038000163" });
            _context.Clientes.Add(new Cliente { Nome = "Cliente 2", Cnpj = "06679167000107" });
            _context.SaveChanges();

            // Act
            var companies = _clienteRepository.GetAllAsNoTracking();

            // Assert
            Assert.Equal(2, companies.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnEntity()
        {
            // Arrange
            var cliente = new Cliente { Nome = "Cliente 1", Cnpj = "54048038000163" };
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // Act
            var foundCliente = await _clienteRepository.GetByIdAsync(cliente.Id);

            // Assert
            Assert.NotNull(foundCliente);
            Assert.Equal(cliente.Nome, foundCliente.Nome);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveEntity()
        {
            // Arrange
            var cliente = new Cliente { Nome = "Cliente 1", Cnpj = "54048038000163" };
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            // Act
            _clienteRepository.Remove(cliente);
            var deletedCompany = await _context.Clientes.FindAsync(cliente.Id);

            // Assert
            Assert.Null(deletedCompany);
        }

        // Add more tests for other repository methods...
    }

}
