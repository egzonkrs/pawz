using Pawz.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Domain.Interfaces;

public interface ICityRepository : IGenericRepository<City, int>
{
    Task<IEnumerable<City>> GetCitiesByCountryIdAsync(int countryId, CancellationToken cancellationToken);
}

