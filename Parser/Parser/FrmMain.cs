using Parser.Lexical;
using Parser.Models;
using Parser.Parse;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Msagl.Core.Geometry.Curves;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Parser.LLTable;
using Action = Parser.State.Action;
using Color = System.Drawing.Color;
using TreeNode = Parser.Parse.TreeNode;

namespace Parser
{
    public partial class FrmMain : Form
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private GrammarRules _grammarRules;
        private Preprocessor _preprocessor;
        private LeftToRight_RightMost_Zero _lrZero;
        private LeftToRight_LookAhead_One _leftToRightLookAhead1;

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

            _leftToRightLookAhead1 = new LeftToRight_LookAhead_One(_grammarRules, progress);

            _leftToRightLookAhead1.Init();
            RestartStopWatch();

            var data = await Task.Run(() => _leftToRightLookAhead1.ProcessTable());
            _stopwatch.Stop();
            var creatingTableTime = _stopwatch.ElapsedMilliseconds;
            
            dataGridViewLL_1.Columns.Clear();
            dataGridViewLL_1.Rows.Clear();
            foreach (KeyValuePair<string, int> keyValuePair in _leftToRightLookAhead1.MapperToNumber.MapTerminalToNumber)
            {
                dataGridViewLL_1.Columns.Add(keyValuePair.Key, keyValuePair.Key);
            }

            foreach (var keyValue in _leftToRightLookAhead1.MapperToNumber.MapVariableToNumber)
            {
                dataGridViewLL_1.Rows.Add(new DataGridViewRow()
                {
                    HeaderCell = { Value = keyValue.Key },
                });
            }

            bool isValid = true;
            for (var i = 0; i < _leftToRightLookAhead1.MapperToNumber.VariableCount; i++)
            {
                for (var j = 0; j < _leftToRightLookAhead1.MapperToNumber.TerminalCount; j++)
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
                _leftToRightLookAhead1.Parse(terminals);
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
            dataGridReportLR.Rows.Clear();
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
            _lrZero = new LeftToRight_RightMost_Zero(_grammarRules,lrType,_preprocessor);
            RestartStopWatch();
            txtLRStates.Text=_lrZero.CalculateStateMachine();
            var grammarTable = _lrZero.FillTable();
            _stopwatch.Stop();
            var tableAndStateMachine = _stopwatch.ElapsedMilliseconds;
            foreach(var keyValuePair in _lrZero.MapperToNumber.MapTerminalToNumber)
            {
                dgvLR_0.Columns.Add(keyValuePair.Key, keyValuePair.Key);
            }
            foreach(var keyValuePair in _lrZero.MapperToNumber.MapVariableToNumber.Skip(1))
            {
                dgvLR_0.Columns.Add(keyValuePair.Key, keyValuePair.Key);
            }
            foreach (var keyValue in _lrZero.FiniteStateMachine.States)
            {
                dgvLR_0.Rows.Add(new DataGridViewRow()
                {
                    HeaderCell = { Value = keyValue.StateId },
                });
            }

            bool valid = true;
            foreach (var state in _lrZero.FiniteStateMachine.States)
            {
                for (int j = 0; j < _lrZero.MapperToNumber.TerminalCount; j++)
                {
                    var parserAction = grammarTable.ActionTable[state.StateId, j];
                    if(parserAction==null) continue;
                    dgvLR_0.Rows[state.StateId].Cells[j].Value = parserAction;
                    dgvLR_0.Rows[state.StateId].Cells[j].Style.BackColor = !parserAction.HasError? Color.LightGreen: Color.Orange;
                    if (parserAction.HasError) valid = false;
                }

                int terminalCount = _lrZero.MapperToNumber.TerminalCount;
                for (int j = 0; j < _lrZero.MapperToNumber.VariableCount; j++)
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
                _lrZero.Parse(terminals,progress);
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

        private void btnFSM_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Form form = new System.Windows.Forms.Form();
            form.WindowState = FormWindowState.Maximized;
            //create a viewer object 
            Microsoft.Msagl.GraphViewerGdi.GViewer viewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
            
            //create a graph object 
            var graph = new Graph("Finite State Machine");
            //create the graph content 
            
            Dictionary<States.State,Node> dictionary = new Dictionary<States.State, Node>();
            foreach (States.State state in _lrZero.FiniteStateMachine.States)
            {
                Node node = new Node(state.ToStringCompact());
                node.Attr.FillColor = state.ReduceOnly ? Microsoft.Msagl.Drawing.Color.SeaGreen :
                                        (state.ShiftOnly ? Microsoft.Msagl.Drawing.Color.LightGreen : Microsoft.Msagl.Drawing.Color.Orange);

                dictionary.Add(state,node);
                graph.AddNode(node);
            }

            foreach (States.State state in _lrZero.FiniteStateMachine.States)
            {
                foreach (KeyValuePair<ISymbol, States.State> stateNextState in state.NextStates)
                {
                    var edge = new Edge(dictionary[state],dictionary[stateNextState.Value],ConnectionToGraph.Connected);
                    edge.LabelText = stateNextState.Key.ToString();
                    graph.AddPrecalculatedEdge(edge);
                }
            }

            viewer.Graph = graph;
            //associate the viewer with the form 
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            //show the form 
            form.ShowDialog();
        }

        private void btnShowParseTree_Click(object sender, EventArgs e)
        {

            Queue<TreeNode> nodes = new Queue<TreeNode>();
            foreach (TreeNode lrZeroNode in _lrZero.Nodes)
            {
                nodes.Enqueue(lrZeroNode);
            }
            ShowParseTree(nodes);
            
        }

        private void btnLLParseTree_Click(object sender, EventArgs e)
        {
            Queue<TreeNode> nodes = new Queue<TreeNode>();
            foreach (TreeNode lrZeroNode in _leftToRightLookAhead1.BaseNode.Nodes)
            {
                nodes.Enqueue(lrZeroNode);
            }
            ShowParseTree(nodes);
        }

        private void ShowParseTree(Queue<TreeNode> nodes)
        {
            var form = new Form();
            form.WindowState = FormWindowState.Maximized;
            GViewer viewer = new GViewer();
            var tree = new PhyloTree();
            

            while (nodes.Count > 0)
            {
                TreeNode treeNode = nodes.Dequeue();
                foreach (TreeNode childNode in treeNode.Nodes)
                {
                    Node node=tree.AddNode(treeNode.ToString());
                    node.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Orange;
                    tree.AddEdge(treeNode.ToString(),childNode.ToString());
                    
                    nodes.Enqueue(childNode);
                }
            }

            viewer.Graph = tree;
            form.SuspendLayout();
            viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            form.Controls.Add(viewer);
            form.ResumeLayout();
            form.ShowDialog();
        }
    }
}
