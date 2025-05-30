﻿
namespace FantasyFootballApp
{
    partial class HomeForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HomeForm));
            dataGridViewHomeForm = new DataGridView();
            labelLeague = new Label();
            comboBoxLeague = new ComboBox();
            btnManagerVsManager = new Button();
            btnPowerRankings = new Button();
            textBoxSeason = new TextBox();
            labelYear = new Label();
            buttonDraftResults = new Button();
            buttonAllPlay = new Button();
            buttonStandings = new Button();
            buttonManager = new Button();
            buttonKeeperCalculation = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewHomeForm).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewHomeForm
            // 
            dataGridViewHomeForm.AllowUserToAddRows = false;
            dataGridViewHomeForm.AllowUserToDeleteRows = false;
            dataGridViewHomeForm.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewHomeForm.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewHomeForm.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewHomeForm.Location = new Point(12, 41);
            dataGridViewHomeForm.MinimumSize = new Size(700, 350);
            dataGridViewHomeForm.Name = "dataGridViewHomeForm";
            dataGridViewHomeForm.ReadOnly = true;
            dataGridViewHomeForm.Size = new Size(948, 488);
            dataGridViewHomeForm.TabIndex = 1;
            // 
            // labelLeague
            // 
            labelLeague.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelLeague.AutoSize = true;
            labelLeague.Location = new Point(641, 16);
            labelLeague.Name = "labelLeague";
            labelLeague.Size = new Size(48, 15);
            labelLeague.TabIndex = 2;
            labelLeague.Text = "League:";
            // 
            // comboBoxLeague
            // 
            comboBoxLeague.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxLeague.DisplayMember = "Display";
            comboBoxLeague.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLeague.FormattingEnabled = true;
            comboBoxLeague.Location = new Point(695, 12);
            comboBoxLeague.Name = "comboBoxLeague";
            comboBoxLeague.Size = new Size(165, 23);
            comboBoxLeague.TabIndex = 3;
            comboBoxLeague.ValueMember = "ID";
            comboBoxLeague.SelectedIndexChanged += comboBoxLeague_SelectedIndexChanged;
            // 
            // btnManagerVsManager
            // 
            btnManagerVsManager.Location = new Point(280, 11);
            btnManagerVsManager.Name = "btnManagerVsManager";
            btnManagerVsManager.Size = new Size(127, 23);
            btnManagerVsManager.TabIndex = 4;
            btnManagerVsManager.Text = "Manager vs Manager";
            btnManagerVsManager.UseVisualStyleBackColor = true;
            btnManagerVsManager.Click += btnManagerVsManager_Click;
            // 
            // btnPowerRankings
            // 
            btnPowerRankings.Location = new Point(86, 11);
            btnPowerRankings.Name = "btnPowerRankings";
            btnPowerRankings.Size = new Size(99, 23);
            btnPowerRankings.TabIndex = 5;
            btnPowerRankings.Text = "Power Rankings";
            btnPowerRankings.UseVisualStyleBackColor = true;
            btnPowerRankings.Click += btnPowerRankings_Click;
            // 
            // textBoxSeason
            // 
            textBoxSeason.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxSeason.Location = new Point(919, 12);
            textBoxSeason.MaxLength = 4;
            textBoxSeason.Name = "textBoxSeason";
            textBoxSeason.Size = new Size(41, 23);
            textBoxSeason.TabIndex = 6;
            textBoxSeason.KeyPress += TextBoxSeason_KeyPress;
            // 
            // labelYear
            // 
            labelYear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelYear.AutoSize = true;
            labelYear.Location = new Point(866, 16);
            labelYear.Name = "labelYear";
            labelYear.Size = new Size(47, 15);
            labelYear.TabIndex = 7;
            labelYear.Text = "Season:";
            // 
            // buttonDraftResults
            // 
            buttonDraftResults.Location = new Point(191, 11);
            buttonDraftResults.Name = "buttonDraftResults";
            buttonDraftResults.Size = new Size(83, 23);
            buttonDraftResults.TabIndex = 8;
            buttonDraftResults.Text = "Draft Results";
            buttonDraftResults.UseVisualStyleBackColor = true;
            buttonDraftResults.Click += buttonDraftResults_Click;
            // 
            // buttonAllPlay
            // 
            buttonAllPlay.Location = new Point(413, 11);
            buttonAllPlay.Name = "buttonAllPlay";
            buttonAllPlay.Size = new Size(58, 23);
            buttonAllPlay.TabIndex = 9;
            buttonAllPlay.Text = "All-Play";
            buttonAllPlay.UseVisualStyleBackColor = true;
            buttonAllPlay.Click += buttonAllPlay_Click;
            // 
            // buttonStandings
            // 
            buttonStandings.Location = new Point(12, 11);
            buttonStandings.Name = "buttonStandings";
            buttonStandings.Size = new Size(68, 23);
            buttonStandings.TabIndex = 10;
            buttonStandings.Text = "Standings";
            buttonStandings.UseVisualStyleBackColor = true;
            buttonStandings.Click += buttonStandings_Click;
            // 
            // buttonManager
            // 
            buttonManager.Location = new Point(477, 11);
            buttonManager.Name = "buttonManager";
            buttonManager.Size = new Size(62, 23);
            buttonManager.TabIndex = 11;
            buttonManager.Text = "Manager";
            buttonManager.UseVisualStyleBackColor = true;
            buttonManager.Click += buttonManager_Click;
            // 
            // buttonKeeperCalculation
            // 
            buttonKeeperCalculation.Location = new Point(545, 12);
            buttonKeeperCalculation.Name = "buttonKeeperCalculation";
            buttonKeeperCalculation.Size = new Size(77, 23);
            buttonKeeperCalculation.TabIndex = 12;
            buttonKeeperCalculation.Text = "Keeper Calc";
            buttonKeeperCalculation.UseVisualStyleBackColor = true;
            buttonKeeperCalculation.Click += buttonKeeperCalculation_Click;
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(972, 541);
            Controls.Add(buttonKeeperCalculation);
            Controls.Add(buttonManager);
            Controls.Add(buttonStandings);
            Controls.Add(buttonAllPlay);
            Controls.Add(buttonDraftResults);
            Controls.Add(labelYear);
            Controls.Add(textBoxSeason);
            Controls.Add(btnPowerRankings);
            Controls.Add(btnManagerVsManager);
            Controls.Add(comboBoxLeague);
            Controls.Add(labelLeague);
            Controls.Add(dataGridViewHomeForm);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(800, 0);
            Name = "HomeForm";
            Text = "Fantasy Football App";
            Load += HomeForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewHomeForm).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private DataGridView dataGridViewHomeForm;
        private Label labelLeague;
        private ComboBox comboBoxLeague;
        private Button btnManagerVsManager;
        private Button btnPowerRankings;
        private TextBox textBoxSeason;
        private Label labelYear;
        private Button buttonDraftResults;
        private Button buttonAllPlay;
        private Button buttonStandings;
        private Button buttonManager;
        private Button buttonKeeperCalculation;
    }
}
