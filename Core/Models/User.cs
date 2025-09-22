using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace np_project.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public long Dni { get; set; }
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
