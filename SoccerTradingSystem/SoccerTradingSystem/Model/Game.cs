using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Game
    {
        public int gameId { get; } = -1;
        public String date { get; } = "";
        public String startTime { get; } = "";
        public String playTime { get; } = "";
        public int homeScore { get; } = 0;
        public int awayScore { get; } = 0;
        public Club homeTeam { get; }
        public Club awayTeam { get; }
        public List<Goal> goals { get; }
        public List<Rating> ratings { get; }

        public Game(int gameId, String date, String startTime, String playTime, int homeScore, int awayScore, Club homeTeam, Club awayTeam, List<Goal> goals, List<Rating> ratings)
        {
            this.gameId = gameId;
            this.date = date;
            this.startTime = startTime;
            this.playTime = playTime;
            this.homeScore = homeScore;
            this.awayScore = awayScore;
            this.homeTeam = homeTeam;
            this.awayTeam = awayTeam;
            this.goals = goals;
            this.ratings = ratings;
        }
    }
}
