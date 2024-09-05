using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;

namespace Pawz.Infrastructure.Repos;

public class PetImageRepository : GenericRepository<PetImage, int>, IPetImageRepository
{
    public PetImageRepository(AppDbContext context) : base(context)
    { }
}

