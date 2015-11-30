using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wpf_emmsil_test.classes
{
    public class CItem : IComparable
    {
        public decimal Value { get; private set; }
        public String Name { get; private set; }
        public int Ttype { get; private set; }

        public CItem(decimal _value,string _name)
        {
            Value = _value;
            Name = _name;
            Ttype = (Name + _value).GetHashCode();
        }

        public int CompareTo(object obj)
        {
            if (obj is CItem)
            {
                return this.Value.CompareTo(((CItem)obj).Value);
            }
            else
            {
                return this.CompareTo(obj);
            }
        }
    }
    public class ItemsStorage<T> 
    {
        protected List<T> List;

        public ItemsStorage()
        {
            List = new List<T>();
        }

        public virtual T GetItem(Func<T,bool> condition)
        {
            var items = List.Where(condition);
            if (items.Count() >= 1)
            {
                var item = items.First();
                return item;
            }
            else
            {
                return default(T);
            }
        }

        public virtual List<T> GetItems(Func<T, bool> condition, int count)
        {
            var items = List.Where(condition);
            if (count == -1)
            {
                return items.ToList();
            }

            if (items.Count() >= count)
            {
                var item = items.Take(count).ToList();
                return item;
            }
            else
            {
                return null;
            }
        }

        public virtual bool RemoveItems(Func<T, bool> condition)
        {
            var items = List.Where(condition);
            if (items.Count() >= 0)
            {
                items.ToList().ForEach(a => List.Remove(a));
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual void AddItem(T _item)
        {
            if (List.Contains(_item))
            {
                throw new ArgumentException("Cannot insert item, it's already inserted", "_item");
            }

            List.Add(_item);
        }

        public virtual void AddItems(List<T> _items)
        {
            if (_items.Where(a => List.Contains(a)).Count() > 0)
            {
                    throw new ArgumentException("Cannot insert items, already inserted", "_items");
            }
            List.AddRange(_items);
        }
    }
}
