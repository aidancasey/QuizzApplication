using System;
using System.Linq;
using System.Web.Mvc;
using QuizzApplication.Models;
using QuizzApplication.Repository;

namespace QuizzApplication.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        public ActionResult Index()
        {
            QuizRepository repo = new QuizRepository();
            var data = repo.GetVotes(1,DateTime.Now.AddHours(-30));
            
            QuizResultViewModel results = new QuizResultViewModel();
            
            results.QuestionId = data.QuestionId;
            
            
            foreach (var option in data.Answers)
            {
                results.Options.Add(new Option
                                        {
                                            AnswerText = option.AnswerText,
                                            NumberOfVotes = option.NumberOfVotes,
                                            Id = option.Id
                                            
                                        });

            }

            SetPercentages(results);

            return View(results);   
        }


        public void SetPercentages(QuizResultViewModel results)
        {
            int totalAnswers = (from x in results.Options
                                select x.NumberOfVotes).Sum();

            //set exact percentage
            
            foreach (var option in results.Options)
            {
                option.Percent = (double) option.NumberOfVotes/(double) totalAnswers*100;
                option.Percent = Math.Round(option.Percent, 2, MidpointRounding.AwayFromZero);

            }




        }
    }
}