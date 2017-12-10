using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerTradingSystem.Model
{
    public class User
    {
        public int uid { get; } = -1;                        // unique user id number
        public String email { get; set; } = "";                 // 사용자의 계정 이메일
        public String password { get; } = "";            // 사용자의 게정 패스워드
        public bool authenticated { get; } = false;     // 가입 승인 여부 

        public User(int uid , String email, String password, bool authenticated )
        {
            this.uid = uid;
            this.email = email;
            this.password = password;
            this.authenticated = authenticated;
        }
    }
}
