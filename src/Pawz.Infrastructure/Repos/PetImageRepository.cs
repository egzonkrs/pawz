using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos;

public class PetImageRepository : GenericRepository<PetImage, int>, IPetImageRepository
{
    private readonly AppDbContext _context;

    public PetImageRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Inserts a new pet image into the repository asynchronously.
    /// </summary>
    /// <param name="petImage">The pet image entity to be inserted.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The ID of the inserted pet image.</returns>
    public async Task<int> InsertAsync(PetImage petImage, CancellationToken cancellationToken)
    {
        await _context.PetImages.AddAsync(petImage, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return petImage.Id;
    }
}

