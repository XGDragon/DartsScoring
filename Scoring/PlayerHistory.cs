using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scoring
{
    public class PlayerHistory
    {
        public PlayerHistory(Player p, Board d)
        {
            ActivePlayer = p;
            DartInfo = d.GetMultiplier() + " x " + d.Score;
            Multiplier = d.GetMultiplier();
            ModifyScore = d.Score * Multiplier;
        }

        public Player ActivePlayer { get; private set; }
        public string DartInfo { get; private set; }
        public int ModifyScore { get; private set; }
        public int Multiplier { get; private set; }

        public int Dart { get; set; }
    }
}
