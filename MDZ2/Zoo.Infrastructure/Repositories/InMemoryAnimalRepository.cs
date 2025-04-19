using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Zoo.Domain.Entities;
using Zoo.Domain.Repositories;

namespace Zoo.Infrastructure.Repositories;

public class InMemoryAnimalRepository : IAnimalRepository
{
    private readonly List<Animal> _animals = new();

    public Task AddAsync(Animal animal)
    {
        _animals.Add(animal);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        _animals.RemoveAll(a => a.Id == id);
        return Task.CompletedTask;
    }

    public Task<List<Animal>> GetAllAsync()
    {
        return Task.FromResult(_animals.ToList());
    }

    public Task<Animal?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_animals.FirstOrDefault(a => a.Id == id));
    }

    public Task UpdateAsync(Animal animal)
    {
        var index = _animals.FindIndex(a => a.Id == animal.Id);
        if (index != -1)
        {
            _animals[index] = animal;
        }
        return Task.CompletedTask;
    }
}
