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
    using Manager = SoccerTradingSystem.Model.Manager;
    using BankAccount = SoccerTradingSystem.Model.BankAccount;
    using JSON = List<Dictionary<string, object>>;
    class BankAccountDAC : MariaDBConnector
    {
        public bool addBankAccountData(Client client, BankAccount bankAccount)
        {
            query = $"INSERT INTO {BankAccountAuthTable} (`secretkey`) VALUES ('{bankAccount.auth.secretKey}'); ";
            query += $"INSERT INTO {BankAccountTable} (`bankAccountAuthId`, `clientid`,`bankName`,`country`) VALUES ( "
                + $" (SELECT `bankAccountAuthId` FROM {BankAccountAuthTable} WHERE `secretkey` = '{bankAccount.auth.secretKey}') , '{client.clientId}', '{bankAccount.bankName}','{bankAccount.country}');  ";
            queryResult = execute(query);
            return true;
        }
        public bool deleteBankAccountData(BankAccount bankAccount)
        {
            query = $"DELETE FROM {BankAccountAuthTable} WHERE secretKey = {bankAccount.auth.secretKey}; ";
            query += $"DELETE FROM {BankAccountTable} WHERE accountId = {bankAccount.accountId}; ";
            queryResult = execute(query);
            return true;
        }
        public bool updateBankAccountData(BankAccount bankAccount)
        {
            query = $"UPDATE {BankAccountTable} SET `bankName`  = {bankAccount.bankName}, `country` = {bankAccount.country}'  WHERE accountId = {bankAccount.accountId}; "; 
            queryResult = execute(query);
            return true;
        }
        public bool updateBalanceData(int bankAccountId, int balance)
        {
            query = $"UPDATE {BankAccountTable} SET `balance`  = {balance} WHERE accountId = {bankAccountId}; "; ;
            queryResult = execute(query);
            return true;
        }
        public JSON getBalanceData(int bankAccountId)
        {
            query = $"SELECT balance FROM {BankAccountTable} WHERE accountId = {bankAccountId}; "; ;
            queryResult = execute(query);
            return queryResult;
        }
        public bool authenticate(BankAccount bankAccount)
        {
            query = $"SELECT * FROM {BankAccountAuthTable} WHERE secretKey = {bankAccount.auth.secretKey}; "; ;
            queryResult = execute(query);
            if(queryResult.Count != 0)  return true;
            return false;
        }
    }
}
