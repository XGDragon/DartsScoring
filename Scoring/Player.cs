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
    public partial class Player : UserControl
    {
        public override string Text { get { return name.Text; } set { name.Text = value; } }
        public int ID { get; set; }
        public int StartingScore { get { return int.Parse(startScore.SelectedItem.ToString()); } }

        private enum Difficulty { None, Singles, Doubles };
        public enum DartReturn { OK, Next, Dead, Win }
        
        private TreeNode _scoreNode;
        private TreeNode[] _dartNode = new TreeNode[3];
        private TreeNode _totalNode;

        public Player() {
            InitializeComponent();

            Darts.ActivePlayerChanged += Darts_ActivePlayerChanged;
            Darts.GameStateChanged += Darts_GameStateChanged;

            difficulty.SelectedIndex = 0;
            startScore.SelectedIndex = 0;
        }

        private void Darts_GameStateChanged(object sender, EventArgs e)
        {
            Darts d = sender as Darts;
            switch (d.GameState)
            {
                case Darts.State.Configure:
                    {
                        Lock(false);
                        break;
                    }
                case Darts.State.InProgress:
                    {
                        Lock();
                        Setup();
                        break;
                    }
            }
        }

        private void Darts_ActivePlayerChanged(object sender, EventArgs e)
        {
            Darts d = sender as Darts;

            if (d.ActivePlayer == this)
            {
                BackColor = SystemColors.ControlDarkDark;
                winnerText.ForeColor = SystemColors.ControlLightLight;
            }
            else
            {
                BackColor = SystemColors.Control;
                winnerText.ForeColor = SystemColors.ControlText;
                foreach (TreeNode tn in _dartNode)
                    tn.ForeColor = SystemColors.GrayText;
            }
        }

        public DartReturn DartResponse(PlayerHistory ph, Dart dart)
        {
            int newScore = ph.PlayerScore[this] - dart.TotalScore;

            Difficulty dif = GetDifficulty();
            if (newScore < 0)
                return DartReturn.Dead;
            if (dif == Difficulty.Doubles)
                if (newScore == 1 || newScore == 0 && dart.GetMultiplier() < 2 && dart.Score != 50) //not bullseye neither
                    return DartReturn.Dead;
            
            if (newScore == 0)
            {
                winnerText.Visible = true;
                return DartReturn.Win;
            }
            if (ph.PlayerDarts[this].Count < 2)
                return DartReturn.OK;
            else return DartReturn.Next;
        }
      

        private void UpdateScoreTree(PlayerHistory ph)
        {
            _scoreNode.Text = ph.PlayerScore[this].ToString();
            int a = ph.PlayerDarts[this].Count;
            int t = 0;
            for (int i = 0; i < 3; i++)
            {
                int score = (a > i) ? ph.PlayerDarts[this][i].TotalScore : 0;
                _dartNode[i].Text = (a > i) ? string.Empty : score.ToString();
                t += score;
            }
            //_dartNode[ph.Dart].ForeColor = SystemColors.WindowText;
            _totalNode.Text = t.ToString();
        }

        public void Lock(bool b = true)
        {
            name.ReadOnly = b;
            startScore.Enabled = !b;
            difficulty.Enabled = !b;
        }

        private void Setup()
        {
            winnerText.Visible = false;

            //resetTree
            darts.Nodes.Clear();
            _scoreNode = darts.Nodes.Add("todo");
            for (int i = 0; i < 3; i++)
                _dartNode[i] = _scoreNode.Nodes.Add(string.Empty);
            _totalNode = _scoreNode.Nodes.Add(string.Empty);
            //_totalNode.NodeFont = new Font(darts.Font, FontStyle.Bold);

            _scoreNode.ExpandAll();
        }

        private Difficulty GetDifficulty()
        {
            string d = difficulty.SelectedItem.ToString();
            if (d == Difficulty.Singles.ToString())
                return Difficulty.Singles;
            if (d == Difficulty.Doubles.ToString())
                return Difficulty.Doubles;
            return Difficulty.None;
        }
    }
}
