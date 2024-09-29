using System.Net;
using System.Text.Json;
using FluentAssertions;
using MockServerClientNet;
using MockServerClientNet.Model;

namespace IntegrationTestMockServer.WebAPI.IntegrationTests.Controllers;

[Collection(nameof(ProjectCollectionFixture))]
public class SampleControllerTests 
{
    protected TestWebApplicationFactory<Program> TestWebApplicationFactory { get; }
    
    protected HttpClient HttpClient { get; }

    public SampleControllerTests()
    {
        this.TestWebApplicationFactory = new TestWebApplicationFactory<Program>();
        this.HttpClient = TestWebApplicationFactory.CreateDefaultClient();
    }

    [Fact(DisplayName = "GetAsync_應返回正確的資料")]
    public async Task GetAsync_ShouldReturnCorrectData()
    {
        // Arrange
        var expectedResponse = new List<string> { "Item1", "Item2", "Item3" };

        // 設置 MockServer 的模擬回應
        var mockServerEndPoint = ProjectFixture.MockServerEndpoint;
        var mockServerClient = new MockServerClient(mockServerEndPoint.Host, mockServerEndPoint.Port);
        
        var mockServerRequest = new HttpRequest()
            .WithPath("/details")
            .WithMethod(HttpMethod.Get);

        var mockServerResponse = new HttpResponse()
            .WithStatusCode(HttpStatusCode.OK)
            .WithHeader("Content-Type", "application/json")
            .WithBody(JsonSerializer.Serialize(new []
            {
                "Item1", "Item2", "Item3" 
            }))
            .WithDelay(TimeSpan.FromMilliseconds(100));
        
        await mockServerClient.When(mockServerRequest)
            .RespondAsync(mockServerResponse);
        
        var url = "api/sample";
        
        // Act
        var response = await HttpClient.GetAsync(url);

        // Assert
        response.Should().Be200Ok().And.BeAs(expectedResponse);
    }
}
