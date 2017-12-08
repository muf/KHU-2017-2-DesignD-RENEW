using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class WeeklyPayment : Payment
    {
        public int weeklyPaymentId { get; }
        public String dayOfWeek { get; }

        public WeeklyPayment(int paymentId, String paymentType, int weeklyPaymentId, String dayOfWeek) : base(paymentId, paymentType)
        {
            this.weeklyPaymentId = weeklyPaymentId;
            this.dayOfWeek = dayOfWeek;
        }
    }
}
