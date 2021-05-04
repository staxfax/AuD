using System;

namespace AuD_Praktikum
{

    //   abstract class Array
    //   {
    //       public int SIZE = 10;
    //       public int[] myArray;

    //       public Array()
    //       {
    //           myArray = new int[SIZE];
    //       }

    //       public void print()
    //       {
    //           for (int i = 0; i < SIZE; i++)
    //           {
    //               Console.WriteLine(myArray(i));
    //           }
    //       }
    //   }

    //   class SetSortedArray : MultiSetSortedArray, ISetsorted
    //   {

    //   }

    //class SetUnsortedArray : MultiSetUnsortedArray, ISetUnsorted
    //   {
    //       public bool insert(int elem)
    //       {

    //           if (search(elem))
    //               return false;

    //           else
    //              return base.insert(elem);
    //       }


    //   }

    //class MultiSetSortedArray : Array, IMultiSetSorted
    //   {

    //   }

    //class MultiSetUnsortedArray : Array, IMultiSetUnsorted
    //   {
    //       public bool insert(int elem)
    //       {
    //           for (int i = 0; i < SIZE; i++)
    //           {
    //               if (myArray[i] == null)
    //               {
    //                   myArray[i] = elem;
    //                   return true;
    //               } 
    //           }

    //           return false;
    //       }

    //       public bool search(int elem)
    //       {

    //           int i = _search_(elem);

    //           if (i > -1)
    //               return true;
    //           else
    //               return false;
    //       }

    //       private int _search_(int elem)
    //       {
    //           int i = 0;

    //           while (myArray[i] != elem)
    //           {
    //               i++;

    //               if (i == SIZE)
    //                   return -1;
    //           }

    //           return i;
    //       }

    //       public bool delete(int elem)
    //       {
    //           int i = _search_(elem);

    //           if (i == -1)
    //               return false;


    //           for (int j = 0; j < SIZE; j++)
    //           {
    //               if (myArray[j] == null)
    //               {
    //                   myArray[i] = myArray[j--];
    //                   return true;
    //               }
    //           }

    //           return false;
    //       }
    //  }
}