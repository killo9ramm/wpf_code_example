using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Wpf_emmsil_test.viewClasses;

namespace Wpf_emmsil_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += Form_Loaded;
        }
        ViewCustomer customer;
        ViewVending vmachine;
        ViewCustomer vmwallet;

        private void Form_Loaded(object sender, RoutedEventArgs e)
        {
            CreateParticipants();

            BindCustomer();
            BindVmachine();
        }

        private void BindVmachine()
        {
            vending_ctrl.DataContext = vmachine;
            vending_ctrl.CancelClicked+=vending_ctrl_CancelClicked;
            vending_ctrl.BuyClicked+=vending_ctrl_BuyClicked;

            vmachineWallet_ctrl.DataContext = vmwallet;

        }

        private void BindCustomer()
        {
            customer_ctrl.DataContext = customer;
            customer_ctrl.CoinInserted+=customer_ctrl_CoinInserted;
        }

        private void vending_ctrl_BuyClicked(object sender, RoutedEventArgs e)
        {
            GoodsItem sellitem = vmachine.GoodsStorage.GetItem(a => a.Ttype == (int)sender);
            vmachine.SellTheItem(sellitem, customer);
            Refresh();

        }

        private void vending_ctrl_CancelClicked(object sender, RoutedEventArgs e)
        {
            vmachine.CancelOrder(customer);
            Refresh();
        }

        private void customer_ctrl_CoinInserted(object sender, RoutedEventArgs e)
        {
            Coin coin=customer.Wallet.GetItem(a=>a.Ttype==(int)sender);
            customer.InsertCoin(coin,vmachine);
            Refresh();
        }

        private void ListViewUpdate(object menu_items_Source)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(menu_items_Source);
            view.Refresh();
        }

        private void Refresh()
        {
            customer.Refresh();
            ListViewUpdate(customer.ItemsList);
            vmachine.Refresh();
            ListViewUpdate(vmachine.ItemsList);
            vmwallet.Refresh();
            ListViewUpdate(vmwallet.ItemsList);
        }

        private void CreateParticipants()
        {
            CreateCustomer();
            CreateVending();
        }

        private void CreateVending()
        {
            vmachine = new ViewVending("vmachine");
            vmachine.MessageEvent += (s,e) =>
            {
                MessageBox.Show(e.Message);
            };

            vmachine.GoodsStorage.AddGoods("Чай",13,10);
            vmachine.GoodsStorage.AddGoods("Кофе",18,20);
            vmachine.GoodsStorage.AddGoods("Кофе с молоком",21,20);
            vmachine.GoodsStorage.AddGoods("Сок",35,15);

            vmachine.Wallet.AddCoins(1,100);
            vmachine.Wallet.AddCoins(2, 100);
            vmachine.Wallet.AddCoins(5, 100);
            vmachine.Wallet.AddCoins(10, 100);
            
            vmachine.Refresh();

            vmwallet = new ViewCustomer(vmachine);
            vmwallet.Refresh();
        }
        private void CreateCustomer()
        {
            customer = new ViewCustomer("customer");

            customer.Wallet.AddCoins(1, 10);
            customer.Wallet.AddCoins(2, 30);
            customer.Wallet.AddCoins(5, 20);
            customer.Wallet.AddCoins(10, 15);
            
            customer.Refresh();
        }

    }
}
