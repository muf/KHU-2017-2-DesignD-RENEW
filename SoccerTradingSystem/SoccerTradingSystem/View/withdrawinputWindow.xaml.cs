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
using System.Data;

using BankAccountLIst = SoccerTradingSystem.View.bankaccount_list;
using BankAccount = SoccerTradingSystem.Model.BankAccount;
using Player = SoccerTradingSystem.Model.Player;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using BankAccountHandler = SoccerTradingSystem.Controller.BankAccountHandler;

namespace SoccerTradingSystem.View
{
    /// <summary>
    /// withdrawinputWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class withdrawinputWindow : Window
    {
        BankAccountLIst BankAccountLIstWindow;
        public withdrawinputWindow(BankAccountLIst _Page)
        {
            BankAccountLIstWindow = _Page;
            InitializeComponent();
        }

        private void withdrawBtn_Click(object sender, RoutedEventArgs e)
        {
            BankAccountHandler bh = new BankAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();

            var row = BankAccountLIstWindow.bankDataGrid.SelectedItems[0] as DataRowView;
            string accountid = row.Row.ItemArray[0].ToString();

            JSON filter = new JSON();
            int _accountid = Convert.ToInt32(accountid);
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("accountId", _accountid);
            List<BankAccount> banks = rh.retrieveBankAccount(filter);
            BankAccount selectedBank = banks[0];
            bool success = bh.withdraw(selectedBank, Convert.ToInt32(inputbox.Text));

            if (success)
            {
                MessageBox.Show("출금에 성공했습니다.");
                BankAccountLIstWindow.AccountsDataGridSetting("");
                this.Close();
            }
            else
            {
                MessageBox.Show("출금에 실패했습니다.");
                this.Close();
            }
        }
    }
}
