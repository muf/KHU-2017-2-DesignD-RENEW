using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Rating
    {
        public int ratingId { get; } = -1;
        public int gameId { get; } = -1;
        public Player player { get; }
        public int ratingGrade { get; } = 0;

        public Rating(int ratingId, int gameId, Player player, int ratingGrade)
        {
            this.ratingId = ratingId;
            this.gameId = gameId;
            this.player = player;
            this.ratingGrade = ratingGrade;
        }
    }
}

