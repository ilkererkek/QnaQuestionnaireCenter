using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Abstract
{
    public interface IUserService
    {
        bool Register(User user, string password);
        User Login(string email, string password);
        User GetByMail(string email);
        User GetById(Guid Id);
        List<User> GetAll();
    }
}
