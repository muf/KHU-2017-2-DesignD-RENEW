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
