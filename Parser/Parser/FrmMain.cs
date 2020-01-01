using Parser.Lexical;
using Parser.Models;
using Parser.Parse;
using System;
using System.Collections.Generic;
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

        }

        private void btnParseGrammar_Click(object sender,EventArgs e)
        {
            listBoxGrammar.Items.Clear();
            listBoxFirst.Items.Clear();
            var text = File.ReadAllText(textBox1.Text);
            LexicalAnalyzer lex = new LexicalAnalyzer(text);
            _grammarRules = lex.Tokenize();
            
            foreach ( ISymbol symbol in _grammarRules.Symbols.Values)
            {
                if(symbol.SymbolType==SymbolType.Variable)
                    listBoxGrammar.Items.Add(((Variable)symbol).ShowRules());
            }
        }

        private void btnPreprocess_Click(object sender,EventArgs e)
        {
            listBoxFirst.Items.Clear();
//            Preprocessor preprocessor = new Preprocessor(_grammarRules);
//            foreach ( Variable grammerRulesKey in _grammarRules.GrammerRules.Keys )
//            {
//                preprocessor.FirstTerminals(grammerRulesKey);
//            }
//
//            foreach ( Variable grammerRulesKey in _grammarRules.GrammerRules.Keys )
//            {
//                preprocessor.FollowTerminals(grammerRulesKey);
//            }
//
//            foreach ( KeyValuePair<ISymbol,HashSet<Terminal>> keyValuePair in _grammarRules.FirstSet )
//            {
//                listBoxFirst.Items.Add(keyValuePair.Key + ":{ " + string.Join(",",keyValuePair.Value) + "}");
//            }
//
//            foreach (KeyValuePair<ISymbol, HashSet<Terminal>> keyValuePair in _grammarRules.FollowSet) 
//            {
//                listBoxFollow.Items.Add(keyValuePair.Key + ":{ " + string.Join(",",keyValuePair.Value) + "}");
//            }
        }

        private void FrmMain_Load(object sender,EventArgs e)
        {
        }
    }
}
