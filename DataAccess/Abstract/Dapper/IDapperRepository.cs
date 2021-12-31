using DataAccess.Utils;
using Entity.Abstract.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.Dapper
{
    public interface IDapperRepository<T> where T : class, IEntity, new()
    {

        static IConfig _config;
        T Add(T entity);
        T Update(T entity);
        void Delete(Guid id);

        T GetById(Guid id);
        T Get(object parameters, string rawSql);
        List<T> GetList(object parameters, string rawSql);
    }
}
