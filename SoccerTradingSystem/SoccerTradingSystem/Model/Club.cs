using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Club : Client
    {
        public int clubId { get; set; } = -1; // unique club id
        public String name { get; set; } = "";
        public String contactNumber { get; set; } = "";
        public int birth { get; set; } = -1;
        public List<Player> players { get; }
        public List<Stadium> stadiums { get; }

        public Club(int uid, String email, String password, bool authenticated, int clientId, List<BankAccount> bankAccounts, int clubId, String name, String contactNumber, int birth,  List<Player> players, List<Stadium> stadiums) : base(uid, email, password, authenticated, clientId, bankAccounts)
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
