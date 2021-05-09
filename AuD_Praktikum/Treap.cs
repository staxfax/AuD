using System;
using System.Collections.Generic;
using System.Text;

namespace AuD_Praktikum
{
    class TreapNode : BinTreeNode
    {
        public int priority;

        public TreapNode(int priority)
        {
            this.priority = priority;
        }

        public override string ToString()
        {
            return $"{base.ToString()}; {priority,3}";
        }

    }
    class Treap : BinSearchTree
    {
        private Random random;

        public Treap()
        {
            root = null;
            random = new Random();
        }

        protected override BinTreeNode insertNode(int elem)
        {
            TreapNode a = new TreapNode(random.Next(0, 51));
            a.zahl = elem;
            a.left = null; a.right = null;
            return a;
        }

        public void RotationHeap(TreapNode n)
        {
            bool left = n.left.zahl < n.right.zahl;
            if (left)
            {
                if (n.zahl > n.left.zahl)
                {
                    BinTreeNode braun = n.left;
                    n.left = braun.left;
                    //tausche die Verbindungen nach rechts
                    BinTreeNode blau = n.right;
                    n.right = braun.right;
                    braun.right = blau;
                    //Vorgänger zeigt auf Nachfolger
                    pred.left = braun;
                    braun.left = n;
                }
            }
            else
            {
                if (n.zahl > n.right.zahl)
                {
                    BinTreeNode braun = n.right;
                    n.right = braun.right;
                    //tausche die Verbindungen nach links
                    BinTreeNode blau = n.left;
                    n.left = braun.left;
                    braun.left = blau;
                    //Vorgänger zeigt auf Nachfolger
                    pred.right = braun;
                    braun.right = n;
                }
            }
        }

        public void DownHeap(TreapNode n)
        {
            if(n != null)
            {
                BinTreeNode found = searchNode(n.zahl);
                if(found != null)
                {
                    RotationHeap(n);
                }
            }
        }

        public void BuildHeap()
        {

        }
    }



}
