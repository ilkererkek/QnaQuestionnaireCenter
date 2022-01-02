using Business.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        private IQuestionnaireService _questionnaireService;
        private IAnswerService _answerService;
        private IQuestionService _questionService;
        public QuestionController(IQuestionnaireService questionnaireService, IAnswerService answerService, IQuestionService questionService)
        {
            this._questionnaireService = questionnaireService;
            this._answerService = answerService;
            this._questionService = questionService;
        }

        public IActionResult AddMultiAnswer(Guid id, Guid parent)
        {
            ViewBag.QuestionnaireId = parent;
            return View(new MultiAnswer() { QuestionId=id});
        }
        [HttpPost]
        public IActionResult AddMultiAnswer(MultiAnswer answer)
        {
            var question = _questionService.GetById(answer.QuestionId);
            if(question != null)
            {
                var res = _answerService.AddMultiAnswer(answer);
                return Redirect($"/Questionnaire/Details/{question.QuestionnaireId}");
            }
            return NotFound();
        }

        public IActionResult AddOptionAnswer(Guid id, Guid parent)
        {
            ViewBag.QuestionnaireId = parent;
            return View(new OptionAnswer() { QuestionId = id });
        }
        [HttpPost]
        public IActionResult AddOptionAnswer(OptionAnswer answer)
        {
            var question = _questionService.GetById(answer.QuestionId);
            if (question != null)
            {
                var res = _answerService.AddOptionAnswer(answer);
                return Redirect($"/Questionnaire/Details/{question.QuestionnaireId}");
            }
            return NotFound();
        }
    }
}
