using Final_Project.Models;

namespace Final_Project.Repo.Interfaces
{
    public interface IReturnRepository
    {
        Task<Return> AddAsync(Return r);
        Task<IEnumerable<Return>> GetAllAsync();
        Task<Return?> UpdateStatusAsync(int id, string status);
        Task<bool> DeleteAsync(int id);

    }
}
