using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using System.Security.Claims;
using Entity.Concrete;

namespace Web.Controllers
{
    public class QuestionnaireController : Controller
    {

        private readonly IQuestionnaireService _questionnaireService;
        private readonly IQuestionService _questionService;

        public QuestionnaireController(IQuestionnaireService questionnaireService, IQuestionService questionService)
        {
            _questionnaireService = questionnaireService;
            _questionService = questionService;
        }


        // GET: QuestionnaireController
        public ActionResult Index()
        {
            var id = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
            var list = _questionnaireService.GetAllByUserId(id);
            return View(list);
        }

        // GET: QuestionnaireController/Details/5
        public ActionResult Details(Guid id)
        {
            var questionnaire = _questionnaireService.GetWithQuestions(id);
            return View(new QuestionnaireViewModel() { Questionnaire = questionnaire});
        }

        // GET: QuestionnaireController/Create
        public ActionResult Create()
        {
            var model = new CreateQuestionnaireViewModel();
            return View(model);
        }

        // POST: QuestionnaireController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateQuestionnaireViewModel model)
        {
            if (ModelState.IsValid)
            {
                var id = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value);
                var res = _questionnaireService.Add(model.ToQuestionnaire(),id);
                if(res != null)
                {
                    return Redirect("Index");
                }
            }
            ViewBag.Error = "Error";
            return View(model);
        }

        // GET: QuestionnaireController/Edit/5
        public ActionResult Edit(Guid id)
        {
            return PartialView(new Questionnaire());
        }

        // POST: QuestionnaireController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: QuestionnaireController/Delete/5
        public ActionResult Delete(Guid id)
        {
            var res = _questionnaireService.Delete(id);
            if (res)
                return Redirect("/Questionnaire/Index");
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(QuestionnaireViewModel model)
        {
            var questionnaire = _questionnaireService.GetWithQuestions(model.Question.QuestionnaireId);
            if(questionnaire != null)
            {
                model.Question.QuestionnaireId = questionnaire.Id;
                var res = _questionService.Add(model.Question);
                if (res == null)
                    ViewBag.Error = "Error";
                else
                {
                    questionnaire.Questions.Add(res);
                }
                return View("Details", new QuestionnaireViewModel() { Questionnaire = questionnaire });

            }
            return NotFound();

        }


    }
}
