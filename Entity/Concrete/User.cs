using Entity.Concrete.Entity;
using Entity.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Organization { get; set; }
        public string? Occupation { get; set; }
        public UserRole UserRole { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public byte[] PasswordHash { get; set; } 
        public byte[] PasswordSalt { get; set; }

        public virtual List<Questionnaire>? Questionnaires { get; set; }
    }
}
