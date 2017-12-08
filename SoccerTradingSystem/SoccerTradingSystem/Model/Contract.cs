using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Contract
    {
        public int contractId { get; } = -1;
        public String startDate { get; }
        public String endDate { get; }
        public int transferFee { get; }
        public Payment payment { get; ; }
        public int penaltyFee { get; }
        public bool leasePossibility { get; }
        public Club club { get; }
        public Player player { get; }
        public String contractType { get; }
        public String tradeType { get; }
        public bool isPublic { get; }

        public Contract(int contractId,  String startDate, String endDate, int transferFee, Payment payment, int penaltyFee,
            bool leasePossibility, Club club, Player player, String contractType, String tradeType, bool isPublic)
        {
            this.contractId = contractId;
            this.startDate = startDate;
            this.endDate = endDate;
            this.transferFee = transferFee;
            this.payment = payment;
            this.penaltyFee = penaltyFee;
            this.leasePossibility = leasePossibility;
            this.club = club;
            this.player = player;
            this.contractType = contractType;
            this.tradeType = tradeType;
            this.isPublic = isPublic;
        }
    }
}
