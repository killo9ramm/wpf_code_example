using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_emmsil_test.classes
{
    public class TradeParticipant
    {
        public Wallet Wallet {get; private set;}
        public GoodsStorage GoodsStorage { get; private set; }
        public string Name { get; private set; }

        public TradeParticipant(string _name)
        {
            Name = _name;
            Wallet = new Wallet();
            GoodsStorage = new GoodsStorage();
        }

        //sell to participant2 what sellItem, for money
        public Transaction Sell(TradeParticipant participant2,GoodsItem sellItem,List<Coin> money)
        {
            return TransactionPerform(this, participant2, money, sellItem, TransactionType.Sell);
        }

        ////buy from participant2 what _goodsItem, for money
        public Transaction Buy(TradeParticipant participant2,GoodsItem buyItem,List<Coin> money)
        {
            return TransactionPerform(this, participant2, money, buyItem, TransactionType.Buy);
        }

        private Transaction TransactionPerform(TradeParticipant participant1,
            TradeParticipant participant2,
            List<Coin> money,
            GoodsItem sellItem, TransactionType Ttype)
        {
            var transaction = Transaction.Create(this, participant2, money, sellItem, Ttype);
            if (transaction != null)
            {
                transaction.Perform();
                return transaction;
            }
            return null;
        }

    }
}
