using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Pawz.Domain.Enums;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedPets
{
    public static async Task SeedPetData(AppDbContext context)
    {
        var petsExists = await context.Pets.AnyAsync();
        if (petsExists) return;

        var userJohn = await context.Users.FirstOrDefaultAsync(u => u.UserName == "john");
        var userJane = await context.Users.FirstOrDefaultAsync(u => u.UserName == "jane");

        string sql = @"
            INSERT INTO Pets
            (Name, SpeciesId, BreedId, AgeYears, AgeMonths, About, Price, Status, CreatedAt, LocationId, PostedByUserId, IsDeleted) VALUES
            ('Buddy', 1, 4, 3, 5, 'Friendly and playful dog.', 300.00, @statusPending, GETDATE(), 1, @userJohnId, 0),
            ('Max', 1, 5, 2, 2, 'Loyal and intelligent dog.', 250.00, @statusApproved, GETDATE(), 2, @userJaneId, 0),
            ('Whiskers', 2, 6, 1, 0, 'Quiet and affectionate cat.', 150.00, @statusRejected, GETDATE(), 3, @userJohnId, 0),
            ('Mittens', 2, 7, 3, 3, 'Active and social cat.', 200.00, @statusPending, GETDATE(), 4, @userJaneId, 0)";

        await context.Database.ExecuteSqlRawAsync(sql,
            new SqlParameter("@statusPending", (int)PetStatus.Pending),
            new SqlParameter("@statusApproved", (int)PetStatus.Approved),
            new SqlParameter("@statusRejected", (int)PetStatus.Rejected),
            new SqlParameter("@userJohnId", userJohn.Id),
            new SqlParameter("@userJaneId", userJane.Id)
        );

        await context.SaveChangesAsync();
    }
}
