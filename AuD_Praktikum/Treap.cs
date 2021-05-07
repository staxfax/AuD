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
        public Treap()
        {
            root = null;
        }

        public TreapNode search(int elem)
        {
            return null;// searchNode();
        }

       
    }



}
