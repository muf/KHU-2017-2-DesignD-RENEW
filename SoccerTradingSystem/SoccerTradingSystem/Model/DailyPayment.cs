using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class DailyPayment : Payment
    {
        public int dailyPaymentId { get; }
        public String time { get; }

        public DailyPayment(int paymentId, String paymentType, int dailyPaymentId, String time) : base(paymentId, paymentType)
        {
            this.dailyPaymentId = dailyPaymentId;
            this.time = time;
        }
    }
}
