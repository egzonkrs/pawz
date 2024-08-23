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
            SET IDENTITY_INSERT Pets ON;

            INSERT INTO Pets
            (Id, Name, SpeciesId, BreedId, AgeYears, AgeMonths, About, Price, Status, CreatedAt, LocationId, PostedByUserId, IsDeleted) VALUES
            (1, 'Buddy', 1, 1, 3, 5, 'Friendly and playful dog.', 300.00, @statusPending, GETDATE(), 1, @userJohnId, 0),
            (2, 'Max', 1, 2, 2, 2, 'Loyal and intelligent dog.', 250.00, @statusApproved, GETDATE(), 2, @userJaneId, 0),
            (3, 'Whiskers', 2, 3, 1, 0, 'Quiet and affectionate cat.', 150.00, @statusRejected, GETDATE(), 3, @userJohnId, 0),
            (4, 'Mittens', 2, 4, 3, 3, 'Active and social cat.', 200.00, @statusPending, GETDATE(), 4, @userJaneId, 0);

            SET IDENTITY_INSERT Pets OFF;";

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
