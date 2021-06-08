using System;

namespace AuD_Praktikum
{
    abstract class Hash : ISetUnsorted
    {
        public int tabGroeße = 12;
        public HashElement[] hashTab;

        public Hash()
        {
            hashTab = new HashElement[tabGroeße];
        }
        public Hash(int gewuenschteGroeße)
        {
            tabGroeße = gewuenschteGroeße;
            hashTab = new HashElement[tabGroeße];
        }
        public abstract bool search(int elem);
        public abstract bool insert(int elem);
        public abstract bool delete(int elem);
        public abstract void print();
    }

    class HashElement
    {
        public int element { get; set; }
        public HashElement nachfolger { get; set; }

        public HashElement(int elem)
        {
            element = elem;
            nachfolger = null;
        }
    }

    class HashTabSepChain : Hash
    {
        public HashTabSepChain() : base() { }
        public HashTabSepChain(int tabGroeße) : base(tabGroeße) { }

        public override bool insert(int elem)
        {
            HashElement einzufügen = new HashElement(elem);
            int pos = getVertikalePos(elem);
            HashElement eingefügeStelle = hashTab[pos];
            if (eingefügeStelle == null)
            {
                hashTab[pos] = einzufügen;
                return true;
            }
            else
            {
                while (eingefügeStelle.nachfolger != null)
                {
                    eingefügeStelle = eingefügeStelle.nachfolger;
                }
                eingefügeStelle.nachfolger = einzufügen;
                return true;
            }
        }

        public override bool search(int elem)
        {
            var (_, aktuelles) = getHorizontalePos(elem);

            if (aktuelles == null)
            {
                return false;
                //new ArgumentOutOfRangeException($"Das Element {elem} wurde nicht gefunden!")
            }
            else
            {
                return true;
            }
        }

        public override bool delete(int elem)
        {
            int pos = getVertikalePos(elem);
            var (vorgänger, aktuelles) = getHorizontalePos(elem);
            if (aktuelles == null)
            {
                return false;
            }
            if (vorgänger == null)
            {
                hashTab[pos] = null;
                return true;
            }
            vorgänger.nachfolger = aktuelles.nachfolger;
            return true;
        }

        public override void print()
        {
            HashElement laufvariable;
            for (int i = 0; i < tabGroeße; i++)
            {
                if (hashTab[i] == null)
                {
                    Console.WriteLine("/");
                }
                else
                {
                    Console.Write($"{hashTab[i].element} ");
                    laufvariable = hashTab[i];
                    while (laufvariable.nachfolger != null)
                    {
                        Console.Write($"-> {laufvariable.nachfolger.element} ");
                        laufvariable = laufvariable.nachfolger;
                    }
                    Console.WriteLine();
                }
            }
        }

        public int getVertikalePos(int elem)
        {
            if (elem < 0)
            {
                new ArgumentOutOfRangeException($"Element muss positiv oder null sein! {elem} < 0!");
                return -1;
            }
            else
            {
                int pos = elem % (tabGroeße - 1);
                return pos;
            }
        }

        public (HashElement vorgänger, HashElement aktuelles) getHorizontalePos(int elem)
        {
            int pos = getVertikalePos(elem);
            HashElement gesuchtes = hashTab[pos];
            HashElement vorgänger = null;

            while (gesuchtes != null)
            {
                if (gesuchtes.element == elem)
                {
                    return (vorgänger, gesuchtes);
                }
                else
                {
                    vorgänger = gesuchtes;
                    gesuchtes = gesuchtes.nachfolger;
                }
            }
            return (null, null);
        }

    }

    class HashTabQuadProb : Hash
    {
        public HashTabQuadProb() : base() { }
        public HashTabQuadProb(int tabGroeße) : base(tabGroeße) { }

        public override bool insert(int elem)
        {
            HashElement einzufügen = new HashElement(elem);
            int i = 0;
            int pos;
            while (1 > 0)
            {
                pos = getHorizontalePosPlus(elem, i);
                if (hashTab[pos] == null)
                {
                    hashTab[pos] = einzufügen;
                    return true;
                }

                pos = getHorizontalePosMinus(elem, i);
                if (hashTab[pos] == null)
                {
                    hashTab[pos] = einzufügen;
                    return true;
                }
                i++;

            }

        }

        public override bool search(int elem)
        {
            int i = 0;
            int pos;
            while (1 > 0)
            {
                pos = getHorizontalePosPlus(elem, i);
                if (hashTab[pos].element == elem)
                {
                    return true;
                }
                pos = getHorizontalePosMinus(elem, i);
                if (hashTab[pos].element == elem)
                {
                    return true;
                }
                i++;
            }
        }

        public override bool delete(int elem)
        {
            int i = 0;
            int pos;
            while (1 > 0)
            {
                pos = getHorizontalePosPlus(elem, i);
                if (hashTab[pos].element == elem)
                {
                    hashTab[pos] = null;
                    return true;
                }
                pos = getHorizontalePosMinus(elem, i);
                if (hashTab[pos].element == elem)
                {
                    hashTab[pos] = null;
                    return true;
                }
                i++;
            }
        }

        public override void print()
        {
            for (int i = 0; i < tabGroeße; i++)
            {
                if (hashTab[i] == null)
                {
                    Console.WriteLine("/");
                }
                else
                {
                    Console.WriteLine(hashTab[i].element);
                }
            }
        }

        public int getHorizontalePosPlus(int elem, int i)
        {
            int umrechner = elem;
            int pos;

            while (umrechner < 0)
            {
                umrechner = umrechner + (tabGroeße - 1);
            }
            pos = (umrechner + (i * i)) % (tabGroeße - 1);
            return pos;
        }

        public int getHorizontalePosMinus(int elem, int i)
        {
            int umrechner = elem;
            int pos;

            while (umrechner < 0)
            {
                umrechner = umrechner + (tabGroeße - 1);
            }
            pos = (umrechner - (i * i)) % (tabGroeße - 1);
            return pos;
        }
    }
}
//Abbruchkriterium für search bei quadratischer Sondierung fehlt noch
//        "         "  insert  "       "             "       "     "
//        "         "  delete  "       "             "       "     "
//Verknüpfung aus Main fehlt noch 