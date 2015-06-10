using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ResourceMerge.Core
{
    internal class MemcacheDictionary<Key, Value>
    {
        private Dictionary<Key, Value> dict = new Dictionary<Key, Value>();

        public Value Get(Key key)
        {
            if (dict.ContainsKey(key))
                return dict[key]; 
            else
                return default(Value);
        }

        public List<Value> GetAll()
        {
            List<Value> d = new List<Value>();
            foreach (var item in dict)
                d.Add(item.Value);
            return d;
        }

        public void Set(Key key, Value value)
        {
            Dictionary<Key, Value> newDict = null;
            Dictionary<Key, Value> oldDict = null;
            do
            {
                oldDict = dict;
                newDict = new Dictionary<Key, Value>(oldDict);
                newDict[key] = value;
            } while (Interlocked.CompareExchange<Dictionary<Key, Value>>(ref dict, newDict, oldDict) != oldDict);
        }

        public void Remove(Key key)
        {
            Dictionary<Key, Value> newDict = null;
            Dictionary<Key, Value> oldDict = null;
            do
            {
                oldDict = dict;
                newDict = new Dictionary<Key, Value>(oldDict);
                newDict.Remove(key);
            } while (Interlocked.CompareExchange<Dictionary<Key, Value>>(ref dict, newDict, oldDict) != oldDict);
        }
    }
}
