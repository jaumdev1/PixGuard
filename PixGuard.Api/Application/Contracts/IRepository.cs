namespace PixGuard.Api.Application.Contracts;

public interface IRepository<T> where T : class
{
   
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(Guid id);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(int id);
}