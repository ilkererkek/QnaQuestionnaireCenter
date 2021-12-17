using Entity.Concrete.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class OptionSelection : BaseEntity
    {
        public Guid TakenQuestionnaireId { get; set; }
        public Guid OptionAnswerId { get; set; }
        public virtual OptionAnswer OptionAnswer { get; set; }
        public virtual TakenQuestionnaire TakenQuestionnaire { get; set; }
    }
}
