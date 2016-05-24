using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Scoring
{
    public partial class Darts : Form
    {
        private const string END_GAME = "End Game";
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

        public void PreviousActivePlayer()
        {
            Player previous = ActivePlayers.Find((Player i) => { return i.ID == ActivePlayer.ID - 1; });
            ActivePlayer = (previous == null) ? ActivePlayers[ActivePlayers.Count - 1] : previous;
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
                        EnableGroupBoxes(false);

                        newGame.Text = NEW_GAME_START;
                        break;
                    }
                case State.InProgress:
                    {
                        playerCount.Enabled = false;
                        EnableGroupBoxes();
                         ActivePlayer = player1;
                        
                        newGame.Text = NEW_GAME;
                        break;
                    }
            }
        }

        private void EnableGroupBoxes(bool enable = true)
        {
            foreach (GroupBox gb in _groupBoxes)
                gb.Enabled = enable;
        }

        private void Board_TargetHit(object sender, EventArgs e)
        {
            Dart d = sender as Dart;

            PlayerHistory ph = new PlayerHistory(ActivePlayer, ActivePlayer.History.Peek(), d);
            ActivePlayer.History.Push(ph);
            ActivePlayer.UpdateScoreTree();

            switch (ph.Response)
            {
                case Player.DartReturn.Next:
                case Player.DartReturn.Dead:
                    NextActivePlayer();
                    break;
                case Player.DartReturn.Win:
                    ActivePlayer.ShowWin();
                    EnableGroupBoxes(false);
                    newGame.Text = END_GAME;
                    break;
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
            else//END_GAME or NEW_GAME_CHECK
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
            if (ActivePlayer == player1 && player1.History.Count <= 1)
                return;

            Player.DartReturn dr = ActivePlayer.History.Peek().Response;
            if (dr == Player.DartReturn.Dead || dr == Player.DartReturn.Next)
                PreviousActivePlayer();

            ActivePlayer.History.Pop();
            ActivePlayer.UpdateScoreTree();

            if (dr == Player.DartReturn.Win)
            {
                EnableGroupBoxes();
                ActivePlayer.ShowWin(false);
                newGame.Text = NEW_GAME;
            }
        }
    }
}
