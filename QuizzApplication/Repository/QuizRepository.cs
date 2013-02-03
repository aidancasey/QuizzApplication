using System;
using System.Linq;
using QuizzApplication.Repository;

namespace QuizzApplication.Repository

{
    public class QuizRepository
    {
        public Question Get(int id)
        {

            using (QuizEntities1 context = new QuizEntities1())
            {

                var question = context.Questions
                               .Include("Answers")
                               .Where(qn => qn.Id == id)

                               .FirstOrDefault();

                return question;
            }
        }



        public void SaveAnswer(QuizResponse data)
        {
            using (QuizEntities1 context = new QuizEntities1())
            {

                context.QuizResponses.AddObject(new QuizResponse
                                                    {
                                                        Date = DateTime.Now,
                                                        AnswerId = data.AnswerId,
                                                        QuestionId = data.QuestionId
                                                    });

                context.SaveChanges();

            }

        }

        public QuizResult GetVotes(int questionId, DateTime startingFrom)
        {

             Question question = Get(questionId);


            QuizResult result = new QuizResult();

            result.QuestionId = questionId;

            foreach (var option in question.Answers)
            {
                result.Answers.Add(new DeeAnswer{AnswerText = option.Answer1, Id = option.AnswerId});
            }


            using (QuizEntities1 context = new QuizEntities1())
            {
                var totals =
                    context.QuizResponses.GroupBy(p => p.AnswerId)
                           .Select(p => new {p.Key, count = p.Count(i => i.Date >= startingFrom)});



                foreach (var c in totals)
                {
                    result.Answers.Find(d=>d.Id == c.Key).NumberOfVotes = c.count;
                }
            }

            return result;
        }
 

     }
}