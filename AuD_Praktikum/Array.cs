using System;

namespace AuD_Praktikum
{

    abstract class Array
    {
        public int SIZE = 10;
        public int[] myArray;

        public Array()
        {
            myArray = new int[SIZE];
        }

        public void print()
        {
            for (int i = 0; i < SIZE; i++)
            {
                Console.WriteLine(myArray(i));
            }
        }
    }

    class SetSortedArray : MultiSetSortedArray, ISetsorted
    {
        public override bool insert(int elem)
        {


            int i = _search_(elem);

            if (i == -1)
            {
                for (int j = SIZE - 1; j == pos; j--)
                {
                    myArray[j] = myArray[j - 1];
                }

                myArray[pos] = elem;
                return true;
            }

            return false;



        }
    }

    class SetUnsortedArray : MultiSetUnsortedArray, ISetUnsorted
    {
        public bool insert(int elem)
        {

            if (search(elem))
                return false;

            else
                return base.insert(elem);
        }


    }

    class MultiSetSortedArray : Array, IMultiSetSorted
    {
        static int pos = -1;

        public bool insert(int elem)
        {
            int i = _search_(elem);

            if (i == -1)
            {
                i = pos;
            }




            for (int j = SIZE - 1; j == i; j--)
            {
                myArray[j] = myArray[j - 1];
            }

            myArray[i] = elem;
            return true;

        }




        public bool search(int elem)
        {

            int i = _search_(elem);

            if (i > -1)
                return true;
            else
                return false;
        }

        private int _search_(int elem)
        {
            int l = 0;
            int r = SIZE - 1;
            int i;

            do
            {
                i = (l + r) / 2;

                if (myArray[i] < elem)
                {
                    l = i + 1;
                }

                else
                {
                    r = i - 1;
                }

                pos = i;

            } while (myArray[i] != elem && l <= r)

            if (myArray[i] == elem)
                return i;

            else
                return -1;
        }

        public bool delete(int elem)
        {
            int i = _search_(elem);

            if (i == -1)
                return false;


            for (int j = i; myArray[j] == null || j == SIZE; j++)
            {
                myArray[j] = myArray[j + 1];
            }


            return true;


        }
    }
}

class MultiSetUnsortedArray : Array, IMultiSetUnsorted
{
    public bool insert(int elem)
    {
        for (int i = 0; i < SIZE; i++)
        {
            if (myArray[i] == null)
            {
                myArray[i] = elem;
                return true;
            }
        }

        return false;
    }

    public bool search(int elem)
    {

        int i = _search_(elem);

        if (i > -1)
            return true;
        else
            return false;
    }

    private int _search_(int elem)
    {
        int i = 0;

        while (myArray[i] != elem)
        {
            i++;

            if (i == SIZE)
                return -1;
        }

        return i;
    }

    public bool delete(int elem)
    {
        int i = _search_(elem);

        if (i == -1)
            return false;


        for (int j = 0; j < SIZE; j++)
        {
            if (myArray[j] == null)
            {
                myArray[i] = myArray[j--];
                return true;
            }
        }

        return false;
    }
}
}