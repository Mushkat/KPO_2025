using System;
using Zoo.Domain.Entities;

namespace Zoo.Domain.Events;

public class AnimalMovedEvent
{
    public Animal Animal { get; }
    public Guid FromEnclosureId { get; }
    public Guid ToEnclosureId { get; }

    public AnimalMovedEvent(Animal animal, Guid fromEnclosureId, Guid toEnclosureId)
    {
        Animal = animal;
        FromEnclosureId = fromEnclosureId;
        ToEnclosureId = toEnclosureId;
    }
}
