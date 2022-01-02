using Entity.Concrete.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class MultiSelection : BaseEntity
    {

        public Guid MultiAnswerId { get; set; }
        public Guid TakenQuestionnaireId { get; set; }

        public virtual TakenQuestionnaire? TakenQuestionnaire { get; set; }
        public virtual MultiAnswer? MultiAnswer { get; set; }

    }
}
