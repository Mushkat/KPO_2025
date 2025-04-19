using System;
using Zoo.Domain.ValueObjects;

namespace Zoo.Domain.Entities;

public class Animal
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public AnimalName Name { get; set; }
    public Species Species { get; set; }
    public Guid EnclosureId { get; set; }

    public Animal(AnimalName name, Species species, Guid enclosureId)
    {
        Name = name;
        Species = species;
        EnclosureId = enclosureId;
    }

    public void MoveToEnclosure(Guid newEnclosureId)
    {
        EnclosureId = newEnclosureId;
    }
}
