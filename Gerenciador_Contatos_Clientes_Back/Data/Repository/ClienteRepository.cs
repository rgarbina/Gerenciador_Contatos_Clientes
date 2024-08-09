using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Data.Repository;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;

namespace Gerenciador_Contatos_Clientes_Back.Data
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    { 
       
        public void Desativar(Cliente obj)
        {
            obj.Ativo = false;

            context.Update(obj);
        }

        public async Task<Cliente?> GetDetalheClienteAsync(int id)
        {
            var oCliente = await context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            var listaContatos = await context.Contatos.Where(c => c.ClienteId == id).ToListAsync();
            oCliente.Contatos.AddRange(listaContatos);

            return oCliente;
        }

        public void RemoverCascata(Cliente obj)
        {
            var listContatos = context.Contatos.Where(w=> w.ClienteId == obj.Id);

            if (listContatos.Any())
            {
                context.Contatos.RemoveRange(listContatos);
            }

            context.Clientes.Remove(obj);
            context.SaveChanges();
        }
    }
}
