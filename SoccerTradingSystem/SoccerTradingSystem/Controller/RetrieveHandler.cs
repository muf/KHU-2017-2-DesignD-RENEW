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
    using Payment = SoccerTradingSystem.Model.Payment;
    using DailyPayment = SoccerTradingSystem.Model.DailyPayment;
    using WeeklyPayment = SoccerTradingSystem.Model.WeeklyPayment;
    using MonthlyPayment = SoccerTradingSystem.Model.MonthlyPayment;
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
                         user = new Club(_uid, data["email"].ToString(), data["password"].ToString(), auth,-1,null, 0, "", "", 0, null, null);
                    }
                    else if (clientType == UserType.Player)
                    {
                        user = new Player(_uid, data["email"].ToString(), data["password"].ToString(), auth, -1,null,0, "", "", "", 0, "", 0, 0, 0, "", null);
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
                bankFilter[0].Add("clientId", clientId);

                List< BankAccount> bankAccounts = retrieveBankAccount(bankFilter);

                Player player = new Player(uid, data["email"].ToString(), data["password"].ToString(), auth, clientId, bankAccounts, playerId,firstName, middleName, lastName, birth, position, 0,
                    weight, height, status,null);

                var flag = true;
                if(filter != null)
                {
                    if (filter[0].ContainsKey("uid"))
                    {
                        if (player.uid != Convert.ToInt32(filter[0]["uid"]))
                        {
                            flag = false;
                        }
                    }
                    if (filter[0].ContainsKey("playerId"))
                    {
                        if (player.playerId != Convert.ToInt32(filter[0]["playerId"]))
                        {
                            flag = false;
                        }
                    }
                }

                if (flag)
                {
                    players.Add(player);
                }
            }
            return players;
        }
        public List<Club> retrieveClub(JSON filter)
        {
            List<Club> clubs = new List<Club>();
            JSON result = rd.getClubsData();
            for (int i = 0; i < result.Count; i++)
            {
                String globalString = "";
                Dictionary<string, object> data = result[i];
                bool auth = data["authenticated"].ToString() == "True" ? true : false;
                int uid = Convert.ToInt32(data["uid"]);
                int clientId = Convert.ToInt32(data["clientId"]);
                int clubId = Convert.ToInt32(data["clubId"]);
                int birth = Convert.ToInt32(data["birth"]);
                String name = data["name"].ToString();
                String contactNumber = data["contactNumber"].ToString();

                

                JSON bankFilter = new JSON();
                bankFilter.Add(new Dictionary<string, object>());
                bankFilter[0].Add("clientId", clientId);

                List<BankAccount> bankAccounts = retrieveBankAccount(bankFilter);

                Club club = new Club(uid, data["email"].ToString(), data["password"].ToString(), auth, clientId, bankAccounts, clubId, name, contactNumber, birth, null, null);

                var flag = true;
                if (filter != null)
                {
                    if (filter[0].ContainsKey("uid"))
                    {
                        if (club.uid != Convert.ToInt32(filter[0]["uid"]))
                        {
                            flag = false;
                        }
                    }
                    if (filter[0].ContainsKey("clubId"))
                    {
                        if (club.clubId != Convert.ToInt32(filter[0]["clubId"]))
                        {
                            flag = false;
                        }
                    }
                    if (filter[0].ContainsKey("name"))
                    {
                        if (club.name != Convert.ToString(filter[0]["name"]))
                        {
                            flag = false;
                        }
                    }
                }

                if (flag)
                {
                    clubs.Add(club);
                }
            }
            return clubs;
        }
        public List<Manager> retrieveManager(JSON filter)
        {

            List<Manager> managers = new List<Manager>();
            JSON result = rd.getManagersData();
            for (int i = 0; i < result.Count; i++)
            {
                String globalString = "";
                Dictionary<string, object> data = result[i];
                bool auth = data["authenticated"].ToString() == "True" ? true : false;
                int uid = Convert.ToInt32(data["uid"]);
                int managerId = Convert.ToInt32(data["managerId"]);
                Manager manager = new Manager(uid, data["email"].ToString(), data["password"].ToString(),auth, managerId, data["name"].ToString(), data["telNumber"].ToString());

                var flag = true;
                if (filter != null)
                {
                    if (filter[0].ContainsKey("uid"))
                    {
                        if (manager.uid != Convert.ToInt32(filter[0]["uid"]))
                        {
                            flag = false;
                        }
                    }
                }

                if (flag)
                {
                    managers.Add(manager);
                }
            }
            return managers;
        }
        public List<Contract> retrieveContract(JSON filter)
        {
            List<Contract> contracts = new List<Contract>();

            JSON result = rd.getContractData();
            String keyword = "";

            for (int i = 0; i < result.Count; i++)
            {
                String globalString = "";
                Dictionary<string, object> data = result[i];
                int contractId = Convert.ToInt32(data["contractId"]);
                int playerId = Convert.ToInt32(data["playerId"]);
                int clubId = Convert.ToInt32(data["clubId"]);
                String startDate = data["startDate"].ToString();
                String endDate = data["endDate"].ToString();
                int transferFee = Convert.ToInt32(data["transferFee"]);
                int paymentId = Convert.ToInt32(data["paymentId"]);
                int penaltyFee = Convert.ToInt32(data["penaltyFee"]);
                bool leasePossibility = data["leasePossibility"].ToString() == "True" ? true : false;
                String contractType = data["contractType"].ToString();
                String tradeType = data["tradeType"].ToString();
                bool isPublic = data["isPublic"].ToString() == "True" ? true : false;
                String type = data["type"].ToString();
                JSON clubFilter = new JSON();
                clubFilter.Add(new Dictionary<string, object>());
                clubFilter[0].Add("clubId", clubId);
                Payment payment = null;
                switch (type)
                {
                    case "DailyPayment":
                        payment = new DailyPayment(paymentId, "DailyPayment", Convert.ToInt32(data["dailyPaymentId"]), data["time"].ToString());
                        break;
                    case "WeeklyPayment":
                        payment = new WeeklyPayment(paymentId, "WeeklyPayment", Convert.ToInt32(data["weeklyPaymentId"]),data["dayOfWeek"].ToString());
                        break;
                    case "MonthlyPayment":
                        payment = new MonthlyPayment(paymentId, "MonthlyPayment", Convert.ToInt32(data["monthlyPaymentId"]), data["day"].ToString());
                        break;
                    default:
                        return null;
                }
                JSON playerFilter = new JSON();
                playerFilter.Add(new Dictionary<string, object>());
                playerFilter[0].Add("playerId", playerId);

                Contract contract = new Contract(contractId, startDate, endDate, transferFee, payment, penaltyFee, leasePossibility, retrieveClub(clubFilter)[0], retrievePlayer(playerFilter)[0], contractType, tradeType, isPublic);
                var flag = true;

                if (filter != null)
                {
                    if (filter[0].ContainsKey("contractId"))
                    {
                        if (contract.contractId != Convert.ToInt32(filter[0]["contractId"]))
                        {
                            flag = false;
                        }
                    }
                    if (filter[0].ContainsKey("clubId"))
                    {
                        if (contract.club.clubId != Convert.ToInt32(filter[0]["clubId"]))
                        {
                            flag = false;
                        }
                    }
                    if (filter[0].ContainsKey("clientId"))
                    {
                        if (contract.club.clubId != Convert.ToInt32(filter[0]["clientId"]))
                        {
                            flag = false;
                        }
                    }
                    else if (filter[0].ContainsKey("playerId"))
                    {
                        if (contract.player.playerId != Convert.ToInt32(filter[0]["playerId"]))
                        {
                            flag = false;
                        }

                    }
                    if (filter[0].ContainsKey("isPublic"))
                    {
                        if (contract.isPublic != Convert.ToBoolean(filter[0]["isPublic"]))
                        {
                            flag = false;
                        }
                    }
                }
                if (flag)
                {
                    contracts.Add(contract);
                }
            }
            return contracts;
        }
        public List<Game> retrieveGame(JSON filter)
        {
            List<Game> games = new List<Game>();

            JSON result = rd.getGameData();
            String keyword = "";

            for(int i = 0; i < result.Count; i++)
            {
                String globalString = "";
                Dictionary<string, object> data = result[i];
                int gameId = Convert.ToInt32(data["gameId"]);
                string date = data["date"].ToString();
                string startTime = data["startTime"].ToString();
                string playTime = data["playTime"].ToString();
                int homeClubId = Convert.ToInt32(data["homeClubId"]);
                int awayClubId = Convert.ToInt32(data["awayClubId"]);
                int homeScore = Convert.ToInt32(data["homeScore"]);
                int awayScore = Convert.ToInt32(data["awayScroe"]);

                Game game = null;
                //Game game = new Game(gameId, date, startTime, playTime, homeScore, awayScore, .....)
                var flag = true;

                if (filter != null)
                {
                    if (filter[0].ContainsKey("gameId"))
                    {
                        if (game.gameId != Convert.ToInt32(filter[0]["gameId"]))
                        {
                            flag = false;
                        }
                    }
                }
                if (flag)
                {
                    games.Add(game);
                }
            }
            return games;
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
                    else if (filter[0].ContainsKey("accountId"))
                    {
                        if (bankAccount.accountId != Convert.ToInt32(filter[0]["accountId"]))
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
