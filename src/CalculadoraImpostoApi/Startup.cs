using CalculadoraImpostoApi.Dados;
using CalculadoraImpostoApi.ImpostoRenda;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace CalculadoraImpostoApi
{
    public class Startup
    {
        private readonly IHostEnvironment _hostEnvironment;
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration, IHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ImpostoRendaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ImpostoRendaDatabase")));

            services.AddScoped<HistoricoCalculoRepositorio>();
            services.AddScoped<CalculadoraImposto>();

            var tabelaJson = @"{""faixas"": [
                            {
                                ""valorInicial"": 0,
                                ""valorFinal"": 1903.98,
                                ""aliquota"": 0
                            },
                            {
                                ""valorInicial"": 1903.99,
                                ""valorFinal"": 2826.65,
                                ""aliquota"": 7.5
                            },
                            {
                                ""valorInicial"": 2826.66,
                                ""valorFinal"": 3751.05,
                                ""aliquota"": 15
                            },
                            {
                                ""valorInicial"": 3751.06,
                                ""aliquota"": 27.5
                            }
                            ]
                        }";

            if (!_hostEnvironment.IsEnvironment("Test"))
            {
                var server = WireMockServer.Start(7070);
                server.Given(Request.Create().UsingAnyMethod())
                    .RespondWith(Response.Create().WithBody(tabelaJson));
            }

            services.AddHttpClient<IServicoImpostoRenda, ServicoImpostoRenda>(x => x.BaseAddress = new Uri("http://localhost:7070/"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}