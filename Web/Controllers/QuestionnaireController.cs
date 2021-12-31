using Bussiness.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

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
            return View();
        }

        // GET: QuestionnaireController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                var res = _questionnaireService.Add(model.ToQuestionnaire());
                if(res != null)
                {
                    return Redirect("/");
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestionnaireController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
