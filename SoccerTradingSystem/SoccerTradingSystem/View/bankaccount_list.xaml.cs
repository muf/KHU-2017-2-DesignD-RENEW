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

using BankAccount = SoccerTradingSystem.Model.BankAccount;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using BankAccountHandler = SoccerTradingSystem.Controller.BankAccountHandler;

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
            int uid = App.cookie.user.uid;
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("uid", uid);
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
    }
}
