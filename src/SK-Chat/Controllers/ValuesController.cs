using Microsoft.AspNetCore.Mvc;
using SK_Chat.Services;

namespace SK_Chat.Controllers;

//[Authorize]
[Route("api/[controller]")]
[ApiController]
public sealed class ValuesController : ControllerBase
{
    private readonly IGenerateService _GenerateService;

    public ValuesController(IGenerateService generateService)
    {
        _GenerateService = generateService;
    }

    [HttpPost]
    public async Task<ActionResult> Invoke(string query)
    {
        var response = await _GenerateService.Generate(query);

        return Ok(response);
    }
}
