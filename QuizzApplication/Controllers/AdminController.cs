using System;
using System.Web.Mvc;
using QuizzApplication.Models;
using QuizzApplication.Repository;

namespace QuizzApplication.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Default1/
        public ActionResult Index()
        {
            QuizRepository repo = new QuizRepository();
            var data = repo.GetVotes(1,DateTime.Now.AddHours(-12));
            ResultsViewModel results = new ResultsViewModel();
            return View(results);   
        }

    }
}
