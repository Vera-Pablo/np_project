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
        private readonly ProjectDbContext _db;

        public AuthService(ProjectDbContext db) => _db = db;

        public async Task<User> LoginAsync(long dni, string password)
        {
            Console.WriteLine("Entramos al Login del Servicio");
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Dni == dni);

            if (user == null) throw new Exception("Usuario no encontrado");
            if (user.Password != password) throw new Exception("Contraseña Incorrecta");

            Console.WriteLine("Se logro logear con el usuario:", user.Name);
            return user;
        }

        public async Task<User> RegisterAsync(long dni, string password, string name)
        {
            if (await _db.Users.AnyAsync(u => u.Dni == dni)) throw new Exception("El usuario ya existe");

            var user = new User
            {
                Dni = dni,
                Password = password,
                Name = name
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }
    }
}
