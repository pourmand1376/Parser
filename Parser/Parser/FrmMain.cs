using System;
using System.IO;
using System.Windows.Forms;

namespace Parser
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                CheckFileExists = true,
                AddExtension = true,
                Multiselect = false,
                CheckPathExists = true,
                DefaultExt = "txt",
                InitialDirectory = AppDomain.CurrentDomain.BaseDirectory,

            };
            
            openFile.ShowDialog();
            textBox1.Text = openFile.FileName;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = File.ReadAllText(textBox1.Text);
            LexicalAnalyzer lex = new LexicalAnalyzer(text);
            lex.Tokenize();
        }
    }
}
