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
using Club = SoccerTradingSystem.Model.Club;
using Player = SoccerTradingSystem.Model.Player;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// contraction_list.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class contraction_list : Page
    {
        bool isPublic;
        public contraction_list(bool _isPublic)
        {
            InitializeComponent();
            isPublic = _isPublic;
            if (isPublic)
                Title.Text = "공개 계약 조회";
            else
                Title.Text = "내 계약 조회";
        }

        private void OnPageLoad(object sender, RoutedEventArgs e)
        {
            ContractionsDataGridSetting("");
        }

        private void Contraction_Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataRowView row = (DataRowView)contractionDataGrid.SelectedItems[0];
            int contractionId = Convert.ToInt32((row[0]));
            ContractionDetailWindow _ContractionDetailWindow = new ContractionDetailWindow(contractionId, isPublic, this);
            _ContractionDetailWindow.Show();
        }

        // 계약 그리드 구성
        public void ContractionsDataGridSetting(string context)
        {
            RetrieveHandler rh = new RetrieveHandler();


            JSON filter = new JSON();
            if (App.cookie == null || isPublic)
            {
                filter.Add(new Dictionary<string, object>());
                filter[0].Add("isPublic", true);
            }
            else
            {
                // Current Logined uid
                JSON uidfilter = new JSON();
                uidfilter.Add(new Dictionary<string, object>());
                uidfilter[0].Add("uid", App.cookie.user.uid);

                if (App.cookie.type == "Player" && !isPublic)
                {
                    List<Player> players = rh.retrievePlayer(uidfilter);
                    filter.Add(new Dictionary<string, object>());
                    filter[0].Add("playerId", players[0].playerId);
                }
                if (App.cookie.type == "Club" && !isPublic)
                {
                    List<Club> clubs = rh.retrieveClub(uidfilter);
                    filter.Add(new Dictionary<string, object>());
                    filter[0].Add("clubId", clubs[0].clubId);
                }
            }
            List<Contract> contracts = rh.retrieveContract(filter);

            // DataTable 생성
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("contractionid", typeof(string));
            dataTable.Columns.Add("cid", typeof(string));
            dataTable.Columns.Add("pid", typeof(string));
            dataTable.Columns.Add("trade_type", typeof(string));
            dataTable.Columns.Add("contract_type", typeof(string));
            dataTable.Columns.Add("start_date", typeof(string));
            dataTable.Columns.Add("end_date", typeof(string));
            dataTable.Columns.Add("lease", typeof(string));
            dataTable.Columns.Add("penalty_fee", typeof(string));
            dataTable.Columns.Add("transfer_fee", typeof(string));
            dataTable.Columns.Add("pay", typeof(string));

            //// 데이터 생성
            for (int i = 0; i < contracts.Count; i++)
            {
                string contractid = Convert.ToString(contracts[i].contractId);
                string cid = Convert.ToString(contracts[i].club.name);
                string pid = Convert.ToString(contracts[i].player.lastName + " " + contracts[i].player.middleName + contracts[i].player.firstName);
                string trade_type = contracts[i].tradeType;
                string contract_type = contracts[i].contractType;
                string start_date = contracts[i].startDate;
                string end_date = contracts[i].endDate;
                string lease = (contracts[i].leasePossibility) ? "TRUE" : "FALSE";
                string penalty_fee = Convert.ToString(contracts[i].penaltyFee);
                string transfer_fee = Convert.ToString(contracts[i].transferFee);
                string pay = contracts[i].payment.pay.ToString();

                dataTable.Rows.Add(new string[] { contractid, cid, pid, trade_type, contract_type, start_date, end_date, lease, penalty_fee, transfer_fee, pay });
            }

            //// DataTable의 Default View를 바인딩하기
            contractionDataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void contractionSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            ContractionsDataGridSetting(contractionSearchBox.Text);
        }
    }
}
