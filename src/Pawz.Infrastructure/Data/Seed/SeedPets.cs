using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Enum;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed
{
    public class SeedPets
    {
        public static async Task SeedPetData(AppDbContext context)
        {
            if (!await context.Pets.AnyAsync())
            {

                var userJohn = await context.Users.FirstOrDefaultAsync(u => u.UserName == "john");
                var userJane = await context.Users.FirstOrDefaultAsync(u => u.UserName == "jane");

                string sql = @"
                    INSERT INTO Pets 
                    (Id, Name, SpeciesId, BreedId, AgeYears, AgeMonths, About, Price, Status, CreatedAt, LocationId, PostedByUserId) VALUES 
                    (1, 'Buddy', 1, 1, 3, 5, 'Friendly and playful dog.', 300.00, @statusPending, datetime('now'), 1, @userJohnId),
                    (2, 'Max', 1, 2, 2, 2, 'Loyal and intelligent dog.', 250.00, @statusApproved, datetime('now'), 2, @userJaneId),
                    (3, 'Whiskers', 2, 3, 1, 0, 'Quiet and affectionate cat.', 150.00, @statusRejected, datetime('now'), 3, @userJohnId),
                    (4, 'Mittens', 2, 4, 3, 3, 'Active and social cat.', 200.00, @statusPending, datetime('now'), 4, @userJaneId)";

                await context.Database.ExecuteSqlRawAsync(sql,
                    new SqliteParameter("@statusPending", (int)PetStatus.Pending),
                    new SqliteParameter("@statusApproved", (int)PetStatus.Approved),
                    new SqliteParameter("@statusRejected", (int)PetStatus.Rejected),
                    new SqliteParameter("@userJohnId", userJohn.Id),
                    new SqliteParameter("@userJaneId", userJane.Id)
                );

                await context.SaveChangesAsync();
            }
        }
    }
}
