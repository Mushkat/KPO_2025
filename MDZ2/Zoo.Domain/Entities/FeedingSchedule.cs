using System;
using Zoo.Domain.ValueObjects;

namespace Zoo.Domain.Entities;

public class FeedingSchedule
{
    public Guid Id { get; set; }
    public Guid AnimalId { get; set; }
    public DateTime FeedingTime { get; set; }
    public Food Food { get; set; }
}
