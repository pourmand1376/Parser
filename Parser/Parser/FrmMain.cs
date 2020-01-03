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
using Parser.LLTable;
using Action = Parser.State.Action;

namespace Parser
{
    public partial class FrmMain : Form
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private GrammarRules _grammarRules;
        private Preprocessor _preprocessor;

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
            if (string.IsNullOrEmpty(txtgrammarFile.Text))
            {
                MessageBox.Show("Grammar file is empty!");
                return;
            }
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
            cmbGrammarType.SelectedIndexChanged -= cmbGrammarType_SelectedIndexChanged;
            cmbGrammarType.SelectedIndex = 0;
            cmbGrammarType.SelectedIndexChanged += cmbGrammarType_SelectedIndexChanged;
        }

        private void TabPreprocess_Enter(object sender, EventArgs e)
        {
            listBoxFirst.Items.Clear();
            listBoxFollow.Items.Clear();
            if (_grammarRules == null)
            {
                MessageBox.Show("Grammar File is empty");
                return;
            }
            _preprocessor = new Preprocessor(_grammarRules);
            RestartStopWatch();
            _preprocessor.CalculateAllFirsts();
            _preprocessor.CalculateAllFollows();
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
            if (_grammarRules == null)
            {
                MessageBox.Show("Grammer file is empty");
                return;
            }
            var leftToRightLookAhead1 = new LeftToRight_LookAhead_One(_grammarRules, progress);
            
            leftToRightLookAhead1.Init();
            RestartStopWatch();

            var data = await Task.Run(() => leftToRightLookAhead1.ProcessTable());
            _stopwatch.Stop();
            var creatingTableTime = _stopwatch.ElapsedMilliseconds;
            
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

            long calculatingString = 0;
            if (isValid)
            {
                var terminals = GetTerminals();
                if (terminals == null)
                {
                    MessageBox.Show("test file is empty!");
                    return;
                }
                RestartStopWatch();
                leftToRightLookAhead1.Parse(terminals);
                _stopwatch.Stop();
                calculatingString = _stopwatch.ElapsedMilliseconds;
            }
            lblTime.Text = $"Creating LookAhead Table took {creatingTableTime} ms. Stack Calculation took {calculatingString} ms.";
        }

        private List<Terminal> GetTerminals()
        {
            if (string.IsNullOrWhiteSpace(txtTestFile.Text)) return null;
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

            if (_preprocessor == null)
            {
                TabPreprocess_Enter(null,null);
                if (_preprocessor == null)
                {
                    return;
                }
            }

            LRType lrType = (LRType) cmbGrammarType.SelectedIndex;
            LeftToRight_RightMost_Zero Lr_zero = new LeftToRight_RightMost_Zero(_grammarRules,lrType,_preprocessor);
            RestartStopWatch();
            txtLRStates.Text=Lr_zero.CalculateStateMachine();
            var grammarTable = Lr_zero.FillTable();
            _stopwatch.Stop();
            var tableAndStateMachine = _stopwatch.ElapsedMilliseconds;
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

            bool valid = true;
            foreach (var state in Lr_zero.FiniteStateMachine.States)
            {
                for (int j = 0; j < Lr_zero.MapperToNumber.TerminalCount; j++)
                {
                    var parserAction = grammarTable.ActionTable[state.StateId, j];
                    if(parserAction==null) continue;
                    dgvLR_0.Rows[state.StateId].Cells[j].Value = parserAction;
                    dgvLR_0.Rows[state.StateId].Cells[j].Style.BackColor = !parserAction.HasError? Color.LightGreen: Color.Orange;
                    if (parserAction.HasError) valid = false;
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

            long stackTime = 0;
            if(valid)
            {
                var terminals = GetTerminals();
                if (terminals == null)
                {
                    MessageBox.Show("Terminal is empty!");
                    return;
                }
                RestartStopWatch();
                Lr_zero.Parse(terminals,progress);
                _stopwatch.Stop();
                stackTime = _stopwatch.ElapsedMilliseconds;
            }
            lblTime.Text = $"Creating LR Table took {tableAndStateMachine} ms. Stack Calculation took {stackTime} ms.";
        }

        private void tabLR_0_Click(object sender, EventArgs e)
        {

        }

        private void cmbGrammarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            tabLR_0_Enter(null,null);
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            
        }

        private void tabItem_Selecting(object sender, TabControlCancelEventArgs e)
        {
//            if (e.TabPageIndex == 0)
//            {
//                if (string.IsNullOrWhiteSpace(txtgrammarFile.Text.Trim()))
//                {
//                    e.Cancel = true;
//                }
//            }
        }
    }
}
