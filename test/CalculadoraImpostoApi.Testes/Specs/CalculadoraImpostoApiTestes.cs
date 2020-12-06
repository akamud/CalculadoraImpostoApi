using CalculadoraImpostoApi.Testes.Suporte;
using FluentAssertions;
using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace CalculadoraImpostoApi.Testes.Specs
{
    public class CalculadoraImpostoApi : TesteBaseApi
    {
        private class ResultadoSalarioLiquidoDto
        {
            public decimal ValorCalculado { get; set; }
        }

        [Test]
        public async Task GetDeveRetornarNoContentQuandoNaoHouverHistoricosInseridos()
        {
            var resposta = await _httpClient.GetAsync("salarioLiquido/historico");

            resposta.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [Test]
        public async Task PostDeveRetornarResultadoDoSalarioCalculadoDeAcordoComTabela()
        {
            const string tabelaIR = @"{""faixas"": [
                            {
                                ""valorInicial"": 2000.00,
                                ""valorFinal"": 3500.00,
                                ""aliquota"": 10
                            }
                        ]}";

            var server = WireMockServer.Start(7070);
            server.Given(Request.Create().UsingAnyMethod())
                .RespondWith(Response.Create().WithBody(tabelaIR));
            
            var response = await _httpClient.PostAsync("salarioLiquido/3000", new StringContent(""));
            
            var tabelaJson = await response.Content.ReadAsStringAsync();
            var resultado = JsonSerializer.Deserialize<ResultadoSalarioLiquidoDto>(tabelaJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            resultado.ValorCalculado.Should().Be(2700);
        }
    }
}