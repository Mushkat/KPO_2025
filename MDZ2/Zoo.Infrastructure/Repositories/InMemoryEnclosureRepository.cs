using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Zoo.Domain.Entities;
using Zoo.Domain.Repositories;

namespace Zoo.Infrastructure.Repositories;

public class InMemoryEnclosureRepository : IEnclosureRepository
{
    private readonly List<Enclosure> _enclosures = new();

    public Task AddAsync(Enclosure enclosure)
    {
        _enclosures.Add(enclosure);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _enclosures.RemoveAll(e => e.Id == id);
        return Task.CompletedTask;
    }

    public Task<List<Enclosure>> GetAllAsync()
    {
        return Task.FromResult(_enclosures.ToList());
    }

    public Task<Enclosure?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_enclosures.FirstOrDefault(e => e.Id == id));
    }

    public Task UpdateAsync(Enclosure enclosure)
    {
        var index = _enclosures.FindIndex(e => e.Id == enclosure.Id);
        if (index != -1)
        {
            _enclosures[index] = enclosure;
        }
        return Task.CompletedTask;
    }
}
