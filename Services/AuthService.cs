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
    class AuthService
    {
        private readonly ProjectDbContext _context;

        public AuthService(ProjectDbContext context) => _context = context;

        public async Task<User> LoginAsync(long dni, string password)
        {
            Console.WriteLine("Entramos al Login del Servicio");
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Dni == dni);

            if (user == null) throw new Exception("Usuario no encontrado");
            if (user.Password != password) throw new Exception("Contraseña Incorrecta");

            Console.WriteLine("Se logro logear con el usuario:", user.Name);
            return user;
        }

        public async Task<User> RegisterAsync(long dni, string password, string name)
        {
            if (await _context.Users.AnyAsync(u => u.Dni == dni)) throw new Exception("El usuario ya existe");

            var user = new User
            {
                Dni = dni,
                Password = password,
                Name = name
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
