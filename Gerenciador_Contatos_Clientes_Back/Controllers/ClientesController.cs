using Gerenciador_Contatos_Clientes_Back.Data;
using Gerenciador_Contatos_Clientes_Back.Data.Entity;
using Gerenciador_Contatos_Clientes_Back.Data.Repository.Interfaces;
using Gerenciador_Contatos_Clientes_Back.Models;
using Gerenciador_Contatos_Clientes_Back.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gerenciador_Contatos_Clientes_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IRepositoryBase<Contato> _contatorepository;

        public ClientesController(IClienteRepository clienteRepository, IRepositoryBase<Contato> contatorepository)
        {
            _clienteRepository = clienteRepository;
            _contatorepository = contatorepository;
        }

        // GET: api/clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            var listCliente = await _clienteRepository.GetAllAsNoTracking().ToListAsync();

            return Ok(listCliente);
        }

        // GET: api/clientes/7
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var oCliente = await _clienteRepository.GetByIdAsync(id);

            if (oCliente == null)
            {
                return NotFound();
            }

            return Ok(oCliente);
        }

        // POST: api/clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteViewModel cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var CNPJExist = _clienteRepository.Where(e => e.Cnpj == cliente.Cnpj).Any();
            if (CNPJExist)
            {
                return BadRequest("CNPJ Já Cadastrado");
            }

            var oCliente = ClienteViewModel.ToEntity(cliente, new Cliente());
            await _clienteRepository.AddAsync(oCliente);
            await _clienteRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = oCliente.Id }, cliente);
        }

        // PUT: api/Clientes/7
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oCliente = _clienteRepository.GetById(id);

            if (id == null)
            {
                return BadRequest();
            }

            oCliente = ClienteViewModel.ToEntity(clienteViewModel, oCliente);

            _clienteRepository.Update(oCliente);

            try
            {
                await _clienteRepository.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!(await ClienteExists(id)))
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

        // DELETE: api/clientes/7
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var oCliente = await _clienteRepository.GetByIdAsync(id);
            if (oCliente == null)
            {
                return NotFound();
            }

            _clienteRepository.Remove(oCliente);
            await _clienteRepository.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/clientes/7/contatos
        [HttpGet("{id}/contatos")]
        public async Task<ActionResult<List<Contato>>> GetContatosCliente(int id)
        {
            var listContatos = await _contatorepository.Where(w => w.ClienteId == id).AsNoTracking().ToListAsync();

            if (!listContatos.Any())
            {
                return NotFound();
            }

            return Ok(listContatos);
        }

        // POST: api/clientes/7/contatos
        [HttpPost("{id}/contatos")]
        public async Task<ActionResult<Contato>> PostContatoCliente(int id, ContatoViewModel contatoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var oContato = ContatoViewModel.ToEntity(contatoViewModel, new Contato { ClienteId = id });
            await _contatorepository.AddAsync(oContato);
            await _contatorepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente), new { id = oContato.Id }, oContato);
        }

        private async Task<bool> ClienteExists(int id)
        {
            var x = await _clienteRepository.Where(e => e.Id == id).AnyAsync();
            return await _clienteRepository.ExistsAsync(e => e.Id == id);
        }
    }
}
