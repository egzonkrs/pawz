using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;

namespace Pawz.Infrastructure.Repositories;

public class LocationRepository : GenericRepository<Location, int>, ILocationRepository
{
    public LocationRepository(AppDbContext context) : base(context)
    {
    }
}
