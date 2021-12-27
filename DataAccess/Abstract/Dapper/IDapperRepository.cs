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
        void Add(T entity);

        void Update(T entity);

        void Delete(Guid Id);

        T? Get(object parameters, string where = "Id = @Id", List<string>? include = null);

        IList<T> GetList(object parameters, string where = "Id = @Id", List<string>? include = null);
    }
}
