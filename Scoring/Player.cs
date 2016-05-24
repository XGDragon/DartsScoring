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

        public enum Difficulty { None, Singles, Doubles };
        public enum DartReturn { OK, Next, Dead, Win }

        public Stack<PlayerHistory> History { get; private set; }
        
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
                foreach (TreeNode tn in _dartNode)
                    tn.ForeColor = SystemColors.ControlText;
                _totalNode.ForeColor = SystemColors.ControlText;
            }
            else
            {
                BackColor = SystemColors.Control;
                foreach (TreeNode tn in _dartNode)
                    tn.ForeColor = SystemColors.GrayText;
                _totalNode.ForeColor = SystemColors.GrayText;
            }
        }

        public void ShowWin(bool show = true)
        {
            winnerText.Visible = show;
        }

        public void UpdateScoreTree()
        {
            PlayerHistory ph = History.Peek();
            _scoreNode.Text = ph.Score.ToString();

            int t = 0;
            for (int i = 0; i < _dartNode.Length; i++)
            {
                PlayerHistory h = (i < ph.Dart) ? ph.HistoryGroup[i] : ph;
                _dartNode[i].Text = (i <= ph.Dart) ? h.DartInfo : string.Empty;
                t += (i <= ph.Dart) ? h.DartScore : 0;
            }
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
            History = new Stack<PlayerHistory>();
            History.Push(new PlayerHistory(this));

            //resetTree
            darts.Nodes.Clear();
            _scoreNode = darts.Nodes.Add(StartingScore.ToString());
            for (int i = 0; i < 3; i++)
                _dartNode[i] = _scoreNode.Nodes.Add(string.Empty);
            _totalNode = _scoreNode.Nodes.Add(string.Empty);
            _totalNode.NodeFont = new Font(darts.Font, FontStyle.Bold);

            _scoreNode.ExpandAll();
        }

        public Difficulty GetDifficulty()
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
