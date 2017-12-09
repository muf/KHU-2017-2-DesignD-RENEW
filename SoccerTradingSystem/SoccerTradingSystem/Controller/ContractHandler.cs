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
    using Contract = SoccerTradingSystem.Model.Contract;
    using Manager = SoccerTradingSystem.Model.Manager;
    using JSON = List<Dictionary<string, object>>;
    using ContractDAC = SoccerTradingSystem.Controller.DAC.ContractDAC;

    class ContractHandler
    {
        private ContractDAC cd  = new ContractDAC();

        bool registerContract(Contract contract)
        {
            return true;
        }
        bool declineContract(Contract contract)
        {
            return true;
        }
        bool unregisterContract(Contract contract)
        {
            return true;
        }
        bool destructContract(Contract contract)
        {
            return true;
        }
        bool acceptContract(Contract contract)
        {
            return true;
        }
        bool reviseContract(Contract contract)
        {
            return true;
        }
    }
}
