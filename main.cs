using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Linq;
/*
Év;Elem;Vegyjel;Rendszám;Felfedező
Ókor;Arany;Au;79;Ismeretlen
Ókor;Ezüst;Ag;47;Ismeretlen
*/
class Elemek
{
    public string ev { get; set; }
    public string elem { get; set; }
    public string vegyjel { get; set; }
    public string rendszam { get; set; }
    public string felfedezo { get; set; }

    public Elemek(string sor)
    {
        var s = sor.Trim().Split(";");
        this.ev = s[0];
        this.elem = s[1];
        this.vegyjel = s[2];
        this.rendszam = s[3];
        this.felfedezo = s[4];
    }

}

class Program
{
    public static bool is_vegyjel(string sor)
    {
        var abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        if (sor.Length > 2 || sor.Length < 1)
        {
            return false;
        }
        foreach (var c in sor)
        {
            if (!abc.Contains(c))
            {
                return false;
            }
        }
        return true;
    }


    public static void Main(string[] args)
    {
        var sr = new StreamReader("felfedezesek.txt");
        var elso = sr.ReadLine();
        var lista = new List<Elemek>();
        while (!sr.EndOfStream)
        {
            lista.Add(new Elemek(sr.ReadLine()));
        }
        sr.Close();

        var teszt = (
            from sor in lista
            select sor.ev
        );

        //3. feladat: Elemek száma: {lista.Count}
        Console.WriteLine($"3. feladat: Elemek száma: {lista.Count}");

        //4. feladat: felfedezések száma az Ókorban
        var okor = (
            from sor in lista
            where sor.ev == "Ókor"
            select sor
        ).Count();
        Console.WriteLine($"4. feladat: felfedezések száma az Ókorban: {okor}");

        //5. feladat: Kérek egy vegyjelet: 
        var jel = "";
        while (true)
        {
            Console.Write("5. feladat: Kérek egy vegyjelet: ");
            jel = Console.ReadLine().ToUpper();
            if (is_vegyjel(jel))
            {
                break;
            }
        }

        //6. feladat: Keresés \n \t Az elem vegyjele: {a.vegyjel} \n \t Az elem neve: {a.elem} \n \t Rendszáma: {a.rendszam} \n \t Felfedezés éve: {a.ev} \n \t Felfedező: {a.felfedezo}

        var kereses = (
            from sor in lista
            where sor.vegyjel.ToUpper() == jel
            select sor
        );
        Console.WriteLine($"6. feladat: Keresés");
        if (kereses.Any())
        {
            var a = kereses.First();
            Console.WriteLine($"\t \t Az elem vegyjele: {a.vegyjel} \n \t\t Az elem neve: {a.elem} \n \t\t Rendszáma: {a.rendszam} \n \t\t Felfedezés éve: {a.ev} \n \t\t Felfedező: {a.felfedezo}");
        }
        else
        {
            Console.WriteLine($"\t Nincs ilyen elem az adatforrásban!");
        }

        //7. feladat: {} év volt a leghosszab időszak két elem felfedezése között.
        var ev = (
            from sor in lista
            where sor.ev != "Ókor"
            select int.Parse(sor.ev)
        ).ToList();

        var kulonbseg = new List<int>();

        for (int i = 0; i < ev.Count() - 1; i++)
        {
            kulonbseg.Add(ev[i + 1] - ev[i]);
        }

        Console.WriteLine($"7. feladat: {kulonbseg.Max()} év volt a leghosszab időszak két elem felfedezése között.");

        /* 
        8. feladat: Statisztika
         \t {s.Key}: {s.Count()} db        
        */
        var nem_okor = (
            from sor in lista
            where sor.ev != "Ókor"
            group sor by sor.ev
        );

        var stat = (
            from sor in nem_okor
            where sor.Count() > 3
            select sor
        );

        Console.WriteLine($"8. feladat: Statisztika");
        foreach (var s in stat)
        {
            Console.WriteLine($" \t\t {s.Key}: {s.Count()} db");
        }


    }
}
