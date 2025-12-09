using System;

namespace FlashcardQuiz_GUI
{
    /// <summary>
    /// Abstract class for future question types
    /// </summary>
    public abstract class Question
    {
        // Hold Text of the question
        public string? QuestionText { get; set; }

        // Hold array of answers
        public string[]? AnswerArray { get; set; }

        // Check for correct answers
        public abstract bool IsCorrect(int userAnswerIndex);
    }

    /// <summary>
    /// Single answer, multiple choice inherit from abstract class Question
    /// </summary>
    public class MultipleChoiceQuestion : Question
    {
        // Hold correct answer
        public int CorrectAnswerIndex { get; set; }

        public MultipleChoiceQuestion(string questionText, string[] answerArray)
        {
            QuestionText = questionText;

            // Set to sentinel value for error checking
            CorrectAnswerIndex = -1;

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
            if (CorrectAnswerIndex == -1)
                throw new Exception("Error: No correct answer indicated with '*'. Set one answer be correct");
            AnswerArray = answerArray;
        }

        public override bool IsCorrect(int userAnswerIndex)
        {
            return userAnswerIndex == CorrectAnswerIndex;
        }
    }
}
