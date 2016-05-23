using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scoring
{
    public partial class Board : UserControl
    {
        public enum ScoreType { Single, Double, Triple, Other };
        public enum OtherType { Bullseye, Single_Bull, Miss};

        public int Score { get; }
        public ScoreType Type { get; }

        public static event EventHandler TargetHit;

        public Board(GroupBox gp, int score, ScoreType type)
        {
            InitializeComponent();

            Score = score;
            button.Text = score.ToString();

            Type = type;
            addToGroupBox(gp);
        }

        public Board(GroupBox gp, OtherType ot)
        {
            InitializeComponent();

            Type = ScoreType.Other;
            button.Text = ot.ToString();

            switch (ot)
            {
                case OtherType.Bullseye:
                    Score = 50; break;
                case OtherType.Single_Bull:
                    Score = 25; button.Text = "SingleBull"; break;
                default:
                    Score = 0; break;
            }
            addToGroupBox(gp);
        }

        public int GetMultiplier()
        {
            switch (Type)
            {
                case ScoreType.Double:
                    return 2;
                case ScoreType.Triple:
                    return 3;
                default:
                    return 1;
            }
        }

        private void addToGroupBox(GroupBox gp)
        {
            gp.Controls.Add(this);

            int w = 55;
            gp.Width += w;
            Location = new Point(7 + (w * (Score - 1)), 20);
        }

        private void button_Click(object sender, EventArgs e)
        {
            TargetHit(this, EventArgs.Empty);
        }
    }
}
