namespace IntegrationTestMockServer.WebAPI.IntegrationTests.TestSettings;

public class TestSettings
{
    /// <summary>
    /// The Environment Name.
    /// </summary>
    public int EnvironmentName { get; set; }
    
    /// <summary>
    /// MockServer
    /// </summary>
    public MockServerSetting MockServer { get; set; }
}

public class ContainerSetting
{
    public string Image { get; set; }

    public string Tag { get; set; }

    public string ContainerName { get; set; }

    public string ContainerReadyMessage { get; set; }

    public Dictionary<string, string> EnvironmentSettings { get; set; }
}

public class MockServerSetting : ContainerSetting
{
    public MockServerSetting()
    {
        this.HostPort = 0;
        this.ContainerPort = 1080;
    }

    public ushort HostPort { get; set; }

    public ushort ContainerPort { get; set; }
    
}