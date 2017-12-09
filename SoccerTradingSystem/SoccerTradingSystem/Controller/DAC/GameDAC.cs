using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Controller.DAC
{
    using User = SoccerTradingSystem.Model.User;
    using Client = SoccerTradingSystem.Model.Client;
    using Player = SoccerTradingSystem.Model.Player;
    using Club = SoccerTradingSystem.Model.Club;
    using Game = SoccerTradingSystem.Model.Game;
    using Manager = SoccerTradingSystem.Model.Manager;
    using JSON = List<Dictionary<string, object>>;

    class GameDAC : MariaDBConnector
    {

        public bool addGameData(Game game)
        {
            query = $"INSERT INTO {GameTable} (`date`, `startTime`, `playTime`,`homeClubId`,`awayClubId`) VALUES ('{game.date}', '{game.startTime}', '{game.playTime}', '{(game.homeTeam).clubId}', '{(game.awayTeam).clubId}');  ";
            for (int idx = 0; idx < game.goals.Count; idx++)
            {
                query += $"INSERT INTO {GoalTable} (`gameId`, `playerId`,`time`) VALUES ( "
                         + $" (SELECT MAX(gameId) FROM Game ), {game.goals[idx].player.playerId},'{game.goals[idx].time}');";
                for (int jdx = 0; jdx < game.goals[idx].assistPlayers.Count; jdx++)
                {
                    query += $"INSERT INTO {AssistTable} (`goalId`, `playerId`) VALUES ( "
                             + $" (SELECT MAX(goalId) FROM Goal), {game.goals[idx].assistPlayers[jdx].playerId});";

                }
            }
            for (int idx = 0; idx < game.ratings.Count; idx++)
            {
                query += $"INSERT INTO {RatingTable} (`gameId`, `playerId`,`ratingGrade`) VALUES ( "
                         + $" (SELECT MAX(gameId) FROM Game ), {game.ratings[idx].player.playerId}, {game.ratings[idx].ratingGrade});";
            }
            queryResult = execute(query);
            return true;
        }
        public bool deleteGameData(Game game)
        {
            query = $"DELETE FROM {GameTable} WHERE gameId = {game.gameId}; ";
            query += $"DELETE FROM {AssistTable} WHERE goalId = ( SELECT goalId FROM {GoalTable} WHERE gameId = {game.gameId}); ";
            query += $"DELETE FROM {GoalTable} WHERE gameId = {game.gameId}; ";
            query += $"DELETE FROM {RatingTable} WHERE gameId = {game.gameId}; ";
            queryResult = execute(query);
            return true;
        }
        public bool updateGameData(Game game)
        {
            deleteGameData(game);
            addGameData(game);
            return true;
        }
    }
}
