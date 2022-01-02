using Entity.Concrete;

namespace Web.Models
{
    public class CreateQuestionnaireViewModel
    {
        public string Name { get; set; }
        public string? Description { get; set; }
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
