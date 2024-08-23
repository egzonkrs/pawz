using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedPetImages
{
    public static async Task SeedPetImageData(AppDbContext context)
    {
        var petImagesExist = await context.PetImages.AnyAsync();
        if (petImagesExist) return;

        string[] dogImageUrls =
        {
                    "https://picsum.photos/id/237/200/300",
                    "https://picsum.photos/id/238/200/300"
        };

        string[] catImageUrls =
        {
                    "https://picsum.photos/id/239/200/300",
                    "https://picsum.photos/id/240/200/300"
        };

        await context.Database.ExecuteSqlRawAsync(@"
            SET IDENTITY_INSERT PetImages ON;

            INSERT INTO PetImages (Id, PetId, ImageUrl, IsPrimary, UploadedAt, Description, IsDeleted, DeletedAt) VALUES
            (1, 1, @dogUrl1, 1, @uploadedAt, 'Dog Image 1', 0, NULL),
            (2, 2, @dogUrl2, 0, @uploadedAt, 'Dog Image 2', 0, NULL),
            (3, 3, @catUrl1, 1, @uploadedAt, 'Cat Image 1', 0, NULL),
            (4, 4, @catUrl2, 0, @uploadedAt, 'Cat Image 2', 0, NULL);

            SET IDENTITY_INSERT PetImages OFF;",
            new SqlParameter("@dogUrl1", dogImageUrls[0]),
            new SqlParameter("@dogUrl2", dogImageUrls[1]),
            new SqlParameter("@catUrl1", catImageUrls[0]),
            new SqlParameter("@catUrl2", catImageUrls[1]),
            new SqlParameter("@uploadedAt", DateTime.UtcNow)
        );
    }
}
