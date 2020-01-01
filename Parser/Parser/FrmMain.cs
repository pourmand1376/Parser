using Parser.Lexical;
using Parser.Models;
using Parser.Parse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Parser
{
    public partial class FrmMain :Form
    {


        private GrammarRules _grammarRules;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnChooseFile_Click(object sender,EventArgs e)
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
            btnParseGrammar_Click(null,null);
        }

        private void btnParseGrammar_Click(object sender,EventArgs e)
        {
            listBoxGrammar.Items.Clear();
            listBoxFirst.Items.Clear();
            var text = File.ReadAllText(textBox1.Text);
            LexicalAnalyzer lex = new LexicalAnalyzer(text);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            _grammarRules = lex.Tokenize();
            stopwatch.Stop();
            lblTime.Text = $"Tokenizing process took {stopwatch.ElapsedMilliseconds} ms.";
            foreach ( ISymbol symbol in _grammarRules.Symbols.Values)
            {
                if(symbol.SymbolType==SymbolType.Variable)
                    listBoxGrammar.Items.Add(((Variable)symbol).ShowRules());
            }
        }

        private void btnPreprocess_Click(object sender,EventArgs e)
        {
            listBoxFirst.Items.Clear();
            listBoxFollow.Items.Clear();

            Preprocessor preprocessor = new Preprocessor(_grammarRules);
            preprocessor.CalculateAllFirsts();
            preprocessor.CalculateAllFollows();

            foreach (ISymbol symbol in _grammarRules.Symbols.Values)
            {
                if (symbol is Variable variable)
                {
                    listBoxFirst.Items.Add(variable.ShowFirsts());
                    listBoxFollow.Items.Add(variable.ShowFollows());
                }
            }
        }

        private void FrmMain_Load(object sender,EventArgs e)
        {
        }

        private void FillLL_1_Click(object sender,EventArgs e)
        {
            var leftToRightLookAhead1 = new LeftToRight_LookAhead_1(_grammarRules);
            leftToRightLookAhead1.Init();
            var data=leftToRightLookAhead1.ProcessTable();

            foreach (KeyValuePair<string, int> keyValuePair in leftToRightLookAhead1.MapTerminalToNumber)
            {
                dataGridViewLL_1.Columns.Add(keyValuePair.Key, keyValuePair.Key);
            }

            foreach (var keyValue in leftToRightLookAhead1.MapVariableToNumber)
            {
                dataGridViewLL_1.Rows.Add(new DataGridViewRow()
                {
                    HeaderCell = {Value = keyValue.Key},
                });
            }

            for (var i = 0; i < leftToRightLookAhead1.VariableCount; i++)
            {
                for (var j = 0; j < leftToRightLookAhead1.TerminalCount; j++)
                {
                    if(data[i,j]!=null)
                    dataGridViewLL_1.Rows[i].Cells[j].Value = string.Join("",data[i, j]);
                }
            }
        }
    }
}
