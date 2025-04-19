using System;

namespace Zoo.Domain.ValueObjects;

public class Species
{
    public string Value { get; }

    public Species(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Species must not be empty.");

        Value = value;
    }

    public override bool Equals(object? obj) =>
        obj is Species other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}
