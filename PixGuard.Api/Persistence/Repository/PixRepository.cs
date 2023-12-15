using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using PixGuard.Api.Application.Contracts;


namespace PixGuard.Api.Persistence.Repository;

public class PixRepository : Repository<Pix>, IPixRepository
{
    private readonly AppDbContext _context;

    public PixRepository(AppDbContext context): base(context)
    {
        _context = context;
    }
   
    public async Task<List<Pix>> GetByValue(string value)
    {
      return  await _context.Set<Pix>().Where(x => x.KeyValue.Contains(value)).ToListAsync();
      
    }
}
