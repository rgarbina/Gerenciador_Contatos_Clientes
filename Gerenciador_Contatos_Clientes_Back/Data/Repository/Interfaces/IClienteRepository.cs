using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Data.Repository.Interfaces;

namespace Gerenciador_Contatos_Clientes_Back.Data
{
    public interface IClienteRepository : IRepositoryBase<Cliente>, IDisposable
    {
        /// <summary>
        /// seta o campo ativo para false, removendo virtualmente o cliente
        /// </summary>
        /// <param name="obj"></param>
        void Desativar(Cliente obj);
    }
}
