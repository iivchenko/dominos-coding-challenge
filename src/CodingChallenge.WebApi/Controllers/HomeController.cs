using System.Reflection;
using CodingChallenge.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodingChallenge.WebApi.Controllers;

// iivc comment: 
// I would remove the controller as it doesn't serve domain or infrastructural purpose
// but I can't as the Assignment  forbit to change APIs and contracts. 
[Route("")]
[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppInformationResponse))]
    public ActionResult<AppInformationResponse> Get()
    {
        var result = new AppInformationResponse(
            "Coding Challenge",
            Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
            Environment.MachineName
        );

        return Ok(result);
    }
}