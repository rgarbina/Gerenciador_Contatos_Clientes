using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Gerenciador_Contatos_Clientes_Front.Data
{
    public class ContatoService
    {
        private readonly HttpClient _httpClient;

        public ContatoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Contato> GetContatoByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Contato>($"api/contatos/{id}");
        }

        public async Task<HttpResponseMessage> UpdateContatoAsync(Contato contato)
        {
            return await _httpClient.PutAsJsonAsync($"api/contatos/{contato.Id}", contato);
        }

        public async Task<HttpResponseMessage> DeleteContatoAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/contatos/{id}");
        }
    }
}
