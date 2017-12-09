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

using Club = SoccerTradingSystem.Model.Club;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// ClubDetailWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ClubDetailWindow : Window
    {
        private int curClubUid;

        public ClubDetailWindow(int uid)
        {
            InitializeComponent();
            curClubUid = uid;
    }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            if (App.cookie != null)
            {
                if (App.cookie.type == "Player")
                {
                    ClubOfferBtn.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    ClubOfferBtn.Visibility = System.Windows.Visibility.Hidden;
                }
            }

            SystemAccountHandler sah = new SystemAccountHandler();
            //Club curClub = sah.retrieveClubData(curClubUid);

            //string cName = curClub.name;
            //string cBirth = curClub.birth.ToString();
            //string cContactNumber = curClub.contactNumber;

            //nameBlock.Text = cName;
            //birthBlock.Text = cBirth;
            //contactNumberBlock.Text = cContactNumber;
        }

        private void ClubOfferBtn_Click(object sender, RoutedEventArgs e)
        {
            MakeContractWindow _MakeContractWindow = new MakeContractWindow(curClubUid);
            _MakeContractWindow.Show();
        }
    }
}
