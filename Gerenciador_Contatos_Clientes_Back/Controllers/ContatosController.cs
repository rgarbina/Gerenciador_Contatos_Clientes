using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Data.Repository.Interfaces;
using Gerenciador_Contatos_Clientes_Back.Models;
using Gerenciador_Contatos_Clientes_Back.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_Contatos_Clientes_Back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly IRepositoryBase<Contato> _contatorepository;

        public ContatosController(IRepositoryBase<Contato> contatorepository)
        {
            _contatorepository = contatorepository;
        }

        // GET: api/contatos/7
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetContato(int id)
        {
            var oContato = await _contatorepository.GetByIdAsync(id);

            if (oContato == null)
            {
                return NotFound();
            }

            return Ok(oContato);
        }

        // PUT: api/Contatos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContato(int id, ContatoViewModel contatoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oContato = await _contatorepository.GetByIdAsync(id);
            oContato = ContatoViewModel.ToEntity(contatoViewModel, oContato);
            _contatorepository.Update(oContato);

            try
            {
                await _contatorepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var contatoExists = await _contatorepository.ExistsAsync(a=> a.Id == id);
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
            var oContato = await _contatorepository.GetByIdAsync(id);
            if (oContato == null)
            {
                return NotFound();
            }

            _contatorepository.Remove(oContato);
            await _contatorepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
