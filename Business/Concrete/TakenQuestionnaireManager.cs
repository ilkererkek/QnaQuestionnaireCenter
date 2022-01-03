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
    public class TakenQuestionnaireManager : ITakenQuestionnaireService
    {
        private ITakenQuestionnaireRepository _takenQuestionnaireRepository;
        private IMultiSelectionRepository _multiSelectionRepository;
        private IOptionSelectionRepository _optionSelectionRepository;
        private IOpenAnswerRepository _openAnswerRepository;
        public TakenQuestionnaireManager(ITakenQuestionnaireRepository takenQuestionnaireRepository, IMultiSelectionRepository multiSelectionRepository
         ,IOptionSelectionRepository optionSelectionRepository, IOpenAnswerRepository openAnswerRepository)
        {
            _takenQuestionnaireRepository = takenQuestionnaireRepository;
            _multiSelectionRepository = multiSelectionRepository;
            _optionSelectionRepository = optionSelectionRepository;
            _openAnswerRepository = openAnswerRepository;
        }
        public TakenQuestionnaire Add(TakenQuestionnaire takenQuestionnaire)
        {
            return _takenQuestionnaireRepository.Add(takenQuestionnaire);
        }

        public void AddMultiSelection(MultiSelection multiSelection)
        {
             _multiSelectionRepository.Add(multiSelection);
        }

        public void AddOpenAnswer(OpenAnswer openAnswer)
        {
            _openAnswerRepository.Add(openAnswer);
        }

        public void AddOptionSelection(OptionSelection optionSelection)
        {
            _optionSelectionRepository.Add(optionSelection);
        }
        public void Finish(Guid Id)
        {
            var taken = GetById(Id);
            if(taken != null)
            {
                taken.FinishTime = DateTime.UtcNow.AddHours(3);
                _takenQuestionnaireRepository.Update(taken);
            }
        }

        public TakenQuestionnaire GetById(Guid Id)
        {
            return _takenQuestionnaireRepository.GetById(Id);
        }

        public List<TakenQuestionnaire> GetByQuestionnireId(Guid Id)
        {
            return _takenQuestionnaireRepository.GetList(new {Id = Id}, "SELECT * FROM TakenQuestionnaires WHERE QuestionnaireId = @Id");
        }

        public List<MultiSelection> GetMultiSelection(Guid MultiAnswerId, Guid TakenQuestionnaireId)
        {
            return _multiSelectionRepository.GetList(new { MultiAnswerId, TakenQuestionnaireId },
                "SELECT * FROM MultiSelections WHERE MultiAnswerId = @MultiAnswerId AND TakenQuestionnaireId = @TakenQuestionnaireId");
        }

        public OpenAnswer GetOpenAnswer(Guid QuestionId, Guid TakenQuestionnaireId)
        {
            return _openAnswerRepository.Get(new { QuestionId, TakenQuestionnaireId },
               "SELECT * FROM OpenAnswers WHERE QuestionId = @QuestionId AND TakenQuestionnaireId = @TakenQuestionnaireId");
        }

        public OptionSelection GetOptionSelection(Guid OptionAnswerId, Guid TakenQuestionnaireId)
        {
            return _optionSelectionRepository.Get(new { OptionAnswerId, TakenQuestionnaireId },
                "SELECT * FROM OptionSelections WHERE OptionAnswerId = @OptionAnswerId AND TakenQuestionnaireId = @TakenQuestionnaireId");
        }
    }
}
