using System;

namespace FlashcardQuiz_GUI
{
    public class Quiz
    {
        public List<Question> Questions { get; private set; }
        public int Score { get; private set; }

        public Quiz()
        {
            Questions = new List<Question>();
        }

        public Quiz(List<Question> questions)
        {
            Questions = questions;
        }

        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        public void incrementScore()
        {
            Score++;
        }
    }
}
