using System;

namespace Zoo.Domain.ValueObjects;

public class AnimalName
{
    public string Value { get; }

    public AnimalName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Animal name must not be empty.");

        Value = value;
    }

    public override bool Equals(object? obj) =>
        obj is AnimalName other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}
