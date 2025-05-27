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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KeeperCalculationForm));
            comboBoxLeague = new ComboBox();
            labelLeague = new Label();
            comboBoxManager = new ComboBox();
            labelManager = new Label();
            labelLastSeasonFinalRoster = new Label();
            groupBoxKeeperCalculationResults = new GroupBox();
            textBoxKeeperValue = new TextBox();
            labelKeeperValue = new Label();
            textBoxMethodBValue = new TextBox();
            labelMethodBValue = new Label();
            textBoxMethodB2Value = new TextBox();
            labelMethodB2Value = new Label();
            textBoxMethodB1Value = new TextBox();
            labelMethodB1Value = new Label();
            textBoxPlayer = new TextBox();
            labelPlayer = new Label();
            textBoxMethodAValue = new TextBox();
            labelMethodAValue = new Label();
            textBoxMethodA2Value = new TextBox();
            labelMethodA2Value = new Label();
            textBoxMethodA1Value = new TextBox();
            labelMethodA1Value = new Label();
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
            comboBoxLeague.Size = new Size(196, 23);
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
            comboBoxManager.Location = new Point(440, 6);
            comboBoxManager.Name = "comboBoxManager";
            comboBoxManager.Size = new Size(254, 23);
            comboBoxManager.TabIndex = 9;
            comboBoxManager.ValueMember = "ID";
            comboBoxManager.SelectedIndexChanged += comboBoxManager_SelectedIndexChanged;
            // 
            // labelManager
            // 
            labelManager.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelManager.AutoSize = true;
            labelManager.Location = new Point(377, 9);
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
            groupBoxKeeperCalculationResults.Controls.Add(textBoxKeeperValue);
            groupBoxKeeperCalculationResults.Controls.Add(labelKeeperValue);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxMethodBValue);
            groupBoxKeeperCalculationResults.Controls.Add(labelMethodBValue);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxMethodB2Value);
            groupBoxKeeperCalculationResults.Controls.Add(labelMethodB2Value);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxMethodB1Value);
            groupBoxKeeperCalculationResults.Controls.Add(labelMethodB1Value);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxPlayer);
            groupBoxKeeperCalculationResults.Controls.Add(labelPlayer);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxMethodAValue);
            groupBoxKeeperCalculationResults.Controls.Add(labelMethodAValue);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxMethodA2Value);
            groupBoxKeeperCalculationResults.Controls.Add(labelMethodA2Value);
            groupBoxKeeperCalculationResults.Controls.Add(textBoxMethodA1Value);
            groupBoxKeeperCalculationResults.Controls.Add(labelMethodA1Value);
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
            groupBoxKeeperCalculationResults.Location = new Point(268, 63);
            groupBoxKeeperCalculationResults.Name = "groupBoxKeeperCalculationResults";
            groupBoxKeeperCalculationResults.Size = new Size(426, 386);
            groupBoxKeeperCalculationResults.TabIndex = 13;
            groupBoxKeeperCalculationResults.TabStop = false;
            groupBoxKeeperCalculationResults.Text = "Calculated Keeper Values";
            // 
            // textBoxKeeperValue
            // 
            textBoxKeeperValue.Enabled = false;
            textBoxKeeperValue.Location = new Point(234, 352);
            textBoxKeeperValue.Name = "textBoxKeeperValue";
            textBoxKeeperValue.Size = new Size(42, 23);
            textBoxKeeperValue.TabIndex = 25;
            // 
            // labelKeeperValue
            // 
            labelKeeperValue.AutoSize = true;
            labelKeeperValue.Enabled = false;
            labelKeeperValue.Location = new Point(151, 355);
            labelKeeperValue.Name = "labelKeeperValue";
            labelKeeperValue.Size = new Size(77, 15);
            labelKeeperValue.TabIndex = 24;
            labelKeeperValue.Text = "Keeper Value:";
            // 
            // textBoxMethodBValue
            // 
            textBoxMethodBValue.Enabled = false;
            textBoxMethodBValue.Location = new Point(302, 305);
            textBoxMethodBValue.Name = "textBoxMethodBValue";
            textBoxMethodBValue.Size = new Size(42, 23);
            textBoxMethodBValue.TabIndex = 23;
            // 
            // labelMethodBValue
            // 
            labelMethodBValue.AutoSize = true;
            labelMethodBValue.Enabled = false;
            labelMethodBValue.Location = new Point(276, 287);
            labelMethodBValue.Name = "labelMethodBValue";
            labelMethodBValue.Size = new Size(93, 15);
            labelMethodBValue.TabIndex = 22;
            labelMethodBValue.Text = "Method B Value:";
            // 
            // textBoxMethodB2Value
            // 
            textBoxMethodB2Value.Enabled = false;
            textBoxMethodB2Value.Location = new Point(347, 245);
            textBoxMethodB2Value.Name = "textBoxMethodB2Value";
            textBoxMethodB2Value.Size = new Size(42, 23);
            textBoxMethodB2Value.TabIndex = 21;
            // 
            // labelMethodB2Value
            // 
            labelMethodB2Value.AutoSize = true;
            labelMethodB2Value.Enabled = false;
            labelMethodB2Value.Location = new Point(318, 227);
            labelMethodB2Value.Name = "labelMethodB2Value";
            labelMethodB2Value.Size = new Size(99, 15);
            labelMethodB2Value.TabIndex = 20;
            labelMethodB2Value.Text = "Method B2 Value:";
            // 
            // textBoxMethodB1Value
            // 
            textBoxMethodB1Value.Enabled = false;
            textBoxMethodB1Value.Location = new Point(248, 245);
            textBoxMethodB1Value.Name = "textBoxMethodB1Value";
            textBoxMethodB1Value.Size = new Size(42, 23);
            textBoxMethodB1Value.TabIndex = 19;
            // 
            // labelMethodB1Value
            // 
            labelMethodB1Value.AutoSize = true;
            labelMethodB1Value.Enabled = false;
            labelMethodB1Value.Location = new Point(219, 227);
            labelMethodB1Value.Name = "labelMethodB1Value";
            labelMethodB1Value.Size = new Size(99, 15);
            labelMethodB1Value.TabIndex = 18;
            labelMethodB1Value.Text = "Method B1 Value:";
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
            // textBoxMethodAValue
            // 
            textBoxMethodAValue.Enabled = false;
            textBoxMethodAValue.Location = new Point(89, 305);
            textBoxMethodAValue.Name = "textBoxMethodAValue";
            textBoxMethodAValue.Size = new Size(42, 23);
            textBoxMethodAValue.TabIndex = 15;
            // 
            // labelMethodAValue
            // 
            labelMethodAValue.AutoSize = true;
            labelMethodAValue.Enabled = false;
            labelMethodAValue.Location = new Point(63, 287);
            labelMethodAValue.Name = "labelMethodAValue";
            labelMethodAValue.Size = new Size(94, 15);
            labelMethodAValue.TabIndex = 14;
            labelMethodAValue.Text = "Method A Value:";
            // 
            // textBoxMethodA2Value
            // 
            textBoxMethodA2Value.Enabled = false;
            textBoxMethodA2Value.Location = new Point(134, 245);
            textBoxMethodA2Value.Name = "textBoxMethodA2Value";
            textBoxMethodA2Value.Size = new Size(42, 23);
            textBoxMethodA2Value.TabIndex = 13;
            // 
            // labelMethodA2Value
            // 
            labelMethodA2Value.AutoSize = true;
            labelMethodA2Value.Enabled = false;
            labelMethodA2Value.Location = new Point(105, 227);
            labelMethodA2Value.Name = "labelMethodA2Value";
            labelMethodA2Value.Size = new Size(100, 15);
            labelMethodA2Value.TabIndex = 12;
            labelMethodA2Value.Text = "Method A2 Value:";
            // 
            // textBoxMethodA1Value
            // 
            textBoxMethodA1Value.Enabled = false;
            textBoxMethodA1Value.Location = new Point(35, 245);
            textBoxMethodA1Value.Name = "textBoxMethodA1Value";
            textBoxMethodA1Value.Size = new Size(42, 23);
            textBoxMethodA1Value.TabIndex = 11;
            // 
            // labelMethodA1Value
            // 
            labelMethodA1Value.AutoSize = true;
            labelMethodA1Value.Enabled = false;
            labelMethodA1Value.Location = new Point(6, 227);
            labelMethodA1Value.Name = "labelMethodA1Value";
            labelMethodA1Value.Size = new Size(100, 15);
            labelMethodA1Value.TabIndex = 10;
            labelMethodA1Value.Text = "Method A1 Value:";
            // 
            // checkBoxSecondWaiverKeeper
            // 
            checkBoxSecondWaiverKeeper.AutoSize = true;
            checkBoxSecondWaiverKeeper.Enabled = false;
            checkBoxSecondWaiverKeeper.Location = new Point(147, 77);
            checkBoxSecondWaiverKeeper.Name = "checkBoxSecondWaiverKeeper";
            checkBoxSecondWaiverKeeper.Size = new Size(124, 19);
            checkBoxSecondWaiverKeeper.TabIndex = 9;
            checkBoxSecondWaiverKeeper.Text = "2nd Waiver Keeper";
            checkBoxSecondWaiverKeeper.UseVisualStyleBackColor = true;
            // 
            // buttonCalculateKeeperValue
            // 
            buttonCalculateKeeperValue.Enabled = false;
            buttonCalculateKeeperValue.Location = new Point(120, 126);
            buttonCalculateKeeperValue.Name = "buttonCalculateKeeperValue";
            buttonCalculateKeeperValue.Size = new Size(142, 23);
            buttonCalculateKeeperValue.TabIndex = 8;
            buttonCalculateKeeperValue.Text = "Calculate Keeper Value";
            buttonCalculateKeeperValue.UseVisualStyleBackColor = true;
            buttonCalculateKeeperValue.Click += buttonCalculateKeeperValue_Click;
            // 
            // textBoxCurrentADP
            // 
            textBoxCurrentADP.Location = new Point(88, 75);
            textBoxCurrentADP.Name = "textBoxCurrentADP";
            textBoxCurrentADP.Size = new Size(42, 23);
            textBoxCurrentADP.TabIndex = 7;
            textBoxCurrentADP.TextChanged += textBoxCurrentADP_TextChanged;
            // 
            // labelCurrentADP
            // 
            labelCurrentADP.AutoSize = true;
            labelCurrentADP.Location = new Point(6, 78);
            labelCurrentADP.Name = "labelCurrentADP";
            labelCurrentADP.Size = new Size(76, 15);
            labelCurrentADP.TabIndex = 6;
            labelCurrentADP.Text = "Current ADP:";
            // 
            // textBoxSeasonsKept
            // 
            textBoxSeasonsKept.Enabled = false;
            textBoxSeasonsKept.Location = new Point(375, 75);
            textBoxSeasonsKept.Name = "textBoxSeasonsKept";
            textBoxSeasonsKept.Size = new Size(42, 23);
            textBoxSeasonsKept.TabIndex = 5;
            // 
            // labelSeasonsKept
            // 
            labelSeasonsKept.AutoSize = true;
            labelSeasonsKept.Enabled = false;
            labelSeasonsKept.Location = new Point(290, 78);
            labelSeasonsKept.Name = "labelSeasonsKept";
            labelSeasonsKept.Size = new Size(79, 15);
            labelSeasonsKept.TabIndex = 4;
            labelSeasonsKept.Text = "Seasons Kept:";
            // 
            // textBoxRound
            // 
            textBoxRound.Enabled = false;
            textBoxRound.Location = new Point(375, 46);
            textBoxRound.Name = "textBoxRound";
            textBoxRound.Size = new Size(42, 23);
            textBoxRound.TabIndex = 3;
            // 
            // labelRound
            // 
            labelRound.AutoSize = true;
            labelRound.Enabled = false;
            labelRound.Location = new Point(324, 49);
            labelRound.Name = "labelRound";
            labelRound.Size = new Size(45, 15);
            labelRound.TabIndex = 2;
            labelRound.Text = "Round:";
            // 
            // textBoxPick
            // 
            textBoxPick.Enabled = false;
            textBoxPick.Location = new Point(375, 20);
            textBoxPick.Name = "textBoxPick";
            textBoxPick.Size = new Size(42, 23);
            textBoxPick.TabIndex = 1;
            // 
            // labelPick
            // 
            labelPick.AutoSize = true;
            labelPick.Enabled = false;
            labelPick.Location = new Point(337, 23);
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
            // KeeperCalculationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(706, 461);
            Controls.Add(listViewFinalRoster);
            Controls.Add(groupBoxKeeperCalculationResults);
            Controls.Add(labelLastSeasonFinalRoster);
            Controls.Add(labelManager);
            Controls.Add(comboBoxManager);
            Controls.Add(comboBoxLeague);
            Controls.Add(labelLeague);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(550, 500);
            Name = "KeeperCalculationForm";
            Text = "Keeper Calculation";
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
        private TextBox textBoxMethodA2Value;
        private Label labelMethodA2Value;
        private TextBox textBoxMethodA1Value;
        private Label labelMethodA1Value;
        private TextBox textBoxMethodAValue;
        private Label labelMethodAValue;
        private TextBox textBoxPlayer;
        private Label labelPlayer;
        private TextBox textBoxMethodBValue;
        private Label labelMethodBValue;
        private TextBox textBoxMethodB2Value;
        private Label labelMethodB2Value;
        private TextBox textBoxMethodB1Value;
        private Label labelMethodB1Value;
        private TextBox textBoxKeeperValue;
        private Label labelKeeperValue;
    }
}