using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Entities;
using Pawz.Domain.Interfaces;
using Pawz.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Repos;

public class NotificationRepository : GenericRepository<Notification, int>, INotificationRepository
{
    public NotificationRepository(AppDbContext context) : base(context)
    {

    }

    public async Task<IEnumerable<Notification>> GetNotificationsForUserAsync(string userId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .Where(n => n.RecipientId == userId)
            .Include(n => n.Pet)
            .Include(n => n.Sender)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Notification> GetNotificationByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet
            .Include(n => n.Pet)
            .Include(n => n.Sender)
            .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }

    public async Task<Notification> GetExistingNotificationAsync(string senderId, string recipientId, int? petId, CancellationToken cancellationToken)
    {
        return await _dbSet
            .FirstOrDefaultAsync(n =>
                n.SenderId == senderId &&
                n.RecipientId == recipientId &&
                n.PetId == petId,
                cancellationToken);
    }
}
