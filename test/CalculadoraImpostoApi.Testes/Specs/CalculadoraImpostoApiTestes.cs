using CalculadoraImpostoApi.Testes.Suporte;
using FluentAssertions;
using NUnit.Framework;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace CalculadoraImpostoApi.Testes.Specs
{
    public class CalculadoraImpostoApi : TesteBaseApi
    {
        private class ResultadoSalarioLiquidoDto
        {
            public decimal ValorCalculado { get; set; }
        }

        [Test]
        public async Task PostDeveRetornarResultadoDoSalario()
        {
            var response = await _httpClient.PostAsync("salarioLiquido/3000", new StringContent(""));
            
            var tabelaJson = await response.Content.ReadAsStringAsync();
            var resultado = JsonSerializer.Deserialize<ResultadoSalarioLiquidoDto>(tabelaJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            resultado.ValorCalculado.Should().Be(2550);
        }
    }
}