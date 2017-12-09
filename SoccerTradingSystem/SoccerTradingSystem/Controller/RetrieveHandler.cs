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
    using Game = SoccerTradingSystem.Model.Game;
    using Contract = SoccerTradingSystem.Model.Contract;
    using BankAccount = SoccerTradingSystem.Model.BankAccount;
    using Manager = SoccerTradingSystem.Model.Manager;
    using UserType = SoccerTradingSystem.Model.Types.UserType ;
    using RetrieveDAC = SoccerTradingSystem.Controller.DAC.RetrieveDAC;

    using JSON = List<Dictionary<string, object>>;

    class RetrieveHandler
    {
        protected JSON queryResult;
        protected RetrieveDAC rd = new RetrieveDAC();

        public List<User> retrieveUser(JSON filter)
        {
            List<User> users = new List<User>();
            JSON result = rd.getUserData();
            // filter -> keyword
            String keyword = "";
            for (int i = 0; i < result.Count; i++)
            {
                String globalString = "";
                Dictionary<string, object> data = result[i];
                bool auth = data["authenticated"].ToString() == "True" ? true : false;
                int _uid = Convert.ToInt32(data["uid"]);

                User user;
                if (data["managerId"].ToString() == "")
                {
                    String clientType = data["clientType"].ToString();
                    user = new User(_uid, data["email"].ToString(), data["password"].ToString(), auth);

                    if (clientType == UserType.Club)
                    {
                         user = new Club(_uid, data["email"].ToString(), data["password"].ToString(), auth, 0, "", "", 0, null, null);
                    }
                    else if (clientType == UserType.Player)
                    {
                        user = new Player(_uid, data["email"].ToString(), data["password"].ToString(), auth, 0, "", "", "", 0, "", 0, 0, 0, "", null);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if(data["clientId"].ToString() == "")
                {
                     user = new Manager(_uid, data["email"].ToString(), data["password"].ToString(), auth, Convert.ToInt32(data["managerId"]), data["name"].ToString(), data["telNumber"].ToString());
                }
                else
                {
                     user = new User(_uid, data["email"].ToString(), data["password"].ToString(), auth);
                }

                var flag = true;
                if (filter != null)
                {
                    if (filter[0].ContainsKey("uid"))
                    {
                        if (user.uid != Convert.ToInt32(filter[0]["uid"]))
                        {
                            flag = false;
                        }
                    }
                }

                if (flag)
                {
                    users.Add(user);
                }
                //if (globalString.IndexOf(keyword) != -1)
                //{
                //    users.Add(user);
                //}
            }
            return users;
        }
        public List<Player> retrievePlayer(JSON filter)
        {

            List<Player> players = new List<Player>();

            JSON result = rd.getPlayersData();
            String keyword = "";

            for (int i = 0; i < result.Count; i++)
            {
                String globalString = "";
                Dictionary<string, object> data = result[i];
                bool auth = data["authenticated"].ToString() == "True" ? true : false;
                int uid = uid = Convert.ToInt32(data["uid"]);
                int weight = Convert.ToInt32(data["weight"]);
                int height = Convert.ToInt32(data["height"]);
                int birth = Convert.ToInt32(data["birth"]);
                int playerId = Convert.ToInt32(data["playerId"]);
                int clientId = Convert.ToInt32(data["clientId"]);
                String firstName = data["firstName"].ToString();
                String middleName = data["middleName"].ToString();
                String lastName = data["lastName"].ToString();
                String position = data["position"].ToString();
                String status = data["status"].ToString();


                JSON bankFilter = new JSON();
                bankFilter.Add(new Dictionary<string, object>());
                //bankFilter[0].Add("accountId", accountId);

                //retrieveBankAccount()

                Player player = new Player(uid, data["email"].ToString(), data["password"].ToString(), auth,playerId, firstName, middleName, lastName, birth, position, 0,
                    weight, height, status, null);
                //queryResult = 
                globalString += player.email + player.uid + +player.birth + player.firstName + player.middleName + player.lastName + player.position + player.status + player.playerId;
                globalString.Replace(" ", "");
                if (globalString.IndexOf(keyword) != -1)
                {
                    players.Add(player);
                }
            }
            return players;
        }
        public List<Club> retrieveClub(JSON filter)
        {

            return null;
        }
        public List<Manager> retrieveManager(JSON filter)
        {

            return null;
        }
        public List<Contract> retrieveContract(JSON filter)
        {

            return null;
        }
        public List<Game> retrieveGame(JSON filter)
        {

            return null;
        }
        public List<BankAccount> retrieveBankAccount(JSON filter)
        {
            List<BankAccount> bankAccounts = new List<BankAccount>();

            JSON result = rd.getBankAccountData();
            String keyword = "";

            for (int i = 0; i < result.Count; i++)
            {
                String globalString = "";
                Dictionary<string, object> data = result[i];
                int accountId = Convert.ToInt32(data["accountId"]);
                int bankAccountAuthId = Convert.ToInt32(data["bankAccountAuthId"]);
                int balance = Convert.ToInt32(data["balance"]);
                int clientId = Convert.ToInt32(data["clientId"]);
                String bankName = data["bankName"].ToString();
                String country = data["country"].ToString();
                int key = Convert.ToInt32(data["secretKey"]);
                BankAccount bankAccount = new BankAccount(accountId,clientId, bankName, country,balance, new Model.BankAccountAuth(key));
                var flag = true;

                if (filter != null)
                {
                    if (filter[0].ContainsKey("clientId"))
                    {
                        if (bankAccount.clientId != Convert.ToInt32(filter[0]["clientId"]))
                        {
                            flag = false;
                        }
                    }
                }
                if (flag)
                {
                    bankAccounts.Add(bankAccount);
                }
            }
            return bankAccounts;
        }
    }
}
