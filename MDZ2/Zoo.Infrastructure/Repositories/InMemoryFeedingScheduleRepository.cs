using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Zoo.Domain.Entities;
using Zoo.Domain.Repositories;

namespace Zoo.Infrastructure.Repositories;

public class InMemoryFeedingScheduleRepository : IFeedingScheduleRepository
{
    private readonly List<FeedingSchedule> _schedules = new();

    public Task AddFeedingScheduleAsync(FeedingSchedule schedule)
    {
        _schedules.Add(schedule);
        return Task.CompletedTask;
    }

    public Task<IEnumerable<FeedingSchedule>> GetAllSchedulesAsync()
    {
        return Task.FromResult<IEnumerable<FeedingSchedule>>(_schedules);
    }

    public Task<IEnumerable<FeedingSchedule>> GetByAnimalIdAsync(Guid animalId)
    {
        var result = _schedules.Where(s => s.AnimalId == animalId);
        return Task.FromResult<IEnumerable<FeedingSchedule>>(result);
    }
}
