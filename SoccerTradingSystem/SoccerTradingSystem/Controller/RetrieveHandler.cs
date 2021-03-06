﻿using System;
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
    using Goal = SoccerTradingSystem.Model.Goal;
    using Rating = SoccerTradingSystem.Model.Rating;
    using DailyPayment = SoccerTradingSystem.Model.DailyPayment;
    using WeeklyPayment = SoccerTradingSystem.Model.WeeklyPayment;
    using MonthlyPayment = SoccerTradingSystem.Model.MonthlyPayment;
    using UserType = SoccerTradingSystem.Model.Types.UserType;
    using RetrieveDAC = SoccerTradingSystem.Controller.DAC.RetrieveDAC;
    using db = SoccerTradingSystem.Controller.DAC.MariaDBConnector;

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
                        JSON filterC = new JSON();
                        filterC.Add(new Dictionary<string, object>());
                        filterC[0].Add("uid", _uid);
                        user = retrieveClub(filterC)[0];
                        //user = new Club(_uid, data["email"].ToString(), data["password"].ToString(), auth, -1, null, 0, "", "", 0, null, null);
                    }
                    else if (clientType == UserType.Player)
                    {
                        JSON filterC = new JSON();
                        filterC.Add(new Dictionary<string, object>());
                        filterC[0].Add("uid", _uid);
                        user = retrievePlayer(filterC)[0];
                        //user = new Player(_uid, data["email"].ToString(), data["password"].ToString(), auth, -1, null, 0, "", "", "", 0, "", 0, 0, 0, "", null);
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (data["clientId"].ToString() == "")
                {
                    JSON filterC = new JSON();
                    filterC.Add(new Dictionary<string, object>());
                    filterC[0].Add("uid", _uid);
                    user = retrieveManager(filterC)[0];
                    //user = new Manager(_uid, data["email"].ToString(), data["password"].ToString(), auth, Convert.ToInt32(data["managerId"]), data["name"].ToString(), data["telNumber"].ToString());
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

                List<BankAccount> bankAccounts = retrieveBankAccount(bankFilter);
                List<Club> clubs = new List<Club>();
                if (filter != null)
                {
                    if (!filter[0].ContainsKey("meta"))
                    {
                        if (filter[0].ContainsKey("full"))
                        {
                            JSON payerFilter = new JSON();
                            payerFilter.Add(new Dictionary<string, object>());
                            payerFilter[0].Add("playerId", playerId);
                            payerFilter[0].Add("contractType", "UNDER");

                            List<Contract> contracts = retrieveContract(payerFilter);
                            for (int cidx = 0; cidx < contracts.Count; cidx++)
                            {
                                JSON clubFilter = new JSON();
                                clubFilter.Add(new Dictionary<string, object>());
                                clubFilter[0].Add("meta", true);
                                clubFilter[0].Add("clubId", contracts[cidx].club.clubId);
                                Club club = retrieveClub(clubFilter)[0];
                                clubs.Add(club);
                            }

                        }
                    }
                }
                Player player = new Player(uid, data["email"].ToString(), data["password"].ToString(), auth, clientId, bankAccounts, playerId, firstName, middleName, lastName, birth, position, 0,
                    weight, height, status, clubs);

                var flag = true;
                if (filter != null)
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
                    if (filter[0].ContainsKey("name"))
                    {
                        if ((player.lastName + " " + player.middleName + player.firstName) != (filter[0]["name"].ToString()))
                        {
                            flag = false;
                        }
                    }
                    if (filter[0].ContainsKey("clubuid"))
                    {
                        if (player.clubs.Count != 0)
                        {
                            if (player.clubs[0].uid != Convert.ToInt32(filter[0]["clubuid"]))
                            {
                                flag = false;
                            }
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

                List<Player> players = new List<Player>();
                if (filter != null)
                {
                    if (!filter[0].ContainsKey("meta"))
                    {
                        if (filter[0].ContainsKey("full"))
                        {
                            JSON payerFilter = new JSON();
                            payerFilter.Add(new Dictionary<string, object>());
                            payerFilter[0].Add("clubId", clubId);
                            payerFilter[0].Add("contractType", "UNDER");
                            List<Contract> contracts = retrieveContract(payerFilter);
                            for (int cidx = 0; cidx < contracts.Count; cidx++)
                            {
                                JSON clubFilter = new JSON();
                                clubFilter.Add(new Dictionary<string, object>());
                                clubFilter[0].Add("meta", true);
                                clubFilter[0].Add("playerId", contracts[cidx].player.playerId);
                                Player player = retrievePlayer(clubFilter)[0];
                                players.Add(player);
                            }

                        }
                    }
                }
                Club club = new Club(uid, data["email"].ToString(), data["password"].ToString(), auth, clientId, bankAccounts, clubId, name, contactNumber, birth, players, null);

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
                Manager manager = new Manager(uid, data["email"].ToString(), data["password"].ToString(), auth, managerId, data["name"].ToString(), data["telNumber"].ToString());

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
                clubFilter[0].Add("meta", true);
                clubFilter[0].Add("clubId", clubId);
                Payment payment = null;
                switch (type)
                {
                    case "DailyPayment":
                        payment = new DailyPayment(paymentId, "DailyPayment", Convert.ToInt32(data["pay"]), Convert.ToInt32(data["dailyPaymentId"]), data["time"].ToString());
                        break;
                    case "WeeklyPayment":
                        payment = new WeeklyPayment(paymentId, "WeeklyPayment", Convert.ToInt32(data["pay"]), Convert.ToInt32(data["weeklyPaymentId"]), data["dayOfWeek"].ToString());
                        break;
                    case "MonthlyPayment":
                        payment = new MonthlyPayment(paymentId, "MonthlyPayment", Convert.ToInt32(data["pay"]), Convert.ToInt32(data["monthlyPaymentId"]), Convert.ToInt32(data["day"]));
                        break;
                    default:
                        return null;
                }
                JSON playerFilter = new JSON();
                playerFilter.Add(new Dictionary<string, object>());
                playerFilter[0].Add("meta", true);
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
                        if (contract.club.clientId != Convert.ToInt32(filter[0]["clientId"]))
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
                    if (filter[0].ContainsKey("contractType"))
                    {
                        if (contract.contractType != filter[0]["contractType"].ToString())
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

            for (int i = 0; i < result.Count; i++)
            {
                String globalString = "";
                Dictionary<string, object> data = result[i];
                int gameId = Convert.ToInt32(data["gameId"]);
                string date = data["date"].ToString();
                string startTime = data["startTime"].ToString();
                string playTime = data["playTime"].ToString();
                int homeClubId = Convert.ToInt32(data["homeClubId"]);
                int awayClubId = Convert.ToInt32(data["awayClubId"]);
                int homeScore = 0;
                int awayScore = 0;

                JSON homeClubFilter = new JSON();
                homeClubFilter.Add(new Dictionary<string, object>());
                homeClubFilter[0].Add("full", true);
                homeClubFilter[0].Add("clubId", homeClubId);
                Club homeClub = retrieveClub(homeClubFilter)[0];

                JSON awayClubFilter = new JSON();
                awayClubFilter.Add(new Dictionary<string, object>());
                awayClubFilter[0].Add("full", true);
                awayClubFilter[0].Add("clubId", awayClubId);
                Club awayClub = retrieveClub(awayClubFilter)[0];


                List<Goal> goals = new List<Goal>();
                var t = new db().execute($"select* from goal where gameId ={gameId} ");
                for (int idx = 0; idx < t.Count; idx++)
                {
                    int playerId = Convert.ToInt32(t[idx]["playerId"]);
                    int goalId = Convert.ToInt32(t[idx]["goalId"]);
                    String time = t[idx]["time"].ToString();
                    JSON home_filter = new JSON();
                    home_filter.Add(new Dictionary<string, object>());
                    home_filter[0].Add("full", true);
                    home_filter[0].Add("playerId", playerId);
                    List<Player> players = retrievePlayer(home_filter);
                    Player player = players[0];
                    int clubId = player.clubs[0].clubId;
                    if (clubId == homeClubId) homeScore++;
                    if (clubId == awayClubId) awayScore++;

                    List<Player> assisters = new List<Player>();
                    var at = new db().execute($"select* from assist where goalId ={goalId} ");
                    for(int adx = 0; adx < at.Count; adx++)
                    {
                        int assistPlayerId = Convert.ToInt32(at[adx]["playerId"]);
                        JSON assistFilter = new JSON();
                        assistFilter.Add(new Dictionary<string, object>());
                        assistFilter[0].Add("full", true);
                        assistFilter[0].Add("playerId", assistPlayerId);
                        Player assistPlayer = retrievePlayer(assistFilter)[0];
                        assisters.Add(assistPlayer);
                    }
                    Goal goal = new Goal(goalId, gameId, player, assisters, time);
                    goals.Add(goal);
                }
                List<Rating> ratings = new List<Rating>();
                var rt = new db().execute($"select* from rating where gameId ={gameId} ");
                for (int idx = 0; idx < rt.Count; idx++)
                {
                    int ratingId = Convert.ToInt32(rt[idx]["ratingId"]);
                    int playerId = Convert.ToInt32(rt[idx]["playerId"]);
                    int ratingGrade = Convert.ToInt32(rt[idx]["ratingGrade"]);
                    JSON home_filter = new JSON();
                    home_filter.Add(new Dictionary<string, object>());
                    home_filter[0].Add("playerId", playerId);
                    Rating rating = new Rating(ratingId, gameId, retrievePlayer(home_filter)[0], ratingGrade);
                    ratings.Add(rating);
                }
                Game game = new Game(gameId, date, startTime, playTime, homeScore, awayScore, homeClub, awayClub, goals, ratings);
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
                BankAccount bankAccount = new BankAccount(accountId, clientId, bankName, country, balance, new Model.BankAccountAuth(key));
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