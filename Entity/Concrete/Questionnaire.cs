using Entity.Concrete.Entity;
using Entity.Concrete.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class Questionnaire : BaseEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsHidden { get; set; }
        public QuestionnaireStatus Status { get; set; }
        public virtual List<User> Users { get; set; }
        public virtual List<Question> Questions { get; set; }
    }
}
