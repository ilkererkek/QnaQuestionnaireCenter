using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class OptionAnswer : BaseAnswer
    {
        public int OrderId { get; set; }
        public virtual List<OptionSelection> OptionSelections { get; set; }
    }
}
