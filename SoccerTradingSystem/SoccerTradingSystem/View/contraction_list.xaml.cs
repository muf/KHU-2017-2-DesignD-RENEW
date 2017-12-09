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

using Contract = SoccerTradingSystem.Model.Contract;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// contraction_list.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class contraction_list : Page
    {
        public contraction_list()
        {
            InitializeComponent();
        }

        private void OnPageLoad(object sender, RoutedEventArgs e)
        {
            ContractionsDataGridSetting("");
        }

        private void Contraction_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)contractionDataGrid.SelectedItems[0];
            int contractionId = Convert.ToInt32((row[0]));

            ContractionDetailWindow _ContractionDetailWindow = new ContractionDetailWindow(contractionId);
            _ContractionDetailWindow.Show();
        }

        // 계약 그리드 구성
        public void ContractionsDataGridSetting(string context)
        {
            //SystemAccountHandler sah = new SystemAccountHandler();
            //List<Contract> list = sah.retrieveContractData(App.cookie.uid, context);

            //// DataTable 생성
            //DataTable dataTable = new DataTable();

            //dataTable.Columns.Add("contractionid", typeof(string));
            //dataTable.Columns.Add("cid", typeof(string));
            //dataTable.Columns.Add("pid", typeof(string));
            //dataTable.Columns.Add("trade_type", typeof(string));
            //dataTable.Columns.Add("contract_type", typeof(string));
            //dataTable.Columns.Add("start_date", typeof(string));
            //dataTable.Columns.Add("end_date", typeof(string));
            //dataTable.Columns.Add("lease", typeof(string));
            //dataTable.Columns.Add("penalty_fee", typeof(string));
            //dataTable.Columns.Add("transfer_fee", typeof(string));
            //dataTable.Columns.Add("yearly_pay", typeof(string));

            //// 데이터 생성
            //for (int i = 0; i < list.Count; i++)
            //{
            //    string contractid = Convert.ToString(list[i].contractId);
            //    string cid = Convert.ToString(list[i].clubId);
            //    string pid = Convert.ToString(list[i].playerId);
            //    string trade_type = list[i].tradeType;
            //    string contract_type = list[i].contractType;
            //    string start_date = list[i].startDate;
            //    string end_date = list[i].endDate;
            //    string lease = (list[i].leasePossibility) ? "TRUE" : "FALSE";
            //    string penalty_fee = Convert.ToString(list[i].penaltyFee);
            //    string transfer_fee = Convert.ToString(list[i].transferFee);
            //    string yearly_pay = Convert.ToString(list[i].yearlyPay);

            //    dataTable.Rows.Add(new string[] { contractid, cid, pid, trade_type, contract_type, start_date, end_date, lease, penalty_fee, transfer_fee, yearly_pay });
            //}

            //// DataTable의 Default View를 바인딩하기
            //contractionDataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void contractionSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            ContractionsDataGridSetting(contractionSearchBox.Text);
        }
    }
}
