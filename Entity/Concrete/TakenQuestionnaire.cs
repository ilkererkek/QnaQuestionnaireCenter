using Entity.Concrete.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class TakenQuestionnaire : BaseEntity
    {
        [ForeignKey("Questionnaire")]
        public Guid QuestionnaireId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime FinishTime { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }
        public virtual List<MultiSelection> MultiSelections { get; set; }
        public virtual List<OptionSelection> OptionSelections { get; set; }
    }
}
