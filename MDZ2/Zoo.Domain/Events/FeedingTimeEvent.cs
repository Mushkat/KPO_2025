using System;

namespace Zoo.Domain.Events;
public record FeedingTimeEvent(Guid AnimalId, DateTime FeedingTime);