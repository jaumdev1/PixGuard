using Microsoft.EntityFrameworkCore;
namespace PixGuard.Api.Persistence;

public class AppDbContext: DbContext
{
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
  
}