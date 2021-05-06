using System;
using System.Collections.Generic;
using System.Text;

namespace AuD_Praktikum
{
    class TreapNode : BinTreeNode
    {
        public int priority;

        public override string ToString()
        {
            return $"{base.ToString()}; {priority,3}";
        }

    }
    class Treap : BinSearchTree
    {
        private TreapNode root;

        public Treap()
        {
            root = null;
        }

    }
}
