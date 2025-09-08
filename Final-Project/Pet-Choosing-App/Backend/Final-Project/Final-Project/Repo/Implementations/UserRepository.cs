﻿using Final_Project.AppDbContext;
using Final_Project.Models;
using Final_Project.Repo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Repo.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly PetDbContext _context;
        public UserRepository(PetDbContext context)
        {
            _context = context;
        }
        public async Task<User?> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User?> GetByEmail(string email)   
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        public async Task Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
