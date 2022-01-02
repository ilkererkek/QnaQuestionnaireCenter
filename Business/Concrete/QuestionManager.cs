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

        public Question Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Question> GetAll()
        {
            throw new NotImplementedException();
        }

        public Question GetById(Guid id)
        {
            return _questionRepository.GetById(id);
        }

        public Question Update(Question question)
        {
            throw new NotImplementedException();
        }
    }
}
