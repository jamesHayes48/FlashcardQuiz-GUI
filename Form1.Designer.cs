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
            btnBack = new Button();
            btnNext = new Button();
            answer4 = new RadioButton();
            answer3 = new RadioButton();
            answer2 = new RadioButton();
            answer1 = new RadioButton();
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
            btnOpenFile.Click += btnOpenFile_ClickAsync;
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
            menuPanel.Controls.Add(titleLabel);
            menuPanel.Controls.Add(btnOpenFile);
            menuPanel.Dock = DockStyle.Fill;
            menuPanel.Location = new Point(0, 0);
            menuPanel.Name = "menuPanel";
            menuPanel.Padding = new Padding(24);
            menuPanel.Size = new Size(800, 450);
            menuPanel.TabIndex = 2;
            // 
            // quizPanel
            // 
            quizPanel.Controls.Add(btnBack);
            quizPanel.Controls.Add(btnNext);
            quizPanel.Controls.Add(answer4);
            quizPanel.Controls.Add(answer3);
            quizPanel.Controls.Add(answer2);
            quizPanel.Controls.Add(answer1);
            quizPanel.Controls.Add(questionLabel);
            quizPanel.Dock = DockStyle.Fill;
            quizPanel.Location = new Point(0, 0);
            quizPanel.Name = "quizPanel";
            quizPanel.Padding = new Padding(24);
            quizPanel.Size = new Size(800, 450);
            quizPanel.TabIndex = 3;
            quizPanel.Visible = false;
            // 
            // btnBack
            // 
            btnBack.Location = new Point(15, 269);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(94, 29);
            btnBack.TabIndex = 6;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnNext
            // 
            btnNext.Location = new Point(483, 269);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(94, 29);
            btnNext.TabIndex = 5;
            btnNext.Text = "Next";
            btnNext.UseVisualStyleBackColor = true;
            btnNext.Click += btnNext_Click;
            // 
            // answer4
            // 
            answer4.AutoSize = true;
            answer4.Location = new Point(227, 274);
            answer4.Name = "answer4";
            answer4.Size = new Size(117, 24);
            answer4.TabIndex = 4;
            answer4.TabStop = true;
            answer4.Tag = "D";
            answer4.Text = "radioButton1";
            answer4.UseVisualStyleBackColor = true;
            answer4.CheckedChanged += Answer_CheckedChanged;
            // 
            // answer3
            // 
            answer3.AutoSize = true;
            answer3.Location = new Point(227, 213);
            answer3.Name = "answer3";
            answer3.Size = new Size(117, 24);
            answer3.TabIndex = 3;
            answer3.TabStop = true;
            answer3.Tag = "C";
            answer3.Text = "radioButton1";
            answer3.UseVisualStyleBackColor = true;
            answer3.CheckedChanged += Answer_CheckedChanged;
            // 
            // answer2
            // 
            answer2.AutoSize = true;
            answer2.Location = new Point(227, 156);
            answer2.Name = "answer2";
            answer2.Size = new Size(117, 24);
            answer2.TabIndex = 2;
            answer2.TabStop = true;
            answer2.Tag = "B";
            answer2.Text = "radioButton1";
            answer2.UseVisualStyleBackColor = true;
            answer2.CheckedChanged += Answer_CheckedChanged;
            // 
            // answer1
            // 
            answer1.AutoSize = true;
            answer1.Location = new Point(227, 97);
            answer1.Name = "answer1";
            answer1.Size = new Size(117, 24);
            answer1.TabIndex = 1;
            answer1.TabStop = true;
            answer1.Tag = "A";
            answer1.Text = "radioButton1";
            answer1.UseVisualStyleBackColor = true;
            answer1.CheckedChanged += Answer_CheckedChanged;
            // 
            // questionLabel
            // 
            questionLabel.AutoSize = true;
            questionLabel.Location = new Point(250, 23);
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
            Controls.Add(quizPanel);
            Controls.Add(menuPanel);
            Name = "Form1";
            Text = "Flashcard Quiz";
            Load += Form1_Load;
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
        private RadioButton answer4;
        private RadioButton answer3;
        private RadioButton answer2;
        private RadioButton answer1;
        private Label questionLabel;
        private Button btnBack;
        private Button btnNext;
    }
}
