using Docker.DotNet.Models;
using DotNet.Testcontainers.Configurations;

namespace IntegrationTestMockServer.WebAPI.IntegrationTests.TestContainers.MockServer;

public sealed class MockServerConfiguration : ContainerConfiguration
{
    public MockServerConfiguration() { }

    public MockServerConfiguration(IResourceConfiguration<CreateContainerParameters> resourceConfiguration)
        : base(resourceConfiguration) { }

    public MockServerConfiguration(IContainerConfiguration resourceConfiguration)
        : base(resourceConfiguration) { }

    public MockServerConfiguration(MockServerConfiguration resourceConfiguration)
        : base(new MockServerConfiguration(),resourceConfiguration) { }

    public MockServerConfiguration(MockServerConfiguration oldValue, MockServerConfiguration newValue)
        : base(oldValue, newValue) { }
}