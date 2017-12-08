using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Goal
    {
        public int goalId { get; } = -1;
        public int gameId { get; } = -1;
        public Player player { get; }
        public List<Player> assistPlayers { get; }
        public String time { get; } = "";

        public Goal(int goalId, int gameId, Player player, List<Player> assistPlayers, String time)
        {
            this.goalId = goalId;
            this.gameId = gameId;
            this.player = player;
            this.assistPlayers = assistPlayers;
            this.time = time;
        }
    }
}
