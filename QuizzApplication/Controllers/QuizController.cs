using System.Collections.Generic;
using System.Web.Mvc;
using QuizzApplication.Models;
using QuizzApplication.Repository;

namespace QuizzApplication.Controllers
{
    public class QuizController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            QuizRepository repo = new QuizRepository();
            var question = repo.Get(1);

            QuestionViewModel result = new QuestionViewModel();
            result.Id = question.Id;
            result.QuestionText = question.QuestionText;
            result.Answers = new List<Models.Answer>();

            foreach (var answer in question.Answers)
            {
                result.Answers.Add(new Models.Answer { AnswerText = answer.Answer1, Id = answer.AnswerId });
            }

            return View(result);           
        }


        [HttpPost]
        public ActionResult Index(QuestionViewModel model)
        {

            QuizRepository repo = new QuizRepository();

            Repository.QuizResponse response = new QuizResponse();
            response.AnswerId = model.SelectedAnswer;
            response.QuestionId = model.Id;
            repo.SaveAnswer(response);


            return RedirectToAction("Thanks");
        }


        public ActionResult Thanks()
        {
            return View();
        }

    }
}
