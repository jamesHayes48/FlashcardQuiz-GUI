using System;

namespace FlashcardQuiz_GUI
{
    public class Question
    {
        public string QuestionText { get; set; }
        public string[] AnswerArray { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public Question(string questionText, string[] answerArray)
        {
            QuestionText = questionText;

            // Assign Correct answer index based on where the star is located
            for (int i = 0; i < answerArray.Length; i++)
            {
                if (answerArray[i].StartsWith("*"))
                {
                    // Set the correct answer index and remove the star
                    CorrectAnswerIndex = i;
                    answerArray[i] = answerArray[i].Substring(1);
                }
            }
            AnswerArray = answerArray;
        }

        public bool IsCorrect(int userAnswerIndex)
        {
            return userAnswerIndex == CorrectAnswerIndex;
        }
    }
}
