using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Player : User
    {
        public int playerId { get; } = -1; // unique player id
        public String firstName { get; } = "";
        public String middleName { get; } = "";
        public String lastName { get; } = "";
        public int birth { get; } = -1;
        public String position { get; } = "";
        public int recentRate { get; } = -1;
        public int weight { get; } = -1;
        public int height { get; } = -1;
        public String status { get; } = "";
        public List<Club> clubs { get; }

        public Player(int uid, String email, String password, bool authenticated, int playerId, String firstName, String middleName, String lastName,
            int birth, String position, int recentRate, int weight, int height, String status, List<Club> club) : base(uid, email, password, authenticated)
        {
            this.playerId = playerId;
            this.firstName = firstName;
            this.middleName = middleName;
            this.lastName = lastName;
            this.birth = birth;
            this.position = position;
            this.recentRate = recentRate;
            this.height = height;
            this.weight = weight;
            this.status = status;
            this.clubs = clubs;
        }
    }
}
