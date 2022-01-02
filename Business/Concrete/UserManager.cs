using Business.Abstract;
using Business.Security;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAll()
        {
            return _userRepository.GetList(new {}, "SELECT * FROM Users");
        }

        public User GetById(Guid Id)
        {
            return _userRepository.GetById(Id);
        }

        public User GetByMail(string email)
        {
            return _userRepository.Get(new { Email = email }, "SELECT * FROM Users WHERE Users.Email = @Email;");
        }

        public User Login(string email, string password)
        {
            var user = GetByMail(email);
            if (user != null)
            {
                if(HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    return user;
                }
            }
            return null;
        }

        public bool Register(User user, string password)
        {
            var oldUser = GetByMail(user.Email);
            if (user != null && oldUser == null)
            {
                byte[] hash, salt;
                HashingHelper.CreatePasswordHash(password,out hash,out salt);
                user.PasswordHash = hash;
                user.PasswordSalt = salt;
                _userRepository.Add(user);

                return true;
            }
            return false;
        }
    }
}
