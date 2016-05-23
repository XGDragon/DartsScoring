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
        private const int MAX_PLAYERS = 6;

        public enum State { Configure, InProgress };
        private State _gameState;
        public State GameState { get { return _gameState; } private set { _gameState = value; GameStateChanged(this, EventArgs.Empty); } }

        private Player[] players = new Player[MAX_PLAYERS];
        private List<Player> _activePlayers = new List<Player>();
        public List<Player> ActivePlayers { get { return _activePlayers; } }
        
        public Stack<PlayerHistory> PlayerHistory { get; private set; }

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
                        break;
                    }
                case State.InProgress:
                    {
                        playerCount.Enabled = false;
                        foreach (GroupBox gb in _groupBoxes)
                            gb.Enabled = true;
                        PlayerHistory = new Stack<Scoring.PlayerHistory>();
                        PlayerHistory.Push(new Scoring.PlayerHistory(this));
                        break;
                    }
            }
        }

        private void Board_TargetHit(object sender, EventArgs e)
        {
            Dart b = sender as Dart;
            PlayerHistory prev = PlayerHistory.Peek();

            switch (ActivePlayer.DartResponse(prev, b))
            {
                case Player.DartReturn.Next:
                    NextActivePlayer();
                    PlayerHistory.Push(new Scoring.PlayerHistory(prev, b, ActivePlayer));
                    break;
                case Player.DartReturn.Dead:
                    NextActivePlayer();
                    PlayerHistory.Push(new Scoring.PlayerHistory(prev, ActivePlayer));
                    break;
                case Player.DartReturn.Win:
                    PlayerHistory.Push(new Scoring.PlayerHistory(prev, b));
                    GameState = State.Configure;
                    break;
                case Player.DartReturn.OK:
                    PlayerHistory.Push(new Scoring.PlayerHistory(prev, b));
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
                        
            new Dart(other, Dart.OtherType.Miss);
            new Dart(other, Dart.OtherType.Single_Bull);
            new Dart(other, Dart.OtherType.Bullseye);
            _groupBoxes.Add(other);
        }

        private void newGame_Click(object sender, EventArgs e)
        {
            if (newGame.Text == NEW_GAME)
                newGame.Text = NEW_GAME_CHECK;
            else if (newGame.Text == NEW_GAME_START)
            {
                //start the game
                //readonly?!
                playerCount.Enabled = false;
                GameState = State.InProgress;
                ActivePlayer = player1;

                newGame.Text = NEW_GAME;
            }
            else
            {
                //allow settings for the new game
                playerCount.Enabled = true;
                GameState = State.Configure;
                foreach (GroupBox gb in _groupBoxes)
                    gb.Enabled = true;
                ActivePlayer = null;

                newGame.Text = NEW_GAME_START;
            }
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
    }
}
