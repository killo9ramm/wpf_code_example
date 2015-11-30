using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_emmsil_test.classes
{
    public enum TransactionType{Sell=0,Buy=1};
    public class Transaction
    {
        private static bool EnableTransactionLog = true;
        private static List<Transaction> SuccessfulTransactions=new List<Transaction>();
        private static List<Transaction> FailedTransactions=new List<Transaction>();
        private static void AddToTransactionLog(Transaction transaction)
        {
            if (transaction.IsSuccessful)
            { SuccessfulTransactions.Add(transaction); }
            else
            { FailedTransactions.Add(transaction); }
        }

        private static int TransactionsCount = 0;

        public static Transaction Create(TradeParticipant participant1,
            TradeParticipant participant2,
            List<Coin> money,
            GoodsItem sellItem, TransactionType Ttype)
        {
            Transaction transaction = new Transaction(
                participant1,
                participant2,
                money,
                sellItem,
                Ttype);

            return transaction;
        }

        private Transaction()
        {
        }

        public string Message { get; private set; }
        public bool IsSuccessful { get; private set; }

        private TradeParticipant participant1;
        private TradeParticipant participant2;
        private List<Coin> money;
        private GoodsItem sellItem;
        private TransactionType Ttype;

        private int TransactionID;

        private Transaction(TradeParticipant participant1,
            TradeParticipant participant2,
            List<Coin> money, 
            GoodsItem sellItem,TransactionType Ttype)
        {
            if (Ttype == TransactionType.Sell)
            {
                this.participant1 = participant1;
                this.participant2 = participant2;
            }
            else
            {
                this.participant1 = participant2;
                this.participant2 = participant1;
            }
            this.money=money;
            this.sellItem=sellItem;
            this.Ttype=Ttype;
        }

        private void setID()
        {
            TransactionsCount++;
            this.TransactionID = TransactionsCount;
        }

        public bool Perform()
        {
            setID();
            IsSuccessful = false;
            if (JustChangeRequested())
            {
                if (EnrollMoney())
                { ReturnChange(); }
                return true;
            }
            if (CheckIfEnoughMoney())
            {
                if (DoesHaveEnoughChange())
                {
                    CommitTransaction();
                    return IsSuccessful;
                }
                else
                {
                    FailedTransaction();
                    return IsSuccessful;
                }
            }
            else
            {
                FailedTransaction();
                return IsSuccessful;
            }
        }

        private bool JustChangeRequested()
        {
            return sellItem == null;
        }

        private bool DoesHaveEnoughChange()
        {
            if (sellItem.Value == money.Sum(a => a.Value))
            {
                return true;
            }
            else
            {
                List<Coin> coins = CountTheChange(sellItem.Value, participant1, money);
                if (coins == null)
                {
                    AddMessage("Sorry change amount is not enough"); 
                    return false;
                }
                return true;
            }
            
            
        }
        private bool CheckIfEnoughMoney()
        {
            decimal moneyAmount = GetMoneyAmount();
            if (moneyAmount >= sellItem.Value)
            {
                return true;
            }
            else
            {
                AddMessage("Sorry money amount is not enough");
                return false;
            }
        }

        private decimal GetMoneyAmount()
        {
            return money.Sum(a => a.Value);
        }

        private void FailedTransaction()
        {
            IsSuccessful = false;
            _AddToTransactionLog();
        }
        
        private void CommitTransaction()
        {
            if (EnrollMoney())
            {
                if (ShipTheGoods())
                {
                    IsSuccessful = true;
                }
                else
                {
                    AddMessage("The goods can't be shipped");
                    IsSuccessful = false;
                }
                if (!ReturnChange())
                {
                    AddMessage("Sorry, change cannot be return");
                    IsSuccessful = false;
                }
            }
            else
            {
                FailedTransaction();
            }
            _AddToTransactionLog();
        }

        private void _AddToTransactionLog()
        {
            if (EnableTransactionLog)
            {
                Transaction.AddToTransactionLog(this);
            }
        }

        private bool EnrollMoney()
        {
            return _EnrollMoney(participant1, participant2, money);
        }

        private bool _EnrollMoney(TradeParticipant to_participant,TradeParticipant from_participant,List<Coin> _money)
        {
            try
            {
                if (CheckWhoesMoneyIs(from_participant, _money))
                {
                    to_participant.Wallet.AddItems(_money);
                    from_participant.Wallet.RemoveItems(a => _money.Contains(a));
                    return true;
                }
                else
                {
                    AddMessage("Sorry, cannot sell this is not your money");
                    return false;
                }
            }catch(Exception ex)
            {
                AddMessage("Can't enroll money");
                AddMessage(AddExceptionInfo(ex));
                return false;
            }
        }

        private bool CheckWhoesMoneyIs(TradeParticipant from_participant, List<Coin> _money)
        {
            foreach(var coin in _money)
            {
                if (from_participant.Wallet.GetItem(a => a == coin) == null)
                {
                    return false;
                }
            }
            return true;
        }

        private bool ReturnChange()
        {
            try
            {
                var allMoney = GetMoneyAmount();
                if (!JustChangeRequested())
                { return _ReturnChange(allMoney-sellItem.Value); }
                else
                {
                    return _ReturnChange(allMoney, money);
                }
            }
            catch (Exception ex)
            {
                AddMessage("Can't return change");
                AddMessage(AddExceptionInfo(ex));
                return false;
            }
        }

        private bool _ReturnChange(decimal changeAmount,List<Coin> additionalList=null)
        {
            List<Coin> coins = CountTheChange(changeAmount,participant1,additionalList);
            if (coins != null)
            {
                _EnrollMoney(participant2, participant1, coins);
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<Coin> CountTheChange(decimal changeAmount,TradeParticipant participant,
            List<Coin> additionalList=null)
        {
            Coin _coin = null;
            List<Coin> _coins = new List<Coin> { };
            while (changeAmount > 0)
            {
                Func<Coin,bool> changeCondition=a => a.Value <= changeAmount && (changeAmount-a.Value)>=0 
                    && !_coins.Contains(a);
                _coin = participant.Wallet.GetMaxCoinUnderCondition(a=>changeCondition(a));

                if (additionalList != null)
                {
                    var _coin1 = additionalList.Where(a => changeCondition(a)).Max();
                    if (_coin1!=null && (_coin==null || _coin1.Value > _coin.Value)) { _coin = _coin1; }
                }

                if (_coin != null)
                {
                    changeAmount -= _coin.Value;
                    _coins.Add(_coin);
                }
                else{ break; }
            }

            if (changeAmount == 0)
            {
                return _coins;
            }
            return null;
        }

        private bool ShipTheGoods()
        {
            try
            {
                if (sellItem != null)
                { 
                    participant2.GoodsStorage.AddItem(sellItem);
                    participant1.GoodsStorage.RemoveItems(a => a.Equals(sellItem));
                }
                else
                {
                    AddMessage("Change the money");
                }
                return true;
            }
            catch (Exception ex)
            {
                AddMessage("Can't ship the goods");
                AddMessage(AddExceptionInfo(ex));
                return false;
            }
        }

        private void AddMessage(string _message)
        {
            Message += _message + Environment.NewLine;
        }

        private string AddExceptionInfo(Exception ex)
        {
            string message = ex.Message;
            if (ex.InnerException != null)
            {
                message += Environment.NewLine + ex.InnerException;
            }
            return message;
        }
    }
}
