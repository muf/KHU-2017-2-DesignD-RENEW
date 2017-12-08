using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    class Manager : User
    {
        public int managerId { get; } = -1; // unique manager id
        public String name { get; }
        public String telNumber { get; }
        public Manager(int uid, String email, String password, bool authenticated , int managerId, String name, String telNumber) : base(uid, email, password, authenticated)
        {
            this.managerId = managerId;
            this.name = name;
            this.telNumber = telNumber;
        }
    }
}
