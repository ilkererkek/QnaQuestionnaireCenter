using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class QuestionnaireManager : IQuestionnaireService
    {
        private readonly IQuestionnaireRepository _questionnaireRepository;

        public QuestionnaireManager(IQuestionnaireRepository questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }
        public Questionnaire Add(Questionnaire questionnaire, Guid UserId)
        {
            var oldQuestionnaire = GetById(questionnaire.Id);
            if(oldQuestionnaire == null)
            {
                return _questionnaireRepository.Add(questionnaire, UserId);
            }
            return null;
        }

        public bool Delete(Guid id)
        {
            var Questionnaire = GetById(id);
            if(Questionnaire != null)
            {
                _questionnaireRepository.Delete(id);
                return true;
            }
            return false;
        }

        public List<Questionnaire> GetAll()
        {
            return _questionnaireRepository.GetList(new { }, "SELECT * FROM Questionnaires;");
        }

        public List<Questionnaire> GetAllByUserId(Guid UserId)
        {
            return _questionnaireRepository.GetList(new { UserId}, "SELECT * FROM Questionnaires " +
                "LEFT JOIN QuestionnaireUser ON Questionnaires.Id = QuestionnaireUser.QuestionnairesId " +
                "WHERE QuestionnaireUser.UsersId = @UserId;");
        }

        public Questionnaire GetById(Guid Id)
        {
            return _questionnaireRepository.GetById(Id);
        }

        public Questionnaire GetWithQuestions(Guid Id)
        {
            var questionnaire =  _questionnaireRepository.GetWithQuestions(new { Id = Id }, "SELECT * FROM Questionnaires " +
                "LEFT JOIN Questions ON Questions.QuestionnaireId = Questionnaires.Id " +
                "WHERE Questionnaires.Id = @Id");
            foreach (var item in questionnaire.Questions)
            {
                switch (item.Type)
                {
                    case Entity.Concrete.Enums.QuestionType.NUMERICAL:
                        break;
                    case Entity.Concrete.Enums.QuestionType.SELECTION:
                        break;
                    case Entity.Concrete.Enums.QuestionType.MULTISELECTION:
                        break;
                    case Entity.Concrete.Enums.QuestionType.OPENENDED:
                        break;
                    default:
                        break;
                }
            }
            return questionnaire;
        }

        public Questionnaire Update(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
