using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_emmsil_test.classes
{
    public class Coin : CItem
    {
        public Coin(decimal _value) : base(_value, _value + " руб.") 
        {
            
        }
    }
    #region trash
    //abstract class CoinClass
    //{
    //    public decimal Value{get; private set;}
    //    public CoinClass(decimal _value)
    //    {
    //        Value = _value;
    //    }
    //}

    //class Coin_1:CoinClass
    //{
    //    public Coin_1():base(1)
    //    {
    //    }
    //}
    //class Coin_2 : CoinClass
    //{
    //    public Coin_2()
    //        : base(2)
    //    {
    //    }
    //}
    //class Coin_5 : CoinClass
    //{
    //    public Coin_5()
    //        : base(5)
    //    {
    //    }
    //}
    //class Coin_10 : CoinClass
    //{
    //    public Coin_10()
    //        : base(10)
    //    {
    //    }
    //}
    #endregion
}
