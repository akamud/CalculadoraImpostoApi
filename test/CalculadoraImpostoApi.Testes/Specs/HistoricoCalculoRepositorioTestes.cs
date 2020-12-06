using CalculadoraImpostoApi.Dados;
using CalculadoraImpostoApi.Testes.Suporte;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CalculadoraImpostoApi.Testes.Specs
{
    public class HistoricoCalculoRepositorioTestes : TesteBaseBanco
    {
        [Test]
        public async Task ObterTodosDeveRetornarTodosHistoricosDoBanco()
        {
            // Arrange
            var historico = new HistoricoCalculo
            {
                ValorCalculado = 3000,
                ValorSalario = 2550
            };
            _contextParaTestes.Add(historico);
            _contextParaTestes.SaveChanges();
            var repositorio = GetService<HistoricoCalculoRepositorio>();
            
            // Act
            var historicosDoBanco = await repositorio.ObterTodos();
            
            // Assert
            historicosDoBanco.Should().BeEquivalentTo(historico);
        }
    }
}