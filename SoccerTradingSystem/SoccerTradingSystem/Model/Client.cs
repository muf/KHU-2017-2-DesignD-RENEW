using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Client : User
    {
        public int clientId { get; } = -1; // unique client id
        protected List<BankAccount> bankAccounts = new List<BankAccount>();
        public Client(int uid, String email, String password, bool authenticated, int clientId, List<BankAccount> bankAccounts) : base(uid, email, password, authenticated)
        {
            this.clientId = clientId;
            this.bankAccounts = bankAccounts;
        }
    }
}
