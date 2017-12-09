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
            query = $"INSERT INTO {GameTable} (`date`, `startTime`, `playTime`,`homeClubId`,`awayClubId`) VALUES ('{game.date}', '{game.startTime}', '{game.playTime}', '{((User)game.homeTeam).uid}', '{((User)game.awayTeam).uid}');  ";
            for (int idx = 0; idx < game.goals.Count; idx++)
            {
                //query += $"INSERT INTO {GoalTable} (`gameId`, `playerId`) VALUES ( "
                //         + $" (SELECT `uid` FROM {GameTable} WHERE `email` = '{manager.email}') , '{manager.name}', '{manager.telNumber}');  
            }
            //for (int idx = 0; idx < game.goals.Count; idx++)
            //{

            //}
            //query += $"INSERT INTO {ManagerTable} (`userId`, `name`, `telNumber`) VALUES ( "
            //    + $" (SELECT `uid` FROM {userTable} WHERE `email` = '{manager.email}') , '{manager.name}', '{manager.telNumber}');  ";
            queryResult = execute(query);
            return true;
        }
        public bool deleteGameData(Game game)
        {
            return true;
        }
        public bool updateGameData(Game game)
        {
            return true;
        }
    }
}
