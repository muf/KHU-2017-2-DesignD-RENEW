using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Club : User
    {
        public int clubId { get; } = -1; // unique club id
        public String name { get; } = "";
        public String contactNumber { get; } = "";
        public int birth { get; } = -1;
        public List<Player> players { get; }
        public List<Stadium> stadiums { get; }

        public Club(int uid, String email, String password, bool authenticated, int clubId, String name, String contactNumber, int birth,  List<Player> players, List<Stadium> stadiums) : base(uid, email, password, authenticated)
        {
            this.clubId = clubId;
            this.name = name;
            this.contactNumber = contactNumber;
            this.birth = birth;
            this.players = players;
            this.stadiums = stadiums;
        }
    }
}
