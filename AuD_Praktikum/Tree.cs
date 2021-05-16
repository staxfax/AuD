﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AuD_Praktikum
{
   
    class BinTreeNode 
    {
        public int zahl;
        public BinTreeNode left;
        public BinTreeNode right;
        public override string ToString()
        {
            return $"{zahl,3}";
        }
    }

    /// <summary>
    /// Representiert eine generic <see cref="BinSearchTree{T}"/> Klasse 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class BinSearchTree : ISetSorted
    {
        protected BinTreeNode root;
        protected BinTreeNode pred; //Ablagevariable für Vorgängerknoten
        protected string dir; //Richtung, in der a von pred aus gesehen hängt

        /// <summary>
        /// offizielle Suchfunktion, enthält searchNode
        /// </summary>
        /// <param name="elem">zu suchendes Element</param>
        /// <returns>true, wenn Element gefunden</returns>
        public bool search(int elem)
        {
            BinTreeNode a = searchNode(elem);
            if (a != null)
                return true;
            else
                return false;

        }

        /// <summary>
        /// Suchfunktion der Klasse, legt pred und dir in Klassenvariable ab
        /// </summary>
        /// <param name="elem">zu suchendes Element</param>
        /// <returns>gefundener Knoten oder null, wenn nicht gefunden</returns>
        protected BinTreeNode searchNode(int elem)
        {
            BinTreeNode a = root;
            if (a == null || a.zahl == elem)
            {
                dir = "root";
                pred = null;
            }
            while (a != null)
            {
                if (elem < a.zahl)
                {
                    pred = a;
                    dir = "left";
                    a = a.left;
                }
                else if (elem > a.zahl)
                {
                    pred = a;
                    dir = "right";
                    a = a.right;
                }
                else
                    break;
            }
            return a;
        }

        /// <summary>
        /// offizielle Einfügefunktion, ruft insert_ auf
        /// </summary>
        /// <param name="elem">einzufügendes Element</param>
        /// <returns>true, wenn Element eingefügt wurde</returns>
        public virtual bool insert(int elem)
        {
            BinTreeNode a = insert_(elem);
            if (a == null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Hilfseinfügefunktion, enthält searchNode und insertNode
        /// </summary>
        /// <param name="elem">einzufügendes Element</param>
        /// <returns>eingefügter Knoten oder null, wenn Element schon vorhanden</returns>
        protected BinTreeNode insert_(int elem)
        {
            BinTreeNode a = searchNode(elem);
            if (a != null)
                return null;
            a = insertNode(elem);
            if (dir == "root")
                root = a;
            else if (dir == "left")
                pred.left = a;
            else
                pred.right = a;
            return a;
        }

        /// <summary>
        /// offizielle Löschfunktion, enthält searchNode
        /// </summary>
        /// <param name="elem">zu löschendes Element</param>
        /// <returns>true, wenn Element gelöscht wurde</returns>
        public virtual bool delete(int elem)
        {
            BinTreeNode a = searchNode(elem);
            BinTreeNode b;      
            if (a == null)
                return false;
            if (a.right != null && a.left != null)
            {
                BinTreeNode c;
                b = a;
                if (b.left.right != null)
                {
                    b = b.left;
                    while (b.right.right != null)
                        b = b.right;
                }
                if (b == a)
                {
                    c = b.left;
                    b.left = c.left;
                }
                else
                {
                    c = b.right;
                    b.right = c.left;
                }
                a.zahl = c.zahl;
                return true;
            }
            if (a.left == null)
                b = a.right;
            else
                b = a.left;
            if (root == a)
            {
                root = b;
                return true;
            }
            if (dir == "left")
                pred.left = b;
            else
                pred.right = b;
            return true;

        }

        /// <summary>
        /// offizielle Print-Funktion
        /// </summary>
        public void print()
        {
            treePrint(root, 0);
            Console.WriteLine();
        }

        /// <summary>
        /// Funktion zum Einfügen eines neuen Knotens, sollte in abgeleiteten Klassen überschrieben werden
        /// </summary>
        /// <param name="elem">Wert des Knotens</param>
        /// <returns>eingefügter Knoten</returns>
        protected virtual BinTreeNode insertNode(int elem)
        {
            BinTreeNode a = new BinTreeNode();
            a.zahl = elem;
            a.left = null; a.right = null;
            return a;
        }

        /// <summary>
        /// rekursive Funktion zur Ausgabe eines Baums
        /// </summary>
        /// <param name="a">aktueller Knoten</param>
        /// <param name="level">Höhe im Baum (0: Wurzel)</param>
        protected void treePrint(BinTreeNode a, int level)
        {
            if (a != null)
            {
                treePrint(a.right, level + 1);
                indentPrint(a, level);
                treePrint(a.left, level + 1);
            }
        }

        /// <summary>
        /// Funktion zur Ausgabe eines Baumknotens, sollte evtl. in abgeleiteten Funktionen überschrieben werden
        /// </summary>
        /// <param name="a">Baumknoten</param>
        /// <param name="level">Höhe im Baum</param>
        protected void indentPrint(BinTreeNode a, int level)
        {
            if (level == 0)
                Console.WriteLine(a);
            else if (level == 1)
                Console.WriteLine("    -- " + a);
            else
            {
                for (int i = 0; i < level - 1; i++)
                {
                    Console.Write("       ");
                }
                Console.WriteLine("    -- " + a);
            }
        }
    }
}
