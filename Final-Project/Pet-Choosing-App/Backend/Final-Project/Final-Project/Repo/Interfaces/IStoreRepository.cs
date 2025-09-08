using Final_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Final_Project.Repo.Interfaces
{
    public interface IStoreRepository
    {
        Task<IEnumerable<Store>> GetAllAsync();
        Task<Store?> GetByIdAsync(int id);
        Task<Store> AddAsync(Store store);
        Task<Store> UpdateAsync(Store store);
        Task<bool> DeleteAsync(int id);
    }
}
