using Parser.Lexical;
using Parser.Models;
using Parser.Parse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Action = Parser.State.Action;

namespace Parser
{
    public partial class FrmMain : Form
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private GrammarRules _grammarRules;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            ChooseFile(txtgrammarFile);
            btnParseGrammar_Click(null, null);
        }

        private void ChooseFile(TextBox textbox)
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
            textbox.Text = openFile.FileName;
        }

        private void btnParseGrammar_Click(object sender, EventArgs e)
        {
            listBoxGrammar.Items.Clear();
            listBoxFirst.Items.Clear();
            var text = File.ReadAllText(txtgrammarFile.Text);
            LexicalAnalyzer lex = new LexicalAnalyzer(text);
            RestartStopWatch();
            _grammarRules = lex.TokenizeGrammar();
            _stopwatch.Stop();
            lblTime.Text = $"Tokenizing process took {_stopwatch.ElapsedMilliseconds} ms.";
            foreach (ISymbol symbol in _grammarRules.SymbolList)
            {
                if (symbol.SymbolType == SymbolType.Variable)
                    listBoxGrammar.Items.Add(((Variable)symbol).ShowRules());
            }
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
        }

        private void TabPreprocess_Enter(object sender, EventArgs e)
        {
            listBoxFirst.Items.Clear();
            listBoxFollow.Items.Clear();

            Preprocessor preprocessor = new Preprocessor(_grammarRules);
            RestartStopWatch();
            preprocessor.CalculateAllFirsts();
            preprocessor.CalculateAllFollows();
            _stopwatch.Stop();
            lblTime.Text = $"First and follow calculation took {_stopwatch.ElapsedMilliseconds} ms.";
            foreach (ISymbol symbol in _grammarRules.SymbolList)
            {
                if (symbol is Variable variable)
                {
                    listBoxFirst.Items.Add(variable.ShowFirsts());
                    listBoxFollow.Items.Add(variable.ShowFollows());
                }
            }
        }

        private async void ll_1_Tab_Enter(object sender, EventArgs e)
        {
            Progress<ParseReportModel> progress = new Progress<ParseReportModel>();
            progress.ProgressChanged += Progress_ProgressChanged;

            var leftToRightLookAhead1 = new LeftToRight_LookAhead_One(_grammarRules, progress);
            leftToRightLookAhead1.Init();
            RestartStopWatch();

            var data = await Task.Run(() => leftToRightLookAhead1.ProcessTable());
            _stopwatch.Stop();
            lblTime.Text = $"Creating LookAhead Table took {_stopwatch.ElapsedMilliseconds} ms.";
            dataGridViewLL_1.Columns.Clear();
            dataGridViewLL_1.Rows.Clear();
            foreach (KeyValuePair<string, int> keyValuePair in leftToRightLookAhead1.MapperToNumber.MapTerminalToNumber)
            {
                dataGridViewLL_1.Columns.Add(keyValuePair.Key, keyValuePair.Key);
            }

            foreach (var keyValue in leftToRightLookAhead1.MapperToNumber.MapVariableToNumber)
            {
                dataGridViewLL_1.Rows.Add(new DataGridViewRow()
                {
                    HeaderCell = { Value = keyValue.Key },
                });
            }

            bool isValid = true;
            for (var i = 0; i < leftToRightLookAhead1.MapperToNumber.VariableCount; i++)
            {
                for (var j = 0; j < leftToRightLookAhead1.MapperToNumber.TerminalCount; j++)
                {
                    if (data[i, j] == null)
                    {
                        continue;
                    }
                    dataGridViewLL_1.Rows[i].Cells[j].Value = string.Join("", data[i, j]);
                    if (data[i, j].Contains(Terminal.Error))
                    {
                        dataGridViewLL_1.Rows[i].Cells[j].Style.BackColor = Color.Orange;
                        isValid = false;
                    }
                    else
                    {
                        dataGridViewLL_1.Rows[i].Cells[j].Style.BackColor = Color.LightGreen;
                    }
                }
            }
            if (isValid)
                leftToRightLookAhead1.Parse(GetTerminals()).ToString();
        }

        private List<Terminal> GetTerminals()
        {
            return new LexicalAnalyzer(
                File.ReadAllText(txtTestFile.Text)).TokenizeInputText();
        }


        private void Progress_ProgressChanged(object sender, ParseReportModel e)
        {
            dataGridViewReport.Rows.Add(e.Stack, e.InputString, e.Output);
        }

        private void RestartStopWatch()
        {
            _stopwatch.Stop();
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        private void btnChooseTestFile_Click(object sender, EventArgs e)
        {
            ChooseFile(txtTestFile);
        }

        private void tabItem_Enter(object sender, EventArgs e)
        {

        }

        private void tabLR_0_Enter(object sender, EventArgs e)
        {
            dgvLR_0.Rows.Clear();
            dgvLR_0.Columns.Clear();

            LeftToRight_RightMost_Zero Lr_zero = new LeftToRight_RightMost_Zero(_grammarRules);
            txtLRStates.Text=Lr_zero.CalculateStateMachine();

            var grammarTable = Lr_zero.FillTable();
            foreach(var keyValuePair in Lr_zero.MapperToNumber.MapTerminalToNumber)
            {
                dgvLR_0.Columns.Add(keyValuePair.Key, keyValuePair.Key);
            }
            foreach(var keyValuePair in Lr_zero.MapperToNumber.MapVariableToNumber.Skip(1))
            {
                dgvLR_0.Columns.Add(keyValuePair.Key, keyValuePair.Key);
            }
            foreach (var keyValue in Lr_zero.FiniteStateMachine.States)
            {
                dgvLR_0.Rows.Add(new DataGridViewRow()
                {
                    HeaderCell = { Value = keyValue.StateId },
                });
            }

            foreach (var state in Lr_zero.FiniteStateMachine.States)
            {
                for (int j = 0; j < Lr_zero.MapperToNumber.TerminalCount; j++)
                {
                    var parserAction = grammarTable.ActionTable[state.StateId, j];
                    if(parserAction==null) continue;
                    dgvLR_0.Rows[state.StateId].Cells[j].Value = parserAction;
                    dgvLR_0.Rows[state.StateId].Cells[j].Style.BackColor = !parserAction.HasError? Color.LightGreen: Color.Orange;
                }

                int terminalCount = Lr_zero.MapperToNumber.TerminalCount;
                for (int j = 0; j < Lr_zero.MapperToNumber.VariableCount; j++)
                {
                    if(grammarTable.GoToTable[state.StateId, j]==null) continue;
                    dgvLR_0.Rows[state.StateId].Cells[j+terminalCount-1].Value = grammarTable.GoToTable[state.StateId, j].StateId;
                    dgvLR_0.Rows[state.StateId].Cells[j+terminalCount-1].Style.BackColor = Color.LightGreen;
                }
            }
            Progress<ParseReportModel> progress = new Progress<ParseReportModel>();
            progress.ProgressChanged += (o, m) =>
            {
                dataGridReportLR.Rows.Add(m.Stack, m.InputString, m.Output);
            };

            Lr_zero.Parse(GetTerminals(),progress);

        }

        private void tabLR_0_Click(object sender, EventArgs e)
        {

        }
    }
}
