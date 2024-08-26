using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Pawz.Infrastructure.Data.Seed
{
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
                INSERT INTO PetImages (Id, PetId, ImageUrl, IsPrimary, UploadedAt, isDeleted, Description) VALUES
                (1, 1, @dogUrl1, 1, @uploadedAt1, false, 'test'),
                (2, 1, @dogUrl2, 1, @uploadedAt2, false, 'test'),
                (3, 2, @catUrl1, 1, @uploadedAt3, false, 'test'),
                (4, 2, @catUrl2, 1, @uploadedAt4, false, 'test')",
                new SqliteParameter("@dogUrl1", dogImageUrls[0]),
                new SqliteParameter("@dogUrl2", dogImageUrls[1]),
                new SqliteParameter("@catUrl1", catImageUrls[0]),
                new SqliteParameter("@catUrl2", catImageUrls[1]),
                new SqliteParameter("@uploadedAt1", DateTime.UtcNow),
                new SqliteParameter("@uploadedAt2", DateTime.UtcNow),
                new SqliteParameter("@uploadedAt3", DateTime.UtcNow),
                new SqliteParameter("@uploadedAt4", DateTime.UtcNow)
            );
        }
    }
}
