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
        private readonly IAnswerService _answerService;

        public QuestionnaireManager(IQuestionnaireRepository questionnaireRepository, IAnswerService answerService)
        {
            _questionnaireRepository = questionnaireRepository;
            _answerService = answerService;
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

        public List<Questionnaire> GetPublished()
        {
            return _questionnaireRepository.GetList(null, "SELECT TOP 10 * FROM Questionnaires WHERE Status = 2 AND IsHidden = 0 ORDER BY UpdatedAt DESC");
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
                   
                    case Entity.Concrete.Enums.QuestionType.SELECTION:
                        item.OptionAnswers = _answerService.GetOptionByQuestionId(item.Id);
                        break;
                    case Entity.Concrete.Enums.QuestionType.MULTISELECTION:
                        item.MultiAnswers = _answerService.GetMultiByQuestionId(item.Id);
                        break;
                }
            }
            return questionnaire;
        }

        public Questionnaire Update(Questionnaire questionnaire)
        {
            var oldQustionnaire = GetById(questionnaire.Id);
            if(oldQustionnaire != null)
            {
                questionnaire.CreatedAt = oldQustionnaire.CreatedAt;
                questionnaire.Status = oldQustionnaire.Status;
                _questionnaireRepository.Update(questionnaire);
                return questionnaire;
            }
            return null;
        }

        public Questionnaire UpdateStatus(Questionnaire questionnaire)
        {
            var oldQustionnaire = GetById(questionnaire.Id);
            if (oldQustionnaire != null)
            {
                questionnaire.CreatedAt = oldQustionnaire.CreatedAt;
                _questionnaireRepository.Update(questionnaire);
                return questionnaire;
            }
            return null;
        }
    }
}
