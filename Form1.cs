namespace FlashcardQuiz_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string SelectedFilePath { get; private set; }
        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                SelectedFilePath = openFileDialog1.FileName;
                MessageBox.Show("You selected: " + SelectedFilePath);
            }
        }
    }
}
