using Entity.Concrete.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class BaseAnswer : BaseEntity
    {
        public BaseAnswer() : base()
        {
            Text = "";
        }
        public string Title { get; set; }
        public string? Text { get; set; }
        public Guid QuestionId { get; set; }
        public virtual Question? Question { get; set; }
    }
}
