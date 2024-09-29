using Docker.DotNet.Models;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;

namespace IntegrationTestMockServer.WebAPI.IntegrationTests.TestContainers.MockServer;

public sealed class MockServerBuilder : ContainerBuilder<MockServerBuilder, MockServerContainer, MockServerConfiguration>
{
    public const string MockServerImage = "mockserver/mockserver:latest";

    public const ushort MockServerPort = 1080;
    
    public MockServerBuilder()
        : this(new MockServerConfiguration())
    {
        DockerResourceConfiguration = Init().DockerResourceConfiguration;
    }

    private MockServerBuilder(MockServerConfiguration resourceConfiguration)
        : base(resourceConfiguration)
    {
        DockerResourceConfiguration = resourceConfiguration;
    }

    protected override MockServerConfiguration DockerResourceConfiguration { get; }

    public override MockServerContainer Build()
    {
        Validate();
        return new MockServerContainer(DockerResourceConfiguration);
    }

    protected override MockServerBuilder Init()
    {
        return base.Init()
            .WithImage(MockServerImage)
            .WithPortBinding(MockServerPort, true);
    }
    
    protected override MockServerBuilder Clone(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new MockServerConfiguration(resourceConfiguration));
    }
    
    protected override MockServerBuilder Clone(IContainerConfiguration resourceConfiguration)
    {
        return Merge(DockerResourceConfiguration, new MockServerConfiguration(resourceConfiguration));
    }
    
    protected override MockServerBuilder Merge(MockServerConfiguration oldValue, MockServerConfiguration newValue)
    {
        return new MockServerBuilder(new MockServerConfiguration(oldValue, newValue));
    }
}