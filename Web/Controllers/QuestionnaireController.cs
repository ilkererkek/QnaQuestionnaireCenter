using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using System.Security.Claims;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace Web.Controllers
{
    [Authorize]
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
            var questionnaire = _questionnaireService.GetById(id);
            if(questionnaire != null)
            {
                return PartialView(questionnaire);
            }
            return NotFound();
        }

        // POST: QuestionnaireController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Questionnaire model)
        {
            if(model.Id != null)
            {
                if (ModelState.IsValid)
                {
                    var res = _questionnaireService.Update(model);
                }
                var questionnaire = _questionnaireService.GetWithQuestions(model.Id);
                ViewBag.Error = "Error";
                return View("Details",new QuestionnaireViewModel() { Questionnaire = questionnaire });
            }
            return NotFound();
        }

        public ActionResult Publish(Guid id)
        {
            var questionnaire = _questionnaireService.GetWithQuestions(id);
            if(questionnaire != null)
            {
                if(questionnaire.Status == Entity.Concrete.Enums.QuestionnaireStatus.DRAFT)
                {
                    questionnaire.Status = Entity.Concrete.Enums.QuestionnaireStatus.PUBLISHED;
                }
                _questionnaireService.Update(questionnaire);
                return View("Details", new QuestionnaireViewModel() { Questionnaire = questionnaire });
            }
            return NotFound();
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
            if(questionnaire != null )
            {
                model.Question.QuestionnaireId = questionnaire.Id;
                if (!ModelState.IsValid && !(questionnaire.Status == Entity.Concrete.Enums.QuestionnaireStatus.CREATED || questionnaire.Status == Entity.Concrete.Enums.QuestionnaireStatus.DRAFT))
                {
                    return View("Details", new QuestionnaireViewModel() { Questionnaire = questionnaire });
                }
                var res = _questionService.Add(model.Question);
                if(questionnaire.Status == Entity.Concrete.Enums.QuestionnaireStatus.CREATED)
                {
                    questionnaire.Status = Entity.Concrete.Enums.QuestionnaireStatus.DRAFT;
                    _questionnaireService.Update(questionnaire);
                }
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
