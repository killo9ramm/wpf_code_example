using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_emmsil_test.classes
{
    public class GoodsItem : CItem
    {
        public GoodsItem(decimal _value, string name) : base(_value, name) { }
    }

    public class GoodsStorage : ItemsStorage<GoodsItem>
    {
        public int GoodsCount
        {
            get
            {
                return List.Count();
            }
        }
        public virtual void AddGoods(string name, decimal price, int count)
        {
            for(int i=0;i<count;i++){
                GoodsItem item = new GoodsItem(price,name);
                List.Add(item);
            }
                
        }
    }
}
