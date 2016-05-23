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
        public int Index { get { return int.Parse(Tag.ToString()); } }

        private enum Difficulty { None, Singles, Doubles };
        public enum DartReturn { OK, Next, Dead, Win }

        private int _darts = 3;

        private ScoreNode _scoreNode;
        private ScoreNode[] _dartNode = new ScoreNode[3];
        private ScoreNode _totalNode;

        public int Score { get; set; }

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
                        Score = int.Parse(startScore.SelectedItem.ToString());
                        Reset();
                        break;
                    }
            }
        }

        private void Darts_ActivePlayerChanged(object sender, EventArgs e)
        {
            Darts d = sender as Darts;
            BackColor = (d.ActivePlayer == this) ? SystemColors.ControlDarkDark : SystemColors.Control;
            winnerText.ForeColor = (d.ActivePlayer == this) ? SystemColors.ControlLightLight : SystemColors.ControlText;
            if (d.ActivePlayer == this)
                Reset();

            _darts = 3;
        }

        public DartReturn AddDart(Board b)
        {
            _darts--;
            int multiplier = b.GetMultiplier();
            int newScore = _scoreNode.Score - (b.Score * multiplier);

            if (newScore < 0)
                return DartReturn.Dead;
            Difficulty d = GetDifficulty();
            if (d == Difficulty.Doubles)
            {
                if (newScore == 1)
                    return DartReturn.Dead;
                if (newScore == 0 && multiplier < 2 && b.Score != 50) //not bullseye neither
                    return DartReturn.Dead;
            }

            //update score
            Score = newScore;
            UpdateScoreTree(b.Score, multiplier);

            if (newScore == 0)
            {
                winnerText.Visible = true;
                return DartReturn.Win;
            }
            if (_darts > 0)
                return DartReturn.OK;
            else return DartReturn.Next;
        }

        public void Lock(bool b = true)
        {
            name.ReadOnly = b;
            startScore.Enabled = !b;
            difficulty.Enabled = !b;
        }

        private void Reset()
        {
            winnerText.Visible = false;
            darts.Nodes.Clear();

            //reset tree
            _scoreNode = new ScoreNode(darts.Nodes.Add(string.Empty));
            _scoreNode.UpdateNode(Score);
            TreeNode s = _scoreNode.Node;
            for (int i = 2; i >= 0; i--)
                _dartNode[i] = new ScoreNode(s.Nodes.Add(string.Empty));
            _totalNode = new ScoreNode(s.Nodes.Add(string.Empty));
            s.ExpandAll();
        }

        private void UpdateScoreTree(int score, int multi)
        {
            _scoreNode.UpdateNode(Score);
            _dartNode[_darts].UpdateNode(score, multi);
            int t = 0;
            for (int i = 0; i < _dartNode.Length; i++)
                t += _dartNode[i].Score;
            _totalNode.UpdateNode(t);
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

        private class ScoreNode
        {
            public ScoreNode(TreeNode node) { Node = node; }

            public TreeNode Node { get; private set; }
            public int Score { get; private set; }

            public void UpdateNode(int score, int multi = 1)
            {
                Score = score * multi;
                Node.Text = (multi > 1) ? score + " x " + multi : score.ToString();
            }
        }
    }
}
