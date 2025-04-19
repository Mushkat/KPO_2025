using System.Collections.Generic;
using System;
using Zoo.Domain.Entities;
using System.Threading.Tasks;

namespace Zoo.Domain.Repositories;

public interface IFeedingScheduleRepository
{
    Task AddFeedingScheduleAsync(FeedingSchedule schedule);
    Task<IEnumerable<FeedingSchedule>> GetAllSchedulesAsync();
    Task<IEnumerable<FeedingSchedule>> GetByAnimalIdAsync(Guid animalId);
}
