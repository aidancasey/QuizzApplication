using System.Collections.Generic;

namespace QuizzApplication.Models
{
    public class ResultsViewModel
    {
        public int QuestionId { get; set; }
        public List<int> Answers { get; set; }
        public List<int> AnswerCount { get; set; }
    }
}