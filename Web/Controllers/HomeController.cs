using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IQuestionnaireService _questionnaireService;
        private ITakenQuestionnaireService _takenQuestionnaireService;

        public HomeController(IQuestionnaireService questionnaireService, ITakenQuestionnaireService takenQuestionnaireService)
        {
            _questionnaireService = questionnaireService;
            _takenQuestionnaireService = takenQuestionnaireService;
        }
        public IActionResult Index()
        {
            var list = _questionnaireService.GetPublished();
            return View(list);
        }

        public IActionResult Details(Guid Id)
        {
            var questionnaire = _questionnaireService.GetWithQuestions(Id);
            questionnaire.Questions.Sort((x, y) => DateTime.Compare(x.CreatedAt, y.CreatedAt));

            return View(new QuestionViewModel()
            {
                Index = 0,
                Name = questionnaire.Name,
                Question= questionnaire.Questions[0],
                TakenQuestionnaireId = _takenQuestionnaireService.Add(new Entity.Concrete.TakenQuestionnaire()
                {
                    StartTime = DateTime.UtcNow.AddHours(3),
                    QuestionnaireId = questionnaire.Id,
                    FinishTime = DateTime.UtcNow.AddHours(3),
                }).Id,
                QuestionId = questionnaire.Questions[0].Id,
                QuestionnaireId = questionnaire.Id,
                Type = questionnaire.Questions[0].Type
            });
        }
        [HttpPost]
        public IActionResult Details(QuestionViewModel model)
        {
            var questionnaire = _questionnaireService.GetWithQuestions(model.QuestionnaireId);
            questionnaire.Questions.Sort((x, y) => DateTime.Compare(x.CreatedAt, y.CreatedAt));
            switch (model.Type)
            {
                case Entity.Concrete.Enums.QuestionType.SELECTION:
                    _takenQuestionnaireService.AddOptionSelection(new Entity.Concrete.OptionSelection()
                    {
                        OptionAnswerId = model.Selection,
                        TakenQuestionnaireId = model.TakenQuestionnaireId,
                    });
                    break;
                case Entity.Concrete.Enums.QuestionType.MULTISELECTION:
                    for(int i = 0; i< model.MultiSelection.Length; i++)
                    {
                        if(model.MultiSelection[i])
                        {
                            _takenQuestionnaireService.AddMultiSelection(new Entity.Concrete.MultiSelection()
                            {
                                TakenQuestionnaireId = model.TakenQuestionnaireId,
                                MultiAnswerId = questionnaire.Questions[model.Index].MultiAnswers[i].Id
                            });
                        }
                    }
                    break;
                case Entity.Concrete.Enums.QuestionType.OPENENDED:
                    _takenQuestionnaireService.AddOpenAnswer(new Entity.Concrete.OpenAnswer()
                    {
                        QuestionId = questionnaire.Questions[model.Index].Id,
                        Answer = model.OpenAnswer,
                        TakenQuestionnaireId = model.TakenQuestionnaireId
                    });
                    break;
            }
            var index = model.Index + 1;
            if(index < questionnaire.Questions.Count)
            {
                return View(new QuestionViewModel()
                {
                    Index = index,
                    Name = questionnaire.Name,
                    Question = questionnaire.Questions[index],
                    TakenQuestionnaireId = model.TakenQuestionnaireId,
                    QuestionId = questionnaire.Questions[index].Id,
                    QuestionnaireId = model.QuestionnaireId,
                    Type = questionnaire.Questions[index].Type

                });
            }
            _takenQuestionnaireService.Finish(model.TakenQuestionnaireId);
            var list = _questionnaireService.GetPublished();
            return View("Index",list);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}