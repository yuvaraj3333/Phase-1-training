using Final_Project.AppDbContext;
using Final_Project.Models;
using Final_Project.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Repo.Implementations
{
    public class ReturnRepository : IReturnRepository
    {
        private readonly PetDbContext _context;

        public ReturnRepository(PetDbContext context)
        {
            _context = context;
        }

        public async Task<Return> AddAsync(Return returnRequest)
        {
            _context.Returns.Add(returnRequest);
            await _context.SaveChangesAsync();
            return returnRequest;
        }

        public async Task<IEnumerable<Return>> GetAllAsync()
        {
            return await _context.Returns
                .Include(r => r.Order)
                .Include(r => r.OrderItem)
                .ToListAsync();
        }

        public async Task<Return> GetByIdAsync(int id)
        {
            return await _context.Returns
                .Include(r => r.Order)
                .Include(r => r.OrderItem)
                .FirstOrDefaultAsync(r => r.ReturnId == id);
        }

        public async Task<Return> UpdateStatusAsync(int id, string status)
        {
            var returnRequest = await _context.Returns.FindAsync(id);
            if (returnRequest == null) return null;

            returnRequest.Status = status;
            await _context.SaveChangesAsync();

            return returnRequest;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var returnRequest = await _context.Returns.FindAsync(id);
            if (returnRequest == null) return false;

            _context.Returns.Remove(returnRequest);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
