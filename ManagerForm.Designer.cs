namespace FantasyFootballApp
{
    partial class ManagerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerForm));
            comboBoxManager = new ComboBox();
            label_Manager = new Label();
            listBoxLeagues = new ListBox();
            labelSelectLeagues = new Label();
            checkBoxUseAllLeagues = new CheckBox();
            dataGridViewManager = new DataGridView();
            buttonManagerReport = new Button();
            comboBoxReport = new ComboBox();
            labelReport = new Label();
            labelSingleLeagueNote = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridViewManager).BeginInit();
            SuspendLayout();
            // 
            // comboBoxManager
            // 
            comboBoxManager.DisplayMember = "Display";
            comboBoxManager.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxManager.FormattingEnabled = true;
            comboBoxManager.Location = new Point(75, 6);
            comboBoxManager.Name = "comboBoxManager";
            comboBoxManager.Size = new Size(191, 23);
            comboBoxManager.TabIndex = 10;
            comboBoxManager.ValueMember = "ID";
            comboBoxManager.SelectedIndexChanged += comboBoxManager_SelectedIndexChanged;
            // 
            // label_Manager
            // 
            label_Manager.AutoSize = true;
            label_Manager.Location = new Point(12, 9);
            label_Manager.Name = "label_Manager";
            label_Manager.Size = new Size(57, 15);
            label_Manager.TabIndex = 9;
            label_Manager.Text = "Manager:";
            // 
            // listBoxLeagues
            // 
            listBoxLeagues.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            listBoxLeagues.Enabled = false;
            listBoxLeagues.FormattingEnabled = true;
            listBoxLeagues.Location = new Point(627, 6);
            listBoxLeagues.Name = "listBoxLeagues";
            listBoxLeagues.SelectionMode = SelectionMode.MultiSimple;
            listBoxLeagues.Size = new Size(175, 49);
            listBoxLeagues.TabIndex = 11;
            listBoxLeagues.Visible = false;
            listBoxLeagues.SelectedIndexChanged += listBoxLeagues_SelectedIndexChanged;
            // 
            // labelSelectLeagues
            // 
            labelSelectLeagues.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelSelectLeagues.AutoSize = true;
            labelSelectLeagues.Enabled = false;
            labelSelectLeagues.Location = new Point(526, 9);
            labelSelectLeagues.Name = "labelSelectLeagues";
            labelSelectLeagues.Size = new Size(95, 15);
            labelSelectLeagues.TabIndex = 12;
            labelSelectLeagues.Text = "Select League(s):";
            labelSelectLeagues.Visible = false;
            // 
            // checkBoxUseAllLeagues
            // 
            checkBoxUseAllLeagues.AutoSize = true;
            checkBoxUseAllLeagues.Checked = true;
            checkBoxUseAllLeagues.CheckState = CheckState.Checked;
            checkBoxUseAllLeagues.Location = new Point(272, 8);
            checkBoxUseAllLeagues.Name = "checkBoxUseAllLeagues";
            checkBoxUseAllLeagues.Size = new Size(108, 19);
            checkBoxUseAllLeagues.TabIndex = 13;
            checkBoxUseAllLeagues.Text = "Use All Leagues";
            checkBoxUseAllLeagues.UseVisualStyleBackColor = true;
            checkBoxUseAllLeagues.CheckedChanged += checkBoxUseAllLeagues_CheckedChanged;
            // 
            // dataGridViewManager
            // 
            dataGridViewManager.AllowUserToAddRows = false;
            dataGridViewManager.AllowUserToDeleteRows = false;
            dataGridViewManager.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewManager.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewManager.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewManager.Location = new Point(12, 65);
            dataGridViewManager.Name = "dataGridViewManager";
            dataGridViewManager.ReadOnly = true;
            dataGridViewManager.Size = new Size(790, 384);
            dataGridViewManager.TabIndex = 14;
            // 
            // buttonManagerReport
            // 
            buttonManagerReport.Location = new Point(272, 31);
            buttonManagerReport.Name = "buttonManagerReport";
            buttonManagerReport.Size = new Size(108, 23);
            buttonManagerReport.TabIndex = 17;
            buttonManagerReport.Text = "Run Report";
            buttonManagerReport.UseVisualStyleBackColor = true;
            buttonManagerReport.Click += buttonManagerReport_Click;
            // 
            // comboBoxReport
            // 
            comboBoxReport.DisplayMember = "Display";
            comboBoxReport.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxReport.FormattingEnabled = true;
            comboBoxReport.Location = new Point(75, 32);
            comboBoxReport.Name = "comboBoxReport";
            comboBoxReport.Size = new Size(191, 23);
            comboBoxReport.TabIndex = 16;
            comboBoxReport.ValueMember = "ID";
            comboBoxReport.SelectedIndexChanged += comboBoxReport_SelectedIndexChanged;
            // 
            // labelReport
            // 
            labelReport.AutoSize = true;
            labelReport.Location = new Point(24, 35);
            labelReport.Name = "labelReport";
            labelReport.Size = new Size(45, 15);
            labelReport.TabIndex = 15;
            labelReport.Text = "Report:";
            // 
            // labelSingleLeagueNote
            // 
            labelSingleLeagueNote.AutoSize = true;
            labelSingleLeagueNote.ForeColor = SystemColors.ControlText;
            labelSingleLeagueNote.Location = new Point(401, 31);
            labelSingleLeagueNote.Name = "labelSingleLeagueNote";
            labelSingleLeagueNote.Size = new Size(183, 30);
            labelSingleLeagueNote.TabIndex = 18;
            labelSingleLeagueNote.Text = "Note: This report can only be ran \r\n           for a single league.";
            labelSingleLeagueNote.Visible = false;
            // 
            // ManagerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(814, 461);
            Controls.Add(labelSingleLeagueNote);
            Controls.Add(buttonManagerReport);
            Controls.Add(comboBoxReport);
            Controls.Add(labelReport);
            Controls.Add(dataGridViewManager);
            Controls.Add(checkBoxUseAllLeagues);
            Controls.Add(labelSelectLeagues);
            Controls.Add(listBoxLeagues);
            Controls.Add(comboBoxManager);
            Controls.Add(label_Manager);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(800, 0);
            Name = "ManagerForm";
            Text = "Manager Breakdown";
            Load += ManagerForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewManager).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxManager;
        private Label label_Manager;
        private ListBox listBoxLeagues;
        private Label labelSelectLeagues;
        private CheckBox checkBoxUseAllLeagues;
        private DataGridView dataGridViewManager;
        private Button buttonManagerReport;
        private ComboBox comboBoxReport;
        private Label labelReport;
        private Label labelSingleLeagueNote;
    }
}