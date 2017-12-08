using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class BankAccount
    {
        public String accountId { get; } = "";      // 계좌 번호
        public int clientId { get; } = -1;         // 계좌를 소유하는 client id
        public String bankName { get;  } = "";    // 은행명
        public String country { get;  } = "";        // 국가
        public BankAccountAuth auth { get;  } = new BankAccountAuth();    // 인증키 값
        public BankAccount(String accountId, int clientId, String bankName, String country, BankAccountAuth auth)
        {
            this.accountId = accountId;
            this.clientId = clientId;
            this.bankName = bankName;
            this.country = country;
            this.auth = auth;
        }
    }
}
