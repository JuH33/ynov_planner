using System;
using System.Net.Http;
using System.IO;
using events_planner;  
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using events_planner.Models;
using events_planner.Data;
using Microsoft.Extensions.Logging;

namespace PlannerApi.Tests.Fixtures {

    public class ServerFixtures : IDisposable {

        public HttpClient Client { get; set; }

        public ServerFixtures() {
            IWebHostBuilder builder = Program.CreateWebHostbuilder(Array.Empty<string>())
                                             .UseEnvironment("test")
                                             .UseContentRoot(Path.GetFullPath("../../../../PlannerApi"));

            TestServer Server = new TestServer(builder);

            using (var scope = Server.Host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                try
                {
                    DbSeeder.InitializeTest(
                        services.GetRequiredService<PlannerContext>()
                    );
                }
                catch (Exception ex)
                {
                    ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            Client = Server.CreateClient();
        }

        public void Dispose()
        {
            Client = null; 
        }
    }
}