namespace FlashcardQuiz_GUI
{
    public partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openFileDialog1 = new OpenFileDialog();
            btnOpenFile = new Button();
            titleLabel = new Label();
            menuPanel = new Panel();
            quizPanel = new Panel();
            questionLabel = new Label();
            menuPanel.SuspendLayout();
            quizPanel.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnOpenFile
            // 
            btnOpenFile.Location = new Point(250, 142);
            btnOpenFile.Name = "btnOpenFile";
            btnOpenFile.Size = new Size(94, 29);
            btnOpenFile.TabIndex = 0;
            btnOpenFile.Text = "Open File";
            btnOpenFile.UseVisualStyleBackColor = true;
            btnOpenFile.Click += btnOpenFile_Click;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(239, 102);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(105, 20);
            titleLabel.TabIndex = 1;
            titleLabel.Text = "Flashcard Quiz";
            // 
            // menuPanel
            // 
            menuPanel.Controls.Add(quizPanel);
            menuPanel.Controls.Add(titleLabel);
            menuPanel.Controls.Add(btnOpenFile);
            menuPanel.Location = new Point(93, 67);
            menuPanel.Name = "menuPanel";
            menuPanel.Size = new Size(594, 318);
            menuPanel.TabIndex = 2;
            // 
            // quizPanel
            // 
            quizPanel.Controls.Add(questionLabel);
            quizPanel.Location = new Point(0, 3);
            quizPanel.Name = "quizPanel";
            quizPanel.Size = new Size(594, 318);
            quizPanel.TabIndex = 2;
            quizPanel.Visible = false;
            // 
            // questionLabel
            // 
            questionLabel.AutoSize = true;
            questionLabel.Location = new Point(248, 20);
            questionLabel.Name = "questionLabel";
            questionLabel.Size = new Size(50, 20);
            questionLabel.TabIndex = 0;
            questionLabel.Text = "label1";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(menuPanel);
            Name = "Form1";
            Text = "Flashcard Quiz";
            menuPanel.ResumeLayout(false);
            menuPanel.PerformLayout();
            quizPanel.ResumeLayout(false);
            quizPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private Button btnOpenFile;
        private Label titleLabel;
        private Panel menuPanel;
        private Panel quizPanel;
        private Label questionLabel;
    }
}
