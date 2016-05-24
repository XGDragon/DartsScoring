using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Scoring
{
    public partial class Darts : Form
    {
        private const string NEW_GAME = "New Game";
        private const string NEW_GAME_START = "Start";
        private const string NEW_GAME_CHECK = "Are you sure?";
        private const int MAX_PLAYERS = 7;

        public enum State { Configure, InProgress };
        private State _gameState;
        public State GameState { get { return _gameState; } private set { _gameState = value; GameStateChanged(this, EventArgs.Empty); } }

        private Player[] players = new Player[MAX_PLAYERS];
        private List<Player> _activePlayers = new List<Player>();
        public List<Player> ActivePlayers { get { return _activePlayers; } }
        
        public Stack<PlayerHistory> PlayerHistories { get; private set; }

        private Player _activePlayer;
        public Player ActivePlayer { get { return _activePlayer; } private set { _activePlayer = value; ActivePlayerChanged(this, EventArgs.Empty); } }

        private List<GroupBox> _groupBoxes = new List<GroupBox>();

        public static event EventHandler ActivePlayerChanged;
        public static event EventHandler GameStateChanged;

        public void NextActivePlayer()
        {
            Player next = ActivePlayers.Find((Player i) => { return i.ID == ActivePlayer.ID + 1; });
            ActivePlayer = (next == null) ? player1 : next;
        }

        public Darts()
        {
            InitializeComponent();
            initPlayers();
            setupBoard();

            Dart.TargetHit += Board_TargetHit;
            GameStateChanged += Darts_GameStateChanged;
            GameState = State.Configure;

            //newGame additional inits
            newGame.LostFocus += newGame_LostFocus;
        }

        private void Darts_GameStateChanged(object sender, EventArgs e)
        {
            switch (GameState)
            {
                case State.Configure:
                    {
                        playerCount.Enabled = true;
                        foreach (GroupBox gb in _groupBoxes)
                            gb.Enabled = false;

                        ActivePlayer = null;
                        newGame.Text = NEW_GAME_START;
                        break;
                    }
                case State.InProgress:
                    {
                        playerCount.Enabled = false;
                        foreach (GroupBox gb in _groupBoxes)
                            gb.Enabled = true;
                        ActivePlayer = player1;

                        PlayerHistories = new Stack<Scoring.PlayerHistory>();
                        PlayerHistories.Push(new Scoring.PlayerHistory(ActivePlayers, ActivePlayer));

                        newGame.Text = NEW_GAME;
                        break;
                    }
            }
        }

        private void Board_TargetHit(object sender, EventArgs e)
        {
            Dart d = sender as Dart;

            PlayerHistory ph = new Scoring.PlayerHistory(PlayerHistories.Peek(), ActivePlayer, d);
            PlayerHistories.Push(ph);
            ActivePlayer.UpdateScoreTree(ph);

            switch (ph.Response)
            {
                case Player.DartReturn.Next:
                case Player.DartReturn.Dead:
                    NextActivePlayer();
                    break;
                case Player.DartReturn.Win:
                    ActivePlayer.ShowWin();
                    GameState = State.Configure;
                    break;
            }

            if (PlayerHistories.Count == 6)
            {
                int dty = 0;
            }
        }

        private void initPlayers()
        {
            foreach (Control c in Controls)
                if (c.GetType() == typeof(Player))
                {
                    Player p = c as Player;
                    int i = p.ID;
                    players[i - 1] = p;
                    p.Text = "Player " + i;
                }
            playerCount.Value = 1;
        }

        private void setupBoard()
        {
            GroupBox[] gps = new GroupBox[3] { singles, doubles, triples };
            Dart.ScoreType[] sts = new Dart.ScoreType[3] { Dart.ScoreType.Single, Dart.ScoreType.Double, Dart.ScoreType.Triple };
            for (int g = 0; g < gps.Length; g++)
            {
                gps[g].Text = sts[g].ToString();
                for (int i = 1; i <= 20; i++)
                    new Dart(gps[g], i, sts[g]);
                _groupBoxes.Add(gps[g]);
            }

            for (int i = 0; i < 3; i++)
                new Dart(other, i * 25, Dart.ScoreType.Other);
            _groupBoxes.Add(other);
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            if (newGame.Text == NEW_GAME)
                newGame.Text = NEW_GAME_CHECK;
            else if (newGame.Text == NEW_GAME_START)
                GameState = State.InProgress;
            else
                GameState = State.Configure;
        }

        private void newGame_LostFocus(object sender, EventArgs e)
        {
            if (newGame.Text == NEW_GAME_CHECK)
                newGame.Text = NEW_GAME;
        }

        private void playerCount_ValueChanged(object sender, EventArgs e)
        {
            int v = (int)((NumericUpDown)sender).Value;
            ActivePlayers.Clear();

            for (int i = 0; i < players.Length; i++)
                if (i < v)
                {
                    players[i].Visible = true;
                    ActivePlayers.Add(players[i]);
                }
                else players[i].Visible = false;
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            if (PlayerHistories.Count > 1)
            {
                PlayerHistories.Pop();
                PlayerHistory ph = PlayerHistories.Peek();

                if (ph.Response == Player.DartReturn.OK)
                {
                    ActivePlayer = ph.Player;
                    PlayerHistory.Scores[ActivePlayer] = ph.Score;
                    ActivePlayer.UpdateScoreTree(ph);
                }
                else
                {

                }
            }
        }
    }
}
