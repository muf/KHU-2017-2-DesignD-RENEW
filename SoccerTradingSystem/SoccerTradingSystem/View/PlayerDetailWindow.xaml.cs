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

using Types = SoccerTradingSystem.Model.Types;
using Player = SoccerTradingSystem.Model.Player;
using RetrieveHandler = SoccerTradingSystem.Controller.RetrieveHandler;
using SystemAccountHandler = SoccerTradingSystem.Controller.SystemAccountHandler;

namespace SoccerTradingSystem.Views
{
    /// <summary>
    /// PlayerDetailWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    using JSON = List<Dictionary<string, object>>;
    public partial class PlayerDetailWindow : Window
    {
        private int curPlayerUid;

        public PlayerDetailWindow(int _curPlayerUid)
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            curPlayerUid = _curPlayerUid;

            if (App.cookie != null)
            {
                if (App.cookie.type == Types.UserType.Club)
                {
                    PlayerOfferBtn.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    PlayerOfferBtn.Visibility = System.Windows.Visibility.Hidden;
                }
            }else
            {
                PlayerOfferBtn.Visibility = System.Windows.Visibility.Hidden;
            }
        }

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
            filter[0].Add("uid", curPlayerUid);
            filter[0].Add("full", true);
            List<Player> players = rh.retrievePlayer(filter);
            rh.retrievePlayer(null);

            Player curPlayer = players[0];

            string pName = curPlayer.firstName + curPlayer.middleName + " " + curPlayer.lastName;
            string pBirth = curPlayer.birth.ToString();
            string pPosition = curPlayer.position;
            string pWeight = curPlayer.weight.ToString();
            string pHeight = curPlayer.height.ToString();
            string club = "자유";
            if (curPlayer.clubs.Count != 0)
                club = curPlayer.clubs[0].name.ToString();
            else
                club = "자유";

            nameBlock.Text = pName;
            birthBlock.Text = pBirth;
            positionBlock.Text = pPosition;
            weightBlock.Text = pWeight;
            heightBlock.Text = pHeight;
            clubBlock.Text = club;
        }

        private bool playerCheck()
        {
            RetrieveHandler rh = new RetrieveHandler();

            JSON filter = new JSON();
            filter.Add(new Dictionary<string, object>());
            filter[0].Add("uid", curPlayerUid);
            List<Player> players = rh.retrievePlayer(filter);
            if(players == null || players.Count == 0)
            {
                return false;
            }
            Player curPlayer = players[0];
            if (players[0].status == Types.PlayerState.Retired)
                return false;
            return true;
        }

        private void PlayerOfferBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!playerCheck())
            {
                MessageBox.Show("플레이어 정보 오류입니다.");
                return;
            }
            MakeContractWindow _MakeContractWindow = new MakeContractWindow(curPlayerUid);
            _MakeContractWindow.Show();
        }
    }
}
