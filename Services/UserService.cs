using Microsoft.EntityFrameworkCore;
using np_project.Data;
using np_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace np_project.Services
{
    public class UserService : IUserService
    {
        private readonly ProjectDbContext _db;

        public UserService(ProjectDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id && u.isActive);
        }

        public async Task<User> CreateAsync(User user)
        {
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            var existingUser = await _db.Users.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Dni = user.Dni;
                existingUser.Name = user.Name;
                existingUser.Role = user.Role;

                
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                user.isActive = false;
                await _db.SaveChangesAsync();
            }
        }
        public async Task ActiveAsync(int id)
        {
            var user = await _db.Users.FindAsync(id);
            if (user != null)
            {
                user.isActive = true;
                await _db.SaveChangesAsync();
            }
        }
    }
}
