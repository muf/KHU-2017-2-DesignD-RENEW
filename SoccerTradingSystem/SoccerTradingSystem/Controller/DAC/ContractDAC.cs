using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Controller.DAC
{
    using Contract = SoccerTradingSystem.Model.Contract;
    using DailyPayment = SoccerTradingSystem.Model.DailyPayment;
    using WeeklyPayment = SoccerTradingSystem.Model.WeeklyPayment;
    using MonthlyPayment = SoccerTradingSystem.Model.MonthlyPayment;
    using ContractType = SoccerTradingSystem.Model.Types.ContractType;
    using JSON = List<Dictionary<string, object>>;
    class ContractDAC : MariaDBConnector
    {
        public bool addContractData(Contract contract)
        {
            query = $"INSERT INTO {PaymentTable} VALUES ( '0', '{contract.payment.paymentType}', {contract.payment.pay});";
            switch (contract.payment.paymentType)
            {
                case "DailyPayment":
                    query += $"INSERT INTO {DailyPaymentTable} VALUES ( '0', (SELECT MAX(paymentId) FROM Payment), '{((DailyPayment)(contract.payment)).time}');";
                    break;
                case "WeeklyPayment":
                    query += $"INSERT INTO {WeeklypaymentTable} VALUES ( '0', (SELECT MAX(paymentId) FROM Payment), '{((WeeklyPayment)(contract.payment)).dayOfWeek}');";
                    break;
                case "MonthlyPayment":
                    query += $"INSERT INTO {MonthlypaymentTable} VALUES ( '0', (SELECT MAX(paymentId) FROM Payment), '{((MonthlyPayment)(contract.payment)).day}');";
                    break;
                default:
                    return false;
            }

            query += $"INSERT INTO {contractTable} VALUES ( '0',  '{contract.club.clubId}', '{contract.player.playerId}',"
                + $" '{contract.startDate}', '{contract.endDate}', '{contract.transferFee}', (SELECT MAX(paymentId) FROM Payment), '{contract.penaltyFee}', '{contract.leasePossibility}',  '{contract.contractType}',  '{contract.tradeType}', '{contract.isPublic}'"
                + ");  ";
            queryResult = execute(query);
            return true;
        }
        public bool updateContractData(Contract contract)
        {
            deleteContractData(contract);
            addContractData(contract);
            return true;
        }
        public bool deleteContractData(Contract contract)
        {
                query = $"DELETE FROM {DailyPaymentTable} WHERE paymentId = {contract.payment.paymentId}; ";
                query += $"DELETE FROM {WeeklypaymentTable} WHERE paymentId = {contract.payment.paymentId}; ";
                query += $"DELETE FROM {MonthlypaymentTable} WHERE paymentId = {contract.payment.paymentId}; ";
                query += $"DELETE FROM {PaymentTable} WHERE paymentId = {contract.payment.paymentId}; ";
                query += $"DELETE FROM {contractTable} WHERE contractId = {contract.contractId}; ";
                queryResult = execute(query);
            return true;
        }
    }
}
