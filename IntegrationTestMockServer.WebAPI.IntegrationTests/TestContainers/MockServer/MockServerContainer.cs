using DotNet.Testcontainers.Containers;

namespace IntegrationTestMockServer.WebAPI.IntegrationTests.TestContainers.MockServer;

public sealed class MockServerContainer : DockerContainer
{
    private readonly MockServerConfiguration _configuration;

    public MockServerContainer(MockServerConfiguration configuration)
        : base(configuration)
    {
        _configuration = configuration;
    }
    
    /// <summary>
    /// Retrieves the MockServer endpoint.
    /// </summary>
    /// <returns>The endpoint URL of the MockServer.</returns>
    public string GetEndpoint()
    {
        return $"http://{Hostname}:{GetMappedPublicPort(MockServerBuilder.MockServerPort)}";
    }
}