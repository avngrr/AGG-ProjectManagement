namespace Application.Common.Interfaces.Data;

public interface IApplicationDbContext 
{
    Task<int> SaveChangesAsync(string userId = null);
}