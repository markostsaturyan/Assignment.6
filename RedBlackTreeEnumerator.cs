using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionaries
{
    internal class RedBlackTreeEnumerator<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        private DictNode<TKey, TValue> dictNodeCurrent;
        private DictNode<TKey, TValue> root;
        private int count;
        private int currentIndex;
        private TKey prevCurrent;
        private TKey rootLeft;


        private KeyValuePair<TKey, TValue> current;

        public KeyValuePair<TKey, TValue> Current
        {
            get
            {
                return current;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return current;
            }
        }

        public RedBlackTreeEnumerator(DictNode<TKey, TValue> node, int count)
        {
            this.root = node;
            this.dictNodeCurrent = node;
            this.count = count;
            this.currentIndex = count;
            this.current = default(KeyValuePair<TKey,TValue>);
            this.prevCurrent = default(TKey);
            this.rootLeft = default(TKey);
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            while (currentIndex != 0)
            {
                if (dictNodeCurrent.Left != null && !current.Key.Equals(dictNodeCurrent.Left.Info.Key) && !prevCurrent.Equals(dictNodeCurrent.Left.Info.Key) && !rootLeft.Equals(dictNodeCurrent.Left.Info.Key))
                {
                    dictNodeCurrent = dictNodeCurrent.Left;
                }
                else
                {
                    if (dictNodeCurrent.Right != null && !current.Key.Equals(dictNodeCurrent.Right.Info.Key) && !prevCurrent.Equals(dictNodeCurrent.Right.Info.Key))
                    {
                        dictNodeCurrent = dictNodeCurrent.Right;
                    }
                    else
                    {
                        prevCurrent = current.Key;
                        current = dictNodeCurrent.Info;
                        dictNodeCurrent = dictNodeCurrent.Parent;
                        currentIndex--;

                        if (rootLeft.Equals(default(TKey)))
                        {
                            if (root.Left != null)
                            {
                                rootLeft = root.Left.Info.Key;
                            }
                            else
                            {
                                rootLeft = default(TKey);
                            }
                        }

                        return true;
                    }
                }
                   
            }
            return false;
        }

        public void Reset()
        {
            this.dictNodeCurrent = root;
            this.current = default(KeyValuePair<TKey, TValue>);
            this.currentIndex = this.count;
            this.prevCurrent = default(TKey);
            this.rootLeft = default(TKey);
        }
    }
}
