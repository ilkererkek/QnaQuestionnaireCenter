using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class OptionAnswer : BaseAnswer
    {
        public OptionAnswer() : base()
        {
            OrderId = 0;
        }
        public int OrderId { get; set; }
        public virtual List<OptionSelection>? OptionSelections { get; set; }
    }
}
