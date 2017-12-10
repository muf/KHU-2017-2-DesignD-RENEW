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
    using ContractType = SoccerTradingSystem.Model.Types.ContractType;
    using ContractDAC = SoccerTradingSystem.Controller.DAC.ContractDAC;

    class ContractHandler: RetrieveHandler
    {
        private ContractDAC cd  = new ContractDAC();

        public bool registerContract(Contract contract)
        {
            return cd.addContractData(contract);
        }
        public bool declineContract(Contract contract)
        {
            if (contract.contractType == ContractType.OFFER)
            {
                contract = new Contract(contract.contractId, contract.startDate, contract.endDate, contract.transferFee, contract.payment, contract.penaltyFee, contract.leasePossibility, contract.club, contract.player, ContractType.DECLINE, contract.tradeType, contract.isPublic);
                return cd.updateContractData(contract);
            }
            return false;
        }
        public bool unregisterContract(Contract contract)
        {

            if (contract.contractType == ContractType.OFFER)
            {
                return cd.deleteContractData(contract);
            }
            return false;
        }
        public bool destructContract(Contract contract)
        {
            // 계산 ..
            // 가능 ..
            // 지불 ..
            // 파기 ..
            if (contract.contractType == ContractType.OFFER)
            {
                contract = new Contract(contract.contractId, contract.startDate, contract.endDate, contract.transferFee, contract.payment, contract.penaltyFee, contract.leasePossibility, contract.club, contract.player, ContractType.DESTRUCT, contract.tradeType, contract.isPublic);
                return cd.updateContractData(contract);
            }
            return false;
        }
        public bool acceptContract(Contract contract)
        {
            if (contract.contractType == ContractType.OFFER) { 
                contract = new Contract(contract.contractId, contract.startDate, contract.endDate, contract.transferFee, contract.payment, contract.penaltyFee, contract.leasePossibility, contract.club, contract.player, ContractType.UNDER, contract.tradeType, contract.isPublic);
               return cd.updateContractData(contract);
             }
            return false;
        }
        public bool reviseContract(Contract contract)
        {
            if (contract.contractType == ContractType.OFFER)
            {
                return cd.updateContractData(contract);
            }
            return false;
        }
    }
}
