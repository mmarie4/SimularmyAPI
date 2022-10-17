namespace Domain.Repositories.Abstractions;

public interface IBaseRepository
{
    Task<int> SaveAsync();
}
