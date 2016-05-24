using System;
using System.Collections.Generic;
using DartReturn = Scoring.Player.DartReturn;
using Difficulty = Scoring.Player.Difficulty;

namespace Scoring
{
    public class PlayerHistory
    {
        public int Score { get; private set; }
        public int Dart { get; private set; }
        public DartReturn Response { get; private set; }

        public DartReturn PreviousResponse { get { return _prev.Response; } }
        public string DartInfo { get { return _dart.Info; } }
        public int DartScore { get { return _dart.TotalScore; } }
        public PlayerHistory[] HistoryGroup { get { return _historyGroup.ToArray(); } }

        private List<PlayerHistory> _historyGroup;
        private Dart _dart;
        private int _turn = 0;
        private Player _player;
        private PlayerHistory _prev;

        public PlayerHistory(Player p)
        {
            Score = p.StartingScore;
            Dart = -1;
            Response = DartReturn.Dead;
            _dart = new Scoring.Dart(new System.Windows.Forms.GroupBox(), 0, Scoring.Dart.ScoreType.Other);
        }

        public PlayerHistory(Player p, PlayerHistory prev, Dart dart)
        {
            _player = p;
            _dart = dart;
            _prev = prev;
            bool dn = (_prev.Response == DartReturn.Dead || _prev.Response == DartReturn.Next);

            Dart = (dn) ? 0 : _prev.Dart + 1;
            Response = DartResponse();

            _turn = _prev._turn;
            if (dn)
                _turn++;
            _historyGroup = GetHistoryGroup();   
                     
            if (Response == DartReturn.Dead)
            {
                PlayerHistory ph = _prev;
                while (_turn == ph._turn)
                    ph = ph._prev;
                Score = ph.Score;
            }
            else Score = _prev.Score - _dart.TotalScore;
        }

        private List<PlayerHistory> GetHistoryGroup()
        {
            List<PlayerHistory> phs = new List<PlayerHistory>();
            PlayerHistory ph = _prev;
            while (_turn == ph._turn)
            {
                phs.Add(ph);
                ph = ph._prev;
            }
            phs.Reverse();
            return phs;
        }

        private DartReturn DartResponse()
        {
            int newScore = _prev.Score - _dart.TotalScore;

            Difficulty dif = _player.GetDifficulty();
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
