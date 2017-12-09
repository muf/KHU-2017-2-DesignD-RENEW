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

using BankAccountHandler = SoccerTradingSystem.Controller.BankAccountHandler;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using Client = SoccerTradingSystem.Model.Client;
using Player = SoccerTradingSystem.Model.Player;
using Club = SoccerTradingSystem.Model.Club;
using UserType = SoccerTradingSystem.Model.Types.UserType;
using BankAccount = SoccerTradingSystem.Model.BankAccount;

namespace SoccerTradingSystem.View
{
    /// <summary>
    /// reg_bankAccount.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class reg_bankAccount : Window
    {
        public reg_bankAccount()
        {
            InitializeComponent();
        }

        private void addBankAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            BankAccountHandler bh = new BankAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();
            BankAccount _BankAccount = new BankAccount(-1, -1, bankNameBox.Text, contryBox.Text);

            bool success = false;

            if (App.cookie.type == "Player")
            {   
                JSON filter = new JSON();
                filter.Add(new Dictionary<string, object>());
                filter[0].Add("uid", App.cookie.user.uid);
                List<Player> players = rh.retrievePlayer(filter);
                success = bh.registerBankAccount(players[0], _BankAccount);
            }
            if (App.cookie.type == "Club")
            {
                JSON filter = new JSON();
                filter.Add(new Dictionary<string, object>());
                filter[0].Add("uid", App.cookie.user.uid);
                List<Club> clubs = rh.retrieveClub(filter);
                success = bh.registerBankAccount(clubs[0], _BankAccount);
            }

            if (success)
            {
                MessageBox.Show("계좌가 성공적으로 개설 되었습니다.");
            }else
            {
                MessageBox.Show("계좌의 개설에 실패했습니다.");
            }
            this.Close();
        }
    }
}
