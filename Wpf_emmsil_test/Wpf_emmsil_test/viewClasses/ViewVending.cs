using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Wpf_emmsil_test.classes;

namespace Wpf_emmsil_test.viewClasses
{
    class ViewVending : VendingMachine, INotifyPropertyChanged
    {
        public List<GroupItem> ItemsList {get; private set;}

        public override decimal DBalance { 
            get{
                return base.DBalance;
                } 
            protected set
            {
                base.DBalance = value;
                OnPropertyChanged("DBalance");
            }
        }

        public ViewVending(string _name)
            : base(_name)
        {
            ItemsList = new List<GroupItem>();
        }

        public void Refresh()
        {
            FillViewList(GoodsStorage.GetItems(a => a == a, -1));
        }

        private void FillViewList(List<GoodsItem> list)
        { 
            ItemsList.Clear();
            var b = from c in list.OrderByDescending(a => a.Value).GroupBy(a => a.Ttype)
                    where c.Count() > 0
                    select new GroupItem(c.Count(), c.FirstOrDefault().Name, c.FirstOrDefault().Ttype,c.FirstOrDefault().Value);

            ItemsList.AddRange(b);

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
