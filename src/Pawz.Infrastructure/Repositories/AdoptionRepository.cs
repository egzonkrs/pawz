using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;

namespace Pawz.Infrastructure.Repositories;

public class AdoptionRepository : GenericRepository<Adoption, int>, IAdoptionRepository
{
    public AdoptionRepository(AppDbContext context) : base(context)
    {
    }
}
