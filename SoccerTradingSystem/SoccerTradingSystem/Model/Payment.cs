using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Payment
    {
        public int paymentId { get; } = -1;
        public String paymentType { get; } = "";
        public int pay { get; } = 0;

        public Payment(int paymentId, String paymentType, int pay)
        {
            this.paymentId = paymentId;
            this.paymentType = paymentType;
            this.pay = pay;
        }
       // virtual public int getPay();
       // @@ c# 가상함수 어떻게 ..?
    }
}
