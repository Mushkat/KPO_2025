using Microsoft.AspNetCore.Mvc;
using Zoo.Application.Services;
using Zoo.Application.DTOs;
using System.Threading.Tasks;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly ZooStatisticsService _statisticsService;

    public StatisticsController(ZooStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    [HttpGet]
    public async Task<ActionResult<ZooStatisticsDto>> Get()
    {
        var stats = await _statisticsService.GetStatistics();
        return Ok(stats);
    }
}
