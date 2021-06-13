using System;

namespace AuD_Praktikum
{
    abstract class Hash : ISetUnsorted      // abstrakte Klasse mit Konstruktoren
    {
        public int tabGroeße = 11;  // Für quadratische Sondierung Tabellengröße in Form von p = 4*k+3
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
                Console.WriteLine($"{elem} wurde eingefügt!");
                return true;
            }
            else
            {
                while (eingefügeStelle.nachfolger != null)
                {
                    eingefügeStelle = eingefügeStelle.nachfolger;
                }
                eingefügeStelle.nachfolger = einzufügen;
                Console.WriteLine($"{elem} wurde eingefügt!");
                return true;
            }
        }

        public override bool search(int elem)            // Suchmethode
        {
            var (_, aktuelles) = getHorizontalePos(elem);

            if (aktuelles == null)
            {
                Console.WriteLine($"{elem} wurde nicht gefunden!");
                return false;
                //new ArgumentOutOfRangeException($"Das Element {elem} wurde nicht gefunden!")
            }
            else
            {
                Console.WriteLine($"{elem} wurde gefunden!");
                return true;
            }
        }

        public override bool delete(int elem)         // Löschmethode
        {
            int pos = getVertikalePos(elem);
            var (vorgänger, aktuelles) = getHorizontalePos(elem);
            if (aktuelles == null)
            {
                Console.WriteLine($"{elem} existiert nicht!");
                return false;
            }
            if (vorgänger == null)
            {
                hashTab[pos] = null;
                Console.WriteLine($"{elem} wurde gelöscht!");
                return true;
            }
            vorgänger.nachfolger = aktuelles.nachfolger;
            Console.WriteLine($"{elem} wurde gelöscht!");
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
                int pos = elem % (tabGroeße);
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
            int abbruch = -1;
            while (abbruch <= tabGroeße)
            {
                pos = getHorizontalePosPlus(elem, i);
                if (hashTab[pos] == null)
                {
                    hashTab[pos] = einzufügen;
                    Console.WriteLine($"{elem} wurde eingefügt!");
                    return true;
                }
                abbruch++;

                pos = getHorizontalePosMinus(elem, i);
                if (hashTab[pos] == null)
                {
                    hashTab[pos] = einzufügen;
                    Console.WriteLine($"{elem} wurde eingefügt!");
                    return true;
                }
                abbruch++;
                i++;

            }
            Console.WriteLine($"{elem} konnte nicht eingefügt werden, da die Hashtabelle bereits voll ist!");
            return false;

        }

        public override bool search(int elem)      // Suchmethode
        {
            int i = 0;
            int pos;
            int abbruch = -1;
            while (abbruch <= tabGroeße)
            {
                pos = getHorizontalePosPlus(elem, i);
                if (hashTab[pos].element == elem)
                {
                    Console.WriteLine($"{elem} wurde gefunden!");
                    return true;
                }
                abbruch++;
                pos = getHorizontalePosMinus(elem, i);
                if (hashTab[pos].element == elem)
                {
                    Console.WriteLine($"{elem} wurde gefunden!");
                    return true;
                }
                abbruch++;
                i++;
            }
        }

        public override bool delete(int elem)     // Löschmethode
        {
            int i = 0;
            int pos;
            int abbruch = -1;
            while (abbruch <= tabGroeße)
            {
                pos = getHorizontalePosPlus(elem, i);
                if (hashTab[pos].element == elem)
                {
                    hashTab[pos] = null;
                    return true;
                }
                abbruch++;
                pos = getHorizontalePosMinus(elem, i);
                if (hashTab[pos].element == elem)
                {
                    hashTab[pos] = null;
                    return true;
                }
                abbruch++;
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
//Negative Zahlen bei separater Verkettung noch anschauen
//Verknüpfung aus Main fehlt noch
