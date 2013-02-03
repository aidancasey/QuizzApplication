using System.Collections.Generic;
using System.Web.Mvc;
using DAL;
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

            int quizId = 1;

            QuizRepository repo = new QuizRepository();
            var question = repo.GetQuestion(quizId);

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
            //save result

            QuizRepository repo = new QuizRepository();

            DAL.QuizResponse response = new QuizResponse();
            response.AnswerId = model.SelectedAnswer;
            response.QuestionId = model.Id;
            repo.SaveAnswer(response);


            return RedirectToAction("Thanks");
        }

        //thank you drive through
        public ActionResult Thanks()
        {
            return View();
        }

    }
}
