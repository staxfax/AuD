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
                    while(!(n.left == null && n.right == null)) // (n.left != null || n.right != null)
                    {
                        bool direction = true;
                        if (n.left == null)
                        {
                            // gehen in die rechte Richtung
                            direction = false;
                        }
                        // Rechts und Links vorhanden
                        else if (n.right != null)
                        {
                            // welches ist der kleinere Schlüssel
                            direction = n.left.zahl < n.right.zahl;
                        }
                        BinTreeNode next;
                        if (direction)
                        {
                            next = n.left;
                            n.left = next.left;
                            //tausche die Verbindungen nach rechts
                            BinTreeNode right = n.right;
                            n.right = next.right;
                            next.right = right;
                            next.left = n;
                        }
                        else
                        {
                            next = n.right;
                            n.right = next.right;
                            //tausche die Verbindungen nach links
                            BinTreeNode left = n.left;
                            n.left = next.left;
                            next.left = left;
                            next.right = n;
                        }
                        //Vorgänger zeigt auf Nachfolger
                        if (pred != null)
                        {
                            if (pred.left == n)
                            {
                                pred.left = next;
                            }
                            else
                            {
                                pred.right = next;
                            }
                        }
                    }
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
                    while(n.zahl >= pred.zahl)
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
        }

        //DownHeap verwenden, um alle Knoten n zu bearbeiten
        public void BuildHeap()
        {
           
        }
    }



}
