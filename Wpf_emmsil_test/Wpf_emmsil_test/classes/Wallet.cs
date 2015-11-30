using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Wpf_emmsil_test.classes
{
    public class Wallet : ItemsStorage<Coin>
    {
        public decimal MoneyAmount
        {
            get
            {
                return List.Sum(a => a.Value);
            }
        }

        public Coin GetMaxCoinUnderCondition(Func<Coin,bool> condition)
        {
            return List.Where(a => condition(a)).Max();
        }

        public virtual void AddCoins(decimal Value,int count)
        {
            for(int i=0;i<count;i++){
                Coin coin = new Coin(Value);
                List.Add(coin);
            }
        }
    }

    //public decimal MoneyAmount { get; private set; }

    //    private List<Coin> Coins;
        
    //    public Wallet()
    //    {
    //        Coins = new List<Coin>();
    //    }

    //    public virtual Coin GetCoin(decimal _coin_value)
    //    {
    //        var coins = Coins.Where(a => a.Value == _coin_value);
    //        if (coins.Count() >= 1)
    //        {
    //            var coin = coins.First();
    //            return coin;
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }

    //    public virtual List<Coin> GetCoins(decimal _coin_value, int count)
    //    {
    //        var coins=Coins.Where(a => a.Value == _coin_value);
    //        if (coins.Count() >= count)
    //        {
    //            var coin = coins.Take(count).ToList();
    //            return coin;
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }

    //    public virtual void AddCoin(Coin _coin)
    //    {
    //        Coins.Add(_coin);
    //    }

        
}
