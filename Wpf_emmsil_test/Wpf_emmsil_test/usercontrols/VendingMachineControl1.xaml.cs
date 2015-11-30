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

namespace Wpf_emmsil_test.usercontrols
{
    /// <summary>
    /// Interaction logic for VendingMachineControl1.xaml
    /// </summary>
    public partial class VendingMachineControl1 : UserControl
    {
        public VendingMachineControl1()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler CancelClicked;
        public event RoutedEventHandler BuyClicked;

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            if (CancelClicked != null)
            {
                CancelClicked(this,null);
            }
        }

        private void Button_Click_Buy(object sender, RoutedEventArgs e)
        {
            if (BuyClicked != null)
            {
                BuyClicked(((Button)sender).Tag, null);
            }
        }
    }
}
