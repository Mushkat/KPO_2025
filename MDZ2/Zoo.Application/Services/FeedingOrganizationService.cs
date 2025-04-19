using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Zoo.Domain.Entities;
using Zoo.Domain.ValueObjects;
using Zoo.Domain.Repositories;

namespace Zoo.Application.Services;

public class FeedingOrganizationService
{
    private readonly IAnimalRepository _animalRepo;
    private readonly IFeedingScheduleRepository _scheduleRepo;

    public FeedingOrganizationService(IAnimalRepository animalRepo, IFeedingScheduleRepository scheduleRepo)
    {
        _animalRepo = animalRepo;
        _scheduleRepo = scheduleRepo;
    }

    public async Task ScheduleFeedingAsync(Guid animalId, DateTime feedingTime, Food Food)
    {
        var animal = await _animalRepo.GetByIdAsync(animalId);
        if (animal == null)
            throw new ArgumentException("Animal not found");

        var schedule = new FeedingSchedule
        {
            Id = Guid.NewGuid(),
            AnimalId = animalId,
            FeedingTime = feedingTime,
            Food = Food
        };

        await _scheduleRepo.AddFeedingScheduleAsync(schedule);
    }

    public Task<IEnumerable<FeedingSchedule>> GetAllSchedulesAsync()
    {
        return _scheduleRepo.GetAllSchedulesAsync();
    }
}
