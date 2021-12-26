using Entity.Abstract.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public static class SqlClientContext
    {

        public static Dictionary<string,Type>? tables { get; set; }
    }
}
