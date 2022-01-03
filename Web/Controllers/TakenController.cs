using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class TakenController : Controller
    {
        public ITakenQuestionnaireService _takenQuestionnaireService;
        public IQuestionnaireService _questionnaireService;
        public TakenController(ITakenQuestionnaireService takenQuestionnaireService, IQuestionnaireService questionnaireService)
        {
            _takenQuestionnaireService = takenQuestionnaireService;
            _questionnaireService = questionnaireService;
        }
        public IActionResult Index(Guid id)
        {
            var list = _takenQuestionnaireService.GetByQuestionnireId(id);
            return View(list);
        }

        public IActionResult Details(Guid id)
        {
            var taken = _takenQuestionnaireService.GetById(id);
            if(taken != null)
            {
                var questionnaire = _questionnaireService.GetWithQuestions(taken.QuestionnaireId);
                foreach (var item in questionnaire.Questions)
                {
                    switch (item.Type)
                    {
                        case Entity.Concrete.Enums.QuestionType.SELECTION:
                            foreach (var ans in item.OptionAnswers)
                            {
                                ans.OptionSelections = new List<Entity.Concrete.OptionSelection>() { _takenQuestionnaireService.GetOptionSelection(ans.Id, id) };
                            }
                            break;
                        case Entity.Concrete.Enums.QuestionType.MULTISELECTION:
                            foreach (var ans in item.MultiAnswers)
                            {
                                ans.MultiSelections = _takenQuestionnaireService.GetMultiSelection(ans.Id, id);
                            }
                            break;
                        case Entity.Concrete.Enums.QuestionType.OPENENDED:
                            item.OpenAnswers = new List<Entity.Concrete.OpenAnswer>() { _takenQuestionnaireService.GetOpenAnswer(item.Id,id) };
                            break;
                    }
                }
                return View(questionnaire);
            }
            var list = _takenQuestionnaireService.GetByQuestionnireId(id);
            return View("Index",list);
        }
    }
}
