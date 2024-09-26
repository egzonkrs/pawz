using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;

namespace Pawz.Infrastructure.Repositories;

public class SpeciesRepository : GenericRepository<Species, int>, ISpeciesRepository
{
    public SpeciesRepository(AppDbContext context) : base(context)
    {
    }
}
