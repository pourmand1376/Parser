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
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.txtgrammarFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.listBoxGrammar = new System.Windows.Forms.ListBox();
            this.tabPreprocess = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.listBoxFollow = new System.Windows.Forms.ListBox();
            this.listBoxFirst = new System.Windows.Forms.ListBox();
            this.ll_1_Tab = new System.Windows.Forms.TabPage();
            this.dataGridViewLL_1 = new System.Windows.Forms.DataGridView();
            this.lblTime = new System.Windows.Forms.Label();
            this.btnChooseTestFile = new System.Windows.Forms.Button();
            this.txtTestFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPreprocess.SuspendLayout();
            this.ll_1_Tab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLL_1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
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
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPreprocess);
            this.tabControl1.Controls.Add(this.ll_1_Tab);
            this.tabControl1.Location = new System.Drawing.Point(12, 10);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(648, 374);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBoxGrammar);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(640, 348);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Scanning Phase";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // listBoxGrammar
            // 
            this.listBoxGrammar.FormattingEnabled = true;
            this.listBoxGrammar.Location = new System.Drawing.Point(6, 90);
            this.listBoxGrammar.Name = "listBoxGrammar";
            this.listBoxGrammar.Size = new System.Drawing.Size(628, 251);
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
            this.tabPreprocess.Size = new System.Drawing.Size(640, 348);
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
            this.listBoxFollow.FormattingEnabled = true;
            this.listBoxFollow.Location = new System.Drawing.Point(327, 65);
            this.listBoxFollow.Name = "listBoxFollow";
            this.listBoxFollow.Size = new System.Drawing.Size(307, 277);
            this.listBoxFollow.TabIndex = 3;
            // 
            // listBoxFirst
            // 
            this.listBoxFirst.FormattingEnabled = true;
            this.listBoxFirst.Location = new System.Drawing.Point(6, 64);
            this.listBoxFirst.Name = "listBoxFirst";
            this.listBoxFirst.Size = new System.Drawing.Size(315, 277);
            this.listBoxFirst.TabIndex = 3;
            // 
            // ll_1_Tab
            // 
            this.ll_1_Tab.Controls.Add(this.dataGridViewLL_1);
            this.ll_1_Tab.Location = new System.Drawing.Point(4, 22);
            this.ll_1_Tab.Name = "ll_1_Tab";
            this.ll_1_Tab.Padding = new System.Windows.Forms.Padding(3);
            this.ll_1_Tab.Size = new System.Drawing.Size(640, 348);
            this.ll_1_Tab.TabIndex = 2;
            this.ll_1_Tab.Text = "LL(1)";
            this.ll_1_Tab.UseVisualStyleBackColor = true;
            this.ll_1_Tab.Enter += new System.EventHandler(this.ll_1_Tab_Enter);
            // 
            // dataGridViewLL_1
            // 
            this.dataGridViewLL_1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewLL_1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewLL_1.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewLL_1.Name = "dataGridViewLL_1";
            this.dataGridViewLL_1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridViewLL_1.Size = new System.Drawing.Size(628, 336);
            this.dataGridViewLL_1.TabIndex = 1;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Location = new System.Drawing.Point(19, 392);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(35, 13);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "label4";
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
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(672, 414);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.tabControl1);
            this.Name = "FrmMain";
            this.Text = "Configure";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPreprocess.ResumeLayout(false);
            this.tabPreprocess.PerformLayout();
            this.ll_1_Tab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewLL_1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.TextBox txtgrammarFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
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
    }
}

