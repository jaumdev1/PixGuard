namespace PixGuard.Api.Application.Contracts;

public interface IRepository where T : class
{
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}