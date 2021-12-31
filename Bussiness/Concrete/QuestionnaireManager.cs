using Bussiness.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness.Concrete
{
    public class QuestionnaireManager : IQuestionnaireService
    {
        private readonly IQuestionnaireRepository _questionnaireRepository;

        public QuestionnaireManager(IQuestionnaireRepository questionnaireRepository)
        {
            _questionnaireRepository = questionnaireRepository;
        }
        public Questionnaire Add(Questionnaire questionnaire)
        {
            return _questionnaireRepository.Add(questionnaire);
        }

        public bool Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Questionnaire> GetAll()
        {
            throw new NotImplementedException();
        }

        public Questionnaire GetById(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Questionnaire GetWithQuestions()
        {
            throw new NotImplementedException();
        }

        public Questionnaire Update(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
