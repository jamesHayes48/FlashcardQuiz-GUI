using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private List<int> UserAnswerIndex;

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set quiz panel to be invisible at start
            menuPanel.Dock = DockStyle.Fill;
            quizPanel.Dock = DockStyle.Fill;
            menuPanel.Visible = true;
            quizPanel.Visible = false;
            quizPanel.BringToFront();

            // Initialize current quiz and user choices
            CurrentQuiz = new Quiz();
            UserAnswerIndex = new List<int>();
        }

        private void Answer_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                SaveCurrentAnswer();
            }
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

                // Populate the user answer index list with -1
                while (UserAnswerIndex.Count <= CurrentQuiz.Questions.Count)
                {
                    UserAnswerIndex.Add(-1);
                }

                menuPanel.Visible = false;
                quizPanel.Visible = true;

                CurrentQuestionIndex = 0;
                try
                {
                    displayQuestion(CurrentQuestionIndex);
                }
                catch (Exception ex) 
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CurrentQuestionIndex < CurrentQuiz.Questions.Count - 1)
            {
                CurrentQuestionIndex++;
                displayQuestion(CurrentQuestionIndex);
            }
        }

        /// <summary>
        /// Move to the previous Question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (CurrentQuestionIndex > 0)
            {
                CurrentQuestionIndex--;
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
                        throw new Exception($"Line {lineNumber}: Expected 1 Question and 4 Answers separated with delimeter {delimiter}, format contains {data.Length} parts");

                    // Take the answers from the line input
                    string[] answers = data.Skip(1).ToArray();

                    // Create and add question to quiz
                    Question newQuestion = new Question(data[0], answers);
                    quiz.AddQuestion(newQuestion);
                }
            }
            return quiz;
        }
        
        private void displayQuestion(int index)
        {
            try 
            {
                // Update the questions and radio buttons
                Question currentQuestion = CurrentQuiz.Questions[index];
                questionLabel.Text = currentQuestion.QuestionText;

                answer1.Text = currentQuestion.AnswerArray[0];
                answer2.Text = currentQuestion.AnswerArray[1];
                answer3.Text = currentQuestion.AnswerArray[2];
                answer4.Text = currentQuestion.AnswerArray[3];

                // Check if current index has been answered already,
                // if not, uncheck the radio buttons
                // if so, Update with which one is checked
                if (index >= UserAnswerIndex.Count)
                {
                    answer1.Checked = false;
                    answer2.Checked = false;
                    answer3.Checked = false;
                    answer4.Checked = false;
                }
                else 
                {
                    int saved = UserAnswerIndex[index];
                    answer1.Checked = (saved == 0);
                    answer2.Checked = (saved == 1);
                    answer3.Checked = (saved == 2);
                    answer4.Checked = (saved == 3);
                }

                // Hide the buttons if at start or end of quiz
                btnBack.Visible = index > 0;
                btnNext.Visible = index < CurrentQuiz.Questions.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying question: " + ex.Message);
            }
        } 

        private void SaveCurrentAnswer()
        {
            int selected = -1;

            if (answer1.Checked) selected = 0;
            else if(answer2.Checked) selected = 1;
            else if(answer3.Checked) selected = 2;
            else if(answer4.Checked) selected = 3;

            UserAnswerIndex[CurrentQuestionIndex] = selected;
            
        }

        private void compareAnswer()
        {

        }
    }
   
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
