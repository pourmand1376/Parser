﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Parser.Lexical;
using Parser.Models;
using Parser.Parse;

namespace Parser
{
    public partial class FrmMain : Form
    {

        private GrammarADT _grammarAdt;

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
            listBoxGrammar.Items.Clear();
            listBoxFirst.Items.Clear();
            var text = File.ReadAllText(textBox1.Text);
            LexicalAnalyzer lex = new LexicalAnalyzer(text);
            lex.Tokenize();
            _grammarAdt = lex.GrammarAdt;


            foreach (KeyValuePair<Variable, List<RightHandSide>> grammarAdtGrammerRule in lex.GrammarAdt.GrammerRules)
            {
                
                foreach (var item in grammarAdtGrammerRule.Value)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Symbol symbol in item.SymbolList)
                    {
                        if (symbol is Terminal term)
                            sb.Append(term.Value);
                        else if (symbol is Variable var)
                            sb.Append(var.Name);
                    }
                    listBoxGrammar.Items.Add(grammarAdtGrammerRule.Key.Name+" ::= " +sb.ToString());
                }
                
                
            }
        }

        private void btnPreprocess_Click(object sender,EventArgs e)
        {
            listBoxFirst.Items.Clear();
            Preprocessor preprocessor = new Preprocessor(_grammarAdt);
            foreach (Variable grammerRulesKey in _grammarAdt.GrammerRules.Keys)
            {
                preprocessor.FirstTerminals(grammerRulesKey);    
            }
            
            foreach (KeyValuePair<Symbol, HashSet<Terminal>> keyValuePair in _grammarAdt.FirstSet)
            {
                if (keyValuePair.Key is Terminal terminal)
                    listBoxFirst.Items.Add(terminal.Value + ":{ " + string.Join(",", keyValuePair.Value) + "}");
                else if (keyValuePair.Key is Variable var)
                    listBoxFirst.Items.Add(var.Name + " :{ " + string.Join(",", keyValuePair.Value) + "}");
            }
        }

        private void FrmMain_Load(object sender,EventArgs e)
        {
        }
    }
}
