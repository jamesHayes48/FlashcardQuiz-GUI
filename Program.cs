using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardQuiz_GUI
{
    class Quiz
    {
        private List<Question> Questions { get; set; }
        public int Score { get; }

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
    }


    class Question
    {
        public string QuestionText { get; set; }
        public string[] AnswerArray { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public Question(string questionText, string[] answerArray)
        {
            QuestionText = questionText;

            // Assign Correct answer index based on where the star is located
            int i = 0;
            foreach (string answer in answerArray)
            {
                if (answer.StartsWith("*"))
                {
                    CorrectAnswerIndex = i;
                    answerArray[i] = answer.Substring(1);
                }
                i++;
            }
            AnswerArray = answerArray;
        }

        public void DisplayAnswer()
        {
            Console.WriteLine($"Correct Answer: {AnswerArray[CorrectAnswerIndex]}");
        }

        public bool IsCorrect(int userAnswerIndex)
        {
            return userAnswerIndex == CorrectAnswerIndex;
        }
    }
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}