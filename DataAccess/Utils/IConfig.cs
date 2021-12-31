using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Utils
{
    public interface IConfig
    {
        public string ConnectionString { get; }
    }

    public class Config : IConfig
    {
        public string? ConnectionString { get; set; }
    }
}
