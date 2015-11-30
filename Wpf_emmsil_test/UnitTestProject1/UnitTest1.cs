using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Wpf_emmsil_test;
using Wpf_emmsil_test.classes;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        #region transaction tests
        [TestMethod]
        public void BaseBuyTest()
        {
            TradeParticipant customer = new TradeParticipant("customer");
            Coin coin = new Coin(10);
            Coin coin1 = new Coin(10);
            Coin coin2 = new Coin(10);
            Coin coin3 = new Coin(5);
            Coin coin4 = new Coin(3);
            Coin coin5 = new Coin(2);
            Coin coin6 = new Coin(1);

            customer.Wallet.AddItems(new List<Coin>{coin,coin1,coin2,coin3,coin4,coin5,coin6});

            TradeParticipant vmachine = new TradeParticipant("vmachine");
            GoodsItem item = new GoodsItem(10, "coffee");
            GoodsItem item1 = new GoodsItem(10, "coffee");
            GoodsItem item2= new GoodsItem(10, "coffee");
            GoodsItem item3 = new GoodsItem(10, "coffee");
            GoodsItem item4 = new GoodsItem(7, "green tea");
            GoodsItem item5 = new GoodsItem(3, "tea");
            GoodsItem item6 = new GoodsItem(3, "tea");
            GoodsItem item7 = new GoodsItem(1, "napkin");
            GoodsItem item8 = new GoodsItem(1, "napkin");

            vmachine.GoodsStorage.AddItems(new List<GoodsItem> 
            { item, item1, item2, item3, item4, item5, item6, item7, item8, item });

            
            Coin coin0 = new Coin(10);
            Coin coin01 = new Coin(10);
            Coin coin02 = new Coin(10);
            Coin coin03 = new Coin(5);
            Coin coin04 = new Coin(3);
            Coin coin05 = new Coin(2);
            Coin coin06 = new Coin(1);

            vmachine.Wallet.AddItems(new List<Coin> { coin0, coin01, coin02, coin03, coin04, coin05, coin06 });

            var cmoney_amount = customer.Wallet.MoneyAmount;
            var cgoods_amount = customer.GoodsStorage.GoodsCount;

            var vmoney_amount = vmachine.Wallet.MoneyAmount;
            var vgoods_count = vmachine.GoodsStorage.GoodsCount;

            customer.Buy(vmachine, item4, new List<Coin>() { coin3,coin4 }); //8-1 coin06 in

            Assert.AreEqual(cmoney_amount - 7, customer.Wallet.MoneyAmount, "customer wallet");
            Assert.AreEqual(vgoods_count - 1, vmachine.GoodsStorage.GoodsCount, "vm goods storage");
            Assert.AreEqual(vmoney_amount + 7, vmachine.Wallet.MoneyAmount, "vm wallet");

            customer.Buy(vmachine, item5, new List<Coin>() { coin5,coin6 });// 0 
            customer.Buy(vmachine, item1, new List<Coin>() { coin1 });//
            customer.Sell(vmachine, item4, new List<Coin>() { coin05,coin03 });//

            Assert.AreEqual(cmoney_amount-7-3-10+7,
                customer.Wallet.MoneyAmount, "customer wallet");
            Assert.AreEqual(2, customer.GoodsStorage.GoodsCount, "customer goods storage");

            
        }

        [TestMethod]
        public void PrimaryBuyTest()
        {
            TradeParticipant customer = new TradeParticipant("customer");
            Coin coin = new Coin(10);
            customer.Wallet.AddItem(coin);

            TradeParticipant vmachine = new TradeParticipant("vmachine");
            GoodsItem item = new GoodsItem(10,"coffee");
            vmachine.GoodsStorage.AddItem(item);

            customer.Buy(vmachine, item, new List<Coin>() { coin });

            
            Assert.AreEqual(0,customer.Wallet.MoneyAmount,"customer wallet");
            Assert.AreEqual(1,customer.GoodsStorage.GoodsCount, "customer goods storage");
            Assert.AreEqual(10,vmachine.Wallet.MoneyAmount, "vm wallet");
            Assert.AreEqual(0,vmachine.GoodsStorage.GoodsCount, "vm goods storage");
        }

        [TestMethod]
        public void NomoneyBuyTest()
        {
            TradeParticipant customer = new TradeParticipant("customer");
            Coin coin = new Coin(5);
            customer.Wallet.AddItem(coin);

            TradeParticipant vmachine = new TradeParticipant("vmachine");
            GoodsItem item = new GoodsItem(10, "coffee");
            vmachine.GoodsStorage.AddItem(item);

            customer.Buy(vmachine, item, new List<Coin>() { coin });

            Assert.AreEqual(5,customer.Wallet.MoneyAmount, "customer wallet");
            Assert.AreEqual(0,customer.GoodsStorage.GoodsCount, "customer goods storage");
            Assert.AreEqual(0,vmachine.Wallet.MoneyAmount, "vm wallet");
            Assert.AreEqual(1,vmachine.GoodsStorage.GoodsCount, "vm goods storage");
        }

        [TestMethod]
        public void ReturnChangeTest1()
        {
            TradeParticipant customer = new TradeParticipant("customer");
            Coin coin = new Coin(10);
            Coin coin1 = new Coin(5);
            customer.Wallet.AddItem(coin);
            customer.Wallet.AddItem(coin1);

            TradeParticipant vmachine = new TradeParticipant("vmachine");
            GoodsItem item = new GoodsItem(10, "coffee");
            vmachine.GoodsStorage.AddItem(item);

            customer.Buy(vmachine, item, new List<Coin>() { coin,coin1 });

            Assert.AreEqual(5,customer.Wallet.MoneyAmount, "customer wallet");
            Assert.AreEqual(1,customer.GoodsStorage.GoodsCount, "customer goods storage");
            Assert.AreEqual(10,vmachine.Wallet.MoneyAmount, "vm wallet");
            Assert.AreEqual(0,vmachine.GoodsStorage.GoodsCount, "vm goods storage");
        }
        [TestMethod]
        public void ReturnChangeTest2()
        {
            TradeParticipant customer = new TradeParticipant("customer");
            Coin coin = new Coin(10);
            Coin coin1 = new Coin(5);
            customer.Wallet.AddItem(coin);
            customer.Wallet.AddItem(coin1);

            TradeParticipant vmachine = new TradeParticipant("vmachine");
            GoodsItem item = new GoodsItem(10, "coffee");
            vmachine.GoodsStorage.AddItem(item);

            customer.Buy(vmachine, null, new List<Coin>() { coin, coin1 });

            Assert.AreEqual(15, customer.Wallet.MoneyAmount, "customer wallet");
            Assert.AreEqual(0, customer.GoodsStorage.GoodsCount, "customer goods storage");
            Assert.AreEqual(0, vmachine.Wallet.MoneyAmount, "vm wallet");
            Assert.AreEqual(1, vmachine.GoodsStorage.GoodsCount, "vm goods storage");
        }

        [TestMethod]
        public void ReturnChangeTest3()
        {
            TradeParticipant customer = new TradeParticipant("customer");
            Coin coin = new Coin(3);
            Coin coin1 = new Coin(5);
            Coin coin2 = new Coin(2);

            customer.Wallet.AddItem(coin);
            customer.Wallet.AddItem(coin1);
            customer.Wallet.AddItem(coin2);

            TradeParticipant vmachine = new TradeParticipant("vmachine");
            GoodsItem item = new GoodsItem(10, "coffee");
            vmachine.GoodsStorage.AddItem(item);

            customer.Buy(vmachine, null, new List<Coin>() { coin, coin1,coin2 });

            Assert.AreEqual(10, customer.Wallet.MoneyAmount, "customer wallet");
            Assert.AreEqual(3, customer.Wallet.GetItems(a => a.Value >0, -1).Count, "customer wallet");

            Coin coin3 = new Coin(10);
            vmachine.Wallet.AddItem(coin3);

            customer.Buy(vmachine, null, new List<Coin>() { coin, coin1, coin2 });

            Assert.AreEqual(10, customer.Wallet.MoneyAmount, "customer wallet");
            Assert.AreEqual(1, customer.Wallet.GetItems(a => a.Value == 10, -1).Count, "customer wallet");

            Assert.AreEqual(10, vmachine.Wallet.MoneyAmount, "vm wallet");
            Assert.AreEqual(3, vmachine.Wallet.GetItems(a => a.Value > 0, -1).Count, "vm wallet");
        }

        [TestMethod]
        public void DoesHaveEnoughChangeTest()
        {
            TradeParticipant customer = new TradeParticipant("customer");
            Coin coin = new Coin(10);
            Coin coin1 = new Coin(5);
            customer.Wallet.AddItem(coin);
            customer.Wallet.AddItem(coin1);

            TradeParticipant vmachine = new TradeParticipant("vmachine");
            GoodsItem item = new GoodsItem(7, "green tea");
            vmachine.GoodsStorage.AddItem(item);

            customer.Buy(vmachine, item, new List<Coin>() { coin, coin1 });

            Assert.AreEqual(15, customer.Wallet.MoneyAmount, "customer wallet");
            Assert.AreEqual(0, customer.GoodsStorage.GoodsCount, "customer goods storage");
            Assert.AreEqual(0, vmachine.Wallet.MoneyAmount, "vm wallet");
            Assert.AreEqual(1, vmachine.GoodsStorage.GoodsCount, "vm goods storage");
        }
        #endregion

        #region Vending tests
       
        [TestMethod]
        public void VendingNoMoneyTest()
        {
            Customer customer = new Customer("customer");
            Coin coin = new Coin(10);
            customer.Wallet.AddItem(coin);

            VendingMachine vmachine = new VendingMachine("vmachine");
            GoodsItem item = new GoodsItem(15, "green tea");
            vmachine.GoodsStorage.AddItem(item);

            Coin coin0 = new Coin(5);
            Coin coin01 = new Coin(3);
            Coin coin02 = new Coin(2);
            vmachine.Wallet.AddItem(coin0);
            vmachine.Wallet.AddItem(coin01);
            vmachine.Wallet.AddItem(coin02);

            customer.InsertCoin(coin, vmachine);

            vmachine.SellTheItem(item, customer);

            Assert.AreEqual(10, customer.Wallet.MoneyAmount, "customer wallet");
            Assert.AreEqual(0, customer.GoodsStorage.GoodsCount, "customer goods storage");
            Assert.AreEqual(1, customer.Wallet.GetItems(a => a == coin, -1).Count, "customer wallet");
            Assert.AreEqual(10, vmachine.Wallet.MoneyAmount, "vm wallet");
            Assert.AreEqual(1, vmachine.GoodsStorage.GoodsCount, "vm goods storage");
        }

        [TestMethod]
        public void VendingReturnChangeTest()
        {
            Customer customer = new Customer("customer");
            Coin coin = new Coin(10);
            Coin coin1 = new Coin(10);
            customer.Wallet.AddItem(coin);
            customer.Wallet.AddItem(coin1);

            VendingMachine vmachine = new VendingMachine("vmachine");
            GoodsItem item = new GoodsItem(15, "green tea");
            vmachine.GoodsStorage.AddItem(item);
            
            Coin coin0 = new Coin(5);
            Coin coin01 = new Coin(3);
            Coin coin02 = new Coin(2);
            vmachine.Wallet.AddItem(coin0);
            vmachine.Wallet.AddItem(coin01);
            vmachine.Wallet.AddItem(coin02);

            customer.InsertCoin(coin, vmachine);
            customer.InsertCoin(coin1, vmachine);

            vmachine.SellTheItem(item, customer);

            Assert.AreEqual(5, customer.Wallet.MoneyAmount, "customer wallet");
            Assert.AreEqual(1, customer.GoodsStorage.GoodsCount, "customer goods storage");
            Assert.AreEqual(1, customer.Wallet.GetItems(a=>a==coin0,-1).Count, "customer wallet");
            Assert.AreEqual(25, vmachine.Wallet.MoneyAmount, "vm wallet");
            Assert.AreEqual(0, vmachine.GoodsStorage.GoodsCount, "vm goods storage");
        }
        [TestMethod]
        public void VendingDoesHaveEnoughChangeTest()
        {
            Customer customer = new Customer("customer");
            Coin coin = new Coin(10);
            Coin coin1 = new Coin(5);
            customer.Wallet.AddItem(coin);
            customer.Wallet.AddItem(coin1);

            VendingMachine vmachine = new VendingMachine("vmachine");
            GoodsItem item = new GoodsItem(7, "green tea");
            vmachine.GoodsStorage.AddItem(item);

            customer.InsertCoin(coin, vmachine);
            customer.InsertCoin(coin1, vmachine);

            vmachine.SellTheItem(item, customer);

            Assert.AreEqual(15, customer.Wallet.MoneyAmount, "customer wallet");
            Assert.AreEqual(0, customer.GoodsStorage.GoodsCount, "customer goods storage");
            Assert.AreEqual(0, vmachine.Wallet.MoneyAmount, "vm wallet");
            Assert.AreEqual(1, vmachine.GoodsStorage.GoodsCount, "vm goods storage");
        }
        #endregion
    }
}
