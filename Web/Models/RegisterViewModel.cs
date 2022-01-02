using Entity.Concrete;
using System.ComponentModel;

namespace Web.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string? Organization { get; set; }
        public string? Occupation { get; set; }
        [DisplayName("Phone Number")]
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }


       public User ToUser()
        {
            return new User()
            {
                Name = Name,
                Surname = Surname,
                Organization = Organization,
                Occupation = Occupation,
                PhoneNumber = PhoneNumber,
                Email = Email
            };
        }
        
    }
}
