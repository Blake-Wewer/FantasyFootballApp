namespace FantasyFootballApp
{
    partial class AllPlayForm
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
            textBoxWeekStart = new TextBox();
            labelWeekStart = new Label();
            groupBoxAdditionalAllPlayParameters = new GroupBox();
            label1 = new Label();
            textBoxSeasonEnd = new TextBox();
            labelSeason = new Label();
            textBoxSeasonStart = new TextBox();
            labelWeekEnd = new Label();
            textBoxWeekEnd = new TextBox();
            comboBoxLeague = new ComboBox();
            labelLeague = new Label();
            comboBoxReport = new ComboBox();
            labelReport = new Label();
            buttonAllPlayReport = new Button();
            dataGridViewAllPlay = new DataGridView();
            checkBoxIncludeByeWeeks = new CheckBox();
            buttonHelp = new Button();
            groupBoxAdditionalAllPlayParameters.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAllPlay).BeginInit();
            SuspendLayout();
            // 
            // textBoxWeekStart
            // 
            textBoxWeekStart.Anchor = AnchorStyles.Right;
            textBoxWeekStart.Location = new Point(80, 21);
            textBoxWeekStart.MaxLength = 4;
            textBoxWeekStart.Name = "textBoxWeekStart";
            textBoxWeekStart.Size = new Size(41, 23);
            textBoxWeekStart.TabIndex = 7;
            textBoxWeekStart.KeyPress += textBoxWeekStart_KeyPress;
            textBoxWeekStart.Validating += Week_Validating;
            // 
            // labelWeekStart
            // 
            labelWeekStart.Anchor = AnchorStyles.Right;
            labelWeekStart.AutoSize = true;
            labelWeekStart.Location = new Point(8, 24);
            labelWeekStart.Name = "labelWeekStart";
            labelWeekStart.Size = new Size(66, 15);
            labelWeekStart.TabIndex = 8;
            labelWeekStart.Text = "Week Start:";
            // 
            // groupBoxAdditionalAllPlayParameters
            // 
            groupBoxAdditionalAllPlayParameters.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            groupBoxAdditionalAllPlayParameters.Controls.Add(label1);
            groupBoxAdditionalAllPlayParameters.Controls.Add(textBoxSeasonEnd);
            groupBoxAdditionalAllPlayParameters.Controls.Add(labelSeason);
            groupBoxAdditionalAllPlayParameters.Controls.Add(textBoxSeasonStart);
            groupBoxAdditionalAllPlayParameters.Controls.Add(labelWeekEnd);
            groupBoxAdditionalAllPlayParameters.Controls.Add(textBoxWeekEnd);
            groupBoxAdditionalAllPlayParameters.Controls.Add(labelWeekStart);
            groupBoxAdditionalAllPlayParameters.Controls.Add(textBoxWeekStart);
            groupBoxAdditionalAllPlayParameters.Location = new Point(430, 12);
            groupBoxAdditionalAllPlayParameters.Name = "groupBoxAdditionalAllPlayParameters";
            groupBoxAdditionalAllPlayParameters.Size = new Size(492, 50);
            groupBoxAdditionalAllPlayParameters.TabIndex = 9;
            groupBoxAdditionalAllPlayParameters.TabStop = false;
            groupBoxAdditionalAllPlayParameters.Text = "Additional Parameters (Optional)";
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Right;
            label1.AutoSize = true;
            label1.Location = new Point(369, 24);
            label1.Name = "label1";
            label1.Size = new Size(70, 15);
            label1.TabIndex = 14;
            label1.Text = "Season End:";
            // 
            // textBoxSeasonEnd
            // 
            textBoxSeasonEnd.Anchor = AnchorStyles.Right;
            textBoxSeasonEnd.Location = new Point(445, 21);
            textBoxSeasonEnd.MaxLength = 4;
            textBoxSeasonEnd.Name = "textBoxSeasonEnd";
            textBoxSeasonEnd.Size = new Size(41, 23);
            textBoxSeasonEnd.TabIndex = 13;
            textBoxSeasonEnd.KeyPress += textBoxSeasonEnd_KeyPress;
            textBoxSeasonEnd.Validating += Season_Validating;
            // 
            // labelSeason
            // 
            labelSeason.Anchor = AnchorStyles.Right;
            labelSeason.AutoSize = true;
            labelSeason.Location = new Point(242, 24);
            labelSeason.Name = "labelSeason";
            labelSeason.Size = new Size(74, 15);
            labelSeason.TabIndex = 12;
            labelSeason.Text = "Season Start:";
            // 
            // textBoxSeasonStart
            // 
            textBoxSeasonStart.Anchor = AnchorStyles.Right;
            textBoxSeasonStart.Location = new Point(322, 21);
            textBoxSeasonStart.MaxLength = 4;
            textBoxSeasonStart.Name = "textBoxSeasonStart";
            textBoxSeasonStart.Size = new Size(41, 23);
            textBoxSeasonStart.TabIndex = 11;
            textBoxSeasonStart.KeyPress += textBoxSeasonStart_KeyPress;
            textBoxSeasonStart.Validating += Season_Validating;
            // 
            // labelWeekEnd
            // 
            labelWeekEnd.Anchor = AnchorStyles.Right;
            labelWeekEnd.AutoSize = true;
            labelWeekEnd.Location = new Point(127, 24);
            labelWeekEnd.Name = "labelWeekEnd";
            labelWeekEnd.Size = new Size(62, 15);
            labelWeekEnd.TabIndex = 10;
            labelWeekEnd.Text = "Week End:";
            // 
            // textBoxWeekEnd
            // 
            textBoxWeekEnd.Anchor = AnchorStyles.Right;
            textBoxWeekEnd.Location = new Point(195, 21);
            textBoxWeekEnd.MaxLength = 4;
            textBoxWeekEnd.Name = "textBoxWeekEnd";
            textBoxWeekEnd.Size = new Size(41, 23);
            textBoxWeekEnd.TabIndex = 9;
            textBoxWeekEnd.KeyPress += textBoxWeekEnd_KeyPress;
            textBoxWeekEnd.Validating += Week_Validating;
            // 
            // comboBoxLeague
            // 
            comboBoxLeague.DisplayMember = "Display";
            comboBoxLeague.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLeague.FormattingEnabled = true;
            comboBoxLeague.Location = new Point(66, 8);
            comboBoxLeague.Name = "comboBoxLeague";
            comboBoxLeague.Size = new Size(206, 23);
            comboBoxLeague.TabIndex = 11;
            comboBoxLeague.ValueMember = "ID";
            comboBoxLeague.SelectedIndexChanged += comboBoxLeague_SelectedIndexChanged;
            // 
            // labelLeague
            // 
            labelLeague.AutoSize = true;
            labelLeague.Location = new Point(12, 12);
            labelLeague.Name = "labelLeague";
            labelLeague.Size = new Size(48, 15);
            labelLeague.TabIndex = 10;
            labelLeague.Text = "League:";
            // 
            // comboBoxReport
            // 
            comboBoxReport.DisplayMember = "Display";
            comboBoxReport.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxReport.FormattingEnabled = true;
            comboBoxReport.Location = new Point(66, 37);
            comboBoxReport.Name = "comboBoxReport";
            comboBoxReport.Size = new Size(206, 23);
            comboBoxReport.TabIndex = 13;
            comboBoxReport.ValueMember = "ID";
            comboBoxReport.SelectedIndexChanged += comboBoxReport_SelectedIndexChanged;
            // 
            // labelReport
            // 
            labelReport.AutoSize = true;
            labelReport.Location = new Point(12, 41);
            labelReport.Name = "labelReport";
            labelReport.Size = new Size(45, 15);
            labelReport.TabIndex = 12;
            labelReport.Text = "Report:";
            // 
            // buttonAllPlayReport
            // 
            buttonAllPlayReport.Location = new Point(288, 37);
            buttonAllPlayReport.Name = "buttonAllPlayReport";
            buttonAllPlayReport.Size = new Size(119, 23);
            buttonAllPlayReport.TabIndex = 14;
            buttonAllPlayReport.Text = "Run All-Play Report";
            buttonAllPlayReport.UseVisualStyleBackColor = true;
            buttonAllPlayReport.Click += buttonAllPlayReport_Click;
            // 
            // dataGridViewAllPlay
            // 
            dataGridViewAllPlay.AllowUserToAddRows = false;
            dataGridViewAllPlay.AllowUserToDeleteRows = false;
            dataGridViewAllPlay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewAllPlay.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewAllPlay.Location = new Point(12, 68);
            dataGridViewAllPlay.Name = "dataGridViewAllPlay";
            dataGridViewAllPlay.ReadOnly = true;
            dataGridViewAllPlay.Size = new Size(910, 481);
            dataGridViewAllPlay.TabIndex = 15;
            // 
            // checkBoxIncludeByeWeeks
            // 
            checkBoxIncludeByeWeeks.AutoSize = true;
            checkBoxIncludeByeWeeks.Location = new Point(288, 12);
            checkBoxIncludeByeWeeks.Name = "checkBoxIncludeByeWeeks";
            checkBoxIncludeByeWeeks.Size = new Size(124, 19);
            checkBoxIncludeByeWeeks.TabIndex = 16;
            checkBoxIncludeByeWeeks.Text = "Include Bye Weeks";
            checkBoxIncludeByeWeeks.UseVisualStyleBackColor = true;
            // 
            // buttonHelp
            // 
            buttonHelp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonHelp.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonHelp.Location = new Point(903, 10);
            buttonHelp.Margin = new Padding(0);
            buttonHelp.Name = "buttonHelp";
            buttonHelp.Size = new Size(22, 22);
            buttonHelp.TabIndex = 17;
            buttonHelp.Text = "?";
            buttonHelp.UseVisualStyleBackColor = true;
            buttonHelp.Click += buttonHelp_Click;
            // 
            // AllPlayForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(934, 561);
            Controls.Add(buttonHelp);
            Controls.Add(checkBoxIncludeByeWeeks);
            Controls.Add(dataGridViewAllPlay);
            Controls.Add(buttonAllPlayReport);
            Controls.Add(comboBoxReport);
            Controls.Add(labelReport);
            Controls.Add(comboBoxLeague);
            Controls.Add(labelLeague);
            Controls.Add(groupBoxAdditionalAllPlayParameters);
            MinimumSize = new Size(940, 500);
            Name = "AllPlayForm";
            Text = "All-Play Form";
            Load += AllPlayForm_Load;
            groupBoxAdditionalAllPlayParameters.ResumeLayout(false);
            groupBoxAdditionalAllPlayParameters.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewAllPlay).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxWeekStart;
        private Label labelWeekStart;
        private GroupBox groupBoxAdditionalAllPlayParameters;
        private Label labelWeekEnd;
        private TextBox textBoxWeekEnd;
        private Label labelSeason;
        private TextBox textBoxSeasonStart;
        private ComboBox comboBoxLeague;
        private Label labelLeague;
        private ComboBox comboBoxReport;
        private Label labelReport;
        private Button buttonAllPlayReport;
        private DataGridView dataGridViewAllPlay;
        private Label label1;
        private TextBox textBoxSeasonEnd;
        private CheckBox checkBoxIncludeByeWeeks;
        private Button buttonHelp;
    }
}