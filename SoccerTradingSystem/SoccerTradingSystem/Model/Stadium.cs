using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Stadium
    {
        public int stadiumId { get; } = -1;
        public int clubId { get; } = -1;
        public Address address { get; }

        public Stadium(int stadiumId, int clubId, Address address)
        {
            this.stadiumId = stadiumId;
            this.clubId = clubId;
            this.address = address;
        }
    }
}
