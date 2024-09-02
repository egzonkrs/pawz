using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos;

public class CityRepository : GenericRepository<City, int>, ICityRepository
{
    public CityRepository(AppDbContext context) : base(context)
    {
    }

    /// <summary>
    /// Retrieves cities associated with a specific country by its Id.
    /// </summary>
    /// <param name="countryId">The Id of the country whose cities should be retrieved.</param>
    /// <param name="cancellationToken">A cancellation token for asynchronous operation.</param>
    /// <returns>A list of cities associated with the specified country.</returns>
    public async Task<IEnumerable<City>> GetCitiesByCountryIdAsync(int countryId, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Where(c => c.CountryId == countryId)
            .ToListAsync(cancellationToken);
    }
}
