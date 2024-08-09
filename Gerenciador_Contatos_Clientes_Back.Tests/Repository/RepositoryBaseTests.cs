using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Data.Repository;
using Gerenciador_Contatos_Clientes_Back.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Gerenciador_Contatos_Clientes_Back.Tests.Repository
{
    public class RepositoryBaseTests : IDisposable
    {
        private readonly GerenciadorContatosContext _context;
        private readonly RepositoryBase<Cliente> _repository;

        public RepositoryBaseTests()
        {
            var options = new DbContextOptionsBuilder<GerenciadorContatosContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new GerenciadorContatosContext(options);
            _repository = new RepositoryBase<Cliente>(); // No context parameter here

            // Seed data
            _context.Clientes.Add(new Cliente { Id = 1, Nome = "Cliente Teste", Cnpj = "12345678000195" });
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void Add_AddsEntityToContext()
        {
            // Arrange
            var client = new Cliente { Id = 2, Nome = "Novo Cliente", Cnpj = "98765432000198" };

            // Act
            _repository.Add(client);
            _repository.SaveChanges();

            // Assert
            var addedClient = _context.Clientes.Find(2);
            Assert.NotNull(addedClient);
            Assert.Equal("Novo Cliente", addedClient.Nome);
            Assert.Equal("98765432000198", addedClient.Cnpj);
        }

        [Fact]
        public async Task AddAsync_AddsEntityToContext()
        {
            // Arrange
            var client = new Cliente { Id = 3, Nome = "Cliente Assíncrono", Cnpj = "12345678000199" };

            // Act
            await _repository.AddAsync(client);
            await _repository.SaveChangesAsync();

            // Assert
            var addedClient = await _context.Clientes.FindAsync(3);
            Assert.NotNull(addedClient);
            Assert.Equal("Cliente Assíncrono", addedClient.Nome);
            Assert.Equal("12345678000199", addedClient.Cnpj);
        }

        [Fact]
        public void GetAllAsNoTracking_ReturnsAllEntities()
        {
            // Act
            var clients = _repository.GetAllAsNoTracking().ToList();

            // Assert
            Assert.Single(clients);
            Assert.Equal("Cliente Teste", clients[0].Nome);
            Assert.Equal("12345678000195", clients[0].Cnpj);
        }

        [Fact]
        public void GetById_ReturnsEntityById()
        {
            // Act
            var client = _repository.GetById(1);

            // Assert
            Assert.NotNull(client);
            Assert.Equal("Cliente Teste", client.Nome);
            Assert.Equal("12345678000195", client.Cnpj);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsEntityByIdAsync()
        {
            // Act
            var client = await _repository.GetByIdAsync(1);

            // Assert
            Assert.NotNull(client);
            Assert.Equal("Cliente Teste", client.Nome);
            Assert.Equal("12345678000195", client.Cnpj);
        }

        [Fact]
        public void Remove_RemovesEntityFromContext()
        {
            // Arrange
            var client = _context.Clientes.Find(1);

            // Act
            _repository.Remove(client);
            _repository.SaveChanges();

            // Assert
            var removedClient = _context.Clientes.Find(1);
            Assert.Null(removedClient);
        }

        [Fact]
        public async Task SaveChangesAsync_SavesChangesToDatabase()
        {
            // Arrange
            var client = new Cliente { Id = 4, Nome = "Cliente SaveChanges", Cnpj = "45678912345678" };
            await _repository.AddAsync(client);

            // Act
            await _repository.SaveChangesAsync();

            // Assert
            var savedClient = await _context.Clientes.FindAsync(4);
            Assert.NotNull(savedClient);
            Assert.Equal("Cliente SaveChanges", savedClient.Nome);
            Assert.Equal("45678912345678", savedClient.Cnpj);
        }

        [Fact]
        public void Update_UpdatesEntityInContext()
        {
            // Arrange
            var client = _context.Clientes.Find(1);
            client.Nome = "Cliente Atualizado";
            client.Cnpj = "99999999000199";

            // Act
            _repository.Update(client);
            _repository.SaveChanges();

            // Assert
            var updatedClient = _context.Clientes.Find(1);
            Assert.Equal("Cliente Atualizado", updatedClient.Nome);
            Assert.Equal("99999999000199", updatedClient.Cnpj);
        }

        [Fact]
        public void Where_FiltersEntitiesByCondition()
        {
            // Act
            var clients = _repository.Where(c => c.Nome.Contains("Teste")).ToList();

            // Assert
            Assert.Single(clients);
            Assert.Equal("Cliente Teste", clients[0].Nome);
        }

        [Fact]
        public async Task ExistsAsync_ReturnsTrueIfExists()
        {
            // Act
            var exists = await _repository.ExistsAsync(c => c.Id == 1);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task ExistsAsync_ReturnsFalseIfNotExists()
        {
            // Act
            var exists = await _repository.ExistsAsync(c => c.Id == 999);

            // Assert
            Assert.False(exists);
        }
    }
}