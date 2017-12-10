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
using BankAccountHandler = SoccerTradingSystem.Controller.BankAccountHandler;
using ContractHandler = SoccerTradingSystem.Controller.ContractHandler;
using Contract = SoccerTradingSystem.Model.Contract;
using Types = SoccerTradingSystem.Model.Types;
using Player = SoccerTradingSystem.Model.Player;
using Club = SoccerTradingSystem.Model.Club;
using BankAccount = SoccerTradingSystem.Model.BankAccount;

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
        bool isPublic;
        contraction_list CL;
        public ContractionDetailWindow(int _contractionId, bool _isPublic, contraction_list _CL)
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            contractionId = _contractionId;
            isPublic = _isPublic;
            CL = _CL;

            RetrieveHandler rh = new RetrieveHandler();
            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("contractId", contractionId);
            List<Contract> contracts = rh.retrieveContract(filter);
            String ContractType = contracts[0].contractType;

            contractAcceptBtn.Visibility = System.Windows.Visibility.Collapsed;
            cancleContractBtn.Visibility = System.Windows.Visibility.Collapsed;
            destructContractBtn.Visibility = System.Windows.Visibility.Collapsed;

            if (!isPublic)
            {
                if (App.cookie.type == "Club")
                {
                    if (ContractType == "OFFER")
                    {
                        cancleContractBtn.Visibility = System.Windows.Visibility.Visible;
                    }
                    if (ContractType == "UNDER")
                    {
                        destructContractBtn.Visibility = System.Windows.Visibility.Visible;
                    }
                }
                else
                {
                    if (ContractType == "OFFER")
                    {
                        contractAcceptBtn.Visibility = System.Windows.Visibility.Visible;
                        cancleContractBtn.Visibility = System.Windows.Visibility.Visible;
                    }
                    if (ContractType == "UNDER")
                    {
                        destructContractBtn.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
        }

        // Eecape
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
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
            clubBlock.Text = Convert.ToString(curContract.club.name);
            playerBlock.Text = Convert.ToString(curContract.player.lastName + " " + curContract.player.middleName + curContract.player.firstName);
            tradeTypeBlock.Text = Convert.ToString(curContract.tradeType);
            contractTypeBlock.Text = Convert.ToString(curContract.contractType);
            startDateBlock.Text = Convert.ToString(curContract.startDate);
            endDataBlock.Text = Convert.ToString(curContract.endDate);
            leaseBlock.Text = Convert.ToString(curContract.leasePossibility);
            penaltyFeeBlock.Text = Convert.ToString(curContract.penaltyFee);
            TransferFeeBlock.Text = Convert.ToString(curContract.transferFee);
            yearlyPayBlock.Text = Convert.ToString(curContract.payment.pay);
        }

        private void cancleContractBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("정말 거절하시겠습니까?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
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
                if (ch.declineContract(curContract))
                {
                    MessageBox.Show("계약이 성공적으로 거절 되었습니다.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("계약의 거절에 실패했습니다.");
                    this.Close();
                }
            }
            CL.ContractionsDataGridSetting("");
        }

        private void destructContractBtn_Click(object sender, RoutedEventArgs e)
        {
            BankAccountHandler bh = new BankAccountHandler();
            ContractHandler ch = new ContractHandler();
            RetrieveHandler rh = new RetrieveHandler();

            // BANK
            JSON bankfilter = new JSON();
            int clientid = 0;
            if (App.cookie.type == Types.UserType.Player)
            {
                clientid = ((Player)App.cookie.user).clientId;
            }
            if (App.cookie.type == Types.UserType.Club)
            {
                clientid = ((Club)App.cookie.user).clientId;
            }
            bankfilter.Add(new Dictionary<string, object>());
            bankfilter[0].Add("clientId", clientid);
            List<BankAccount> banks = rh.retrieveBankAccount(bankfilter);

            // CONTRACT
            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("contractId", contractionId);
            List<Contract> contracts = rh.retrieveContract(filter);
            Contract curContract = contracts[0];
            int penaltyFee = curContract.penaltyFee;

            if (MessageBox.Show("수수료는 " + penaltyFee.ToString() + "입니다.\n" + "정말 파기하시겠습니까?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                int balance = banks[0].balance - penaltyFee;
                if (balance < 0)
                {
                    MessageBox.Show("계약 파기에 대한 수수료가 모자릅니다. 취소합니다.");
                    this.Close();
                    return;
                }

                if (ch.destructContract(curContract))
                {
                    bh.withdraw(banks[0], penaltyFee);
                    MessageBox.Show("계약의 파기가 성공적으로 완료되었습니다.");
                    this.Close();
                    return;
                }
                else
                {
                    MessageBox.Show("계약의 파기에 실패했습니다.");
                    this.Close();
                    return;
                }
            }
            CL.ContractionsDataGridSetting("");
        }

        private void contractAcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("수락 하시겠습니까?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
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
                if (ch.acceptContract(curContract))
                {
                    MessageBox.Show("계약이 성공적으로 완료되었습니다.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("계약이 실패했습니다.");
                    this.Close();
                }
            }
            CL.ContractionsDataGridSetting("");
        }
    }
}
