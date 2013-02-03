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

            int quizId = 1;
            int hoursToAggregateVotes = -12;


            var data = repo.GetVotes(quizId,DateTime.Now.AddHours(hoursToAggregateVotes));
            
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

            CalculateVotingPercentages(results);

            return View(results);   
        }

        public void CalculateVotingPercentages(QuizResultViewModel results)
        {
            int totalAnswers = (from x in results.Options
                                select x.NumberOfVotes).Sum();

            //set exact percentage to 2 decimal places
            
            foreach (var option in results.Options)
            {
                option.Percent = (double) option.NumberOfVotes/(double) totalAnswers*100;
                option.Percent = Math.Round(option.Percent, 2, MidpointRounding.AwayFromZero);

            }

        }
    }
}