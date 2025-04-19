using System;

public class Food
{
    public string Value { get; }

    public Food(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Food cannot be empty.");

        Value = value;
    }

    public override bool Equals(object? obj) =>
        obj is Food other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();
}
