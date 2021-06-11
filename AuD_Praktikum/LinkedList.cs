using System;
namespace AuD_Praktikum
{
    abstract class LinkedList : IDictionary
    {
        protected class LElem
        {
            public int elem;

            public LElem next = null;
            //public LElem prev = null;

            public LElem(int elem) { this.elem = elem; }
        }

        protected LElem first = null, last = null, position = null;

        //public int? index; // Index zur Zwischenspeicherung der Position nach Suche

        protected int count; // Anzahl der Elemente in der Liste

        // print bei einfach und doppelt verkettet ohnehin gleich
        public void print()
        {
            Console.WriteLine($"Anzahl der Elemente: {count}");
            for (LElem tmp = first; tmp != null; tmp = tmp.next)
                Console.WriteLine(tmp.elem);
        }

        // Suche
        public bool search(int elem)
        {
            position = null;
            if (first == null) // Liste ist leer
            {
                //position = null;
                return false;
            }
            else if (first.elem.CompareTo(elem) == 0) // Das gesuchte Element befindet sich am Anfang
            {
                position = first;
                return true;
            }
            else // Das gesuchte Element befindet sich mittendrin ODER am Ende
            {
                LElem tmp = first;
                while (tmp.elem.CompareTo(elem) != 0) // 0: tmp.elem = elem -> Suche gibt Position des jeweils HINTERSTEN elem zurück, nach dem dann eingefügt wird
                {
                    if (tmp.elem.CompareTo(elem) < 1) // Falls elem nicht vorhanden, wird zusätzlich Stelle des größten kleineren elem gespeichert (nötig für Sorted insert)
                        position = tmp;
                    if (tmp.next == null)
                        break;
                    tmp = tmp.next;
                }
                if (tmp.next != null) // While stoppte obwohl Liste nicht zu Ende -> true
                {
                    //position = tmp;
                    return true;
                }
                else if (last.elem.CompareTo(elem) == 0) // Das gesuchte Element befindet sich am Ende
                {
                    position = last;
                    return true;
                }
                else // Das gesuchte Element ist nicht vorhanden
                {
                    return false;
                }
            }
        }

        // Löschen
        // löscht das jeweils 'vorderste' gefundene Element
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
                    if (position == first) // Erstes Element löschen
                    {
                        first = first.next;
                        //first.prev = null;
                        count--;
                        return true;
                    }
                    else if (position != last) // Element mittendrin löschen
                    {
                        //position.prev.next = position.next;
                        //position.next.prev = position.prev;

                        position.next.next = position.next.next.next;
                        count--;
                        return true;
                    }
                    else // Letztes Element löschen
                    {
                        //last = last.prev;

                        LElem tmp = first;
                        while (tmp.next != position)
                            tmp = tmp.next;
                        tmp.next = tmp.next.next;
                        position.next = null;
                        last = position;
                        count--;
                        return true;
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
            search(elem);
            //142175
            if (first == null) // Liste leer
            {
                first = last = nelem;
                count++;
                return true;
            }
            else if (count == 1) // Liste hat nur ein Element
            {
                if (elem > last.elem) // Add End
                {
                    last.next = nelem;
                    last = last.next;
                    count++;
                    return true;
                }
                else // (elem <= last.elem) Add Front
                {
                    nelem.next = first;
                    first = nelem;
                    count++;
                    return true;
                }
            }
            //else if (position == first) // Am Anfang einfügen
            //{
            //    nelem.next = first;
            //    //first.prev = nelem;
            //    first = nelem;
            //    count++;
            //    return true;
            //}
            else if (position != last) // Mittendrin einfügen - Add After position
            {
                nelem.next = position.next; // Vorwärtsverkettung vom neuen Element zur Restliste
                //nelem.prev = position;      // Rückwärtsverkettung vom neuen Elenent zum Listenanfangsstück
                //position.next.prev = nelem; // Rückwärtsverkettung vom nachfolgenden Listenelement
                position.next = nelem;      // Vorwärtsverkettung zum neuen Element
                count++;
                return true;
            }
            else // Am Ende einfügen
            {
                last.next = nelem;
                //nelem.prev = last;
                last = last.next;
                count++;
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
                count++;
                return true;
            }
            else // Liste nicht leer -> AddEnd
            {
                last.next = nelem;
                //nelem.prev = last;
                last = last.next;
                count++;
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