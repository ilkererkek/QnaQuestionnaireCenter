using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utils
{
    public interface IConfiguration
    {
        public string ConnectionString { get; }
    }

    public class Configuration : IConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
