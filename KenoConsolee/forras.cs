using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keno
{
    internal class Forras
    {
        public static int Szorzo(NapiKeno keno, List<int> tippek)
        {
            Dictionary<String, int> nyeroParok = new Dictionary<string, int>(){
                {"10-10",1000000}, {"10-9",8000}, {"10-8",350}, {"10-7",30}, {"10-6",3}, {"10-5",1}, {"10-0",2},
                {"9-9",100000}, {"9-8",1200}, {"9-7",100}, {"9-6",12}, {"9-5",3}, {"9-0",1},
                {"8-8",20000}, {"8-7",350}, {"8-6",25}, {"8-5",5}, {"8-0",1},
                {"7-7",5000}, {"7-6",60}, {"7-5",6}, {"7-4",1}, {"7-0",1},
                {"6-6",500}, {"6-5",20}, {"6-4",3}, {"6-0",1},
                {"5-5",200}, {"5-4",10}, {"5-3",2},
                {"4-4",100}, {"4-3",2},
                {"3-3",15}, {"3-2",1},
                {"2-2",6},
                {"1-1",2}
            };
            int jatekTipus = tippek.Count;
            int talalatokSzama = keno.TalalatSzam(tippek);
            string kulcs = jatekTipus + "-" + talalatokSzama;

            if (nyeroParok.Keys.Contains(kulcs))
                return nyeroParok[kulcs];
            else
                return 0;
        }
    }
}
