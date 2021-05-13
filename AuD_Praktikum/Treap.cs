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

        /// <summary>
        /// Defines the Min-Heap (true) or Max-Heap (false)
        /// </summary>
        public bool MinHeap = true;

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

        //Funktion, um die jeweiligen Knoten zu tauschen und deren Pointer wechseln 
        private void SwapDown(TreapNode n)
        {
            bool left = true;
            if (n.left == null)
            {
                // gehen in die rechte Richtung
                left = false;
            }
            // Rechts und Links vorhanden
            else if (n.right != null)
            {
                // welches ist der kleinere Schlüssel
                left = n.left.zahl < n.right.zahl;
            }
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
                    if (pred != null)
                    {
                        if (pred.left == n)
                        {
                            pred.left = braun;
                        }
                        else
                        {
                            pred.right = braun;
                        }
                    }
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
                    if (pred != null)
                    {
                        if (pred.left == n)
                        {
                            pred.left = braun;
                        }
                        else
                        {
                            pred.right = braun;
                        }
                    }
                    braun.right = n;
                }
            }
        }

        /// <summary>
        /// Funktion damit Heap-Bedingung erfüllt ist, Zahlen "durchsickern"
        /// </summary>
        /// <param name="n"></param>
        public void DownHeap(TreapNode n)
        {
            if(n != null && (n.left != null || n.right != null))
            {
                //look in tree to find pred(parent) node
                BinTreeNode found = searchNode(n.zahl);
                if(found != null)
                {
                    SwapDown(n);
                }
            }
        }

        public void RotationHeap(TreapNode n)
        {
            if (n.left == null && n.right == null)
            {
                //look in tree to find pred(parent) node
                BinTreeNode found = searchNode(n.zahl);
                if (found != null && pred != null)
                {
                    BinTreeNode vater = pred;
                    searchNode(vater.zahl);
                    BinTreeNode opa = pred;
                    if (opa != null)
                    {
                        if (opa.left == vater)
                        {
                            opa.left = n;
                        }
                        else
                        {
                            opa.right = n;
                        }
                    }
                    if (n == vater.right)
                    {
                        n.left = vater;
                        vater.right = null;
                    }
                    else
                    {
                        n.right = vater;
                        vater.left = null;
                    }
                }
            }
        }

        //DownHeap verwenden, um alle Knoten n zu bearbeiten
        public void BuildHeap()
        {
           
        }
    }



}
