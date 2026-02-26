namespace keno
{
    public class NapiKeno
    {
        public int ev { get; private set; }
        private int _het;
        public int het { get => _het; private set => _het = Math.Clamp(value, 1, 52); }

        private int _nap;
        public int nap { get => _nap; private set => _het = Math.Clamp(value, 1, 7); }
        public string huzasDatum { get; private set; }
        public List<int> huzottSzamok { get; private set; } = new ();



        public NapiKeno(string line)
        {
            string[] parts = line.Split(';');
            ev = int.Parse(parts[0]);
            het = int.Parse(parts[1]);
            nap = int.Parse(parts[2]);
            huzasDatum = parts[3];

            foreach (var part in parts.Skip(4)) huzottSzamok.Add(int.Parse(part)); 
        }

        public int TalalatSzam(List<int> tippek)
        {
            return huzottSzamok.Count(x=>tippek.Contains(x));
        }

        public bool Helyes()
        {
            if (huzottSzamok.Count != 20) return false; 
            if (huzottSzamok.Any(x => x > 80 || x < 1)) return false;
            return true;
        }
    }
}