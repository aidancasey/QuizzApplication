using System.Collections.Generic;

namespace QuizzApplication.Models
{
    public class QuizResultViewModel
    {
        public int QuestionId { get; set; }

        public List<Option> Options { get; set; }

        public QuizResultViewModel()
        {
            Options = new List<Option>();
        }
    }

    public class Option
    {
        public int Id { set; get; }
        public string AnswerText { set; get; }
        public int NumberOfVotes { get; set; }
        public double Percent { get; set; }
    }
}