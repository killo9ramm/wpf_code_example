using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using Wpf_emmsil_test.classes;

namespace Wpf_emmsil_test.viewClasses
{
    class ViewCustomer : Customer
    { 
        public List<GroupItem> ItemsList {get; private set;}
        public ViewCustomer(string _name)
            : base(_name)
        {
            ItemsList = new List<GroupItem>();
        }

        public ViewCustomer(TradeParticipantSimple customer)
            : base(customer.Name)
        {
            base.Wallet = customer.Wallet;
            base.GoodsStorage = customer.GoodsStorage;
            ItemsList = new List<GroupItem>();
        }

        public void Refresh()
        {
            FillViewList(Wallet.GetItems(a => a == a, -1));
        }

        private void FillViewList(List<Coin> list)
        { 
            ItemsList.Clear();
            var b = from c in list.OrderByDescending(a => a.Value).GroupBy(a => a.Ttype)
                    where c.Count() > 0
                    select new GroupItem(c.Count(), c.FirstOrDefault().Name, c.FirstOrDefault().Ttype);

            ItemsList.AddRange(b);

        }

        //public event EventHandler StateChanged;

        //private void OnStateChanged()
        //{
        //    Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(delegate()
        //    {
        //        Refresh();
        //    }));
        //    //if (StateChanged != null)
        //    //{
        //    //    Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.ContextIdle, new Action(delegate()
        //    //                        {
        //    //                            StateChanged(this,null);
        //    //                        }));
        //    //}
        //}
       
        //public override void InsertCoin(Coin coin,VendingMachine vm)
        //{
        //    OnStateChanged();
        //    base.InsertCoin(coin, vm);
        //}

        //public override bool GetTheGood(GoodsItem goodItem)
        //{
        //    OnStateChanged();
        //    return base.GetTheGood(goodItem);
        //}

        //public override bool GetCoins(List<Coin> coins)
        //{
        //    OnStateChanged();
        //    return base.GetCoins(coins);
        //}
    }
}
