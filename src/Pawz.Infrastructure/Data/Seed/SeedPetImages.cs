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
                "https://picsum.photos/id/238/200/300",
                "https://picsum.photos/id/241/200/300",
                "https://picsum.photos/id/242/200/300"
            };

            string[] catImageUrls =
            {
                "https://picsum.photos/id/239/200/300",
                "https://picsum.photos/id/240/200/300",
                "https://picsum.photos/id/243/200/300",
                "https://picsum.photos/id/244/200/300"
            };

            await context.Database.ExecuteSqlRawAsync(@"
                INSERT INTO PetImages (Id, PetId, ImageUrl, IsPrimary, UploadedAt, isDeleted, Description) VALUES
                (1, 1, @dogUrl1, 1, @uploadedAt1, false, 'Dog Image 1'),
                (2, 1, @dogUrl2, 0, @uploadedAt1, false, 'Dog Image 2'),
                (3, 1, @dogUrl3, 0, @uploadedAt1, false, 'Dog Image 3'),
                (4, 1, @dogUrl4, 0, @uploadedAt1, false, 'Dog Image 4'),
                (5, 2, @dogUrl1, 1, @uploadedAt2, false, 'Dog Image 1'),
                (6, 2, @dogUrl2, 0, @uploadedAt2, false, 'Dog Image 2'),
                (7, 2, @dogUrl3, 0, @uploadedAt2, false, 'Dog Image 3'),
                (8, 2, @dogUrl4, 0, @uploadedAt2, false, 'Dog Image 4'),
                (9, 3, @catUrl1, 1, @uploadedAt3, false, 'Cat Image 1'),
                (10, 3, @catUrl2, 0, @uploadedAt3, false, 'Cat Image 2'),
                (11, 3, @catUrl3, 0, @uploadedAt3, false, 'Cat Image 3'),
                (12, 3, @catUrl4, 0, @uploadedAt3, false, 'Cat Image 4'),
                (13, 4, @catUrl1, 1, @uploadedAt4, false, 'Cat Image 1'),
                (14, 4, @catUrl2, 0, @uploadedAt4, false, 'Cat Image 2'),
                (15, 4, @catUrl3, 0, @uploadedAt4, false, 'Cat Image 3'),
                (16, 4, @catUrl4, 0, @uploadedAt4, false, 'Cat Image 4')",
                new SqliteParameter("@dogUrl1", dogImageUrls[0]),
                new SqliteParameter("@dogUrl2", dogImageUrls[1]),
                new SqliteParameter("@dogUrl3", dogImageUrls[2]),
                new SqliteParameter("@dogUrl4", dogImageUrls[3]),
                new SqliteParameter("@catUrl1", catImageUrls[0]),
                new SqliteParameter("@catUrl2", catImageUrls[1]),
                new SqliteParameter("@catUrl3", catImageUrls[2]),
                new SqliteParameter("@catUrl4", catImageUrls[3]),
                new SqliteParameter("@uploadedAt1", DateTime.UtcNow),
                new SqliteParameter("@uploadedAt2", DateTime.UtcNow),
                new SqliteParameter("@uploadedAt3", DateTime.UtcNow),
                new SqliteParameter("@uploadedAt4", DateTime.UtcNow)
            );
        }
    }
}
