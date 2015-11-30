using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_emmsil_test.classes
{
    public class Customer : TradeParticipantSimple
    {
        public Customer(string _name) : base(_name)
        {
        }

        public virtual void InsertCoin(Coin coin,VendingMachine vm)
        {
            if (vm.GetCoin(coin))
            {
                Wallet.RemoveItems(a => a.Equals(coin));
            }
        }

        public virtual bool GetTheGood(GoodsItem goodItem)
        {
            GoodsStorage.AddItem(goodItem);
            return true;
        }

        public virtual bool GetCoins(List<Coin> coins)
        {
            Wallet.AddItems(coins);
            return true;    
        }
    }
}
