using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_emmsil_test.viewClasses
{
    public class GroupItem
    {
        public int Count { get; private set; }
        public String Name { get; private set; }
        public int Ttype { get; private set; }
        public decimal Value { get; private set; }

        private GroupItem() { }
        public GroupItem(int _count, string _name, int _type)
        {
            Count = _count;
            Name = _name;
            Ttype = _type;
        }
        public GroupItem(int _count, string _name,int _type,decimal _value)
        {
            Count = _count;
            Name = _name;
            Ttype = _type;
            Value = _value;
        }
    }
}
