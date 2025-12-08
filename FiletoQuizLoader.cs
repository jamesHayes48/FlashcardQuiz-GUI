using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashcardQuiz_GUI
{
    /// <summary>
    /// Interface for different Types of files to load
    /// </summary>
    public interface IQuizLoader
    {
        Task<Quiz> LoadQuizAsync(string filePath);
    }

    /// <summary>
    /// Load from a .txt file to a quiz object.
    /// Implement the IQuizLoader.
    /// </summary>
    public class FiletoQuizLoader : IQuizLoader
    {
        private char delimiter = '|';

        /// <summary>
        /// Start loading the quiz asynchronusly from the selected file path
        /// </summary>
        /// <param name="filePath"></param>
        public async Task<Quiz> LoadQuizAsync(string filePath)
        {
            Quiz quiz = new Quiz();
            int lineNumber = 0;

            // Read file asynchronusly
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            using (var streamReader = new StreamReader(fileStream))
            {
                string? line;
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    // Skip line if it is null or whitespace
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] data = line.Split(delimiter);

                    // Check if line is formatted with delimeter
                    if (data.Length < 5)
                        throw new Exception($"Line {lineNumber}: Expected 1 Question and 4 Answers separated with delimeter {delimiter}. " +
                            $"Current format contains {data.Length} parts");

                    // Take the answers from the line input
                    string[] answers = data.Skip(1).ToArray();

                    // Create and add question to quiz
                    Question newQuestion = new MultipleChoiceQuestion(data[0], answers);
                    quiz.AddQuestion(newQuestion);
                }
            }

            // Prevent user from completing a quiz that does not exist
            if (quiz.Questions.Count == 0)
            {
                throw new("Error: Selected file is all whitespace or is empty");
            }
            return quiz;
        }
    }
}
