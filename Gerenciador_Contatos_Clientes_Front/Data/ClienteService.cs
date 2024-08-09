using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Gerenciador_Contatos_Clientes_Front.Data
{
    public class ClienteService
    {
        private readonly HttpClient _httpClient;

        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Cliente[]> GetClientesAsync()
        {
            Cliente[] arrayCliente = null;
            arrayCliente = await _httpClient.GetFromJsonAsync<Cliente[]>("api/clientes");
            return arrayCliente;
        }

        public async Task<Cliente> GetClienteByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Cliente>($"api/clientes/{id}");
        }

        public async Task<Contato[]> GetContatosClienteByIdAsync(int id)
        {
            Contato[] arrayContato = null;
            arrayContato = await _httpClient.GetFromJsonAsync<Contato[]>($"api/clientes/{id}/contatos");

            return arrayContato;
        }

        public async Task<HttpResponseMessage> CreateClienteAsync(Cliente cliente)
        {
            return await _httpClient.PostAsJsonAsync("api/clientes", cliente);
        }

        public async Task<HttpResponseMessage> UpdateClienteAsync(int id, Cliente cliente)
        {
            return await _httpClient.PutAsJsonAsync($"api/clientes/{id}", cliente);
        }

        public async Task<HttpResponseMessage> CreateContatoClienteAsync(Contato contato)
        {
           return await _httpClient.PostAsJsonAsync($"api/clientes/{contato.ClienteId}/contatos", contato);
        }

        public async Task<HttpResponseMessage> DeleteClienteAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/clientes/{id}");
        }
    }
}
