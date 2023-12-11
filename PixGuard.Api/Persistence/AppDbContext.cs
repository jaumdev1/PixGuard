using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
namespace PixGuard.Api.Persistence;
using  Domain.Entities;
public class AppDbContext: DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.GetProperty("IsDeleted") != null)
            {
                modelBuilder.Entity(entityType.Name).HasQueryFilter(GetIsDeletedRestriction(entityType.ClrType));
            }
        }
        modelBuilder.Entity<Pix>()
            .HasOne(p => p.User)
            .WithMany(u => u.PixKeys)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Restrict); 
       


        
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Pix> Pixs { get; set; }
    public DbSet<Assessment> Assessments { get; set; }
   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
   

    
    private static LambdaExpression GetIsDeletedRestriction(Type type)
    {
        var param = Expression.Parameter(type, "t");
        var body = Expression.Equal(Expression.Property(param, "IsDeleted"), Expression.Constant(false));
        return Expression.Lambda(body, param);
    }
      
 
    
}