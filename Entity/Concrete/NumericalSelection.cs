using Entity.Concrete.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class NumericalSelection : BaseEntity
    {
        public Guid NumericalAnswerId { get; set; }
        public int NumericalValue { get; set; }
        public Guid TakenQuestionnaireId { get; set; }
        public virtual NumericalAnswer NumericalAnswer  { get; set; }
        public virtual TakenQuestionnaire TakenQuestionnaire { get; set; }
    }
}
