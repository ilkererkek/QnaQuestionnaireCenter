﻿using DataAccess.Abstract.Dapper;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserRepository : IDapperRepository<User>
    {
        User GetWithQuestionnaires(object parameters, string rawSql);
        List<User> GetListWithQuestionnaires(object parameters, string rawSql);
    }
}
