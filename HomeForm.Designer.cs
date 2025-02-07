
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
            btnManagers = new Button();
            dataGridView1 = new DataGridView();
            label_League = new Label();
            comboBoxLeague = new ComboBox();
            btnManagerVsManager = new Button();
            btnPowerRankings = new Button();
            textBoxSeason = new TextBox();
            labelYear = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // btnManagers
            // 
            btnManagers.Location = new Point(12, 12);
            btnManagers.Name = "btnManagers";
            btnManagers.Size = new Size(75, 23);
            btnManagers.TabIndex = 0;
            btnManagers.Text = "Managers";
            btnManagers.UseVisualStyleBackColor = true;
            btnManagers.Click += btnManagers_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 41);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(776, 397);
            dataGridView1.TabIndex = 1;
            // 
            // label_League
            // 
            label_League.AutoSize = true;
            label_League.Location = new Point(469, 16);
            label_League.Name = "label_League";
            label_League.Size = new Size(48, 15);
            label_League.TabIndex = 2;
            label_League.Text = "League:";
            // 
            // comboBoxLeague
            // 
            comboBoxLeague.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            comboBoxLeague.DisplayMember = "Display";
            comboBoxLeague.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLeague.FormattingEnabled = true;
            comboBoxLeague.Location = new Point(523, 12);
            comboBoxLeague.Name = "comboBoxLeague";
            comboBoxLeague.Size = new Size(165, 23);
            comboBoxLeague.TabIndex = 3;
            comboBoxLeague.ValueMember = "ID";
            comboBoxLeague.SelectedIndexChanged += comboBoxLeague_SelectedIndexChanged;
            // 
            // btnManagerVsManager
            // 
            btnManagerVsManager.Location = new Point(198, 12);
            btnManagerVsManager.Name = "btnManagerVsManager";
            btnManagerVsManager.Size = new Size(127, 23);
            btnManagerVsManager.TabIndex = 4;
            btnManagerVsManager.Text = "Manager vs Manager";
            btnManagerVsManager.UseVisualStyleBackColor = true;
            btnManagerVsManager.Click += btnManagerVsManager_Click;
            // 
            // btnPowerRankings
            // 
            btnPowerRankings.Location = new Point(93, 12);
            btnPowerRankings.Name = "btnPowerRankings";
            btnPowerRankings.Size = new Size(99, 23);
            btnPowerRankings.TabIndex = 5;
            btnPowerRankings.Text = "Power Rankings";
            btnPowerRankings.UseVisualStyleBackColor = true;
            btnPowerRankings.Click += btnPowerRankings_Click;
            // 
            // textBoxSeason
            // 
            textBoxSeason.Location = new Point(747, 12);
            textBoxSeason.MaxLength = 4;
            textBoxSeason.Name = "textBoxSeason";
            textBoxSeason.Size = new Size(41, 23);
            textBoxSeason.TabIndex = 6;
            textBoxSeason.KeyPress += TextBoxSeason_KeyPress;
            // 
            // labelYear
            // 
            labelYear.AutoSize = true;
            labelYear.Location = new Point(694, 16);
            labelYear.Name = "labelYear";
            labelYear.Size = new Size(47, 15);
            labelYear.TabIndex = 7;
            labelYear.Text = "Season:";
            // 
            // HomeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(labelYear);
            Controls.Add(textBoxSeason);
            Controls.Add(btnPowerRankings);
            Controls.Add(btnManagerVsManager);
            Controls.Add(comboBoxLeague);
            Controls.Add(label_League);
            Controls.Add(dataGridView1);
            Controls.Add(btnManagers);
            Name = "HomeForm";
            Text = "Fantasy Football App";
            Load += HomeForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnManagers;
        private DataGridView dataGridView1;
        private Label label_League;
        private ComboBox comboBoxLeague;
        private Button btnManagerVsManager;
        private Button btnPowerRankings;
        private TextBox textBoxSeason;
        private Label labelYear;
    }
}
