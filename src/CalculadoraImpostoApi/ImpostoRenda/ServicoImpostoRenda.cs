using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ComecandoTestes
{
    public class ServicoImpostoRenda : IServicoImpostoRenda
    {
        private readonly HttpClient _httpClient;

        public ServicoImpostoRenda(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
        public async Task<decimal> ObterAliquota(decimal valor)
        {
            var tabelaJson = await _httpClient.GetStringAsync("tabelaIR");
            var tabelaIR = JsonSerializer.Deserialize<TabelaIR>(tabelaJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var faixa = tabelaIR.Faixas.First(x => (valor >= x.ValorInicial && valor <= x.ValorFinal) ||
                                          (valor >= x.ValorInicial && x.ValorFinal == null));
            return faixa.Aliquota;
        }
    }
}