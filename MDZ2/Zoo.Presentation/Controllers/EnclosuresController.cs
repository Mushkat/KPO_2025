using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Zoo.Domain.Entities;
using Zoo.Domain.Repositories;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnclosuresController : ControllerBase
{
    private readonly IEnclosureRepository _enclosureRepo;

    public EnclosuresController(IEnclosureRepository enclosureRepo)
    {
        _enclosureRepo = enclosureRepo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _enclosureRepo.GetAllAsync();
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] Enclosure enclosure)
    {
        await _enclosureRepo.AddAsync(enclosure);
        return CreatedAtAction(nameof(GetAll), new { id = enclosure.Id }, enclosure);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _enclosureRepo.DeleteAsync(id);
        return NoContent();
    }
}
