using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed;

public class SeedPetImages
{
    public static async Task SeedPetImageData(AppDbContext context)
    {
        var petImagesExist = await context.Breeds.AnyAsync();
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
                    INSERT INTO PetImages (Id, PetId, ImageUrl, IsPrimary, UploadedAt) VALUES
                    (1, 1, @dogUrl1, 1, @uploadedAt),
                    (2, 1, @dogUrl2, 0, @uploadedAt),
                    (3, 2, @catUrl1, 1, @uploadedAt),
                    (4, 2, @catUrl2, 0, @uploadedAt)",
            new SqliteParameter("@dogUrl1", dogImageUrls[0]),
            new SqliteParameter("@dogUrl2", dogImageUrls[1]),
            new SqliteParameter("@catUrl1", catImageUrls[0]),
            new SqliteParameter("@catUrl2", catImageUrls[1]),
            new SqliteParameter("@uploadedAt", DateTime.UtcNow)
        );
    }
}
