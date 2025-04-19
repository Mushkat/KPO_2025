using System;

namespace Zoo.Domain.Entities;

public class Enclosure
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;

    public Enclosure(string name, string type)
    {
        Name = name;
        Type = type;
    }
}
