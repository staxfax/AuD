using System;

namespace AuD_Praktikum
{

    abstract class Array
    {
        public int SIZE = 10;
        public int[] myArray;

        public Array()
        {
            myArray = new int[SIZE] ;
            
        }

        public void print()
        {
            for (int i = 0; i < SIZE; i++)
            {
                    Console.WriteLine(myArray[i]);
                //if (myArray[i] == 0)
                    //Console.WriteLine("is 0 oida");

                //else
                    //Console.WriteLine("basst eh");

            }

            
        }
    }

    class SetSortedArray : MultiSetSortedArray, ISetSorted
    {
        public override bool insert(int elem)
        {


            /*int i = _search_(elem);
            int pos = 0;
            if (i == -1)
            {
                for (int j = SIZE - 1; j == pos; j--)
                {
                    myArray[j] = myArray[j - 1];
                }

                myArray[pos] = elem;
                return true;
            }

            return false;*/

            int ausprobieren = _inserthelp_(elem, false);
            Console.WriteLine(ausprobieren);

            if(ausprobieren == -1)
            {
                return false;
            }



            for (int j = SIZE - 1; j > ausprobieren; j--)
            {
                myArray[j] = myArray[j - 1];
                Console.WriteLine(j + " to " + (j - 1));
            }
            //Console.WriteLine(ausprobieren);
            //myArray[ausprobieren - 1] = myArray[ausprobieren];
            myArray[ausprobieren] = elem;
            return true;

        }
    }

    class SetUnsortedArray : MultiSetUnsortedArray, ISetUnsorted
    {
        public override bool insert(int elem)
        {

            if (search(elem))
                return false;

            else
                return base.insert(elem);
        }


    }

    class MultiSetSortedArray : Array, IMultiSetSorted
    {
        //protected static int pos = 0;//-1;

        public virtual bool insert(int elem)
        {
           // if (myArray[0] == 0)
            /*{
                myArray[0] = elem;
                return true;
            }*/

            //else
            
                int ausprobieren = _inserthelp_(elem, true);
                //Console.WriteLine(i);
                




                for (int j = SIZE-1; j > ausprobieren; j--)
                {
                    myArray[j] = myArray[j-1];
                    Console.WriteLine(j + " to " + (j - 1));
                }
                //Console.WriteLine(ausprobieren);
                //myArray[ausprobieren - 1] = myArray[ausprobieren];
                myArray[ausprobieren] = elem;
                return true;
            
        }

        public int pos(int elem)
        {

            for (int i = 0; i < SIZE-1; i++)
            {
                if (myArray[i] == elem)
                    return i;

                else if (myArray[i] > elem)
                    return i;
            }

            return SIZE - 1;
        }


        public bool search(int elem)
        {

            int i = _search_(elem);

            if (i > -1)
                return true;
            else
                return false;
        }

        protected int _search_(int elem)
        {
            int l = 0;
            int r = SIZE - 1;
            int i = 0;

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
            } while (myArray[i] != elem && l <= r);

            if (myArray[i] == elem)
                return i;

            else
                return -1;
        }
        protected int _inserthelp_(int elem, bool multiset)
        {
            int l = 0;
            int r = SIZE - 1;
            //Console.WriteLine(r);
            int i = 0;

            if(multiset==false && myArray[i]==elem)
            {
                return -1;
            }

            while( l < r )//do
            {
                //Console.WriteLine(r+"=r"+l+"=l"+i+"=i");
                i = ((l + r) / 2);

                if( myArray[i] == 0 || myArray[i] > elem )
                {
                    r = i - 1;
                }

                else
                {
                    l = i + 1;
                }

                if( myArray[i] == elem )
                {
                    if (multiset)
                    {
                        return i;
                    }

                    else
                    {
                        return -1;
                    }
                    //return i;
                }

                if( r == l )
                {
                    while( myArray[i] > elem )
                    {
                        i--;

                        if( i < 0)
                        {
                            i++;
                            break;
                        }
                    }

                    if(myArray[i] == 0)
                    {
                        i--;
                    }

                    while( myArray[i] < elem && myArray[i] != 0 )
                    {
                        i++;

                        if( i == SIZE)
                        {
                            i--;
                            break;
                        }
                    }

                    return i;
                }
                /*if (myArray[i] < elem)
                {
                    l = i + 1;
                }

                else
                {
                    r = i - 1;
                }*/

                
                
            } //while (myArray[i] == elem || l > r);
            Console.WriteLine("i=" + i + " l=" + l + " r=" + r + " elem=" + elem);

            return i;
            //if (myArray[i] == elem)
               // return i + 1;//l++;

            //return l-1;

           /* else if (myArray[i] < elem)
            {
                if (l < r)
                    return i - 1;

                return i + 1;
            }
            else
                return i - 1;*/
            /*{


                while (l < r && myArray[i] == 0)
                {
                    l++;
                    i = (l + r) / 2;
                }
            }*/
            //else
                //return l; 
        }

        public bool delete(int elem)
        {
            int i = _search_(elem);

            if (i == -1)
            {
                
                Console.WriteLine("der hund gibt -1 zruck");
                return false;
            }

            for (int j = i;  j < SIZE-1; j++)
            {
                myArray[j] = myArray[j + 1];
                Console.WriteLine(j + 1 + "to" + j);
            }

            myArray[SIZE - 1] = 0;

            return true;


        }
    }


    class MultiSetUnsortedArray : Array, IMultiSetUnsorted
    {
        public virtual bool insert(int elem)
        {
            for (int i = 0; i < SIZE; i++)
            {
                if (myArray[i] == 0)
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

            int j = 0;

            
            while(myArray[j]!=0)
            {
                j++;

                if (j == SIZE)
                    break;
            }

            j--;

            myArray[i] = myArray[j];
            myArray[j] = 0;
            /*for (int j = 0; j < SIZE; j++)
            {
                if (myArray[j] == 0)
                {
                    myArray[i] = myArray[j--];
                    return true;
                }
            }*/

            return false;
        }
    }
}