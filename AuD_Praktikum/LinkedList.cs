using System;
namespace AuD_Praktikum
{
    //// Insgesamt vier Klassen für verkettete Listen

    //abstract class LElem : IDictionary
    //{
    //    protected string elem;
    //    protected LElem next, prev; // einfach oder doppelt verkette Liste??

    //    protected LElem(string elem) { this.elem = elem; }

    //    LElem first, last;

    //    // print() bei einfach und doppelt verkettet ohnehin gleich
    //    void IDictionary.print()
    //    {
    //        for (LElem tmp = first; tmp != null; tmp = tmp.next)
    //            Console.WriteLine(tmp.elem);
    //    }

    //    bool IDictionary.search(int elem)
    //    {
    //        // Fall 1: Liste ist leer
    //        // Fall 2: Das gesuchte Element befindet sich am Anfang
    //        // Fall 3: Das gesuchte Element befindet sich am Ende
    //        // Fall 4: Das gesuchte Element befindet sich mittendrin

    //        if (first == null) // Fall 1
    //            return false;
    //        if (first.elem.CompareTo(elem) == 0) // Fall 2
    //            return true;
    //        else if (last.elem.CompareTo(elem) == 0) // Fall 3
    //            return true;
    //        else
    //        {
    //            LElem tmp = first.next;
    //            while (tmp.next != null && tmp.elem.CompareTo(elem) != 0)
    //                tmp = tmp.next;
    //            if (tmp.next != null)
    //                return true;
    //            else
    //                return false;
    //        }
    //    }

    //    //bool IDictionary.delete(int elem)
    //    //{
    //    //    IDictionary.search(elem);
    //    //}

    //    //abstract bool IDictionary.insert(int elem) { }


    //}


    //class SetSortedLinkedList : ISetSorted
    //{

    //}

    //class SetUnsortedLinkedList : ISetUnsorted
    //{

    //}

    //class MultiSetSortedLinkedList : IMultiSetSorted
    //{

    //}

    //class MultiSetUnsortedLinkedList : IMultiSetUnsorted
    //{

    //}

}
