using System;
using static System.Collections.Specialized.BitVector32;

namespace FlashcardQuiz_GUI
{
	public class QuizSession ()
	{
        // Represent the current Quiz from the selected file
        public Quiz CurrentQuiz { get; set; } = new Quiz ();

        // Hold current index of question
        public int CurrentQuestionIndex { get; set; } = 0;

        // Hold saved answers 
        public List<int> UserAnswerIndex { get; set; } = new List<int> ();

        // Hold current score
        public int Score { get; private set; } = 0;

        // Hold state of program
        public bool Submitted { get; set; } = false;

        public void IntializeFromQuiz(Quiz currentQuiz)
        {
            CurrentQuiz = currentQuiz;
            
            // Populate the user answer index list with -1
            while (UserAnswerIndex.Count != CurrentQuiz.Questions.Count)
            {
                UserAnswerIndex.Add(-1);
            }

            // Set score intially to be the number of questions in the quiz
            Score = CurrentQuiz.Questions.Count;
        }
        
        /// <summary>
        /// Calculate the Final Score
        /// </summary>
        public void CalcFinalScore()
        {
            for (int i = 0; i < UserAnswerIndex.Count; i++)
            {
                // If answers do not match, decrement score
                if (!CurrentQuiz.Questions[i].IsCorrect(UserAnswerIndex[i]))
                {
                    Score--;
                }
            }
        }
    }
}
