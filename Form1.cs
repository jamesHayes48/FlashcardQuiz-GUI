using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [DefaultValue(null)]
        public Quiz CurrentQuiz { get; set; }
        private int CurrentQuestionIndex { get; set; }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set quiz panel to be invisible at start
            menuPanel.Dock = DockStyle.Fill;
            quizPanel.Dock = DockStyle.Fill;
            menuPanel.Visible = true;
            quizPanel.Visible = false;
            CurrentQuiz = new Quiz();
        }
      
        /// <summary>
        /// Open file dialog to select quiz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnOpenFile_ClickAsync(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SelectedFilePath = openFileDialog1.FileName;
                MessageBox.Show("You selected: " + SelectedFilePath);

                // Load current selected quiz
                CurrentQuiz = await loadQuizAsync(SelectedFilePath);
                menuPanel.Visible = false;
                quizPanel.Visible = true;

                CurrentQuestionIndex = 0;
                displayQuestion(CurrentQuestionIndex);
            }
        }

        /// <summary>
        /// Start loading the quiz from the selected file path
        /// </summary>
        /// <param name="filePath"></param>
        private async Task<Quiz> loadQuizAsync(string filePath)
        {
            char delimiter = '|';
            Quiz quiz = new Quiz();

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
            using (var streamReader = new StreamReader(fileStream))
            {
                string? line;
                while ((line = await streamReader.ReadLineAsync()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    string[] data = line.Split(delimiter);

                    // Take the answers from the line input
                    string[] answers = data.Skip(1).ToArray();

                    Question newQuestion = new Question(data[0], answers);
                    quiz.AddQuestion(newQuestion);
                }
            }
            return quiz;
        }
        
        private void displayQuestion(int index)
        {
            Question currentQuestion = CurrentQuiz.Questions[index];
            questionLabel.Text = currentQuestion.QuestionText;

            answer1.Text = currentQuestion.AnswerArray[0];
            answer2.Text = currentQuestion.AnswerArray[1];
            answer3.Text = currentQuestion.AnswerArray[2];
            answer4.Text = currentQuestion.AnswerArray[3];
        } 
    }
   
    public class Quiz
    {
        public List<Question> Questions { get; private set; }
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
