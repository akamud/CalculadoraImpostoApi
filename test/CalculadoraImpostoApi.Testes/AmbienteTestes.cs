using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ToDoSample;
using ToDoSample.Dados;

namespace CalculadoraImpostoApi.Testes
{
    [SetUpFixture]
    public class AmbienteTestes
    {
        public static WebApplicationFactory<Startup> Factory;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Factory = new WebApplicationFactory<Startup>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                    {
                        // Configura mocks
                    })
                    .UseEnvironment("Test");
            });

            using var scopeMigration = Factory.Services.CreateScope();
            var impostoRendaContext = scopeMigration.ServiceProvider.GetService<ImpostoRendaContext>();

            impostoRendaContext.Database.EnsureDeleted();
            impostoRendaContext.Database.Migrate();

            // Adiciona seed inicial de dados no banco
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Factory.Dispose();
        }
    }
}