using Entity.Abstract.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract.Entity
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        T Get(Expression<Func<T, bool>>? filter = null);

        IList<T> GetList(Expression<Func<T, bool>>? filter = null);

        T GetWithInclude(Expression<Func<T,bool>>? filter = null, List<string>? includes = null);
        List<T> GetListWithInclude(Expression<Func<T,bool>>? filter = null, List<string>? includes = null);


            

    }
}
