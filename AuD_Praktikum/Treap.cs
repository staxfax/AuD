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
            // für die Priorität
            random = new Random();

            // test delete 9,30
            //root = new TreapNode(5) { zahl = 7 };
            //root.left = new TreapNode(33) { zahl = 5, parent = root as TreapNode };
            ////root.left.right = new TreapNode(22) { zahl = 2, parent = root.left as TreapNode };

            //root.right = new TreapNode(30) { zahl = 9, parent = root as TreapNode };
            //root.right.left = new TreapNode(46) { zahl = 8, parent = root.right as TreapNode };
            //root.right.right = new TreapNode(30) { zahl = 17, parent = root.right as TreapNode };

            //root.right.right.left = new TreapNode(41) { zahl = 16, parent = root.right.right as TreapNode };
            //root.right.right.right = new TreapNode(40) { zahl = 30, parent = root.right.right as TreapNode };

            //root.right.right.left.left = new TreapNode(41) { zahl = 12, parent = root.right.right.left as TreapNode };
            //root.right.right.left.left.right = new TreapNode(44) { zahl = 15, parent = root.right.right.left.left as TreapNode };
        }

        /// <summary>
        /// Neue Funktion<see cref="TreapNode"/> gegebenen Parameter verwenden <paramref name="elem"/>
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        protected override BinTreeNode insertNode(int elem)
        {
            TreapNode a = new TreapNode(random.Next(0, 50)); 
            a.zahl = elem;
            a.left = null; a.right = null;
            return a;
        }

        /// <inheritdoc/>
        public override bool insert(int elem)
        {
            // Neu casten
            TreapNode a = insert_(elem) as TreapNode;
            // eingefügtes Element nicht null
            if (a != null )
            {
                // Parent Knoten initialisieren 
                a.parent = pred as TreapNode;
                // Nach Priorität sortieren
                RotationHeap(a);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Macht eine Rotation nach Prinzip von Min-Heap
        /// </summary>
        /// <param name="n">Blatt eines Baumes</param>
        private void RotationHeap(TreapNode n)
        {
            // checken ob n ein Blatt
            if (n.left == null && n.right == null)
            {
                // Schleife solange parent vorhanden und priotity kleiner priority partent
                while (n.parent != null && n.priority < n.parent.priority)
                {
                    // Parent und Grandparent initialisieren
                    TreapNode parent = n.parent;
                    TreapNode grandParent = n.parent.parent;
                    // checken, ob Grandparent vorhanden
                    if (grandParent != null)
                    {
                        // Wenn Konten Parent von n links steht
                        if (grandParent.left == n.parent)
                        {
                            grandParent.left = n;
                        }
                        // rechts
                        else
                        {
                            grandParent.right = n;
                        }
                    }
                    n.parent = grandParent;

                    TreapNode right = (TreapNode)n.right; // behalte n->right
                    TreapNode left = (TreapNode)n.left; // behalte n->left

                    // Position für Parent finden
                    if (n == parent.left)
                    {
                        // n ist links
                        parent.left = right;
                        if (right != null)
                            right.parent = parent;
                        n.right = parent;
                    }
                    else
                    {
                        //n ist rechts
                        parent.right = left;
                        if (left != null)
                            left.parent = parent;
                        n.left = parent;
                    }
                    // Parent ersetzen
                    parent.parent = n;
                }
                if (n.parent == null)
                    root = n;
            }
        }

        /// <summary>
        /// Funktion um Element löschen zu können, Zahlen "durchsickern" bis Blatt
        /// </summary>
        /// <param name="n"></param>
        private void DownHeap(TreapNode n)
        {
            TreapNode left = n.left as TreapNode;
            TreapNode right = n.right as TreapNode;
            // Richtung wählen
            // Wenn linke Seite nicht null und rechte Seite gleich null -> dann nach links
            // Wenn linke Seite nicht null und rechte auch nicht und die Priorität links kleiner als rechts -> dann nach links
            bool direction = left != null && (right == null || (right != null && left.priority < right.priority));
            while (!(n.left == null && n.right == null)) // (n.left != null || n.right != null)
            {
                TreapNode next;
                // Wenn direction = true, dann links
                if (direction)
                {
                    // links von n speichern
                    next = n.left as TreapNode;
                    if (next != null)
                    {
                        n.left = next.right;
                        // tausche die Verbindungen nach rechts
                        next.right = n;
                    }
                }
                else
                {
                    // rechts von n speichern
                    next = n.right as TreapNode;
                    if (next != null)
                    {
                        n.right = next.left;
                        // tausche die Verbindungen nach links
                        next.left = n;
                    }
                }

                //Vorgänger zeigt auf Nachfolger
                if (n.parent != null)
                {
                    if (n.parent.left == n)
                    {
                        // Parent zeigt auf nächstes Element
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
                // Solange bis es keinen n.right mehr gibt
                direction = n.right == null;
            }
        }


        public override bool delete(int elem)
        {
            TreapNode a = searchNode(elem) as TreapNode;
            if (a != null)
            {
                // Verwendung von DownHeap um Element nach unten "sickern" zu lassen
                DownHeap(a);
                // wenn nur noch ein Element vorhanden ist, dann ist dieses die Wurzel
                if(a.parent == null)
                {
                    root = null;
                }
                else
                {
                    // Entweder rechts oder links die Verbindung löschen
                    if (a == a.parent.left)
                    {
                        a.parent.left = null;
                    }
                    else
                    {
                        a.parent.right = null;
                    }
                }
                return true;
            }
            return false;
        }
    }



}
