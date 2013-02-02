using System.Collections.Generic;

namespace QuizzApplication.Models
{
        public class QuestionViewModel
        {
            public int Id { set; get; }
            public string QuestionText { set; get; }
            public List<Answer> Answers { set; get; }
            public int SelectedAnswer { set; get; }

            public QuestionViewModel()
            {
                Answers = new List<Answer>();
            }
        }
        public class Answer
        {
            public int Id { set; get; }
            public string AnswerText { set; get; }
        }
}