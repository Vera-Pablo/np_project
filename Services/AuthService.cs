using Microsoft.EntityFrameworkCore;
using np_project.Core.DTOs;
using np_project.Core.Models;
using np_project.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace np_project.Services
{
    public class AuthService
    {
        private readonly ProjectDbContext _context;

        public AuthService(ProjectDbContext context)
        {
            _context = context;
        }

        public async Task<UserDto> LoginAsync(long dni, string password)
        {
            var user = await _context.Users.Where(user => user.Dni == dni && user.Password == password).FirstOrDefaultAsync();

            if (user == null) throw new ArgumentException("Usuario no encontrado");
            if (user.Password != password) throw new ArgumentException("Credenciales incorrectas");

            return new UserDto
            {
                Dni = user.Dni,
                Name = user.Name,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }
    }
}
