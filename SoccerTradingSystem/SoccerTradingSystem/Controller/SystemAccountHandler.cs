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
    using UserType = SoccerTradingSystem.Model.Types.UserType;
    using SystemAccountDAC = SoccerTradingSystem.Controller.DAC.SystemAccountDAC;
    using JSON = List<Dictionary<string, object>>;

    class SystemAccountHandler : RetrieveHandler
    {
        private SystemAccountDAC sad = new SystemAccountDAC();
        public LocalData login(String email, String password)
        {
            queryResult = sad.authenticate(email, password);  // 인증 정보로 인증 쿼리 전송 후 결과 저장
            if (queryResult.Count == 1)
            {
                int uid = Convert.ToInt32(queryResult[0]["uid"]);
                JSON filter = new JSON();
                filter.Add(new Dictionary<string, object>());
                filter[0].Add("uid", uid);
                List<User> users = retrieveUser(filter);
                LocalData cookie = new LocalData();
                if (users.Count == 1)
                {
                    cookie.user = users[0];
                    cookie.type = users[0].GetType().Name;
                    cookie.userType = cookie.type == UserType.Manager ? UserType.Manager : "Client";
                }
                return cookie; // 정상적인 경우 cookie 셋팅 후 전달
            }
            else
                return null;
        }
        public bool registerClubAccount(Club club)
        {
            sad.addClubData(club);
            return true;
        }
        public bool registerPlayerAccount(Player player)
        {
            sad.addPlayerData(player);
            return true;
        }
        public bool registerManagerAccount(Manager manager)
        {
            sad.addManagerData(manager);
            return true;
        }
        public bool updateUser(User user)
        {
            sad.updateUserData(user);
            return true;
        }
        public LocalData updateLocalUser(LocalData cookie)
        {
            login(cookie.user.email, cookie.user.password);
            return cookie;
        }
        public bool updatePlayerAccount(Player player)
        {
            sad.updatePlayerData(player);
            return true;
        }
        public bool updateClubAccount(Club club)
        {
            sad.updateClubData(club);
            return true;
        }
        public bool updateManagerAccount(Manager manager)
        {
            sad.updateManagerData(manager);
            return true;
        }
        public bool unregisterClubAccount(Club club)
        {
            sad.deleterClubData(club);
            return true;
        }
        public bool unregisterPlayerAccount(Player player)
        {
            sad.deletePlayerData(player);
            return true;
        }
        public bool unregisterManagerAccount(Manager manager)
        {
            sad.deleteManagerData(manager);
            return true;
        }
    }
}
