using Entity.Concrete.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class MultiAnswer : BaseAnswer
    {
        
        public virtual List<MultiSelection>? MultiSelections { get; set; }
    }
}
