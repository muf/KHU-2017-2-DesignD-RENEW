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
        public int yearlyPay { get; } = 0; // ..? 정체 불명인데 이거 좀

        public Payment(int paymentId, String paymentType)
        {
            this.paymentId = paymentId;
            this.paymentType = paymentType;
        }
       // virtual public int getPay();
       // @@ c# 가상함수 어떻게 ..?
    }
}
