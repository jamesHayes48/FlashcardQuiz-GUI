using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardQuiz_GUI
{
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

            // Begin with opening a file dialog for quiz
            Form1 form = new Form1();
            Application.Run(form);
            string filePath = form.SelectedFilePath;

            char delimiter = ';';
            Quiz quiz = new Quiz();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(delimiter);

                    // Take the answers from the line input
                    string[] answers = data.Skip(1).ToArray();

                    Question newQuestion = new Question(data[0], answers);
                    newQuestion.DisplayAnswer();
                    quiz.AddQuestion(newQuestion);
                }
            }
        }
    }
}