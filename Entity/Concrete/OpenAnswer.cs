using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class OpenAnswer : BaseAnswer
    {
        public OpenAnswer(): base()
        {
            Title = "";
            Text = "";
        }
        public string Answer { get; set; }
        public Guid TakenQuestionnaireId { get; set; }
    }
}
