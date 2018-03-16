using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionaries
{
    public class DictNode<TKey, TValue>
    {
        public ColorEnum Color;

        public DictNode<TKey, TValue> Parent;
        public DictNode<TKey, TValue> Left;
        public DictNode<TKey, TValue> Right;

        public KeyValuePair<TKey, TValue> Info;

        public DictNode(KeyValuePair<TKey, TValue> info, DictNode<TKey, TValue> parent, DictNode<TKey, TValue> left = null, DictNode<TKey, TValue> right = null, ColorEnum color = ColorEnum.Red)
        {
            this.Info = info;
            this.Parent = parent;
            this.Left = left;
            this.Right = right;
            this.Color = color;
        }
    }
}

