using IntegrationTestMockServer.WebAPI.IntegrationTests.TestContainers.MockServer;
using IntegrationTestMockServer.WebAPI.IntegrationTests.TestFixtures;

namespace IntegrationTestMockServer.WebAPI.IntegrationTests;
public class ProjectFixture : IAsyncLifetime
{

    private static MockServerContainer _mockServerContainer;

    public static string EnvironmentName { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ProjectFixture"/> class
    /// </summary>
    public ProjectFixture()
    {
        // 確保路徑跨平台運行
        var baseDirectory = AppContext.BaseDirectory;
        var settingsFilePath = Path.Combine(baseDirectory, "TestSettings.json");
        
        TestSettingProvider.FilePath = settingsFilePath;
        
        EnvironmentName = TestSettingProvider.GetEnvironmentName(typeof(ProjectFixture));
        
        // Create Mock Server
        var mockServerSettings = TestSettingProvider.GetMockServerSettings();
       
        _mockServerContainer = MockServerFixture.CreateContainer(mockServerSettings, EnvironmentName);
    }

    internal static Uri MockServerEndpoint => new Uri(_mockServerContainer.GetEndpoint());
    
    /// <summary>
    /// Initializes this instance
    /// </summary>
    public async Task InitializeAsync()
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(5));
        
        await _mockServerContainer.StartAsync(cts.Token);
    }

    /// <summary>
    /// Disposes this instance
    /// </summary>
    public async Task DisposeAsync()
    {
        await _mockServerContainer.StopAsync();
        await _mockServerContainer.DisposeAsync();
    }
    
}