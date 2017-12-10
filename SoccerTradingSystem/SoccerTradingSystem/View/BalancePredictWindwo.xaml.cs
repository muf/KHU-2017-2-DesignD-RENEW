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

namespace SoccerTradingSystem.View
{
    /// <summary>
    /// BalancePredictWindwo.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class BalancePredictWindwo : Window
    {
        public BalancePredictWindwo()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void predictbalance_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
