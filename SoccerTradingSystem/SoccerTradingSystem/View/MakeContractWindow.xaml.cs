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
using System.Windows.Shapes;

using Player = SoccerTradingSystem.Model.Player;
using Club = SoccerTradingSystem.Model.Club;
using Manager = SoccerTradingSystem.Model.Manager;
using User = SoccerTradingSystem.Model.User;
using Contract = SoccerTradingSystem.Model.Contract;
using Payment = SoccerTradingSystem.Model.Payment;
using MonthlyPayment = SoccerTradingSystem.Model.MonthlyPayment;
using WeeklyPayment = SoccerTradingSystem.Model.WeeklyPayment;
using DailyPayment = SoccerTradingSystem.Model.DailyPayment;
using ContractType = SoccerTradingSystem.Model.Types.ContractType;
using TradeType = SoccerTradingSystem.Model.Types.TradeType;
using DayOfWeek = SoccerTradingSystem.Model.Types.DayOfWeek;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using ContractHandler = SoccerTradingSystem.Controller.ContractHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// MakeContractWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class MakeContractWindow : Window
    {
        private int targetID;
        public MakeContractWindow(int uid)
        {
            InitializeComponent();
            targetID = uid;
            SystemAccountHandler sah = new SystemAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();
            if (App.cookie.type == "Club")
            {
                JSON clubfilter = new JSON();
                clubfilter.Add(new Dictionary<string, object>());
                clubfilter[0].Add("uid", App.cookie.user.uid);
                clubNameBox.Text = rh.retrieveClub(clubfilter)[0].name;


                JSON playerfilter = new JSON();
                playerfilter.Add(new Dictionary<string, object>());
                playerfilter[0].Add("uid", targetID);
                playerNameBox.Text = rh.retrievePlayer(playerfilter)[0].lastName + " " + rh.retrievePlayer(playerfilter)[0].firstName + rh.retrievePlayer(playerfilter)[0].middleName;
            }
            else if (App.cookie.type == "Player")
            {
                JSON clubfilter = new JSON();
                clubfilter.Add(new Dictionary<string, object>());
                clubfilter[0].Add("uid", targetID);
                clubNameBox.Text = rh.retrieveClub(clubfilter)[0].name;


                JSON playerfilter = new JSON();
                playerfilter.Add(new Dictionary<string, object>());
                playerfilter[0].Add("uid", App.cookie.user.uid);
                playerNameBox.Text = rh.retrievePlayer(playerfilter)[0].lastName + " " + rh.retrievePlayer(playerfilter)[0].firstName + rh.retrievePlayer(playerfilter)[0].middleName;
            }
            else
            {
                MessageBox.Show("비 정상적인 접근 입니다.");
                Close();
            }
        }

        private void ContractBtn_Click(object sender, RoutedEventArgs e)
        {
            int clubUid = 0, playerUid = 0;

            if (App.cookie.type == "Club")
            {
                clubUid = App.cookie.user.uid;
                playerUid = targetID;
            }
            else if (App.cookie.type == "Player")
            {
                clubUid = targetID;
                playerUid = App.cookie.user.uid;
            }
            else
            {
                MessageBox.Show("비 정상적인 접근 입니다.");
                Close();
            }

            // Validation


            // Value
            String startDate = startDateBox.Text;
            String endDate = endDateBox.Text;
            int transferFee = Convert.ToInt32(tranferFeeBox.Text);
            int pay = Convert.ToInt32(yearlyPayBox.Text);
            int penaltyFee = Convert.ToInt32(penaltyBox.Text);
            int day = Convert.ToInt32(dayBox.Text);
            String time = Convert.ToString(dayBox.Text);
            Payment payment = null;
            switch (paymentComboBox.SelectedValue.ToString())
            {
                case "System.Windows.Controls.ComboBoxItem: 월급":
                    payment = new MonthlyPayment(-1, "MonthlyPayment", pay , -1, day);
                    break;
                case "System.Windows.Controls.ComboBoxItem: 주급":
                    payment = new WeeklyPayment(-1, "WeeklyPayment", pay, -1, DayofWeekComboBox.SelectedValue.ToString());
                    break;
                case "System.Windows.Controls.ComboBoxItem: 일급":
                    payment = new DailyPayment(-1, "DailyPayment", pay, - 1, time);
                    break;
                default:
                    break;
            }
            bool leasePossibility = leaseCheckBox.IsChecked.Value;
            String contractType = "";
            switch (contractionTypeComboBox.SelectedItem.ToString())
            {
                case "System.Windows.Controls.ComboBoxItem: 제안":
                    contractType = ContractType.OFFER;
                    break;
                case "System.Windows.Controls.ComboBoxItem: 만료":
                    contractType = ContractType.EXPIRE;
                    break;
                case "System.Windows.Controls.ComboBoxItem: 파기":
                    contractType = ContractType.DESTRUCT;
                    break;
                case "System.Windows.Controls.ComboBoxItem: 거절":
                    contractType = ContractType.DECLINE;
                    break;
                case "System.Windows.Controls.ComboBoxItem: 보류 중":
                    contractType = ContractType.UNDER;
                    break;
                default:
                    break;
            }
            String tradeType = "";
            switch (tradeTypeComboBox.SelectedItem.ToString())
            {
                case "System.Windows.Controls.ComboBoxItem: 일반 계약":
                    tradeType = TradeType.BELONG;
                    break;
                case "System.Windows.Controls.ComboBoxItem: 리스 계약":
                    tradeType = TradeType.LEASE;
                    break;
                default:
                    break;
            }
            bool isPublic = isPublicCheckBox.IsChecked.Value;

            JSON clubfilter = new JSON();
            clubfilter.Add(new Dictionary<string, object>());
            clubfilter[0].Add("uid", clubUid);


            JSON playerfilter = new JSON();
            playerfilter.Add(new Dictionary<string, object>());
            playerfilter[0].Add("uid", playerUid);

            RetrieveHandler rh = new RetrieveHandler();
            ContractHandler ch = new ContractHandler();

            Contract _Contract = new Contract(-1, startDate, endDate, transferFee, payment, penaltyFee, leasePossibility,
                rh.retrieveClub(clubfilter)[0], rh.retrievePlayer(playerfilter)[0], contractType, tradeType, isPublic);

            if (ch.registerContract(_Contract))
            {
                MessageBox.Show("계약서를 성공적으로 전달했습니다.");
                Close();
            }
            else
            {
                MessageBox.Show("계약서를 전달하는데 실패했습니다.");
                Close();
            }
        }
    }
}
