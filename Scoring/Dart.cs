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
    public partial class Dart : UserControl
    {
        public enum ScoreType { Single, Double, Triple, Other };
        private ScoreType _type;

        public int Score { get; }
        public int TotalScore { get { return Score * Multiplier; } }
        public int Multiplier { get; private set; }
        public string Info { get; private set; } 

        public static event EventHandler TargetHit;

        public Dart(GroupBox gp, int score, ScoreType type)
        {
            InitializeComponent();

            _type = type;
            Score = score;
            Multiplier = GetMultiplier();

            ProcessInfo();
            
            addToGroupBox(gp);
        }

        private void ProcessInfo()
        {
            if (Score % 25 != 0)
            {
                button.Text = Score.ToString();
                Info = Multiplier + " x " + Score;
            }
            else
            {
                switch (Score)
                {
                    case 0:
                        button.Text = "Miss";
                        Info = "X";
                        break;
                    case 25:
                        button.Text = "Single Bull";
                        Info = "S 25";
                        break;
                    case 50:
                        button.Text = "Bulls\nEye";
                        Info = "B 50";
                        break;
                }
            }
        }

        private int GetMultiplier()
        {
            switch (_type)
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

            int w = 60;
            gp.Width += w;
            Location = new Point(7 + (w * (gp.Controls.Count - 1)), 20);
        }

        private void button_Click(object sender, EventArgs e)
        {
            TargetHit(this, EventArgs.Empty);
        }
    }
}
