using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Models;
using Gerenciador_Contatos_Clientes_Back.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_Contatos_Clientes_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly GerenciadorContatosContext _context;

        public ContatosController(GerenciadorContatosContext context)
        {
            _context = context;
        }

        // PUT: api/Contatos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ContatoViewModel contatoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oContato = await _context.Contatos.FindAsync(id);
            oContato = ContatoViewModel.ToEntity(contatoViewModel, oContato);
            _context.Entry(oContato).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var contatoExists = _context.Contatos.Any(a=> a.Id == id);
                if (!contatoExists)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/contatos/7
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            var oContato = await _context.Contatos.FindAsync(id);
            if (oContato == null)
            {
                return NotFound();
            }

            _context.Contatos.Remove(oContato);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
