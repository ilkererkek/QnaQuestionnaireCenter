using Entity.Concrete;
using System.ComponentModel;

namespace Web.Models
{
    public class CreateQuestionnaireViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        [DisplayName("Private")]
        public bool IsHidden { get; set; }

        public Questionnaire ToQuestionnaire()
        {
            return new Questionnaire()
            {
                Name = Name,
                Description = Description,
                IsHidden = IsHidden,
                Status = Entity.Concrete.Enums.QuestionnaireStatus.CREATED
            };
        }
    }


}
