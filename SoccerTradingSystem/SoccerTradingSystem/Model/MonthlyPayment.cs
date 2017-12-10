using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class MonthlyPayment : Payment
    {
        public int monthlyPaymentId { get; }
        public int day { get; }

        public MonthlyPayment(int paymentId, String paymentType,int pay, int day, String dayOfWeek) : base(paymentId, paymentType,pay)
        {
            this.monthlyPaymentId = monthlyPaymentId;
            this.day = day;
        }
    }
}
