using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;

namespace Pawz.Infrastructure.Repos
{
    public class PetRepository(AppDbContext context) : GenericRepository<Pet>(context), IPetRepository
    {
    }
}