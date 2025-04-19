using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Zoo.Application.Services;
using Zoo.Domain.Entities;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class FeedingScheduleController : ControllerBase
{
    private readonly FeedingOrganizationService _feedingService;

    public FeedingScheduleController(FeedingOrganizationService feedingService)
    {
        _feedingService = feedingService;
    }

    [HttpPost]
    public async Task<IActionResult> AddSchedule([FromBody] FeedingSchedule schedule)
    {
        try
        {
            await _feedingService.ScheduleFeedingAsync(schedule.AnimalId, schedule.FeedingTime, schedule.Food);
            return Ok(new { message = "Feeding scheduled successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetSchedules()
    {
        var schedules = await _feedingService.GetAllSchedulesAsync();
        return Ok(schedules);
    }
}
