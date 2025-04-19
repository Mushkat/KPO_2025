using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Zoo.Application.Services;
using Zoo.Domain.Entities;
using Zoo.Domain.Repositories;

namespace Zoo.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{
    private readonly IAnimalRepository _animalRepo;
    private readonly AnimalTransferService _transferService;

    public AnimalsController(IAnimalRepository animalRepo, AnimalTransferService transferService)
    {
        _animalRepo = animalRepo;
        _transferService = transferService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var animals = await _animalRepo.GetAllAsync();
        return Ok(animals);
    }

    [HttpPost]
    public async Task<IActionResult> AddAnimal([FromBody] Animal animal)
    {
        await _animalRepo.AddAsync(animal);
        return CreatedAtAction(nameof(GetAll), new { id = animal.Id }, animal);
    }

    [HttpPost("{animalId:guid}/transfer/{enclosureId:guid}")]
    public async Task<IActionResult> TransferAnimal(Guid animalId, Guid enclosureId)
    {
        var evt = await _transferService.TransferAnimalAsync(animalId, enclosureId);
        if (evt == null) return NotFound("Animal or Enclosure not found");
        return Ok(evt);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _animalRepo.DeleteAsync(id);
        return NoContent();
    }
}
