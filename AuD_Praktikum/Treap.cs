using System;

namespace AuD_Praktikum
{
    class TreapNode : BinTreeNode
    {
        public int priority;
        public TreapNode parent;

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

        /// <summary>
        /// Creates a new <see cref="TreapNode"/> using the given <paramref name="elem"/>
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        protected override BinTreeNode insertNode(int elem)
        {
            TreapNode a = new TreapNode(random.Next(0, 51));
            a.zahl = elem;
            a.left = null; a.right = null;
            return a;
        }

        /// <inheritdoc/>
        public override bool insert(int elem)
        {
            TreapNode a = insert_(elem) as TreapNode;
            if (a != null )
            {
                // initialize the parent node
                a.parent = pred as TreapNode;
                RotationHeap(a);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Performs rotation of Min-Heap
        /// </summary>
        /// <param name="n">leaf of tree</param>
        private void RotationHeap(TreapNode n)
        {
            if (n.left == null && n.right == null)
            {
                //schleife solange parent vorhanden und priotity kleiner priority partent
                while (n.parent != null && n.priority <= n.parent.priority)
                {
                    BinTreeNode grandParent = n.parent.parent;
                    if (grandParent != null)
                    {
                        if (grandParent.left == n.parent)
                        {
                            grandParent.left = n;
                        }
                        else
                        {
                            grandParent.right = n;
                        }
                    }
                    if (n == n.parent.right)
                    {
                        BinTreeNode left = n.left;
                        n.left = n.parent.left;
                        n.parent.left = left;
                        n.parent.right = n.right;
                        n.right = n.parent;
                    }
                    else
                    {
                        BinTreeNode right = n.right;
                        n.right = n.parent.right;
                        n.parent.right = right;
                        n.parent.left = n.left;
                        n.left = n.parent;
                    }
                    // replace the parent
                    TreapNode parent = n.parent;
                    n.parent = parent.parent;
                    parent.parent = n;
                }
                if (n.parent == null)
                    root = n;
            }
        }

        /// <summary>
        /// Funktion damit Heap-Bedingung erfüllt ist, Zahlen "durchsickern"
        /// </summary>
        /// <param name="n"></param>
        private void DownHeap(TreapNode n)
        {
            while (!(n.left == null && n.right == null)) // (n.left != null || n.right != null)
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

                TreapNode next;
                //If direction true go left
                if (direction)
                {
                    next = n.left as TreapNode;
                    n.left = next.left;
                    //tausche die Verbindungen nach rechts
                    BinTreeNode right = n.right;
                    n.right = next.right;
                    next.right = right;
                    next.left = n;
                }
                else
                {
                    next = n.right as TreapNode;
                    n.right = next.right;
                    //tausche die Verbindungen nach links
                    BinTreeNode left = n.left;
                    n.left = next.left;
                    next.left = left;
                    next.right = n;
                }

                //Vorgänger zeigt auf Nachfolger
                if (n.parent != null)
                {
                    if (n.parent.left == n)
                    {
                        n.parent.left = next;
                    }
                    else
                    {
                        n.parent.right = next;
                    }
                }

                TreapNode grandParent = n.parent;
                n.parent = next;
                next.parent = grandParent;

            }
        }


        public override bool delete(int elem)
        {
            TreapNode a = searchNode(elem) as TreapNode;
            if (a != null)
            {
                DownHeap(a);
                if(a == a.parent.left)
                {
                    a.parent.left = null;
                }
                else
                {
                    a.parent.right = null;
                }
                return true;
            }
            return false;
        }
    }



}
