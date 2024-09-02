using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;

namespace Pawz.Infrastructure.Repos;

public class BreedRepository : GenericRepository<Breed, int>, IBreedRepository
{
    public BreedRepository(AppDbContext context) : base(context)
    {
    }
}
