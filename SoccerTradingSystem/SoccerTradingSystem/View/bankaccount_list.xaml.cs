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
using System.Data;

using BankAccountDepositWindow = SoccerTradingSystem.View.depositInputWindow;
using BankAccountWithdrawWindow = SoccerTradingSystem.View.withdrawinputWindow;
using BankAccountWindow = SoccerTradingSystem.View.reg_bankAccount;
using BankAccount = SoccerTradingSystem.Model.BankAccount;
using Player = SoccerTradingSystem.Model.Player;
using Club = SoccerTradingSystem.Model.Club;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using BankAccountHandler = SoccerTradingSystem.Controller.BankAccountHandler;
using Types = SoccerTradingSystem.Model.Types;


namespace SoccerTradingSystem.View
{
    /// <summary>
    /// bankaccount_list.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class bankaccount_list : Page
    {
        public bankaccount_list()
        {
            InitializeComponent();
        }

        private void OnPageLoad(object sender, RoutedEventArgs e)
        {
            AccountsDataGridSetting("");
        }

        // 계좌 그리드 구성
        public void AccountsDataGridSetting(string context)
        {
            //SystemAccountHandler sah = new SystemAccountHandler();
            BankAccountHandler bh = new BankAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();
            JSON filter = new JSON();
            int clientid = 0;
            if(App.cookie.type == Types.UserType.Player)
            {
                clientid = ((Player)App.cookie.user).clientId;
            }
            if (App.cookie.type == Types.UserType.Club)
            {
                clientid = ((Club)App.cookie.user).clientId;
            }
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("clientId", clientid);
            List<BankAccount> banks = rh.retrieveBankAccount(filter);

            // DataTable 생성
            DataTable dataTable = new DataTable();

            // 컬럼 생성
            dataTable.Columns.Add("accountid", typeof(string));
            dataTable.Columns.Add("name", typeof(string));
            dataTable.Columns.Add("contry", typeof(string));
            dataTable.Columns.Add("balance", typeof(string));

            // 데이터 생성
            for (int i = 0; i < banks.Count; i++)
            {
                string accountid = Convert.ToString(banks[i].accountId);
                string name = Convert.ToString(banks[i].bankName);
                string contry = banks[i].country;
                string balance = Convert.ToString(banks[i].balance);
                dataTable.Rows.Add(new object[] { accountid, name, contry, balance });
            }

            // DataTable의 Default View를 바인딩하기
            bankDataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void newAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            BankAccountWindow _BankAccountWindow = new BankAccountWindow(this);
            _BankAccountWindow.Show();
        }

        private void depositBtn_Click(object sender, RoutedEventArgs e)
        {
            BankAccountDepositWindow _DepositWindow = new BankAccountDepositWindow(this);
            _DepositWindow.Show();
        }

        private void withdrawBtn_Click(object sender, RoutedEventArgs e)
        {
            BankAccountWithdrawWindow _withdrawWindow = new BankAccountWithdrawWindow(this);
            _withdrawWindow.Show();
        }

        private void delAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            BankAccountHandler bh = new BankAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();

            var row = bankDataGrid.SelectedItems[0] as DataRowView;
            string accountid = row.Row.ItemArray[0].ToString();

            JSON filter = new JSON();
            int _accountid = Convert.ToInt32(accountid);
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("accountId", _accountid);
            List<BankAccount> banks = rh.retrieveBankAccount(filter);
            BankAccount selectedBank = banks[0];
            bool success = bh.unregisterBankAccount(selectedBank);

            if (success)
            {
                MessageBox.Show("계좌 해지에 성공했습니다.");
                AccountsDataGridSetting("");
            }
            else
            {
                MessageBox.Show("계좌 해지에 실패했습니다.");
            }
        }
    }
}
