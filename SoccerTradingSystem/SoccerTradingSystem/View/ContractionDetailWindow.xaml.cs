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

using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;
using ContractHandler = SoccerTradingSystem.Controller.ContractHandler;
using Contract = SoccerTradingSystem.Model.Contract;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// ContractionDetailWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
    using JSON = List<Dictionary<string, object>>;
    public partial class ContractionDetailWindow : Window
    {
        private int contractionId;
        public ContractionDetailWindow(int _contractionId)
        {
            InitializeComponent();
            contractionId = _contractionId;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            SystemAccountHandler sah = new SystemAccountHandler();
            RetrieveHandler rh = new RetrieveHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("contractId", contractionId);
            List<Contract> contracts = rh.retrieveContract(filter);

            Contract curContract = contracts[0];

            clubBlock.Text = Convert.ToString(curContract.club.clubId);
            playerBlock.Text = Convert.ToString(curContract.player.playerId);
            tradeTypeBlock.Text = Convert.ToString(curContract.tradeType);
            contractTypeBlock.Text = Convert.ToString(curContract.contractType);
            startDateBlock.Text = Convert.ToString(curContract.startDate);
            endDataBlock.Text = Convert.ToString(curContract.endDate);
            leaseBlock.Text = Convert.ToString(curContract.leasePossibility);
            penaltyFeeBlock.Text = Convert.ToString(curContract.penaltyFee);
            TransferFeeBlock.Text = Convert.ToString(curContract.transferFee);
        }

        private void cancleContractBtn_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("정말 파기하시겠습니까?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                ContractHandler ch = new ContractHandler();
                RetrieveHandler rh = new RetrieveHandler();

                JSON filter = new JSON();
                filter.Add(new Dictionary<string, object>());
                filter[0].Add("contractId", contractionId);
                List<Contract> contracts = rh.retrieveContract(filter);

                Contract curContract = contracts[0];
                if (ch.destructContract(curContract))
                {
                    MessageBox.Show("계약의 파기가 성공적으로 완료되었습니다.");
                    this.Close();
                }else
                {
                    MessageBox.Show("계약의 파기에 실패했습니다.");
                    this.Close();
                }
            }
        }
    }
}
