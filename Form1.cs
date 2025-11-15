using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FlashcardQuiz_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public string SelectedFilePath { get; private set; }
        public Quiz CurrentQuiz { get; private set; }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
      
        /// <summary>
        /// Open file dialog to select quiz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SelectedFilePath = openFileDialog1.FileName;
                MessageBox.Show("You selected: " + SelectedFilePath);
                btnOpenFile.Visible = false;
            }
        }

        /// <summary>
        /// Start loading the quiz from the selected file path
        /// </summary>
        /// <param name="filePath"></param>
        private Quiz loadQuiz(string filePath)
        {
            char delimiter = '|';
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
            return quiz;
        }
    }
   
    public class Quiz
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

        public void DisplayAnswer()
        {
            Console.WriteLine($"Correct Answer: {AnswerArray[CorrectAnswerIndex]}");
        }

        public bool IsCorrect(int userAnswerIndex)
        {
            return userAnswerIndex == CorrectAnswerIndex;
        }
    }
}
