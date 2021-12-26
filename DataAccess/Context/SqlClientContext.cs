using Entity.Abstract.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class SqlClientContext
    {
        private string connectionString = "";

        private Dictionary<IEntity, string> tables;
    }
}
