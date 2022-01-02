using Bussiness.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using System.Security.Claims;

namespace Web.Controllers
{
    public class QuestionnaireController : Controller
    {

        private readonly IQuestionnaireService _questionnaireService;

        public QuestionnaireController(IQuestionnaireService questionnaireService)
        {
            _questionnaireService = questionnaireService;
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
            return View(model);
        }

        // GET: QuestionnaireController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
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
            return View("Details",new QuestionnaireViewModel() { Questionnaire = questionnaire });
        }


    }
}
