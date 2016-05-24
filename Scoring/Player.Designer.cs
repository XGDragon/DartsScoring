namespace Scoring
{
    partial class Player
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.name = new System.Windows.Forms.TextBox();
            this.difficulty = new System.Windows.Forms.ComboBox();
            this.darts = new System.Windows.Forms.TreeView();
            this.startScore = new System.Windows.Forms.ComboBox();
            this.winnerText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(4, 4);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(143, 20);
            this.name.TabIndex = 0;
            // 
            // difficulty
            // 
            this.difficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.difficulty.FormattingEnabled = true;
            this.difficulty.Items.AddRange(new object[] {
            "Singles",
            "Doubles"});
            this.difficulty.Location = new System.Drawing.Point(4, 30);
            this.difficulty.MaxDropDownItems = 2;
            this.difficulty.Name = "difficulty";
            this.difficulty.Size = new System.Drawing.Size(69, 21);
            this.difficulty.TabIndex = 1;
            // 
            // darts
            // 
            this.darts.Location = new System.Drawing.Point(4, 57);
            this.darts.Name = "darts";
            this.darts.ShowLines = false;
            this.darts.ShowPlusMinus = false;
            this.darts.Size = new System.Drawing.Size(143, 84);
            this.darts.TabIndex = 2;
            // 
            // startScore
            // 
            this.startScore.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.startScore.FormattingEnabled = true;
            this.startScore.Items.AddRange(new object[] {
            "301",
            "401",
            "501",
            "601"});
            this.startScore.Location = new System.Drawing.Point(78, 30);
            this.startScore.MaxDropDownItems = 2;
            this.startScore.Name = "startScore";
            this.startScore.Size = new System.Drawing.Size(69, 21);
            this.startScore.TabIndex = 3;
            // 
            // winnerText
            // 
            this.winnerText.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.winnerText.Location = new System.Drawing.Point(3, 144);
            this.winnerText.Name = "winnerText";
            this.winnerText.Size = new System.Drawing.Size(144, 21);
            this.winnerText.TabIndex = 4;
            this.winnerText.Text = "Winner!";
            this.winnerText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.winnerText.Visible = false;
            // 
            // Player
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winnerText);
            this.Controls.Add(this.startScore);
            this.Controls.Add(this.darts);
            this.Controls.Add(this.difficulty);
            this.Controls.Add(this.name);
            this.Name = "Player";
            this.Size = new System.Drawing.Size(150, 170);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.ComboBox difficulty;
        private System.Windows.Forms.TreeView darts;
        private System.Windows.Forms.ComboBox startScore;
        private System.Windows.Forms.Label winnerText;
    }
}
