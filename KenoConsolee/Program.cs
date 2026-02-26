using keno;

namespace KenoConsolee
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<NapiKeno> huzasok = Load();
            Console.WriteLine("6. Feladat: Állo,ány beolvasása sikeres!");
            Console.WriteLine($"7. Feladat: A hibás sorok száma: {huzasok.Count(x => !x.Helyes())} ");
            Console.WriteLine("9. Feladat: Nyeremény számítása");

            int[] input_nums;
            int input_bet;
            while (true)
            {
                Console.WriteLine("Kérem a tippjét! Vesszővel elválasztva sorolja fel a száokat: ");
                string raw_nums = Console.ReadLine() ?? "";
                if (raw_nums!= "") input_nums = raw_nums.Split(',').Select(int.Parse).ToArray();
                else input_nums = new int[0];
                if (input_nums.Length == 0 || input_nums.Length>10 )
                {
                    Console.WriteLine("A játéktípus 1..10 lehet!");
                    continue;
                } 
                break;
            }
            while (true)
            {
                Console.WriteLine("Kérem a fogadási összeget! :");
                string raw_bet = Console.ReadLine() ?? "";
                input_bet = int.Parse(raw_bet);
                if (input_bet > 1000 || input_bet < 200 || input_bet % 200 != 0)
                {
                    Console.WriteLine("Hibás összeg!");
                    continue;
                } break;
            }

            int szorzo = Forras.Szorzo(huzasok[0], input_nums.ToList());

            if (szorzo != 0) Console.WriteLine("Nyereménye: " + input_bet * szorzo); 
            else Console.WriteLine("Sajnos nem nyert!");

            Console.WriteLine("10. Feladat\n8-as játék 2020-ban, tét:4X [17,28,32,44,54,63,72,75]");
            List<int> nyeremenyek = new();
            foreach (NapiKeno nap in huzasok.Where(x=>x.ev == 2020))
            {
                int nyeremeny = Forras.Szorzo(nap, [17, 28, 32, 44, 54, 63, 72, 75]) * 800;
                nyeremenyek.Add(nyeremeny);
                if (nyeremeny!=0) Console.WriteLine($"{nap.huzasDatum} - {nyeremeny}");

                
            }
            Console.WriteLine($"Összesen {800 * nyeremenyek.Count} Ft-ot költött Kenóra");
            Console.WriteLine($"Összesen {nyeremenyek.Sum()} Ft-ot nyert");
        }

        public static List<NapiKeno> Load()
        {
            return File.ReadAllLines("Huzasok.csv")
                .Skip(1)
                .Select(line => new NapiKeno(line))
                .ToList();
        }
    }
}
