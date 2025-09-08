using Final_Project.Models;

namespace Final_Project.Repo.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(int id);
        Task<User?> GetByEmail(string email);
        Task Add(User user);
        Task Update(User user);
        Task Delete(int id);
    }
}
