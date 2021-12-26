using Entity.Abstract.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Entity
{
    public class SQLClientEntity<T>
        where T : class, IEntity, new()
    {
        public T Model { get; set; }
        public string Table { get; set; }
    }
}
