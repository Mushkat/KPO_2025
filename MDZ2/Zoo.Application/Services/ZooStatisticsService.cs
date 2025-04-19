using Zoo.Domain.Repositories;
using Zoo.Application.DTOs;
using System.Linq;
using System.Threading.Tasks;

namespace Zoo.Application.Services
{
    public class ZooStatisticsService
    {
        private readonly IAnimalRepository _animalRepository;
        private readonly IEnclosureRepository _enclosureRepository;

        public ZooStatisticsService(IAnimalRepository animalRepository, IEnclosureRepository enclosureRepository)
        {
            _animalRepository = animalRepository;
            _enclosureRepository = enclosureRepository;
        }

        public async Task<ZooStatisticsDto> GetStatistics()
        {
            var animals = await _animalRepository.GetAllAsync();
            var enclosures = await _enclosureRepository.GetAllAsync();

            int totalAnimals = animals.Count();
            int totalEnclosures = enclosures.Count();
            int emptyEnclosures = enclosures.Count(e =>
                !animals.Any(a => a.EnclosureId == e.Id));

            return new ZooStatisticsDto
            {
                TotalAnimals = totalAnimals,
                TotalEnclosures = totalEnclosures,
                EmptyEnclosures = emptyEnclosures
            };
        }
    }
}
