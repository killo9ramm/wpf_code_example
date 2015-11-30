using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf_emmsil_test.classes;

namespace Wpf_emmsil_test.usercontrols
{
    /// <summary>
    /// Interaction logic for CustomerControl.xaml
    /// </summary>
    public partial class CustomerControl : UserControl
    {
        public CustomerControl()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler CoinInserted;

        private void coin_insert_click(object sender, RoutedEventArgs e)
        {
            if (CoinInserted != null)
            {
                CoinInserted(ReturnSelectedCoin(sender), null);
            }
        }

        private int ReturnSelectedCoin(object sender)
        {
            int coin_type = (int)((Button)sender).Tag;
            return coin_type;
        }

        
    }
}
