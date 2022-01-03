using Entity.Concrete;
using Entity.Concrete.Enums;

namespace Web.Models
{
    public class QuestionViewModel
    {
        public Guid TakenQuestionnaireId { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public Guid QuestionId { get; set; }
        public Guid QuestionnaireId { get; set; }
        public QuestionType Type { get; set; }
        public Question Question { get; set; }
        public string OpenAnswer { get; set; }
        public bool[] MultiSelection { get; set; }
        public Guid Selection { get; set; }
    }
}
