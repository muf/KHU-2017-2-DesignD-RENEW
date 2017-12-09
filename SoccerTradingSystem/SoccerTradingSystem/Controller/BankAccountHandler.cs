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
    using Manager = SoccerTradingSystem.Model.Manager;
    using BankAccount = SoccerTradingSystem.Model.BankAccount;
    using BankAccountDAC = SoccerTradingSystem.Controller.DAC.BankAccountDAC;
    class BankAccountHandler
    {
        private BankAccountDAC bad = new BankAccountDAC();

        public bool registerBankAccount(Client client, BankAccount bankAccount)
        {
            bad.addBankAccountData(client, bankAccount);
            return true;
        }
        public bool unregisterBankAccount(BankAccount bankAccount)
        {
            bad.deleteBankAccountData(bankAccount);
            return true;
        }
        public bool updateBankAccount(BankAccount bankAccount)
        {
            bad.updateBankAccountData(bankAccount);
            return true;
        }
        public bool deposit(BankAccount bankAccount, int input)
        {
            bad.updateBalanceData(bankAccount.accountId, bankAccount.balance+input);
            return true;
        }
        public bool withdraw(BankAccount bankAccount, int output)
        {
            if (bankAccount.balance < output) return false;
            bad.updateBalanceData(bankAccount.accountId, bankAccount.balance - output);
            return true;
        }
        public int getBalance(BankAccount bankAccount)
        {
            int balance = 0;
            var result = bad.getBalanceData(bankAccount.accountId);
            balance = Convert.ToInt32(result[0]["balance"]);
            return balance;
        }
    }
}
