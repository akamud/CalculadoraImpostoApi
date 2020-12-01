using NUnit.Framework;
using System.Net.Http;

namespace CalculadoraImpostoApi.Testes.Suporte
{
    public class TesteBaseApi : TesteBaseBanco
    {
        protected HttpClient _httpClient;

        [SetUp]
        public void SetUpHttpClient()
        {
            _httpClient = AmbienteTestes.Factory.CreateClient();
        }
    }
}