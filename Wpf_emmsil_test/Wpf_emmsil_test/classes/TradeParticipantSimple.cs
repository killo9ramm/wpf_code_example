using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_emmsil_test.classes
{
    public class TradeParticipantSimple
    {
        public Wallet Wallet {get; protected set;}
        public GoodsStorage GoodsStorage { get; protected set; }
        public string Name { get; private set; }
        public string Message { get; private set; }

        public event MessageEventHandler MessageEvent; 

        public TradeParticipantSimple(string _name)
        {
            Name = _name;
            Wallet = new Wallet();
            GoodsStorage = new GoodsStorage();
        }

        public void NewMessage(string message)
        {
            Message+=message+Environment.NewLine;
            if (MessageEvent != null)
            {
                MessageEvent(this,new StringEventArgs(message));
            }
        }
    }

    public delegate void MessageEventHandler(object sender, StringEventArgs e);
    public class StringEventArgs:EventArgs
    {
        public string Message{get; private set;}
        public StringEventArgs(string _string) : base()
        {
            Message=_string;
        }
    }
}
