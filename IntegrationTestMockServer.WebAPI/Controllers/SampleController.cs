using IntegrationTestMockServer.WebAPI.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace IntegrationTestMockServer.WebAPI.Controllers;

[ApiController]
[Route("api/sample")]
public class SampleController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    private readonly IOptions<WebServiceOptions> _optionsOfWebService;
    
    public SampleController(IHttpClientFactory httpClientFactory,IOptions<WebServiceOptions> optionsOfWebService)
    {
        _httpClientFactory = httpClientFactory;
        _optionsOfWebService = optionsOfWebService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        using var httpClient = _httpClientFactory.CreateClient();

        var url = $"{_optionsOfWebService.Value.SampleUrl}/details";

        var response = await httpClient.GetAsync(url);

        response.EnsureSuccessStatusCode();

        var collection = await response.Content.ReadFromJsonAsync<List<string>>();

        return Ok(collection);
    }
}