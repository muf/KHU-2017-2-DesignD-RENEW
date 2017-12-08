using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class BankAccountAuth
    {
        public int secretKey { get; } = 0;
        public BankAccountAuth()
        {
            this.secretKey = makeSecretKey();
        }
        public BankAccountAuth(int key)
        {
            this.secretKey = key;
        }
        public int makeSecretKey()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            int key = rnd.Next(1, int.MaxValue);
            return key;
        }
    }
}
