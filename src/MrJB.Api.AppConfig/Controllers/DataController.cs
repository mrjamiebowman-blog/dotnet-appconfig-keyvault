using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MrJB.Api.AppConfig.Configuration;

namespace MrJB.Api.AppConfig.Controllers;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    // logger
    private readonly ILogger<DataController> _logger;

    // configuration
    private readonly ApplicationConfiguration _applicationConfiguration;

    public DataController(ILogger<DataController> logger, IOptions<ApplicationConfiguration> applicationConfiguration)
    {
        _logger = logger;
        _applicationConfiguration = applicationConfiguration.Value;
    }

    [HttpGet(Name = "Results")]
    public string Get()
    {
        _logger.LogInformation("Result: {result}", _applicationConfiguration.Data);
        return _applicationConfiguration.Data ?? "No Value";
    }
}
