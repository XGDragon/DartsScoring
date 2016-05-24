using System;
using System.Collections.Generic;
using DartReturn = Scoring.Player.DartReturn;
using Difficulty = Scoring.Player.Difficulty;

namespace Scoring
{
    public class PlayerHistory
    {
        private PlayerHistory _previous;
        private long _turn;
        private Dart _dart;
        public static Dictionary<Player, int> Scores = new Dictionary<Player, int>();

        public Player Player { get; private set; }
        public int Dart { get; private set; }
        public string DartInfo { get; private set; }
        public int Score { get; private set; }
        public int RunningScore { get; private set; }
        public DartReturn Response { get; private set; }

        public PlayerHistory(List<Player> players, Player start)
        {
            Scores.Clear();
            foreach (Player p in players)
                Scores.Add(p, p.StartingScore);
            _turn = 0;
            Player = start;
            Score = start.StartingScore;
            Response = DartReturn.Dead;
        }
        
        public PlayerHistory(PlayerHistory prev, Player current, Dart dart)
        {
            _previous = prev;
            if (_previous.Response == DartReturn.Dead || _previous.Response == DartReturn.Next)
                _turn = _previous._turn + 1;
            else _turn = _previous._turn;
            _dart = dart;

            Player = current;
            DartInfo = dart.Info;

            RunningScore = 0;
            Dart = 0;
            foreach (PlayerHistory p in HistoryGroup())
            {
                RunningScore += p._dart.TotalScore;
                Dart++;
            }

            Response = DartResponse();
            if (Response == DartReturn.Dead)
                Scores[current] += RunningScore;
            else
                Scores[current] -= _dart.TotalScore;

            Score = Scores[current];
            RunningScore += _dart.TotalScore;
        }

        public List<PlayerHistory> HistoryGroup()
        {
            List<PlayerHistory> phs = new List<PlayerHistory>();
            PlayerHistory pp = _previous;
            while (pp != null && pp._turn == _turn)
            { 
                phs.Add(pp);
                pp = pp._previous;
            }            
            return phs;
        }

        private DartReturn DartResponse()
        {
            int newScore = Scores[Player] - _dart.TotalScore;

            Difficulty dif = Player.GetDifficulty();
            if (newScore < 0)
                return DartReturn.Dead;
            if (dif == Difficulty.Doubles)
                if (newScore == 1 || newScore == 0 && _dart.Multiplier < 2 && _dart.Score != 50) //not bullseye neither
                    return DartReturn.Dead;

            if (newScore == 0)
                return DartReturn.Win;
            if (Dart < 2)
                return DartReturn.OK;
            else return DartReturn.Next;
        }
    }
}
