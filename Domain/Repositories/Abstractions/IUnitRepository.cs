namespace Domain.Repositories.Abstractions;

public interface IUnitRepository
{
    Task RefreshCache();
}
