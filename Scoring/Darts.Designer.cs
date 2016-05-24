namespace Scoring
{
    partial class Darts
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
            this.newGame = new System.Windows.Forms.Button();
            this.singles = new System.Windows.Forms.GroupBox();
            this.doubles = new System.Windows.Forms.GroupBox();
            this.triples = new System.Windows.Forms.GroupBox();
            this.other = new System.Windows.Forms.GroupBox();
            this.playerCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.Undo = new System.Windows.Forms.Button();
            this.player7 = new Scoring.Player();
            this.player6 = new Scoring.Player();
            this.player5 = new Scoring.Player();
            this.player4 = new Scoring.Player();
            this.player3 = new Scoring.Player();
            this.player2 = new Scoring.Player();
            this.player1 = new Scoring.Player();
            ((System.ComponentModel.ISupportInitialize)(this.playerCount)).BeginInit();
            this.SuspendLayout();
            // 
            // newGame
            // 
            this.newGame.Location = new System.Drawing.Point(12, 38);
            this.newGame.Name = "newGame";
            this.newGame.Size = new System.Drawing.Size(125, 23);
            this.newGame.TabIndex = 1;
            this.newGame.Text = "Start";
            this.newGame.UseVisualStyleBackColor = true;
            this.newGame.Click += new System.EventHandler(this.newGame_Click);
            // 
            // singles
            // 
            this.singles.Location = new System.Drawing.Point(13, 194);
            this.singles.Name = "singles";
            this.singles.Size = new System.Drawing.Size(14, 88);
            this.singles.TabIndex = 4;
            this.singles.TabStop = false;
            this.singles.Text = "groupBox1";
            // 
            // doubles
            // 
            this.doubles.Location = new System.Drawing.Point(13, 288);
            this.doubles.Name = "doubles";
            this.doubles.Size = new System.Drawing.Size(14, 88);
            this.doubles.TabIndex = 5;
            this.doubles.TabStop = false;
            this.doubles.Text = "groupBox1";
            // 
            // triples
            // 
            this.triples.Location = new System.Drawing.Point(13, 382);
            this.triples.Name = "triples";
            this.triples.Size = new System.Drawing.Size(14, 88);
            this.triples.TabIndex = 5;
            this.triples.TabStop = false;
            this.triples.Text = "groupBox1";
            // 
            // other
            // 
            this.other.Location = new System.Drawing.Point(13, 476);
            this.other.Name = "other";
            this.other.Size = new System.Drawing.Size(14, 88);
            this.other.TabIndex = 6;
            this.other.TabStop = false;
            // 
            // playerCount
            // 
            this.playerCount.Location = new System.Drawing.Point(110, 12);
            this.playerCount.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.playerCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.playerCount.Name = "playerCount";
            this.playerCount.Size = new System.Drawing.Size(28, 20);
            this.playerCount.TabIndex = 7;
            this.playerCount.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.playerCount.ValueChanged += new System.EventHandler(this.playerCount_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Number of Players";
            // 
            // Undo
            // 
            this.Undo.Location = new System.Drawing.Point(14, 130);
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(124, 23);
            this.Undo.TabIndex = 15;
            this.Undo.Text = "Undo";
            this.Undo.UseVisualStyleBackColor = true;
            this.Undo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // player7
            // 
            this.player7.ID = 7;
            this.player7.Location = new System.Drawing.Point(1082, 12);
            this.player7.Margin = new System.Windows.Forms.Padding(4);
            this.player7.Name = "player7";
            this.player7.Size = new System.Drawing.Size(150, 170);
            this.player7.TabIndex = 14;
            this.player7.Tag = "6";
            // 
            // player6
            // 
            this.player6.ID = 6;
            this.player6.Location = new System.Drawing.Point(924, 12);
            this.player6.Margin = new System.Windows.Forms.Padding(4);
            this.player6.Name = "player6";
            this.player6.Size = new System.Drawing.Size(150, 170);
            this.player6.TabIndex = 12;
            this.player6.Tag = "6";
            // 
            // player5
            // 
            this.player5.ID = 5;
            this.player5.Location = new System.Drawing.Point(768, 12);
            this.player5.Margin = new System.Windows.Forms.Padding(4);
            this.player5.Name = "player5";
            this.player5.Size = new System.Drawing.Size(150, 170);
            this.player5.TabIndex = 11;
            this.player5.Tag = "5";
            // 
            // player4
            // 
            this.player4.ID = 4;
            this.player4.Location = new System.Drawing.Point(612, 12);
            this.player4.Margin = new System.Windows.Forms.Padding(4);
            this.player4.Name = "player4";
            this.player4.Size = new System.Drawing.Size(150, 170);
            this.player4.TabIndex = 10;
            this.player4.Tag = "4";
            // 
            // player3
            // 
            this.player3.ID = 3;
            this.player3.Location = new System.Drawing.Point(456, 12);
            this.player3.Margin = new System.Windows.Forms.Padding(4);
            this.player3.Name = "player3";
            this.player3.Size = new System.Drawing.Size(150, 170);
            this.player3.TabIndex = 9;
            this.player3.Tag = "3";
            // 
            // player2
            // 
            this.player2.ID = 2;
            this.player2.Location = new System.Drawing.Point(300, 12);
            this.player2.Margin = new System.Windows.Forms.Padding(4);
            this.player2.Name = "player2";
            this.player2.Size = new System.Drawing.Size(150, 170);
            this.player2.TabIndex = 8;
            this.player2.Tag = "2";
            // 
            // player1
            // 
            this.player1.ID = 1;
            this.player1.Location = new System.Drawing.Point(144, 12);
            this.player1.Margin = new System.Windows.Forms.Padding(4);
            this.player1.Name = "player1";
            this.player1.Size = new System.Drawing.Size(150, 170);
            this.player1.TabIndex = 0;
            this.player1.Tag = "";
            // 
            // Darts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 581);
            this.Controls.Add(this.Undo);
            this.Controls.Add(this.player7);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.player6);
            this.Controls.Add(this.player5);
            this.Controls.Add(this.player4);
            this.Controls.Add(this.player3);
            this.Controls.Add(this.player2);
            this.Controls.Add(this.playerCount);
            this.Controls.Add(this.other);
            this.Controls.Add(this.triples);
            this.Controls.Add(this.doubles);
            this.Controls.Add(this.singles);
            this.Controls.Add(this.newGame);
            this.Controls.Add(this.player1);
            this.Name = "Darts";
            this.Text = "Darts Counter";
            ((System.ComponentModel.ISupportInitialize)(this.playerCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Player player1;
        private System.Windows.Forms.Button newGame;
        private System.Windows.Forms.GroupBox singles;
        private System.Windows.Forms.GroupBox doubles;
        private System.Windows.Forms.GroupBox triples;
        private System.Windows.Forms.GroupBox other;
        private System.Windows.Forms.NumericUpDown playerCount;
        private Player player2;
        private Player player3;
        private Player player4;
        private Player player5;
        private Player player6;
        private System.Windows.Forms.Label label1;
        private Player player7;
        private System.Windows.Forms.Button Undo;
    }
}

