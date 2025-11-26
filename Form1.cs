using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
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

        // Define currrent Session
        QuizSession session = new QuizSession();

        private void Form1_Load(object sender, EventArgs e)
        {
            // Set quiz panel to be invisible at start
            menuPanel.Dock = DockStyle.Fill;
            quizPanel.Dock = DockStyle.Fill;
            menuPanel.Visible = true;
            quizPanel.Visible = false;
            quizPanel.BringToFront();
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
                // File path chosen by user
                string userFilePath = openFileDialog1.FileName;

                // Load current selected quiz
                Quiz loadedQuiz = await LoadQuizAsync(userFilePath);

                // Randomize order of questions
                loadedQuiz.RandomizeQuestionOrder();

                // Intialize the current sessions variables based on selected path
                session.IntializeFromQuiz(loadedQuiz);

                // Switch to quiz mode
                menuPanel.Visible = false;
                quizPanel.Visible = true;

                session.CurrentQuestionIndex = 0;
                try
                {
                    displayQuestion(session.CurrentQuestionIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        /// <summary>
        /// Check if Answer is checked and which radio button was it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Answer_CheckedChanged(object? sender, EventArgs e)
        {
            if (sender is RadioButton rb && rb.Checked)
            {
                SaveCurrentAnswer();
            }
        }

        /// <summary>
        /// Move to the next question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (quizPanel.Visible == true)
                if (session.CurrentQuestionIndex < session.CurrentQuiz.Questions.Count - 1)
                {
                    session.CurrentQuestionIndex++;
                    displayQuestion(session.CurrentQuestionIndex);
                }
                else
                {
                    session.CurrentQuestionIndex++;
                    //displaySummaryQuestion(CurrentQuestionIndex);
                }
        }

        /// <summary>
        /// Move to the previous Question
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBack_Click(object sender, EventArgs e)
        {
            if (session.CurrentQuestionIndex > 0)
            {
                session.CurrentQuestionIndex--;
                displayQuestion(session.CurrentQuestionIndex);
            }
        }

        /// <summary>
        /// Start loading the quiz asynchronusly from the selected file path
        /// </summary>
        /// <param name="filePath"></param>
        private async Task<Quiz> LoadQuizAsync(string filePath)
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
                        throw new Exception($"Line {lineNumber}: Expected 1 Question and 4 Answers separated with delimeter {delimiter}. " +
                            $"Current format contains {data.Length} parts");

                    // Take the answers from the line input
                    string[] answers = data.Skip(1).ToArray();

                    // Create and add question to quiz
                    Question newQuestion = new Question(data[0], answers);
                    quiz.AddQuestion(newQuestion);
                }
            }
            return quiz;
        }

        /// <summary>
        /// Display the current question
        /// Display will change  to show correct and incorrect answers when completed.
        /// </summary>
        /// <param name="index"></param>
        private void displayQuestion(int index)
        {
            try
            {
                // Update the questions and radio buttons
                Question currentQuestion = session.CurrentQuiz.Questions[index];
                questionLabel.Text = $"Question {index+1}: {currentQuestion.QuestionText}";

                answer1.Text = currentQuestion.AnswerArray[0];
                answer2.Text = currentQuestion.AnswerArray[1];
                answer3.Text = currentQuestion.AnswerArray[2];
                answer4.Text = currentQuestion.AnswerArray[3];

                // Ensure UserAnswerIndex is initialized
                if (session.UserAnswerIndex == null)
                {
                    session.UserAnswerIndex = new List<int>();
                }

                // Check if current index has been answered already,
                // if not, uncheck the radio buttons
                // if so, Update with which one is checked
                if (index >= session.UserAnswerIndex.Count)
                {
                    answer1.Checked = answer2.Checked = 
                        answer3.Checked = answer4.Checked = false;
                }
                else
                {
                    int saved = session.UserAnswerIndex[index];
                    answer1.Checked = (saved == 0);
                    answer2.Checked = (saved == 1);
                    answer3.Checked = (saved == 2);
                    answer4.Checked = (saved == 3);

                    if (session.Submitted == true)
                    {
                        ShowCorrectAnswers(saved, index);
                    }
                }

                // Hide the buttons if at start or end of quiz
                btnBack.Visible = index > 0;
                btnNext.Visible = index < session.CurrentQuiz.Questions.Count - 1;
                btnSubmit.Visible = index == session.CurrentQuiz.Questions.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying question: " + ex.Message);
            }
        }

        /// <summary>
        /// Save the selected answer
        /// </summary>
        private void SaveCurrentAnswer()
        {
            int selected = -1;

            // Check for which radio button was selected then check which was selected through switch case
            var checkedRb = quizPanel.Controls.OfType<RadioButton>().FirstOrDefault(answer => answer.Checked);
            switch (checkedRb?.Tag)
            {
                case "A":
                    selected = 0;
                    break;
                case "B":
                    selected = 1; 
                    break;
                case "C":
                    selected = 2;
                    break;
                case "D":
                    selected = 3;
                    break;
                default:
                    selected = -1; 
                    break;
            }

            // Save the answer in the session's answer list
            session.UserAnswerIndex[session.CurrentQuestionIndex] = selected;

        }

        /// <summary>
        /// Check if user can submit. Make sure user submits answers for all questions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Decide if all questions are answered
            bool answeredAll = true;

            // Hold which questions are unanswered
            List<int> unanswered = new List<int>();

            for (int i = 0; i <= session.UserAnswerIndex.Count - 1; i++)
            {
                if (session.UserAnswerIndex[i] == -1)
                {
                    unanswered.Add(i + 1);
                    answeredAll = false;
                }
            }

            // Submit quiz if user answered all questions
            if (answeredAll)
            {
                MessageBox.Show("Quiz Completed");
                session.Submitted = true;
                session.CurrentQuestionIndex = 0;

                // Disable submit button after quiz is finished
                btnSubmit.Enabled = false;

                // Display UI elements for final score and percentage
                DisplayFinalStatsUI();

                // Enable return to menu function
                btnReturn.Visible = true;

                // Disable interactivity with answers
                answer1.Enabled = answer2.Enabled = answer3.Enabled = answer4.Enabled = false;

                displayQuestion(session.CurrentQuestionIndex);
            }
            // Display all questions that need answered
            else
            {
                MessageBox.Show($"Must answer all questions before submitting quiz: \n{string.Join("\n", unanswered)}");
            }
        }

        private void ShowCorrectAnswers(int userAnswer, int index)
        {
            answer1.BackColor = answer2.BackColor =
                           answer3.BackColor = answer4.BackColor = System.Drawing.Color.White;

            // Highlight the incorrect answer if user got it correctly
            if (userAnswer != session.CurrentQuiz.Questions[index].CorrectAnswerIndex)
            {
                switch (userAnswer)
                {
                    case 0:
                        answer1.BackColor = Color.Red;
                        break;
                    case 1:
                        answer2.BackColor = Color.Red;
                        break;
                    case 2:
                        answer3.BackColor = Color.Red;
                        break;
                    case 3:
                        answer4.BackColor = Color.Red;
                        break;

                }
            }

            // Highlight correct answer when completed
            switch (session.CurrentQuiz.Questions[index].CorrectAnswerIndex)
            {
                case 0:
                    answer1.BackColor = Color.Green;
                    break;
                case 1:
                    answer2.BackColor = Color.Green;
                    break;
                case 2:
                    answer3.BackColor = Color.Green;
                    break;
                case 3:
                    answer4.BackColor = Color.Green;
                    break;

            }
        }

        /// <summary>
        /// Display final stats for UI element
        /// </summary>
        void DisplayFinalStatsUI()
        {
            session.CalcFinalScore();
            finalAttrLabel.Text = $"Final Score: {session.Score}/{session.CurrentQuiz.Questions.Count} " +
                $"Grade: {(((double) session.Score / session.CurrentQuiz.Questions.Count) * 100):F2}%";
            finalAttrLabel.Visible = true;
        }

        /// <summary>
        /// Restart application to return to menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturn_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
