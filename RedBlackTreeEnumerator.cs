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

        public RedBlackTreeEnumerator(DictNode<TKey,TValue> node)
        {
            this.dictNodeCurrent = node;
            this.root = node;
            this.current = default(KeyValuePair<TKey,TValue>);
        }

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            while (dictNodeCurrent != null)
            {
                if (dictNodeCurrent.Left != null && !current.Key.Equals(dictNodeCurrent.Left.Info.Key))
                {
                    dictNodeCurrent = dictNodeCurrent.Left;
                }
                else
                {
                    if (dictNodeCurrent.Right != null && !current.Key.Equals(dictNodeCurrent.Right.Info.Key))
                    {
                        dictNodeCurrent = dictNodeCurrent.Right;
                    }
                    else
                    {
                        current = dictNodeCurrent.Info;
                        dictNodeCurrent = dictNodeCurrent.Parent;
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
        }
    }
}
