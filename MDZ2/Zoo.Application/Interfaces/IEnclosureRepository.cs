using System.Collections.Generic;
using System;
using Zoo.Domain.Entities;

namespace Zoo.Application.Interfaces;
public interface IEnclosureRepository
{
    Enclosure? GetById(Guid id);
    void Save(Enclosure enclosure);
    IEnumerable<Enclosure> GetAll();
}