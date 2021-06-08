using System;
namespace AuD_Praktikum
{
    abstract class LinkedList : IDictionary
    {
        protected class LElem
        {
            public int elem;

            public LElem next = null;
            public LElem prev = null;

            public LElem(int elem) { this.elem = elem; }
        }

        protected LElem first, last;

        protected LElem position; // Position des Elements VOR gefundenen Elements, null falls nicht gefunden

        //public int? index; // Index zur Zwischenspeicherung der Position nach Suche

        protected int count; // Anzahl der Elemente in der Liste

        // print bei einfach und doppelt verkettet ohnehin gleich
        public void print()
        {
            for (LElem tmp = first; tmp != null; tmp = tmp.next)
                Console.WriteLine(tmp.elem);
        }

        // Suche
        public bool search(int elem)
        {
            if (first == null) // Liste ist leer
            {
                position = null;
                return false;
            }
            if (first.elem.CompareTo(elem) == 0) // Das gesuchte Element befindet sich am Anfang
            {
                position = first.prev; // Das Element vor gesuchtem Element
                return true;
            }
            else if (last.elem.CompareTo(elem) != 0) // Das gesuchte Element befindet sich mittendrin
            {
                LElem tmp = first.next;
                while (tmp.next != null && tmp.elem.CompareTo(elem) != 0)
                {
                    tmp = tmp.next;
                }
                if (tmp.next != null)
                {
                    position = tmp.prev; // Das Element vor gesuchtem Element
                    return true;
                }
                else
                {
                    position = null;
                    return false;
                }
            }
            else // Das gesuchte Element befindet sich am Ende
            {
                position = last.prev; // Das Element vor gesuchtem Element
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
                    if (position.prev == null) // Erstes Element löschen
                    {
                        first = first.next;
                        first.prev = null;
                        count--;
                        return true;
                    }
                    else if (position.prev != null && position.next != null) // Element in der Mitte löschen
                    {
                        position.prev.next = position.next;
                        position.next.prev = position.prev;
                        count--;
                        return true;
                    }
                    else if (position.next == null) // Letztes Element löschen
                    {
                        last = last.prev;
                        last.next = null;
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

    // Insgesamt vier Klassen für verkettete Listen

    class MultiSetSortedLinkedList : LinkedList
    {
        public override bool insert(int elem)
        {
            LElem nelem = new LElem(elem);
            if (position.prev == null) // Gesuchtes Element ist erstes
            {
                if (first == null) // Liste hat nur ein Element
                {
                    first = last = nelem;
                    count += 1;
                    return true;
                }
                else // Liste hat mehr als ein Element
                {
                    nelem.next = first;
                    first.prev = nelem;
                    first = nelem;
                    count += 1;
                    return true;
                }
            }
            else if (position.next == null) // Gesuchtes Element ist letztes
            {
                if (first == null) // Liste hat nur ein Element
                {
                    first = last = nelem;
                    count += 1;
                    return true;
                }
                else  // Liste hat mehr als ein Element
                {
                    last.next = nelem;
                    nelem.prev = last;
                    last = nelem;
                    count += 1;
                    return true;
                }
            }
            else // Gesuchtes Element ist in der Mitte
            {
                search(elem);
                nelem.next = position.next; // Vorwärtsverkettung vom neuen Element zur Restliste
                nelem.prev = position;      // Rückwärtsverkettung vom neuen Elenent zum Listenanfangsstück
                position.next.prev = nelem; // Rückwärtsverkettung vom nachfolgenden Listenelement
                position.next = nelem;      // Vorwärtsverkettung zum neuen Element
                count += 1;
                return true;
            }
        }
    }

    class MultiSetUnsortedLinkedList : LinkedList
    {
        public override bool insert(int elem)
        {
            LElem nelem = new LElem(elem);
            if (first == null) // Liste leer
            {
                first = last = nelem;
                count += 1;
                return true;
            }
            else // Liste nicht leer -> AddEnd
            {
                last.next = nelem;
                nelem.prev = last;
                last = nelem;
                count += 1;
                return true;

            }
        }
    }

    class SetSortedLinkedList : MultiSetSortedLinkedList
    {
        public override bool insert(int elem)
        {
            if (search(elem) == false)
            {
                base.insert(elem);
                return true;
            }
            else
                return false;
        }
    }

    class SetUnsortedLinkedList : MultiSetUnsortedLinkedList
    {
        public override bool insert(int elem)
        {
            if (search(elem) == false) // Element noch nicht vorhanden
            {
                base.insert(elem);
                return true;
            }
            else // Element bereits vorhanden
                return false;
        }
    }


}
