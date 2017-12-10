using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Controller
{
    using User = SoccerTradingSystem.Model.User;
    using Client = SoccerTradingSystem.Model.Client;
    using Player = SoccerTradingSystem.Model.Player;
    using Club = SoccerTradingSystem.Model.Club;
    using Game = SoccerTradingSystem.Model.Game;
    using Manager = SoccerTradingSystem.Model.Manager;
    using JSON = List<Dictionary<string, object>>;
    using GameDAC = SoccerTradingSystem.Controller.DAC.GameDAC;

    class GameHandler: RetrieveHandler
    {
        private GameDAC gd = new GameDAC();
        public bool registerGame(Game game)
        {
            return gd.addGameData(game);
        }
        public bool unregisterGame(Game game)
        {
            return gd.deleteGameData(game);
        }
        public bool updateGame(Game game)
        {
            return gd.updateGameData(game);
        }
    }
}
