using Entity.Concrete.Entity;
using Entity.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Question : BaseEntity
    {
        public Question() : base()
        {

        }
        public string Text { get; set; }
        public int OrderNo { get; set; }
        public QuestionType Type { get; set; }
        public Guid QuestionnaireId { get; set; }
        public virtual Questionnaire Questionnaire { get; set; }
        public virtual List<BaseAnswer> Answers { get; set; }
    }
}
