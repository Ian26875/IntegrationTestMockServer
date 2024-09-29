using IntegrationTestMockServer.WebAPI.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTestMockServer.WebAPI.IntegrationTests;

public class TestWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // 使用 mock 替換 IOptions<WebServiceOptions>
            services.Configure<WebServiceOptions>(options =>
            {
                options.SampleUrl = ProjectFixture.MockServerEndpoint.ToString().TrimEnd('/');
            });
            
        });
    }
}
