using System;
using System.Collections.Generic;

namespace FlashcardQuiz_GUI
{
    public class Quiz
    {
        public List<Question> Questions { get; private set; }

        public Quiz()
        {
            Questions = new List<Question>();
        }

        /// <summary>
        /// Parameterized Constructor for creating a Quiz with a pre-defined list of questions
        /// </summary>
        /// <param name="questions"></param>
        public Quiz(List<Question> questions)
        {
            Questions = questions;
        }

        /// <summary>
        /// Add a new question to list Questions
        /// </summary>
        /// <param name="question"></param>
        public void AddQuestion(Question question)
        {
            Questions.Add(question);
        }

        /// <summary>
        /// Randomize order of questions in the quiz
        /// </summary>
        public void RandomizeQuestionOrder()
        {
            // Based on code created by grenade on StackOverflow
            // Source: https://stackoverflow.com/questions/273313/randomize-a-listt
            Random rng = new Random();
            int n = Questions.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Question q = Questions[k];
                Questions[k] = Questions[n];
                Questions[n] = q;
            }
        }
    }
}
