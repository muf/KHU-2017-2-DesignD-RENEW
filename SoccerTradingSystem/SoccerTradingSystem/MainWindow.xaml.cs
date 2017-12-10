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
            //DateTime localDate = DateTime.Now;
            계약읽기();


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
            Contract contract = new Contract(0, "1", "1", 500, new Model.DailyPayment(0, "DailyPayment", 0, "33m"), 300, true, club, player, ContractType.OFFER, TradeType.BELONG, true);
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
            sah.login("huryip@naver.com", "tmxhs8282");
            return true;
        }
        bool 계좌추가()
        {
            BankAccount bankAccount = new BankAccount(0, 4, "SC", "korea",0);
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
            Manager manager = new Manager(0, "test5@naver.com", "tmxhs8282", false,0, "admin", "000");
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
            Player player = new Player(0, "test4@naver.com", "tmxhs8282", false,-1,null, 0,"jung","hyun","park",19930103,"GK",0,77,178,"free",null);
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
            Club club = new Club(0, "test2@naver.com", "tmxhs8282", false,-1,null, 0, "Seoul", "000-000", 19930831, null, null);
            // 외부에서 club 입력
            try
            {
                SystemAccountHandler sah = new SystemAccountHandler();
                sah.registerClubAccount(club);
                return true;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("@@ERR : "+e.Message.ToString());
                System.Console.WriteLine("@@ERR : " + e.Message.ToString());
                return false;
            }
        }
    }
}
