using System;

namespace AuD_Praktikum
{
    abstract class Hash : ISetUnsorted      // abstrakte Klasse mit Konstruktoren
    {
        public int tabGroeße = 12;
        public HashElement[] hashTab;

        public Hash()                      // Konstruktor 1, falls keine TabGröße gewünscht, Standardgröße 12
        {
            hashTab = new HashElement[tabGroeße];
        }
        public Hash(int gewuenschteGroeße) // Konstruktor 2, falls TabGröße gewünscht
        {
            tabGroeße = gewuenschteGroeße;
            hashTab = new HashElement[tabGroeße];
        }
        public abstract bool search(int elem);   // abstrakte Methoden aus ISetUnsorted bzw IDictionary
        public abstract bool insert(int elem);
        public abstract bool delete(int elem);
        public abstract void print();
    }

    class HashElement                    // Klasse für Hashelemente, v.a. für Verkettung wichtig -> einfache Liste
    {
        public int element { get; set; }
        public HashElement nachfolger { get; set; }

        public HashElement(int elem)
        {
            element = elem;
            nachfolger = null;
        }
    }

    class HashTabSepChain : Hash        // Klasse für separate Verkettung
    {
        public HashTabSepChain() : base() { }                        // Konstruktoren aus class Hash
        public HashTabSepChain(int tabGroeße) : base(tabGroeße) { }

        public override bool insert(int elem)                // Einfügemethode
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

        public override bool search(int elem)            // Suchmethode
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

        public override bool delete(int elem)         // Löschmethode
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

        public override void print()                 //Darstellmethode
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

        public int getVertikalePos(int elem)      // Methode zum bestimmen der "vertikalen" Position, also der Position in der Hash Tabelle
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

        public (HashElement vorgänger, HashElement aktuelles) getHorizontalePos(int elem)   // Methode zum Bestimmen der "horzontalen" Position, also der Position in der verketteten Liste
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
            return (null, null);       // Element existiert nicht in Hashtabelle
        }

    }

    class HashTabQuadProb : Hash      // Klasse für quadratische Sondierung
    {
        public HashTabQuadProb() : base() { }                     // Konstruktoren aus class Hash
        public HashTabQuadProb(int tabGroeße) : base(tabGroeße) { }

        public override bool insert(int elem)         // Einfügemethode
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

        public override bool search(int elem)      // Suchmethode
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

        public override bool delete(int elem)     // Löschmethode
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

        public override void print()           // Ausgabefunktion
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

        public int getHorizontalePosPlus(int elem, int i)   // Methode für Hashfunktion mit quadratischer Sondierung, Teil mit Addition
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

        public int getHorizontalePosMinus(int elem, int i)  // Methode für Hashfunktion mit quadratischer Sondierung, Teil mit Subtraktion
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