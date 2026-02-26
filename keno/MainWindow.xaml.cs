using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace keno
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int[] sorsolt = new int[20];
        List<Szelveny> szelvenyek = new();

        public class Szelveny
        {
            public int[] huzasok;
            public int szorzo;
            public int jatek;
            public string raw;
            public Szelveny(string line)
            {
                raw = line;
                string[] parts = line.Split(',', '!');
                Debug.WriteLine(parts[0].Skip(2).ToString());
                jatek = int.Parse(parts[0].Substring(2));
                szorzo = int.Parse(parts[1]);

                huzasok = new int[parts.Length - 2];
                for (int i = 2; i < parts.Length; i++)
                {
                    huzasok[i-2] = int.Parse(parts[i]);
                }  
            }
            public int Nyeremeny(List<int> szamok) { return szorzo * 200 * ForrasGUI.Szorzo(szamok, huzasok.ToList()); }
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        public List<Szelveny> Load()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TextFile (*.txt)|*.txt";
            ofd.ShowDialog();
            if (!ofd.CheckFileExists) return new();

            return File.ReadAllLines(ofd.FileName).Select(x=>new Szelveny(x)).ToList();
        }

        private void Sorsolas(object sender, RoutedEventArgs e)
        {
            lbRandom.Items.Clear();

            Random rnd = new Random();
            List<int> numbers = new List<int>();

            for (int i = 0; i < 20; i++)
            {
                int num = rnd.Next(1, 81);
                numbers.Add(num);
                sorsolt[i] = num;
            }

            numbers.Sort();

            foreach (int n in numbers)
            {
                TextBlock tb = new TextBlock();
                tb.Text = n.ToString();
                lbRandom.Items.Add(tb);
            }
        }

        private void LoadBtn(object sender, RoutedEventArgs e)
        {
            szelvenyek = Load();
            foreach (var s in szelvenyek)
            {
                TextBlock tb = new TextBlock();
                tb.Text = s.raw;
                lbSzelvenyek.Items.Add(tb);
            }
        }

        private void lbSzelvenyek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Label ch in lbSzamok.Children)
            {
                ch.Background = Brushes.LightGreen;
            }
            foreach (int num in szelvenyek[lbSzelvenyek.SelectedIndex].huzasok)
            {
               Label lb = ((Label) lbSzamok.Children[num-1]);
                lb.Background = Brushes.Yellow;
            }

            lbSLabel.Content = $"Szorzó: {szelvenyek[lbSzelvenyek.SelectedIndex].szorzo}";
            lbNLabel.Content = $"Nyeremény: {szelvenyek[lbSzelvenyek.SelectedIndex].Nyeremeny(sorsolt.ToList()).ToString()}";
        }
    }
}