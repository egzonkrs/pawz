using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;

namespace Pawz.Infrastructure.Repos
{
    public class PetRepository : GenericRepository<Pet, int>, IPetRepository
    { 
        public PetRepository(AppDbContext context) : base(context)
        {
        }
    }
}