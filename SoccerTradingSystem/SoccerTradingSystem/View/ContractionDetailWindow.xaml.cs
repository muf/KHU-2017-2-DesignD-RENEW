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

using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;
using Contract = SoccerTradingSystem.Model.Contract;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// ContractionDetailWindow.xaml에 대한 상호 작용 논리
    /// </summary>
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
            //Contract curContract = sah.retrieveContractData(contractionId, "");

            //string c = curPlayer.firstName + curPlayer.middleName + " " + curPlayer.lastName;
            //string pBirth = curPlayer.birth.ToString();
            //string pPosition = curPlayer.position;
            //string pWeight = curPlayer.weight.ToString();
            //string pHeight = curPlayer.height.ToString();

            //nameBlock.Text = pName;
            //birthBlock.Text = pBirth;
            //positionBlock.Text = pPosition;
            //weightBlock.Text = pWeight;
            //heightBlock.Text = pHeight;

            //ClubID"/>
            //PlyaerID"/>
            //Trade Type"/>
            //Contract Type"/>
            //Start Date"/>
            //End Date"/>
            //Lease Possible"/>
            //Penalty Fee"/>
            //Transfer Fee"/>
            //Yearly Pay"/>

        }

    }
}
