using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("recognition")]
public class VisualRecognitionController : ControllerBase
{
    private readonly IVisualRecognitionService _visualRecognitionService;

    public VisualRecognitionController(IVisualRecognitionService visualRecognitionService)
    {
        _visualRecognitionService = visualRecognitionService;
    }

    [HttpGet("occurrences")]
    public async Task<IActionResult> GetOccurrences([FromQuery] string? objectName, [FromQuery] string? variant, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var result = await _visualRecognitionService.GetOccurencesAsync(objectName, variant, from, to);
        return Ok(result);
    }

    [HttpGet("stats/count")]
    public async Task<IActionResult> GetCount([FromQuery] string? objectName, [FromQuery] string? variant, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var result = await _visualRecognitionService.GetCountAsync(objectName, variant, from, to);
        return Ok(result);
    }

    [HttpGet("stats/most-frequent")]
    public async Task<IActionResult> GetMostFrequent([FromQuery] string? objectName, [FromQuery] string? variant, [FromQuery] DateTime? from, [FromQuery] DateTime? to)
    {
        var result = await _visualRecognitionService.GetMostFrequentAsync(objectName, variant, from, to);
        return Ok(result);
    }
}
