using System.Threading.Tasks;
using System;
using Zoo.Domain.Entities;
using Zoo.Domain.Events;
using Zoo.Domain.Repositories;

namespace Zoo.Application.Services;

public class AnimalTransferService
{
    private readonly IAnimalRepository _animalRepo;
    private readonly IEnclosureRepository _enclosureRepo;

    public AnimalTransferService(IAnimalRepository animalRepo, IEnclosureRepository enclosureRepo)
    {
        _animalRepo = animalRepo;
        _enclosureRepo = enclosureRepo;
    }

    public async Task<AnimalMovedEvent?> TransferAnimalAsync(Guid animalId, Guid newEnclosureId)
    {
        var animal = await _animalRepo.GetByIdAsync(animalId);
        if (animal == null) return null;

        var newEnclosure = await _enclosureRepo.GetByIdAsync(newEnclosureId);
        if (newEnclosure == null) return null;

        var fromEnclosureId = animal.EnclosureId;
        animal.MoveToEnclosure(newEnclosureId);
        await _animalRepo.UpdateAsync(animal);

        return new AnimalMovedEvent(animal, fromEnclosureId, newEnclosureId);
    }
}
