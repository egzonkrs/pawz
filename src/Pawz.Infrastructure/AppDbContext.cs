using Microsoft.EntityFrameworkCore;

namespace Pawz.Infrastructure;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    
}
