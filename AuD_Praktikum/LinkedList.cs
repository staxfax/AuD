using System;
namespace AuD_Praktikum
{
    //// Insgesamt vier Klassen für verkettete Listen

    abstract class LinkedList : IDictionary
    {
        private class LElem
        {
            public int elem;
           
            public LElem next, prev; // einfach oder doppelt verkette Liste besser??

            public LElem(int elem) { this.elem = elem; }
        }

        LElem first, last;

        // print() bei einfach und doppelt verkettet ohnehin gleich
        public void print()
        {
            for (LElem tmp = first; tmp != null; tmp = tmp.next)
                Console.WriteLine(tmp.elem);
        }

        // Suche, noch zu ergänzen: Zwicshenspeicher der Position des gefundenen Elements
        public bool search(int elem)
        {
            // Fall 1: Liste ist leer
            // Fall 2: Das gesuchte Element befindet sich am Anfang
            // Fall 3: Das gesuchte Element befindet sich am Ende
            // Fall 4: Das gesuchte Element befindet sich mittendrin

            if (first == null) // Fall 1
                return false;
            if (first.elem.CompareTo(elem) == 0) // Fall 2
                return true;
            else if (last.elem.CompareTo(elem) == 0) // Fall 3
                return true;
            else
            {
                LElem tmp = first.next;
                while (tmp.next != null && tmp.elem.CompareTo(elem) != 0)
                    tmp = tmp.next;
                if (tmp.next != null)
                    return true;
                else
                    return false;
            }
        }

        // Löschen
        public bool delete(int elem)
        {
            search(elem);
            return false;
        }

        // Einfügen, wird Listen spezifisch implementiert, da nicht bei allen gleich
        public abstract bool insert(int elem);


    }

    //class SetSortedLinkedList : LElem
    //{
    //    public override bool insert(int elem)
    //    {
    //        return false;
    //    }
    //}

    //class SetUnsortedLinkedList : LElem
    //{
    //    public override bool insert(int elem)
    //    {
    //        return false;
    //    }
    //}

    //class MultiSetSortedLinkedList : LElem
    //{
    //    public override bool insert(int elem)
    //    {
    //        return false;
    //    }
    //}

    //class MultiSetUnsortedLinkedList : LElem
    //{
    //    public override bool insert(int elem)
    //    {
    //        return false;
    //    }
    //}

}
