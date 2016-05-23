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

        private enum Difficulty { None, Singles, Doubles };
        public enum DartReturn { OK, Next, Dead, Win }

        private int _darts;
        private int _runningScore;
        public int Score { get; set; }
        
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
            _scoreNode.Text = Score.ToString();

            if (d.ActivePlayer == this)
            {
                BackColor = SystemColors.ControlDarkDark;
                winnerText.ForeColor = SystemColors.ControlLightLight;
                _runningScore = 0;
                _darts = 0;
            }
            else
            {
                BackColor = SystemColors.Control;
                winnerText.ForeColor = SystemColors.ControlText;
                foreach (TreeNode tn in _dartNode)
                    tn.ForeColor = SystemColors.GrayText;
            }
        }

        public DartReturn UpdateHistory(Darts d, PlayerHistory ph)
        {
            int newScore = Score - ph.ModifyScore;

            if (newScore < 0)
            {
                Score += _runningScore;
                return DartReturn.Dead;
            }
            Difficulty dif = GetDifficulty();
            if (dif == Difficulty.Doubles)
                if (newScore == 1 || newScore == 0 && ph.Multiplier < 2 && ph.ModifyScore != 50) //not bullseye neither
                {
                    Score += _runningScore;
                    return DartReturn.Dead;
                }

            //update score
            Score = newScore;
            _runningScore += ph.ModifyScore;
            ph.Dart = _darts;
            UpdateScoreTree(ph);
            _darts++;

            if (newScore == 0)
            {
                winnerText.Visible = true;
                return DartReturn.Win;
            }
            if (_darts < 3)
                return DartReturn.OK;
            else return DartReturn.Next;
        }

        private void UpdateScoreTree(PlayerHistory ph)
        {
            _scoreNode.Text = Score.ToString();
            _dartNode[ph.Dart].Text = ph.DartInfo;
            _dartNode[ph.Dart].ForeColor = SystemColors.WindowText;
            _totalNode.Text = _runningScore.ToString();
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
            Score = int.Parse(startScore.SelectedItem.ToString());

            //resetTree
            darts.Nodes.Clear();
            _scoreNode = darts.Nodes.Add(Score.ToString());
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
