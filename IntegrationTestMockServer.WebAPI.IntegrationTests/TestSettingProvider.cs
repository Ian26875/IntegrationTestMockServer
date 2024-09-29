using IntegrationTestMockServer.WebAPI.IntegrationTests.TestSettings;
using Microsoft.Extensions.Configuration;

namespace IntegrationTestMockServer.WebAPI.IntegrationTests;

/// <summary>
/// class TestSettingProvider
/// </summary>
public static class TestSettingProvider
{
    public static string FilePath { get; set; } = "TestSettings.json";

    
    /// <summary>
    /// Get Environment Name
    /// </summary>
    /// <returns></returns>
    public static string GetEnvironmentName(Type typeOfTarget)
    {
        var configuration = GetConfiguration();

        var environmentName = string.IsNullOrWhiteSpace(configuration["EnvironmentName"])
            ? typeOfTarget.Assembly.GetName().Name.ToLower().Replace(".", "-")
            : configuration["EnvironmentName"].ToLower();

        return environmentName;
    }
    
    /// <summary>
    /// Get Environment Variables Settings
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="section"></param>
    /// <returns></returns>
    public static Dictionary<string, string> GetEnvironmentSettings(IConfigurationRoot configuration, string section)
    {
        var environments = new Dictionary<string, string>();

        var children = configuration.GetSection(section)?.GetChildren().ToArray();
        if (children is null)
        {
            return environments;
        }

        foreach (var item in children.Select(child => child.Value))
        {
            var value = item.Split("=");
            environments.Add(value[0], value[1]);
        }

        return environments;
    }
    

    /// <summary>
    /// 取得建立測試資料庫指定使用的 docker 設定資料
    /// </summary>
    /// <returns>System.String.</returns>
    public static MockServerSetting GetMockServerSettings()
    {
        var configuration = GetConfiguration();

        var databaseSettings = new MockServerSetting
        {
            Image = configuration["MockServer:Image"],
            Tag = configuration["MockServer:Tag"],
            ContainerName = configuration["MockServer:ContainerName"],
            ContainerReadyMessage = configuration["MockServer:ContainerReadyMessage"],
            EnvironmentSettings = GetEnvironmentSettings(configuration, "MockServer:EnvironmentSettings"),
            HostPort = string.IsNullOrWhiteSpace(configuration["MockServer:HostPort"])
                           ? GetRandomPort()
                           : configuration["Mssql:HostPort"].Equals("0")
                               ? GetRandomPort()
                               : ushort.TryParse(configuration["MockServer:HostPort"], out var databasePort)
                                   ? databasePort
                                   : GetRandomPort(),
            ContainerPort = string.IsNullOrWhiteSpace(configuration["MockServer:ContainerPort"])
                                ? (ushort)1080
                                : ushort.TryParse(configuration["MockServer:ContainerPort"], out var containerPort)
                                    ? containerPort
                                    : (ushort)1080
        };

        return databaseSettings;
    }
    
    /// <summary>
    /// Get Random Port
    /// </summary>
    /// <returns></returns>
    private static ushort GetRandomPort()
    {
        var result = Random.Shared.Next(49152, 65535);
        return (ushort)result;
    }

    /// <summary>
    /// Get ConfigurationRoot
    /// </summary>
    /// <returns></returns>
    private static IConfigurationRoot GetConfiguration()
    {
        var configuration = new ConfigurationBuilder().AddJsonFile(FilePath).Build();
        return configuration;
    }
}