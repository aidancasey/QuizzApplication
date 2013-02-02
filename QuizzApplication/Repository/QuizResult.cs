using System.Collections.Generic;

namespace QuizzApplication.Repository
{
    public class QuizResult
    {
        public int QuestionId { get; set; }

        public List<DeeAnswer> Answers { get; set; }

        public QuizResult()
        {
            Answers = new List<DeeAnswer>();
        }
    }

    public class DeeAnswer
    {
        public int Id { set; get; }
        public string AnswerText { set; get; }
        public int NumberOfVotes { get; set; }
    }
}