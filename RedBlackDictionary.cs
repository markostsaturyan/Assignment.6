using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionaries
{
    public class RedBlackDictionary<TKey,TValue>: IDictionary<TKey, TValue> where TKey:IComparable<TKey>
    {
        /// <summary>
        /// The root of red-black tree.
        /// </summary>
        private DictNode<TKey, TValue> root;

        /// <summary>
        /// The count of nodes in tree.
        /// </summary>
        private int countOfNodes;

        /// <summary>
        /// Keys collection
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                TKey[] keys = new TKey[this.countOfNodes];
                DictNode<TKey, TValue> current=this.root;
                int i = 0;

                while (i <= countOfNodes)
                {
                    if (current != null)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Parent;
                        keys[i]=current.Info.Key;
                        i++;
                        current = current.Right;
                    }
                }

                return keys;
            }
        }

        /// <summary>
        /// Values cilection
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                TValue[] values=new TValue[this.countOfNodes];
                DictNode<TKey, TValue> current = this.root;
                int i = 0;

                while (i <= countOfNodes)
                {
                    if (current != null)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        current = current.Parent;
                        values[i] = current.Info.Value;
                        i++;
                        current = current.Right;

                    }
                }

                return values;
            }
        }

        /// <summary>
        /// Return the count of nodes.
        /// </summary>
        public int Count
        {
            get
            {
                return countOfNodes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue this[TKey key]
        {
            get
            {
                DictNode<TKey, TValue> current = this.root;

                int cmp;

                while (current != null)
                {
                    cmp = key.CompareTo(current.Info.Key);

                    if (cmp < 0)
                    {
                        current = current.Left;
                    }

                    if (cmp > 0)
                    {
                        current = current.Right;
                    }

                    return current.Info.Value;
                }

                throw new KeyNotFoundException();
            }

            set
            {
                DictNode<TKey, TValue> current = this.root;
                int cmp;

                while (current != null)
                {
                    cmp = key.CompareTo(current.Info.Key);

                    if (cmp < 0)
                    {
                        current = current.Left;
                    }
                    else
                    {
                        if (cmp > 0)
                        {
                            current = current.Right;
                        }
                        else
                        {
                            current.Info = new KeyValuePair<TKey, TValue>(key, value);
                            return;
                        }
                    }
                }

                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// The parametrless constructor.
        /// </summary>
        public RedBlackDictionary()
        {
            this.root = null;
            this.countOfNodes = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key">The key of element</param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            DictNode<TKey, TValue> current = root;

            while (current != null)
            {
                if (current.Info.Key.CompareTo(key) < 0)
                {
                    current = current.Left;
                    continue;
                }

                if (current.Info.Key.CompareTo(key) > 0)
                {
                    current = current.Right;
                    continue;
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Add element in dictionary.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            DictNode<TKey, TValue> node = null;
            DictNode<TKey, TValue> current = this.root;

            int cmp;

            while (current != null)
            {
                node = current;
                cmp = key.CompareTo(current.Info.Key);

                if (cmp < 0)
                {
                    current = current.Left;
                }
                if (cmp > 0)
                {
                    current = current.Right;
                    continue;
                }

                throw new Exception("An element with the same key already exists in the dictionary.");
            }

            if (node == null)
            {
                this.root = new DictNode<TKey, TValue>(new KeyValuePair<TKey, TValue>(key, value), null,color:ColorEnum.Black);
            }
            else
            {
                cmp = key.CompareTo(node.Info.Key);
                if (cmp < 0)
                {
                    node.Left = new DictNode<TKey, TValue>(new KeyValuePair<TKey, TValue>(key, value), node);
                    BalansingAfterAdd(node.Left);
                }
                else
                {
                    node.Right = new DictNode<TKey, TValue>(new KeyValuePair<TKey, TValue>(key, value), node);
                    BalansingAfterAdd(node.Right);
                }
            }
            countOfNodes++;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            DictNode<TKey, TValue> current = this.root;
            DictNode<TKey, TValue> node;
            DictNode<TKey, TValue> curr;

            int cmp;

            while (current != null)
            {
                cmp = key.CompareTo(current.Info.Key);

                if (cmp < 0)
                {
                    current = current.Left;
                }

                if (cmp > 0)
                {
                    current = current.Right;
                    continue;
                }

                break;
            }

            if (current == null)
            {
                return false;
            }

            if(current.Left==null || current.Right == null)
            {
                node = current;
            }
            else
            {
                node = current.Left;

                while (node.Right != null)
                {
                    node = node.Right;
                }
                
            }

            if (node.Left != null)
            {
                curr = node.Left;
                
            }
            else
            {
                curr = node.Right;
            }

            if (curr != null)
            {
                curr.Parent = node.Parent;

                if (node == node.Parent.Left)
                {
                    node.Parent.Left = curr;
                }
                else
                {
                    node.Parent.Right = curr;
                }
            }
            else
            {
                curr = node;
            }
            

            if (node.Parent == null)
            {
                this.root = curr;
                return true;
            }

            if (node != current)
            {
                current.Info = new KeyValuePair<TKey, TValue>(node.Info.Key, node.Info.Value);
            }

            if (node.Color == ColorEnum.Black)
            {
                Balancing(curr);
            }
            countOfNodes--;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            DictNode<TKey, TValue> current = this.root;

            int cmp;

            while(current!= null)
            {
                cmp = key.CompareTo(current.Info.Key);
                if (cmp < 0)
                {
                    current = current.Left;
                }
                if (cmp > 0)
                {
                    current = current.Right;
                }

                value = current.Info.Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Add item in dictionary.
        /// </summary>
        /// <param name="item"></param>
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Clear dictionary.
        /// </summary>
        public void Clear()
        {
            this.root = null;
            this.countOfNodes = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            if (item.Value.Equals(this[item.Key]))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException("arrayIndex is less than 0.");

            if (array.Length - arrayIndex < countOfNodes)
                throw new ArgumentException("The number of elements in the source is greater than the available space from arrayIndex to the end of the destination array.");

            if (array == null)
                throw new ArgumentException("Array is null");

            Stack<DictNode<TKey, TValue>> stack = new Stack<DictNode<TKey, TValue>>();

            DictNode<TKey, TValue> current = this.root;

            while (stack.Count != 0 || current != null)
            {
                if (current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }
                else
                {
                    current = stack.Pop();
                    array[arrayIndex] = current.Info;
                    arrayIndex++;
                    current = current.Right;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (this[item.Key].Equals(item.Value))
            {
                return Remove(item.Key);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new RedBlackTreeEnumerator<TKey,TValue>(this.root,countOfNodes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return new RedBlackTreeEnumerator<TKey, TValue>(this.root,countOfNodes);
        }

        /// <summary>
        /// Right rotate.
        /// </summary>
        /// <param name="current"></param>
        private void RightRotate(DictNode<TKey, TValue> grandparent)
        {
            DictNode<TKey, TValue> node = grandparent.Left;
            grandparent.Left = node.Right;

            if (node.Right != null)
            {
                node.Right.Parent = grandparent;
            }
            node.Parent = grandparent.Parent;
            if (grandparent.Parent == null)
            {
                this.root = node;
            }
            else
            {
                if (grandparent == grandparent.Parent.Right)
                {
                    grandparent.Parent.Right = node;
                }
                else
                {
                    grandparent.Parent.Left = node;
                }
            }
            node.Right = grandparent;
            grandparent.Parent = node;
        }

        /// <summary>
        /// Left rotate.
        /// </summary>
        /// <param name="current"></param>
        private void LeftRotate(DictNode<TKey, TValue> grandparent)
        {
            DictNode<TKey, TValue> node = grandparent.Right;
            grandparent.Right = node.Left;
            if (node.Left != null)
            {
                node.Left.Parent = grandparent;
            }
            node.Parent = grandparent.Parent;
            if (grandparent.Parent == null)
            {
                this.root = node;
            }
            else
            {
                if (grandparent == grandparent.Parent.Left)
                {
                    grandparent.Parent.Left = node;
                }
                else
                {
                    grandparent.Parent.Right = node;
                }
            }
            node.Left = grandparent;
            grandparent.Parent = node;
        }

        /// <summary>
        /// Balancing red-black tree after insert.
        /// </summary>
        /// <param name="node"></param>
        private void BalansingAfterAdd(DictNode<TKey, TValue> node)
        {
            DictNode<TKey, TValue> uncle;

            while (node!=this.root && node.Parent.Color == ColorEnum.Red)
            {
                if (node.Parent == node.Parent.Parent.Left)
                {
                    uncle = node.Parent.Parent.Right;
                    if (uncle?.Color == ColorEnum.Red)
                    {
                        node.Parent.Color = ColorEnum.Black;
                        uncle.Color = ColorEnum.Black;
                        node.Parent.Parent.Color = ColorEnum.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {
                            node = node.Parent;
                            LeftRotate(node);
                        }

                        node.Parent.Color = ColorEnum.Black;
                        node.Parent.Parent.Color = ColorEnum.Red;
                        RightRotate(node.Parent.Parent);
                    }
                }
                else
                {
                    uncle = node.Parent.Parent.Left;
                    if (uncle?.Color == ColorEnum.Red)
                    {
                        node.Parent.Color = ColorEnum.Black;
                        uncle.Color = ColorEnum.Black;
                        node.Parent.Parent.Color = ColorEnum.Red;
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            RightRotate(node);
                        }
                        
                        node.Parent.Color = ColorEnum.Black;
                        node.Parent.Parent.Color = ColorEnum.Red;
                        LeftRotate(node.Parent.Parent);
                    }
                }
            }

            this.root.Color = ColorEnum.Black;
        }

        /// <summary>
        /// Balancing red-black tree.
        /// </summary>
        /// <param name="current"></param>
        private void Balancing(DictNode<TKey, TValue> current)
        {
            while (current != root && current.Color==ColorEnum.Black)
            {
                DictNode<TKey, TValue> node;

                if (current == current.Parent.Left)
                {
                    node = current.Parent.Right;
                    if (node.Color == ColorEnum.Red)
                    {
                        node.Color = ColorEnum.Black;
                        current.Parent.Color = ColorEnum.Red;
                        LeftRotate(current.Parent);
                        node = current.Parent.Right;
                    }

                    if (node.Left == null && node.Right == null)
                    {
                        node.Color = ColorEnum.Red;
                        current = current.Parent;
                    }
                    else
                    {

                        if (node.Left.Color == ColorEnum.Black && node.Right.Color == ColorEnum.Black)
                        {
                            node.Color = ColorEnum.Red;
                            current = current.Parent;
                        }
                        else
                        {
                            if (node.Right.Color == ColorEnum.Black)
                            {
                                node.Left.Color = ColorEnum.Black;
                                node.Color = ColorEnum.Red;
                                RightRotate(node);
                                node = current.Parent.Right;
                            }
                            node.Color = current.Parent.Color;
                            current.Parent.Color = ColorEnum.Black;
                            node.Right.Color = ColorEnum.Black;
                            LeftRotate(current.Parent);
                            current.Right = null;
                            current = this.root;
                        }
                    }
                }
                else
                {
                    node = current.Parent.Left;
                    if (node.Color == ColorEnum.Red)
                    {
                        node.Color = ColorEnum.Black;
                        current.Parent.Color = ColorEnum.Red;
                        RightRotate(current.Parent);
                        node = current.Parent.Left;
                    }
                    if (node.Left == null && node.Right == null)
                    {
                        node.Color = ColorEnum.Red;
                        current = current.Parent;
                    }
                    else
                    {

                        if (node.Left.Color == ColorEnum.Black && node.Right.Color == ColorEnum.Black)
                        {
                            node.Color = ColorEnum.Red;
                            current = current.Parent;
                        }
                        else
                        {
                            if (node.Left.Color == ColorEnum.Black)
                            {
                                node.Right.Color = ColorEnum.Black;
                                node.Color = ColorEnum.Red;
                                LeftRotate(node);
                                node = current.Parent.Right;
                            }
                            node.Color = current.Parent.Color;
                            current.Parent.Color = ColorEnum.Black;
                            node.Left.Color = ColorEnum.Black;
                            RightRotate(current.Parent);
                            current.Left = null;
                            current = this.root;
                        }
                    }
                }
            }
            current.Color = ColorEnum.Black;
        }

    }
}
