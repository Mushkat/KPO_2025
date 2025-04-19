using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Zoo.Domain.Entities;

namespace Zoo.Domain.Repositories;

public interface IAnimalRepository
{
    Task AddAsync(Animal animal);
    Task DeleteAsync(Guid id);
    Task<Animal?> GetByIdAsync(Guid id);
    Task<List<Animal>> GetAllAsync();
    Task UpdateAsync(Animal animal);
}
