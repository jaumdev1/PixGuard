using Domain.Entities;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using PixGuard.Api.Application.Contracts;


namespace PixGuard.Api.Persistence.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context): base(context)
    {
        _context = context;
    }
   
    public async Task<User> GetByEmail(string email)
    {
      return  await _context.Set<User>().Where(x => x.Email == email).SingleOrDefaultAsync();
      
    }
}
