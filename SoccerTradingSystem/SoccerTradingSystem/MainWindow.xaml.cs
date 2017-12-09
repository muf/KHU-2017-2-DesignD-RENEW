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
    using Manager = SoccerTradingSystem.Model.Manager;
    using BankAccount = SoccerTradingSystem.Model.BankAccount;
    using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;
    using BankAccountHandler = SoccerTradingSystem.Controller.BankAccountHandler;
    using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
    using RetrieveDAC = SoccerTradingSystem.Controller.DAC.RetrieveDAC;

    using JSON = List<Dictionary<string, object>>;

    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            RetrieveHandler rh = new RetrieveHandler();
            BankAccountHandler bah = new BankAccountHandler();
            int balance = bah.getBalance(new BankAccount(3, 0, "", ""));
            var result = rh.retrieveManager(null);

            //rd.getClubsData(9);
            //rd.getPlayersData();
            //클럽계정등록();
            //선수계정등록();
            //관리자계정등록();
            로그인();
        }
        bool 로그인()
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            sah.login("huryip@naver.com", "tmxhs8282");
            return true;
        }
        bool 계좌추가()
        {
            BankAccount bankAccount = new BankAccount(0, 4, "SC", "korea");
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
