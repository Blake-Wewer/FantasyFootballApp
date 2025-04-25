namespace FantasyFootballApp
{
    partial class KeeperCalculationForm
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
            comboBoxLeague = new ComboBox();
            labelLeague = new Label();
            comboBoxManager = new ComboBox();
            labelManager = new Label();
            labelLastSeasonFinalRoster = new Label();
            groupBoxKeeperCalculationResults = new GroupBox();
            textBoxKeeperValue = new TextBox();
            labelKeeperValue = new Label();
            textBoxMethod2Value = new TextBox();
            labelMethod2Value = new Label();
            textBoxMethod1Value = new TextBox();
            labelMethod1Value = new Label();
            checkBoxSecondWaiverKeeper = new CheckBox();
            buttonCalculateKeeperValue = new Button();
            textBoxCurrentADP = new TextBox();
            labelCurrentADP = new Label();
            textBoxSeasonsKept = new TextBox();
            labelSeasonsKept = new Label();
            textBoxRound = new TextBox();
            labelRound = new Label();
            textBoxPick = new TextBox();
            labelPick = new Label();
            listViewFinalRoster = new ListView();
            textBoxPlayer = new TextBox();
            labelPlayer = new Label();
            groupBoxKeeperCalculationResults.SuspendLayout();
            SuspendLayout();
            // 
            // comboBoxLeague
            // 
            comboBoxLeague.DisplayMember = "Display";
            comboBoxLeague.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLeague.FormattingEnabled = true;
            comboBoxLeague.Location = new Point(66, 6);
            comboBoxLeague.Name = "comboBoxLeague";
            comboBoxLeague.Size = new Size(165, 23);
            comboBoxLeague.TabIndex = 5;
            comboBoxLeague.ValueMember = "ID";
            comboBoxLeague.SelectedIndexChanged += comboBoxLeague_SelectedIndexChanged;
            // 
            // labelLeague
            // 
            labelLeague.AutoSize = true;
            labelLeague.Location = new Point(12, 9);
            labelLeague.Name = "labelLeague";
            labelLeague.Size = new Size(48, 15);
            labelLeague.TabIndex = 4;
            labelLeague.Text = "League:";
            // 
            // comboBoxManager
            // 
            comboBoxManager.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxManager.DisplayMember = "Display";
            comboBoxManager.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxManager.FormattingEnabled = true;
            comboBoxManager.Location = new Point(331, 6);
            comboBoxManager.Name = "comboBoxManager";
            comboBoxManager.Size = new Size(191, 23);
            comboBoxManager.TabIndex = 9;
            comboBoxManager.ValueMember = "ID";
            comboBoxManager.SelectedIndexChanged += comboBoxManager_SelectedIndexChanged;
            // 
            // labelManager
            // 
            labelManager.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelManager.AutoSize = true;
            labelManager.Location = new Point(268, 9);
            labelManager.Name = "labelManager";
            labelManager.Size = new Size(57, 15);
            labelManager.TabIndex = 10;
            labelManager.Text = "Manager:";
            // 
            // labelLastSeasonFinalRoster
            // 
            labelLastSeasonFinalRoster.AutoSize = true;
            labelLastSeasonFinalRoster.Location = new Point(12, 45);
            labelLastSeasonFinalRoster.Name = "labelLastSeasonFinalRoster";
            labelLastSeasonFinalRoster.Size = new Size(143, 15);
            labelLastSeasonFinalRoster.TabIndex = 12;
            labelLastSeasonFinalRoster.Text = "Last Season's Final Roster:";
            // 
            // groupBoxKeeperCalculationResults
            // 
            groupBoxKeeperCalculationResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            groupBoxKeeperCalculationResults.Controls.Add(textBoxPlayer);
            groupBoxKeeperCalculationResults.Controls.Add(labelPlayer);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxKeeperValue);
            groupBoxKeeperCalculationResults.Controls.Add(labelKeeperValue);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxMethod2Value);
            groupBoxKeeperCalculationResults.Controls.Add(labelMethod2Value);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxMethod1Value);
            groupBoxKeeperCalculationResults.Controls.Add(labelMethod1Value);
            groupBoxKeeperCalculationResults.Controls.Add(checkBoxSecondWaiverKeeper);
            groupBoxKeeperCalculationResults.Controls.Add(buttonCalculateKeeperValue);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxCurrentADP);
            groupBoxKeeperCalculationResults.Controls.Add(labelCurrentADP);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxSeasonsKept);
            groupBoxKeeperCalculationResults.Controls.Add(labelSeasonsKept);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxRound);
            groupBoxKeeperCalculationResults.Controls.Add(labelRound);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxPick);
            groupBoxKeeperCalculationResults.Controls.Add(labelPick);
            groupBoxKeeperCalculationResults.Location = new Point(286, 63);
            groupBoxKeeperCalculationResults.Name = "groupBoxKeeperCalculationResults";
            groupBoxKeeperCalculationResults.Size = new Size(236, 386);
            groupBoxKeeperCalculationResults.TabIndex = 13;
            groupBoxKeeperCalculationResults.TabStop = false;
            groupBoxKeeperCalculationResults.Text = "Calculated Keeper Values";
            // 
            // textBoxKeeperValue
            // 
            textBoxKeeperValue.Enabled = false;
            textBoxKeeperValue.Location = new Point(97, 357);
            textBoxKeeperValue.Name = "textBoxKeeperValue";
            textBoxKeeperValue.Size = new Size(42, 23);
            textBoxKeeperValue.TabIndex = 15;
            // 
            // labelKeeperValue
            // 
            labelKeeperValue.AutoSize = true;
            labelKeeperValue.Enabled = false;
            labelKeeperValue.Location = new Point(80, 339);
            labelKeeperValue.Name = "labelKeeperValue";
            labelKeeperValue.Size = new Size(77, 15);
            labelKeeperValue.TabIndex = 14;
            labelKeeperValue.Text = "Keeper Value:";
            // 
            // textBoxMethod2Value
            // 
            textBoxMethod2Value.Enabled = false;
            textBoxMethod2Value.Location = new Point(147, 297);
            textBoxMethod2Value.Name = "textBoxMethod2Value";
            textBoxMethod2Value.Size = new Size(42, 23);
            textBoxMethod2Value.TabIndex = 13;
            // 
            // labelMethod2Value
            // 
            labelMethod2Value.AutoSize = true;
            labelMethod2Value.Enabled = false;
            labelMethod2Value.Location = new Point(122, 279);
            labelMethod2Value.Name = "labelMethod2Value";
            labelMethod2Value.Size = new Size(92, 15);
            labelMethod2Value.TabIndex = 12;
            labelMethod2Value.Text = "Method 2 Value:";
            // 
            // textBoxMethod1Value
            // 
            textBoxMethod1Value.Enabled = false;
            textBoxMethod1Value.Location = new Point(48, 297);
            textBoxMethod1Value.Name = "textBoxMethod1Value";
            textBoxMethod1Value.Size = new Size(42, 23);
            textBoxMethod1Value.TabIndex = 11;
            // 
            // labelMethod1Value
            // 
            labelMethod1Value.AutoSize = true;
            labelMethod1Value.Enabled = false;
            labelMethod1Value.Location = new Point(23, 279);
            labelMethod1Value.Name = "labelMethod1Value";
            labelMethod1Value.Size = new Size(92, 15);
            labelMethod1Value.TabIndex = 10;
            labelMethod1Value.Text = "Method 1 Value:";
            // 
            // checkBoxSecondWaiverKeeper
            // 
            checkBoxSecondWaiverKeeper.AutoSize = true;
            checkBoxSecondWaiverKeeper.Enabled = false;
            checkBoxSecondWaiverKeeper.Location = new Point(145, 152);
            checkBoxSecondWaiverKeeper.Name = "checkBoxSecondWaiverKeeper";
            checkBoxSecondWaiverKeeper.Size = new Size(85, 34);
            checkBoxSecondWaiverKeeper.TabIndex = 9;
            checkBoxSecondWaiverKeeper.Text = "2nd Waiver\r\nKeeper";
            checkBoxSecondWaiverKeeper.UseVisualStyleBackColor = true;
            // 
            // buttonCalculateKeeperValue
            // 
            buttonCalculateKeeperValue.Enabled = false;
            buttonCalculateKeeperValue.Location = new Point(47, 190);
            buttonCalculateKeeperValue.Name = "buttonCalculateKeeperValue";
            buttonCalculateKeeperValue.Size = new Size(142, 23);
            buttonCalculateKeeperValue.TabIndex = 8;
            buttonCalculateKeeperValue.Text = "Calculate Keeper Value";
            buttonCalculateKeeperValue.UseVisualStyleBackColor = true;
            buttonCalculateKeeperValue.Click += buttonCalculateKeeperValue_Click;
            // 
            // textBoxCurrentADP
            // 
            textBoxCurrentADP.Location = new Point(88, 158);
            textBoxCurrentADP.Name = "textBoxCurrentADP";
            textBoxCurrentADP.Size = new Size(42, 23);
            textBoxCurrentADP.TabIndex = 7;
            textBoxCurrentADP.TextChanged += textBoxCurrentADP_TextChanged;
            // 
            // labelCurrentADP
            // 
            labelCurrentADP.AutoSize = true;
            labelCurrentADP.Location = new Point(6, 161);
            labelCurrentADP.Name = "labelCurrentADP";
            labelCurrentADP.Size = new Size(76, 15);
            labelCurrentADP.TabIndex = 6;
            labelCurrentADP.Text = "Current ADP:";
            // 
            // textBoxSeasonsKept
            // 
            textBoxSeasonsKept.Enabled = false;
            textBoxSeasonsKept.Location = new Point(140, 92);
            textBoxSeasonsKept.Name = "textBoxSeasonsKept";
            textBoxSeasonsKept.Size = new Size(42, 23);
            textBoxSeasonsKept.TabIndex = 5;
            // 
            // labelSeasonsKept
            // 
            labelSeasonsKept.AutoSize = true;
            labelSeasonsKept.Enabled = false;
            labelSeasonsKept.Location = new Point(55, 95);
            labelSeasonsKept.Name = "labelSeasonsKept";
            labelSeasonsKept.Size = new Size(79, 15);
            labelSeasonsKept.TabIndex = 4;
            labelSeasonsKept.Text = "Seasons Kept:";
            // 
            // textBoxRound
            // 
            textBoxRound.Enabled = false;
            textBoxRound.Location = new Point(171, 56);
            textBoxRound.Name = "textBoxRound";
            textBoxRound.Size = new Size(42, 23);
            textBoxRound.TabIndex = 3;
            // 
            // labelRound
            // 
            labelRound.AutoSize = true;
            labelRound.Enabled = false;
            labelRound.Location = new Point(120, 59);
            labelRound.Name = "labelRound";
            labelRound.Size = new Size(45, 15);
            labelRound.TabIndex = 2;
            labelRound.Text = "Round:";
            // 
            // textBoxPick
            // 
            textBoxPick.Enabled = false;
            textBoxPick.Location = new Point(62, 56);
            textBoxPick.Name = "textBoxPick";
            textBoxPick.Size = new Size(42, 23);
            textBoxPick.TabIndex = 1;
            // 
            // labelPick
            // 
            labelPick.AutoSize = true;
            labelPick.Enabled = false;
            labelPick.Location = new Point(24, 59);
            labelPick.Name = "labelPick";
            labelPick.Size = new Size(32, 15);
            labelPick.TabIndex = 0;
            labelPick.Text = "Pick:";
            // 
            // listViewFinalRoster
            // 
            listViewFinalRoster.FullRowSelect = true;
            listViewFinalRoster.Location = new Point(12, 63);
            listViewFinalRoster.MultiSelect = false;
            listViewFinalRoster.Name = "listViewFinalRoster";
            listViewFinalRoster.Size = new Size(250, 386);
            listViewFinalRoster.TabIndex = 14;
            listViewFinalRoster.UseCompatibleStateImageBehavior = false;
            listViewFinalRoster.View = View.Details;
            listViewFinalRoster.SelectedIndexChanged += listViewFinalRoster_SelectedIndexChanged;
            // 
            // textBoxPlayer
            // 
            textBoxPlayer.Enabled = false;
            textBoxPlayer.Location = new Point(54, 20);
            textBoxPlayer.Name = "textBoxPlayer";
            textBoxPlayer.Size = new Size(176, 23);
            textBoxPlayer.TabIndex = 17;
            // 
            // labelPlayer
            // 
            labelPlayer.AutoSize = true;
            labelPlayer.Enabled = false;
            labelPlayer.Location = new Point(6, 23);
            labelPlayer.Name = "labelPlayer";
            labelPlayer.Size = new Size(42, 15);
            labelPlayer.TabIndex = 16;
            labelPlayer.Text = "Player:";
            // 
            // KeeperCalculationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(534, 461);
            Controls.Add(listViewFinalRoster);
            Controls.Add(groupBoxKeeperCalculationResults);
            Controls.Add(labelLastSeasonFinalRoster);
            Controls.Add(labelManager);
            Controls.Add(comboBoxManager);
            Controls.Add(comboBoxLeague);
            Controls.Add(labelLeague);
            MinimumSize = new Size(550, 500);
            Name = "KeeperCalculationForm";
            Text = "Keeper Calculation   (Work In Progress)";
            Load += KeeperCalculationForm_Load;
            groupBoxKeeperCalculationResults.ResumeLayout(false);
            groupBoxKeeperCalculationResults.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBoxLeague;
        private Label labelLeague;
        private ComboBox comboBoxManager;
        private Label labelManager;
        private Label labelLastSeasonFinalRoster;
        private GroupBox groupBoxKeeperCalculationResults;
        private ListView listViewFinalRoster;
        private TextBox textBoxPick;
        private Label labelPick;
        private TextBox textBoxRound;
        private Label labelRound;
        private TextBox textBoxSeasonsKept;
        private Label labelSeasonsKept;
        private TextBox textBoxCurrentADP;
        private Label labelCurrentADP;
        private Button buttonCalculateKeeperValue;
        private CheckBox checkBoxSecondWaiverKeeper;
        private TextBox textBoxMethod2Value;
        private Label labelMethod2Value;
        private TextBox textBoxMethod1Value;
        private Label labelMethod1Value;
        private TextBox textBoxKeeperValue;
        private Label labelKeeperValue;
        private TextBox textBoxPlayer;
        private Label labelPlayer;
    }
}