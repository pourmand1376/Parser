namespace Parser
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnChooseTestFile = new System.Windows.Forms.Button();
            this.txtTestFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.txtgrammarFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabItem = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listBoxGrammar = new System.Windows.Forms.ListBox();
            this.tabPreprocess = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxFollow = new System.Windows.Forms.ListBox();
            this.listBoxFirst = new System.Windows.Forms.ListBox();
            this.ll_1_Tab = new System.Windows.Forms.TabPage();
            this.dataGridViewReport = new System.Windows.Forms.DataGridView();
            this.Stack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InputText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Result = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewLL_1 = new System.Windows.Forms.DataGridView();
            this.tabLR_0 = new System.Windows.Forms.TabPage();
            this.cmbGrammarType = new System.Windows.Forms.ComboBox();
            this.dataGridReportLR = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvLR_0 = new System.Windows.Forms.DataGridView();
            this.txtLRStates = new System.Windows.Forms.RichTextBox();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnFSM = new System.Windows.Forms.Button();
            this.btnShowParseTree = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tabItem.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPreprocess.SuspendLayout();
            this.ll_1_Tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLL_1)).BeginInit();
            this.tabLR_0.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReportLR)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLR_0)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnChooseTestFile);
            this.groupBox1.Controls.Add(this.txtTestFile);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnChooseFile);
            this.groupBox1.Controls.Add(this.txtgrammarFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(628, 78);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Choose File";
            // 
            // btnChooseTestFile
            // 
            this.btnChooseTestFile.Location = new System.Drawing.Point(570, 48);
            this.btnChooseTestFile.Name = "btnChooseTestFile";
            this.btnChooseTestFile.Size = new System.Drawing.Size(45, 23);
            this.btnChooseTestFile.TabIndex = 5;
            this.btnChooseTestFile.Text = "...";
            this.btnChooseTestFile.UseVisualStyleBackColor = true;
            this.btnChooseTestFile.Click += new System.EventHandler(this.btnChooseTestFile_Click);
            // 
            // txtTestFile
            // 
            this.txtTestFile.Enabled = false;
            this.txtTestFile.Location = new System.Drawing.Point(117, 49);
            this.txtTestFile.Name = "txtTestFile";
            this.txtTestFile.Size = new System.Drawing.Size(447, 20);
            this.txtTestFile.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Test File Path:";
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(570, 19);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(45, 23);
            this.btnChooseFile.TabIndex = 2;
            this.btnChooseFile.Text = "...";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // txtgrammarFile
            // 
            this.txtgrammarFile.Enabled = false;
            this.txtgrammarFile.Location = new System.Drawing.Point(117, 20);
            this.txtgrammarFile.Name = "txtgrammarFile";
            this.txtgrammarFile.Size = new System.Drawing.Size(447, 20);
            this.txtgrammarFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Grammar File Path:";
            // 
            // tabItem
            // 
            this.tabItem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabItem.Controls.Add(this.tabPage1);
            this.tabItem.Controls.Add(this.tabPreprocess);
            this.tabItem.Controls.Add(this.ll_1_Tab);
            this.tabItem.Controls.Add(this.tabLR_0);
            this.tabItem.Location = new System.Drawing.Point(12, 10);
            this.tabItem.Name = "tabItem";
            this.tabItem.SelectedIndex = 0;
            this.tabItem.Size = new System.Drawing.Size(648, 461);
            this.tabItem.TabIndex = 3;
            this.tabItem.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabItem_Selecting);
            this.tabItem.Enter += new System.EventHandler(this.tabItem_Enter);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBoxGrammar);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(640, 435);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Scanning Phase";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Leave += new System.EventHandler(this.tabPage1_Leave);
            // 
            // listBoxGrammar
            // 
            this.listBoxGrammar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxGrammar.FormattingEnabled = true;
            this.listBoxGrammar.Location = new System.Drawing.Point(6, 90);
            this.listBoxGrammar.Name = "listBoxGrammar";
            this.listBoxGrammar.Size = new System.Drawing.Size(628, 329);
            this.listBoxGrammar.TabIndex = 2;
            // 
            // tabPreprocess
            // 
            this.tabPreprocess.Controls.Add(this.label3);
            this.tabPreprocess.Controls.Add(this.label2);
            this.tabPreprocess.Controls.Add(this.listBoxFollow);
            this.tabPreprocess.Controls.Add(this.listBoxFirst);
            this.tabPreprocess.Location = new System.Drawing.Point(4, 22);
            this.tabPreprocess.Name = "tabPreprocess";
            this.tabPreprocess.Padding = new System.Windows.Forms.Padding(3);
            this.tabPreprocess.Size = new System.Drawing.Size(640, 435);
            this.tabPreprocess.TabIndex = 1;
            this.tabPreprocess.Text = "Preprocessing Phase";
            this.tabPreprocess.UseVisualStyleBackColor = true;
            this.tabPreprocess.Enter += new System.EventHandler(this.TabPreprocess_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(446, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Follow Sets";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(70, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "First Sets";
            // 
            // listBoxFollow
            // 
            this.listBoxFollow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxFollow.FormattingEnabled = true;
            this.listBoxFollow.Location = new System.Drawing.Point(327, 65);
            this.listBoxFollow.Name = "listBoxFollow";
            this.listBoxFollow.Size = new System.Drawing.Size(307, 277);
            this.listBoxFollow.TabIndex = 3;
            // 
            // listBoxFirst
            // 
            this.listBoxFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxFirst.FormattingEnabled = true;
            this.listBoxFirst.Location = new System.Drawing.Point(6, 64);
            this.listBoxFirst.Name = "listBoxFirst";
            this.listBoxFirst.Size = new System.Drawing.Size(315, 277);
            this.listBoxFirst.TabIndex = 3;
            // 
            // ll_1_Tab
            // 
            this.ll_1_Tab.Controls.Add(this.dataGridViewReport);
            this.ll_1_Tab.Controls.Add(this.dataGridViewLL_1);
            this.ll_1_Tab.Location = new System.Drawing.Point(4, 22);
            this.ll_1_Tab.Name = "ll_1_Tab";
            this.ll_1_Tab.Padding = new System.Windows.Forms.Padding(3);
            this.ll_1_Tab.Size = new System.Drawing.Size(640, 435);
            this.ll_1_Tab.TabIndex = 2;
            this.ll_1_Tab.Text = "LL(1)";
            this.ll_1_Tab.UseVisualStyleBackColor = true;
            this.ll_1_Tab.Enter += new System.EventHandler(this.ll_1_Tab_Enter);
            // 
            // dataGridViewReport
            // 
            this.dataGridViewReport.AllowUserToAddRows = false;
            this.dataGridViewReport.AllowUserToDeleteRows = false;
            this.dataGridViewReport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Stack,
            this.InputText,
            this.Result});
            this.dataGridViewReport.Location = new System.Drawing.Point(6, 182);
            this.dataGridViewReport.Name = "dataGridViewReport";
            this.dataGridViewReport.ReadOnly = true;
            this.dataGridViewReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewReport.Size = new System.Drawing.Size(628, 247);
            this.dataGridViewReport.TabIndex = 2;
            // 
            // Stack
            // 
            this.Stack.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Stack.HeaderText = "Stack";
            this.Stack.Name = "Stack";
            this.Stack.ReadOnly = true;
            // 
            // InputText
            // 
            this.InputText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.InputText.HeaderText = "InputText";
            this.InputText.Name = "InputText";
            this.InputText.ReadOnly = true;
            // 
            // Result
            // 
            this.Result.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Result.HeaderText = "Result";
            this.Result.Name = "Result";
            this.Result.ReadOnly = true;
            // 
            // dataGridViewLL_1
            // 
            this.dataGridViewLL_1.AllowUserToAddRows = false;
            this.dataGridViewLL_1.AllowUserToDeleteRows = false;
            this.dataGridViewLL_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewLL_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLL_1.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewLL_1.Name = "dataGridViewLL_1";
            this.dataGridViewLL_1.ReadOnly = true;
            this.dataGridViewLL_1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewLL_1.Size = new System.Drawing.Size(628, 170);
            this.dataGridViewLL_1.TabIndex = 1;
            // 
            // tabLR_0
            // 
            this.tabLR_0.Controls.Add(this.btnShowParseTree);
            this.tabLR_0.Controls.Add(this.btnFSM);
            this.tabLR_0.Controls.Add(this.cmbGrammarType);
            this.tabLR_0.Controls.Add(this.dataGridReportLR);
            this.tabLR_0.Controls.Add(this.dgvLR_0);
            this.tabLR_0.Controls.Add(this.txtLRStates);
            this.tabLR_0.Location = new System.Drawing.Point(4, 22);
            this.tabLR_0.Name = "tabLR_0";
            this.tabLR_0.Padding = new System.Windows.Forms.Padding(3);
            this.tabLR_0.Size = new System.Drawing.Size(640, 435);
            this.tabLR_0.TabIndex = 3;
            this.tabLR_0.Text = "LR Grammars";
            this.tabLR_0.UseVisualStyleBackColor = true;
            this.tabLR_0.Click += new System.EventHandler(this.tabLR_0_Click);
            this.tabLR_0.Enter += new System.EventHandler(this.tabLR_0_Enter);
            // 
            // cmbGrammarType
            // 
            this.cmbGrammarType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGrammarType.FormattingEnabled = true;
            this.cmbGrammarType.Items.AddRange(new object[] {
            "LR(0)",
            "SLR(1)",
            "CLR(1)"});
            this.cmbGrammarType.Location = new System.Drawing.Point(6, 6);
            this.cmbGrammarType.Name = "cmbGrammarType";
            this.cmbGrammarType.Size = new System.Drawing.Size(185, 21);
            this.cmbGrammarType.TabIndex = 4;
            this.cmbGrammarType.SelectedIndexChanged += new System.EventHandler(this.cmbGrammarType_SelectedIndexChanged);
            // 
            // dataGridReportLR
            // 
            this.dataGridReportLR.AllowUserToAddRows = false;
            this.dataGridReportLR.AllowUserToDeleteRows = false;
            this.dataGridReportLR.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridReportLR.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridReportLR.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.dataGridReportLR.Location = new System.Drawing.Point(197, 212);
            this.dataGridReportLR.Name = "dataGridReportLR";
            this.dataGridReportLR.ReadOnly = true;
            this.dataGridReportLR.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridReportLR.Size = new System.Drawing.Size(437, 184);
            this.dataGridReportLR.TabIndex = 3;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn1.HeaderText = "Stack";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "InputText";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn3.HeaderText = "Result";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dgvLR_0
            // 
            this.dgvLR_0.AllowUserToAddRows = false;
            this.dgvLR_0.AllowUserToDeleteRows = false;
            this.dgvLR_0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLR_0.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLR_0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLR_0.Location = new System.Drawing.Point(197, 6);
            this.dgvLR_0.Name = "dgvLR_0";
            this.dgvLR_0.ReadOnly = true;
            this.dgvLR_0.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvLR_0.Size = new System.Drawing.Size(437, 200);
            this.dgvLR_0.TabIndex = 2;
            // 
            // txtLRStates
            // 
            this.txtLRStates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLRStates.Location = new System.Drawing.Point(6, 29);
            this.txtLRStates.Name = "txtLRStates";
            this.txtLRStates.Size = new System.Drawing.Size(185, 367);
            this.txtLRStates.TabIndex = 0;
            this.txtLRStates.Text = "";
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(13, 474);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(35, 13);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "label4";
            // 
            // btnFSM
            // 
            this.btnFSM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFSM.Location = new System.Drawing.Point(6, 402);
            this.btnFSM.Name = "btnFSM";
            this.btnFSM.Size = new System.Drawing.Size(185, 27);
            this.btnFSM.TabIndex = 5;
            this.btnFSM.Text = "View Finite State Machine";
            this.btnFSM.UseVisualStyleBackColor = true;
            this.btnFSM.Click += new System.EventHandler(this.btnFSM_Click);
            // 
            // btnShowParseTree
            // 
            this.btnShowParseTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowParseTree.Location = new System.Drawing.Point(197, 402);
            this.btnShowParseTree.Name = "btnShowParseTree";
            this.btnShowParseTree.Size = new System.Drawing.Size(437, 27);
            this.btnShowParseTree.TabIndex = 5;
            this.btnShowParseTree.Text = "Show Parse Tree";
            this.btnShowParseTree.UseVisualStyleBackColor = true;
            this.btnShowParseTree.Click += new System.EventHandler(this.btnShowParseTree_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(672, 496);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.tabItem);
            this.Name = "FrmMain";
            this.Text = "Configure";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabItem.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPreprocess.ResumeLayout(false);
            this.tabPreprocess.PerformLayout();
            this.ll_1_Tab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLL_1)).EndInit();
            this.tabLR_0.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridReportLR)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLR_0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.TextBox txtgrammarFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabItem;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPreprocess;
        private System.Windows.Forms.ListBox listBoxGrammar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox listBoxFirst;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox listBoxFollow;
        private System.Windows.Forms.TabPage ll_1_Tab;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.DataGridView dataGridViewLL_1;
        private System.Windows.Forms.Button btnChooseTestFile;
        private System.Windows.Forms.TextBox txtTestFile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridViewReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stack;
        private System.Windows.Forms.DataGridViewTextBoxColumn InputText;
        private System.Windows.Forms.DataGridViewTextBoxColumn Result;
        private System.Windows.Forms.TabPage tabLR_0;
        private System.Windows.Forms.RichTextBox txtLRStates;
        private System.Windows.Forms.DataGridView dgvLR_0;
        private System.Windows.Forms.DataGridView dataGridReportLR;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.ComboBox cmbGrammarType;
        private System.Windows.Forms.Button btnFSM;
        private System.Windows.Forms.Button btnShowParseTree;
    }
}

