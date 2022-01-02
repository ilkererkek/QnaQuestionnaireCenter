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
    public class QuestionManager : IQuestionService
    {
        private IQuestionRepository _questionRepository;

        public QuestionManager(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        public Question Add(Question question)
        {
            var oldQuestion = GetById(question.Id);
            if(oldQuestion == null)
            {
                return _questionRepository.Add(question);
            }
            return null;
        }

        public bool Delete(Guid id)
        {
            var question = GetById(id);
            if(question != null)
            {
                _questionRepository.Delete(id);
                return true;
            }
            return false;
        }

        public List<Question> GetAll()
        {
            return _questionRepository.GetList(null,"SELECT * FROM Questions;");
        }

        public List<Question> GetAllByQuestionnaireId(Guid Id)
        {
            return _questionRepository.GetList(new { Id = Id}, "SELECT * FROM Questions WHERE Questions.QuestionnaireId = @Id;");
        }

        public Question GetById(Guid id)
        {
            return _questionRepository.GetById(id);
        }

        public Question Update(Question question)
        {
            var oldQuestion = GetById(question.Id);
            if (oldQuestion != null)
            {
                question.CreatedAt = oldQuestion.CreatedAt;
                _questionRepository.Update(question);
                return question;
            }
            return null;
        }
    }
}
