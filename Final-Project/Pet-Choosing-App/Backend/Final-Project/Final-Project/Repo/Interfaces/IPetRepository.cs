using Final_Project.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Final_Project.Repo.Interfaces
{
    public interface IPetRepository
    {
        Task<IEnumerable<Pet>> GetAllAsync();
        Task<Pet> GetByIdAsync(int id);
        Task<Pet> AddAsync(Pet pet);
        Task<Pet> UpdateAsync(Pet pet);
        Task<bool> DeleteAsync(int id);
    }
}
