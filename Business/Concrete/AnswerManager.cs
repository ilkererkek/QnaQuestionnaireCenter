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
    public class AnswerManager : IAnswerService
    {
        private IMultiAnswerRepository _multiAnswerRepository;
        private IOptionAnswerRepository _optionAnswerRepository;
        public AnswerManager(IMultiAnswerRepository multiAnswerRepository, IOptionAnswerRepository optionAnswerRepository)
        {
            _multiAnswerRepository = multiAnswerRepository;
            _optionAnswerRepository = optionAnswerRepository;
        }
        public MultiAnswer AddMultiAnswer( MultiAnswer multiAnswer)
        {
            var oldAnswer = MultiAnswerGetById(multiAnswer.Id);
            if(oldAnswer == null)
            {
                return _multiAnswerRepository.Add(multiAnswer);
            }
            return null;
        }

        public OptionAnswer AddOptionAnswer(OptionAnswer optionAnswer)
        {
            var oldAnswer = OptionAnswerGetById(optionAnswer.Id);
            if (oldAnswer == null)
            {
                return _optionAnswerRepository.Add(optionAnswer);
            }
            return null;
        }

        public List<MultiAnswer> GetMultiByQuestionId(Guid id)
        {
            return _multiAnswerRepository.GetByQuestionId(id);
        }

        public List<OptionAnswer> GetOptionByQuestionId(Guid id)
        {
            return _optionAnswerRepository.GetByQuestionId(id);
        }

        public MultiAnswer MultiAnswerGetById(Guid id)
        {
            return _multiAnswerRepository.GetById(id);
        }
        public OptionAnswer OptionAnswerGetById(Guid id)
        {
            return _optionAnswerRepository.GetById(id);
        }
    }
}
