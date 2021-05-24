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
            return $"{zahl}({priority})";
        }

    }
    class Treap : BinSearchTree
    {
        private Random random;


        public Treap()
        {
            root = null;
            random = new Random();

            // test
            root = new TreapNode(2) { zahl = 4};
            root.left = new TreapNode(19) { zahl = 1, parent = root as TreapNode };
            root.left.right = new TreapNode(22) { zahl = 2, parent = root.left as TreapNode };

            root.right = new TreapNode(13) { zahl = 7, parent = root as TreapNode };
            root.right.left = new TreapNode(39) { zahl = 5, parent = root.right as TreapNode };
            root.right.right = new TreapNode(40) { zahl = 8, parent = root.right as TreapNode };
        }

        /// <summary>
        /// Creates a new <see cref="TreapNode"/> using the given <paramref name="elem"/>
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        protected override BinTreeNode insertNode(int elem)
        {
            TreapNode a = new TreapNode(1);// random.Next(0, 51));
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
            // check if n is leaf
            if (n.left == null && n.right == null)
            {
                //schleife solange parent vorhanden und priotity kleiner priority partent
                while (n.parent != null && n.priority < n.parent.priority)
                {
                    TreapNode parent = n.parent;
                    TreapNode grandParent = n.parent.parent;
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
                    n.parent = grandParent;

                    // find position for parent
                    if (n == parent.left)
                    {
                        // n is on the left side
                        BinTreeNode left = n.left;
                        n.left = n != n.parent.left ? n.parent.left : null;
                        if (left.zahl < n.right.zahl)
                        {
                            n.parent.left = left;
                            n.parent.right = n.right;
                        }
                        else
                        {
                            n.parent.left = n.right;
                            n.parent.right = left;
                        }
                        n.right = n.parent;
                    }
                    else
                    {
                        //+++++++++++++++++
                        // n is on the right side
                        BinTreeNode right = n.right; // keep the n->right
                        BinTreeNode left = n.left; // keep the n->left
                        if (right != null)
                        {
                            if (parent.left != null)
                            {
                                if (left.zahl > parent.zahl)
                                {
                                    parent.right = left;
                                }
                                else
                                {

                                }
                            }
                        }
                        if (left != null)
                        {
                            if (left.zahl > parent.zahl)
                            {
                                parent.right = left;
                                n.left = parent.left;
                            }
                            else
                            {
                                n.parent.right = n.left;
                                n.parent.left = right;
                            }
                        }
                        else
                        {
                            parent.right = null;
                        }

                        n.left = parent;
                    }
                    // replace the parent
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
            bool direction = n.left != null;
            while (!(n.left == null && n.right == null)) // (n.left != null || n.right != null)
            {
                TreapNode next;
                //If direction true go left
                if (direction)
                {
                    next = n.left as TreapNode;
                    if (next != null)
                    {
                        n.left = next.right;
                        //tausche die Verbindungen nach rechts
                        next.right = n;
                    }
                }
                else
                {
                    next = n.right as TreapNode;
                    if (next != null)
                    {
                        n.right = next.right; 
                        //tausche die Verbindungen nach links
                        BinTreeNode left = n.left;
                        n.left = next.left;
                        next.left = left;
                        next.right = n;
                    }
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
                if (next != null)
                {
                    next.parent = grandParent;
                    if (next.parent == null)
                    {
                        root = next;
                    }
                }
                direction = n.right == null;
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
