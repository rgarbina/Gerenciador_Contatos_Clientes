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
            try
            {
                //arrayCliente = await _httpClient.GetFromJsonAsync<Cliente[]>("api/clientes");
                var response = await _httpClient.GetAsync("api/clientes");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                arrayCliente = JsonSerializer.Deserialize<Cliente[]>(responseBody);
            }
            catch (Exception e)
            {

            }

            return arrayCliente;
        }
    }
}
