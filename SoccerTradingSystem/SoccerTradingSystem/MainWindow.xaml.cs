using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SoccerTradingSystem
{
    using User = SoccerTradingSystem.Model.User;
    using Client = SoccerTradingSystem.Model.Client;
    using Player = SoccerTradingSystem.Model.Player;
    using Club = SoccerTradingSystem.Model.Club;
    using Goal = SoccerTradingSystem.Model.Goal;
    using Rating = SoccerTradingSystem.Model.Rating;
    using Manager = SoccerTradingSystem.Model.Manager;
    using Game = SoccerTradingSystem.Model.Game;
    using BankAccount = SoccerTradingSystem.Model.BankAccount;
    using LocalData = SoccerTradingSystem.Model.LocalData;
    using Contract = SoccerTradingSystem.Model.Contract;
    using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;
    using BankAccountHandler = SoccerTradingSystem.Controller.BankAccountHandler;
    using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
    using GameHandler = SoccerTradingSystem.Controller.GameHandler;
    using ContractType = SoccerTradingSystem.Model.Types.ContractType;
    using TradeType = SoccerTradingSystem.Model.Types.TradeType;
    using ContractHandler = SoccerTradingSystem.Controller.ContractHandler;

    using JSON = List<Dictionary<string, object>>;

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            RetrieveHandler rh = new RetrieveHandler();

            List<Game>games = rh.retrieveGame(null);

        }
        void 계약동시추가()
        {
            for (int i = 5; i < 8; i++)
            {
                GameHandler gh = new GameHandler();
                RetrieveHandler rh = new RetrieveHandler();
                ContractHandler ch = new ContractHandler();
                SystemAccountHandler sah = new SystemAccountHandler();
                JSON filter = new JSON();
                filter.Add(new Dictionary<string, object>());
                filter[0].Add("contractId", 2);
                List<Contract> contracts = rh.retrieveContract(filter);
                Contract contract = contracts[0];
                contract.club.clubId = 2;
                contract.player.playerId = i;
                ch.registerContract(contract);
            }
        }
        void 선수동시추가()
        {
            GameHandler gh = new GameHandler();
            RetrieveHandler rh = new RetrieveHandler();
            ContractHandler ch = new ContractHandler();
            SystemAccountHandler sah = new SystemAccountHandler();
            List<String> positionList = new List<String>() { "RWB", "RB", "RCB", "CB", "LCB", "LB", "LWB", "RDM", "CDM", "LDM", "CM", "RM", "RAM", "CAM", "LAM", "LM", "CF", "LW", "RS", "ST", "LS", "GK" };
            List<String> firstNameList = new List<String>() { "Rolando", "Tammy", "lbrahim", "Benik", "Sergio", "Baniel", "Ahmed", "Nathan", "Chuba", "Marc" };
            List<String> middleNameList = new List<String>() { "Toby", "Alexi", "Trent", "Garcia", "de", "Alexander", "al", "Albrighton", "Ake", "Akpom" };
            List<String> lastNameList = new List<String>() { "Park", "Pereira", "Arlauskis", "Arfield", "Valencia", "Ayew", "Alonso", "Allsop", "Nynom", "Arnold" };
            //List<String> playerList = new List<String>() { "Chelsea", "CrystalPalace", "Everton", "LiverPool" };
            for (int idx = 0; idx < 3; idx++)
            {

                JSON filter = new JSON();
                filter.Add(new Dictionary<string, object>());
                filter[0].Add("uid", 8);
                List<Player> players = rh.retrievePlayer(filter);
                Player player = players[0];


                Random rnd = new Random(DateTime.Now.Millisecond);
                int num1 = rnd.Next(0, 9);
                int num2 = rnd.Next(0, 9);
                int num3 = rnd.Next(0, 9);
                player.firstName = firstNameList[num1];
                player.middleName = middleNameList[num2];
                player.lastName = lastNameList[num3];
                int num0 = rnd.Next(0, 100);
                player.email = player.firstName + player.middleName + player.lastName + num0.ToString() + "@email.com";

                int num4 = rnd.Next(1700, 2015);
                int num5 = rnd.Next(1, 12);
                int num6 = rnd.Next(1, 30);
                int birth = num4 * 10000 + num5 * 100 + num6;
                player.birth = birth;
                player.weight = rnd.Next(70, 120);
                player.height = rnd.Next(160, 220);
                player.position = positionList[rnd.Next(0, 21)];

                sah.registerPlayerAccount(player);
            }
        }
        void 클럽동시추가()
        {

            GameHandler gh = new GameHandler();
            RetrieveHandler rh = new RetrieveHandler();
            ContractHandler ch = new ContractHandler();
            SystemAccountHandler sah = new SystemAccountHandler();
            List<String> clubList = new List<String>() { "Chelsea", "CrystalPalace", "Everton", "LiverPool" };
            for (int idx = 0; idx < clubList.Count; idx++)
            {
                JSON clubF = new JSON();
                clubF.Add(new Dictionary<string, object>());
                clubF[0].Add("uid", 3);
                List<Club> clubs = rh.retrieveClub(clubF);
                Club club = clubs[0];
                club.email = clubList[idx] + "@email.com";
                club.name = clubList[idx];

                Random rnd = new Random(DateTime.Now.Millisecond);
                int num1 = rnd.Next(10, 20);
                int num2 = rnd.Next(0, 999);
                int num3 = rnd.Next(0, 999);
                String contractNumber = "+" + num1.ToString() + "-" + num2.ToString() + "-" + num3.ToString();
                club.contactNumber = contractNumber;

                int num4 = rnd.Next(1700, 2015);
                int num5 = rnd.Next(1, 12);
                int num6 = rnd.Next(1, 30);
                int birth = num4 * 10000 + num5 * 100 + num6;
                club.birth = birth;
                sah.registerClubAccount(club);
            }
        }
        void 계약읽기()
        {
            GameHandler gh = new GameHandler();
            RetrieveHandler rh = new RetrieveHandler();
            ContractHandler ch = new ContractHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("uid", 10);

            List<Contract> contracts = ch.retrieveContract(filter);
            ch.declineContract(contracts[1]);
        }
        void 계약등록()
        {
            GameHandler gh = new GameHandler();
            RetrieveHandler rh = new RetrieveHandler();
            ContractHandler ch = new ContractHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("uid", 10);


            JSON filter2 = new JSON();
            filter2.Add(new Dictionary<string, object>());
            filter2[0].Add("uid", 16);

            Player player = rh.retrievePlayer(filter2)[0];
            Club club = rh.retrieveClub(filter)[0];
            Contract contract = new Contract(0, "1", "1", 500, new Model.DailyPayment(0, "DailyPayment", 500, 0, "33m"), 300, true, club, player, ContractType.OFFER, TradeType.BELONG, true);
            ch.registerContract(contract);
        }
        void 게임테스트()
        {
            GameHandler gh = new GameHandler();
            RetrieveHandler rh = new RetrieveHandler();
            BankAccountHandler bah = new BankAccountHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("uid", 10);

            JSON filter2 = new JSON();
            filter2.Add(new Dictionary<string, object>());
            filter2[0].Add("uid", 16);

            List<Goal> goals = new List<Goal>();
            Player player = rh.retrievePlayer(filter2)[0];
            List<Player> assisters = new List<Player>();
            assisters.Add(player);
            assisters.Add(player);
            goals.Add(new Goal(0, 0, rh.retrievePlayer(filter2)[0], assisters, "35m35s"));
            List<Rating> ratings = new List<Rating>();
            ratings.Add(new Rating(0, 0, player, 5));
            ratings.Add(new Rating(0, 0, player, 4));
            Game game = new Game(9, "2017-01-02", "15:30", "78", 3, 2, rh.retrieveClub(filter)[0], rh.retrieveClub(filter)[0], goals, ratings);
            gh.registerGame(game);
            gh.unregisterGame(game);
        }
        bool 로그인()
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            LocalData cookie = sah.login("huryip@naver.com", "123");
            return true;
        }
        bool 계좌추가()
        {
            BankAccount bankAccount = new BankAccount(0, 4, "SC", "korea", 0);
            Client client = new Client(0, "", "", false, 4, null);
            try
            {
                BankAccountHandler bah = new BankAccountHandler();
                bah.registerBankAccount(client, bankAccount); // 해당 client에 bankAccount를 추가
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("@@ERR : " + e.Message.ToString());
                System.Console.WriteLine("@@ERR : " + e.Message.ToString());
                return false;
            }
        }
        bool 관리자계정등록()
        {
            Manager manager = new Manager(0, "test5@naver.com", "tmxhs8282", false, 0, "admin", "000");
            // 외부에서 club 입력
            try
            {
                SystemAccountHandler sah = new SystemAccountHandler();
                sah.registerManagerAccount(manager);
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("@@ERR : " + e.Message.ToString());
                System.Console.WriteLine("@@ERR : " + e.Message.ToString());
                return false;
            }
        }
        bool 선수계정등록()
        {
            Player player = new Player(0, "test4@naver.com", "tmxhs8282", false, -1, null, 0, "jung", "hyun", "park", 19930103, "GK", 0, 77, 178, "free", null);
            // 외부에서 club 입력
            try
            {
                SystemAccountHandler sah = new SystemAccountHandler();
                sah.registerPlayerAccount(player);
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("@@ERR : " + e.Message.ToString());
                System.Console.WriteLine("@@ERR : " + e.Message.ToString());
                return false;
            }
        }
        bool 클럽계정등록()
        {
            Club club = new Club(0, "test2@naver.com", "tmxhs8282", false, -1, null, 0, "Seoul", "000-000", 19930831, null, null);
            // 외부에서 club 입력
            try
            {
                SystemAccountHandler sah = new SystemAccountHandler();
                sah.registerClubAccount(club);
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("@@ERR : " + e.Message.ToString());
                System.Console.WriteLine("@@ERR : " + e.Message.ToString());
                return false;
            }
        }
    }
}
