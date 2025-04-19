using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Zoo.Domain.Entities;

namespace Zoo.Domain.Repositories;

public interface IEnclosureRepository
{
    Task<Enclosure?> GetByIdAsync(Guid id);
    Task<List<Enclosure>> GetAllAsync();
    Task AddAsync(Enclosure enclosure);
    Task UpdateAsync(Enclosure enclosure);
    Task DeleteAsync(Guid id);
}
