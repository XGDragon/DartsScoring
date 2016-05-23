using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoring
{
    public class PlayerHistory
    {
        public Dictionary<Player, int> PlayerScore { get; private set; }
        public Dictionary<Player, List<Dart>> PlayerDarts { get; private set; }
        public Player ActivePlayer { get; private set; }
        
        /// <summary>
        /// OK, NEXT or WIN
        /// </summary>
        public PlayerHistory(PlayerHistory previous, Dart d, Player newActivePlayer = null)
        {
            ActivePlayer = (newActivePlayer == null) ? previous.ActivePlayer : newActivePlayer;

            PlayerScore = new Dictionary<Player, int>(previous.PlayerScore);
            PlayerScore[previous.ActivePlayer] -= d.TotalScore;
            PlayerDarts = new Dictionary<Player, List<Dart>>(PlayerDarts);
            PlayerDarts[previous.ActivePlayer].Add(d);
        }

        /// <summary>
        /// DEAD
        /// </summary>
        public PlayerHistory(PlayerHistory previous, Player newActivePlayer)
        {
            PlayerScore = new Dictionary<Player, int>(previous.PlayerScore);
            PlayerDarts = new Dictionary<Player, List<Dart>>(PlayerDarts);
            foreach (Dart pd in PlayerDarts[previous.ActivePlayer])
                PlayerScore[previous.ActivePlayer] += pd.TotalScore;
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public PlayerHistory(Darts d)
        {
            ActivePlayer = d.ActivePlayer;

            PlayerScore = new Dictionary<Player, int>();
            PlayerDarts = new Dictionary<Player, List<Dart>>();
            foreach (Player p in d.ActivePlayers)
            {
                PlayerScore.Add(p, p.StartingScore);
                PlayerDarts.Add(p, new List<Dart>());
            }
        }
    }
}
