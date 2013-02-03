using System;
using System.Linq;
using DAL;
using QuizzApplication.Repository;


namespace QuizzApplication.Repository

{
    public class QuizRepository
    {
        /// <summary>
        /// Gets the question and answers by id
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public Question GetQuestion(int questionId)
        {

            using (QuizEntities context = new QuizEntities())
            {

                var question = context.Questions
                               .Include("Answers")
                               .Where(qn => qn.Id == questionId)

                               .FirstOrDefault();

                return question;
            }
        }


        /// <summary>
        /// Save a single user answer to the db
        /// </summary>
        /// <param name="data"></param>
        public void SaveAnswer(QuizResponse data)
        {
            using (QuizEntities context = new QuizEntities())
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


        /// <summary>
        /// Returns all votes aggregated for a particular date range
        /// </summary>
        /// <param name="questionId"></param>
        /// <param name="startingFrom"></param>
        /// <returns></returns>
        public QuizResult GetVotes(int questionId, DateTime startingFrom)
        {

             Question question = GetQuestion(questionId);


            QuizResult result = new QuizResult();

            result.QuestionId = questionId;

            foreach (var option in question.Answers)
            {
                result.Answers.Add(new DeeAnswer{AnswerText = option.Answer1, Id = option.AnswerId});
            }


            using (QuizEntities context = new QuizEntities())
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