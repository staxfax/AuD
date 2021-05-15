using System;
namespace AuD_Praktikum
{
    // Insgesamt vier Klassen für verkettete Listen

    abstract class LinkedList : IDictionary
    {
        private class LElem
        {
            public int elem;

            //private int index;
            //public int Index
            //{
            //    get { return index; }
            //    set { index = value; }
            //}

            public LElem next; // einfach oder doppelt verkette Liste besser??

            public LElem(int elem) { this.elem = elem; }
        }

        LElem first, last;

        public int? index; // Index zur Zwischenspeicherung der Position nach Suche

        public int count; // Anzahl der Elemente in der Liste

        // LElem tmp; tmp von überall zugreifbar machen, eliminiert Index Zwang??

        // print bei einfach und doppelt verkettet ohnehin gleich
        public void print()
        {
            for (LElem tmp = first; tmp != null; tmp = tmp.next)
                Console.WriteLine(tmp.elem);
        }

        // Suche
        public bool search(int elem)
        {
            // Fall 1: Liste ist leer
            // Fall 2: Das gesuchte Element befindet sich am Anfang
            // Fall 3: Das gesuchte Element befindet sich mittendrin
            // Fall 4: Das gesuchte Element befindet sich am Ende

            if (first == null) // Fall 1
            { 
                index = null;
                return false;
            }
            if (first.elem.CompareTo(elem) == 0) // Fall 2
            { 
                index = 0;
                return true;
            }
            else if(last.elem.CompareTo(elem) != 0) // Fall 3
            { 
                LElem tmp = first.next;
                index = -1;
                while (tmp.next != null && tmp.elem.CompareTo(elem) != 0)
                {
                    tmp = tmp.next;
                    index++;
                }
                if (tmp.next != null)
                    return true;
                else
                {
                    index = null;
                    return false;
                }
            }
            else // Fall 4
            { 
                index = count - 1;
                return true;
            }


        }

        // Löschen
        // löscht (aktuell) nur das erste gefundene Element
        public bool delete(int elem)
        {
            if (search(elem) == true)
            {
                if (first == last) // Nur ein Element
                {
                    first = last = null;
                    count--;
                    return true;
                }
                else // Mindestens zwei Elemente
                {
                    if (index == 0) // Erstes Element löschen
                    {
                        first = first.next;
                        count--;
                        return true;
                    }
                    else if (index < count - 1) // Element in der Mitte löschen
                    {
                        LElem tmp = first.next;
                        while (tmp.next != null && tmp.elem.CompareTo(elem) != 0) // macht irgendwie das Selbe wie search() --> schlauer lösbar????
                        {
                            tmp = tmp.next;
                        }
                        tmp.next = tmp.next.next;
                        count--;
                        return true;
                    }
                    else if (index == count - 1) // Letztes Element löschen
                    {
                        LElem secondlast = first;
                        while (secondlast.next != last)
                        {
                            secondlast = secondlast.next;
                        }
                        secondlast.next = null;
                        last = secondlast; // Neues Ende
                        count--;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else { return false; }
        }

        // Einfügen, wird Listen spezifisch implementiert, da nicht bei allen gleich
        public abstract bool insert(int elem);


    }

    class MultiSetSortedLinkedList : LinkedList
    {
        public override bool insert(int elem)
        {
            return false;
        }
    }

    class MultiSetUnsortedLinkedList : LinkedList
    {
        public override bool insert(int elem)
        {
            return false;
        }
    }

    class SetSortedLinkedList : MultiSetSortedLinkedList
    {
        public override bool insert(int elem)
        {
            return false;
        }
    }

    class SetUnsortedLinkedList : MultiSetUnsortedLinkedList
    {
        public override bool insert(int elem)
        {
            return false;
        }
    }


}
