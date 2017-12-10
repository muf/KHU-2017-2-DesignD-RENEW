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

using BankAccountDepositWindow = SoccerTradingSystem.View.depositInputWindow;
using BankAccountWithdrawWindow = SoccerTradingSystem.View.withdrawinputWindow;
using BankAccountWindow = SoccerTradingSystem.View.reg_bankAccount;
using BankAccount = SoccerTradingSystem.Model.BankAccount;
using Player = SoccerTradingSystem.Model.Player;
using Club = SoccerTradingSystem.Model.Club;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using BankAccountHandler = SoccerTradingSystem.Controller.BankAccountHandler;
using Types = SoccerTradingSystem.Model.Types;
using BalancePredictWindow = SoccerTradingSystem.View.BalancePredictWindwo;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.View
{
    /// <summary>
    /// BalancePredictWindwo.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class BalancePredictWindwo : Window
    {
        public BalancePredictWindwo()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void predictbalance_Click(object sender, RoutedEventArgs e)
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("uid", App.cookie.user.uid);
            List<Club> clubs = rh.retrieveClub(filter);
            string _data = predictDateBox.Text;
            int PredictedBalance = sah.predictBalance(clubs[0], _data);
            string str_PredictedBalance = PredictedBalance.ToString();
            MessageBox.Show("예상 되는 잔고는 " + str_PredictedBalance + "원 입니다.");
        }
    }
}
