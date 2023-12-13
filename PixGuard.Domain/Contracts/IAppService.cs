namespace Domain.Contracts;

public interface IAppService<TDto, TCreateDto>
    where TDto : class
    where TCreateDto : class
{
    Task<TDto> GetById(Guid id);
    Task<IEnumerable<TDto>> GetAll();
    Task<bool> Add(TCreateDto createDto);

}