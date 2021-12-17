using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class NumericalAnswer : BaseAnswer
    {
        public int NumericalValue { get; set; }
        public virtual List<NumericalSelection> NumericalSelections { get; set; }
    }
}
