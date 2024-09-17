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
    private readonly AppDbContext _context;
    public NotificationRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Notification>> GetNotificationsForUserAsync(string userId, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .Where(n => n.RecipientId == userId)
            .OrderByDescending(n => n.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Notification> GetNotificationByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Notifications
            .FirstOrDefaultAsync(n => n.Id == id, cancellationToken);
    }

    public async Task<Notification> GetExistingNotificationAsync(string senderId, string recipientId, int? petId, CancellationToken cancellationToken)
    {
        return await _context.Notifications
            .FirstOrDefaultAsync(n =>
                n.SenderId == senderId &&
                n.RecipientId == recipientId &&
                n.PetId == petId,
                cancellationToken);
    }
}
