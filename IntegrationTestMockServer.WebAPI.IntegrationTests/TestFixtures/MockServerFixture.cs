using IntegrationTestMockServer.WebAPI.IntegrationTests.TestContainers.MockServer;
using IntegrationTestMockServer.WebAPI.IntegrationTests.TestSettings;

namespace IntegrationTestMockServer.WebAPI.IntegrationTests.TestFixtures;

public static class MockServerFixture
{
    public static MockServerContainer CreateContainer(MockServerSetting mockServerSetting, string environmentName)
    {
        var containerName = mockServerSetting.ContainerName;
        var container = new MockServerBuilder()
            .WithImage($"{mockServerSetting.Image}:{mockServerSetting.Tag}")
            .WithEnvironment(mockServerSetting.EnvironmentSettings)
            .WithName($"{environmentName}-{containerName}")
            .WithPortBinding(mockServerSetting.HostPort, mockServerSetting.ContainerPort)
            .WithAutoRemove(true)
            .Build();
        return container;
    }
}