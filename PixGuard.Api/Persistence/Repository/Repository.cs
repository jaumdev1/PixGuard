using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using PixGuard.Api.Application.Contracts;


namespace PixGuard.Api.Persistence.Repository;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task Add(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Could not add ");

        }
        
    }
    public async Task Update(T entity )
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null) return;

        entity.IsDeleted = true;
        await _context.SaveChangesAsync();
    }
}
