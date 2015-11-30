using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Wpf_emmsil_test.classes
{
    public class VendingMachine : TradeParticipantSimple
    {
        public virtual decimal DBalance { get; protected set; }

        public VendingMachine(string _name) : base(_name)
        {
            DBalance = 0;
        }

        /// <summary>
        /// получаем монету
        /// </summary>
        /// <param name="coin"></param>
        /// <returns></returns>
        public virtual bool GetCoin(Coin coin)
        {
                Wallet.AddItem(coin);
                DBalance += coin.Value;
                return true;   
        }

        /// <summary>
        /// продажа товара
        /// </summary>
        /// <param name="sellItem"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public virtual bool SellTheItem(GoodsItem sellItem, Customer customer)
        {
            if (ChechGoodsIsEnough(sellItem))
            {
                if (CheckIfEnoughMoney(sellItem,DBalance))
                {
                    if (DoesHaveEnoughChange(sellItem, DBalance))
                    {
                        return _SellTheItem(sellItem, customer);
                    }
                    else
                    {
                        CancelOrder(customer);
                        DoesNotHaveEnoughChange();
                        return false;
                    }
                }
                else
                {
                    //CancelOrder(customer);
                    DoesNotHaveEnoughMoney();
                    return false;
                }
            }
            else
            {
                CancelOrder(customer);
                DoesNotHaveEnoughGoods();
                return false;
            }
        }

        /// <summary>
        /// отмена заказа
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public virtual bool CancelOrder(Customer customer)
        {
            List<Coin> change = CountChange(null, DBalance);
            if (customer.GetCoins(change))
            {
                Wallet.RemoveItems(a => change.Contains(a));
                DBalance = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// продаем товар
        /// </summary>
        /// <param name="sellItem"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        private bool _SellTheItem(GoodsItem sellItem, Customer customer)
        {
            List<Coin> change;
            if (customer.GetTheGood(sellItem))
            {
                GoodsStorage.RemoveItems(a => a == sellItem);
                change = CountChange(sellItem, DBalance);
            }
            else
            {
                change = CountChange(null, DBalance);
            }
            if (customer.GetCoins(change))
            {
                Wallet.RemoveItems(a => change.Contains(a));
                DBalance = 0;
                NewMessage("Thank you!");
                return true;
            }
            return false;
        }


        /// <summary>
        /// проверяем хватает ли денег
        /// </summary>
        /// <param name="sellItem"></param>
        /// <param name="DBalance"></param>
        /// <returns></returns>
        private bool CheckIfEnoughMoney(GoodsItem sellItem, decimal DBalance)
        {
            decimal moneyAmount = DBalance;
            if (moneyAmount >= sellItem.Value)
            {
                return true;
            }
            else
            {
                //NewMessage("Sorry money amount is not enough");
                return false;
            }
        }

        /// <summary>
        /// Проверяем хватает ли сдачи
        /// </summary>
        /// <param name="sellItem"></param>
        /// <param name="DBalance"></param>
        /// <returns></returns>
        private bool DoesHaveEnoughChange(GoodsItem sellItem, decimal DBalance)
        {
            decimal changeAmount = GetChangeAmount(sellItem, DBalance);
            if (changeAmount == 0) return true;

            if (CountChange(sellItem, DBalance) != null) return true;
            return false;
        }

        /// <summary>
        /// Рассчитываем сдачу
        /// </summary>
        /// <param name="sellItem"></param>
        /// <param name="DBalance"></param>
        /// <returns></returns>
        private List<Coin> CountChange(GoodsItem sellItem, decimal DBalance)
        {
            decimal changeAmount = GetChangeAmount(sellItem, DBalance);
            Coin _coin = null;
            List<Coin> _coins = new List<Coin> { };
            while (changeAmount > 0)
            {
                Func<Coin, bool> changeCondition = a => a.Value <= changeAmount && (changeAmount - a.Value) >= 0
                    && !_coins.Contains(a);
                _coin = Wallet.GetMaxCoinUnderCondition(a => changeCondition(a));

                if (_coin != null)
                {
                    changeAmount -= _coin.Value;
                    _coins.Add(_coin);
                }
                else { break; }
            }

            if (changeAmount == 0)
            {
                return _coins;
            }
            return null;
        }
                
        private decimal GetChangeAmount(GoodsItem sellItem, decimal DBalance)
        {
            if (sellItem != null)
            {
                return DBalance - sellItem.Value;
            }
            else
            {
                return DBalance;
            }
        }

        private void DoesNotHaveEnoughMoney()
        {
            NewMessage("Sorry, doesn't have enough money");
        }
        private void DoesNotHaveEnoughChange()
        {
            NewMessage("Sorry, doesn't have enough change");
        }
        private void DoesNotHaveEnoughGoods()
        {
            NewMessage("Sorry, doesn't have enough goods");
        }

        /// <summary>
        /// Проверяем наличие товара
        /// </summary>
        /// <param name="sellItem"></param>
        /// <returns></returns>
        private bool ChechGoodsIsEnough(GoodsItem sellItem)
        {
            if (GoodsStorage.GetItem(a => a.Equals(sellItem)) != null)
            {
                return true;
            }
            return false;
        }  

        /// <summary>
        /// Выдаем товар
        /// </summary>
        /// <param name="goodItem"></param>
        /// <param name="customer"></param>
        public virtual void TakeTheGood(GoodsItem goodItem,Customer customer)
        {
            if (customer.GetTheGood(goodItem))
            {
                GoodsStorage.RemoveItems(a => a.Equals(goodItem));
                DBalance = 0;
            }
        }
    }
}
