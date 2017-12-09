using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SoccerTradingSystem.Controller
{ 
    using User = SoccerTradingSystem.Model.User;
    using Client = SoccerTradingSystem.Model.Client;
    using Player = SoccerTradingSystem.Model.Player;
    using Club = SoccerTradingSystem.Model.Club;
    using Manager = SoccerTradingSystem.Model.Manager;
    using LocalData = SoccerTradingSystem.Model.LocalData;
    using SystemAccountDAC = SoccerTradingSystem.Controller.DAC.SystemAccountDAC;

    class SystemAccountHandler : RetrieveHandler
    {
        private SystemAccountDAC sad = new SystemAccountDAC();
        //public LocalData login(String email, String password)
        //{
        //    queryResult = saDAC.authenticate(email, password);  // 인증 정보로 인증 쿼리 전송 후 결과 저장
        //    if (queryResult.Count == 1)
        //    {
        //        String authenticated = queryResult[0]["authenticated"].ToString();
        //        String userType = queryResult[0]["type"].ToString();
        //        LocalData cookie = new LocalData();
        //        cookie.uid = Convert.ToInt32(queryResult[0]["uid"]);
        //        cookie.email = email;
        //        cookie.authenticated = authenticated == "True" ? true : false;
        //        cookie.userType = userType;
        //        cookie.type = cookie.userType == "Client" ? retrieveHandler.getClientType(cookie.uid) : cookie.userType;
        //        return cookie; // 정상적인 경우 cookie 셋팅 후 전달
        //    }
        //    else
        //        return null;
        //}

        public bool registerClubAccount(Club club)
        {
            sad.addClubData(club);
            return true;
        }
        public bool registerPlayerAccount(Player player)
        {
            SystemAccountDAC sad = new SystemAccountDAC();
            sad.addPlayerData(player);
            return true;
        }
        public bool registerManagerAccount(Manager manager)
        {
            SystemAccountDAC sad = new SystemAccountDAC();
            sad.addManagerData(manager);
            return true;
        }
    }
}
