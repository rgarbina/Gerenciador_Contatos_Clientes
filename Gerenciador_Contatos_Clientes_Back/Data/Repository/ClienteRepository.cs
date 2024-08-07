using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Data.Repository;

namespace Gerenciador_Contatos_Clientes_Back.Data
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    { 
       
        public void Desativar(Cliente obj)
        {
            obj.Ativo = false;

            context.Update(obj);
        }
    }
}
