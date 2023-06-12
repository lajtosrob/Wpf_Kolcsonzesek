using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Wpf_Kolcsonzesek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Kolcsonzes> kolcsonzesek = new List<Kolcsonzes>();
        string[] lines;
        public MainWindow()
        {
            InitializeComponent();
            tbNevbekeres.Focus();

            //4.

            Adatbeolvasas("DataSource\\kolcsonzesek.txt");

            //5. 

            lblFeladat5.Content = $"5. feladat: Napi kölcsönzések száma: {lines.Length - 1}";

            //8. 

            var sumLoan1 = KolcsonzesiIdo();
            lblFeladat8.Content = $"8. feladat: A napi árbevétel: {sumLoan1 * 2400}";

        }

        public void Adatbeolvasas(string filename)
        {
            lines = File.ReadAllLines(filename);

            for (var index = 1; index < lines.Length; index++)
            {
                string[] parts = lines[index].Split(';');

                string nev = parts[0];
                char jarmuId = char.Parse(parts[1]);
                int elvitelOra = int.Parse(parts[2]);
                int elvitelPerc = int.Parse(parts[3]);
                int visszaOra = int.Parse(parts[4]);
                int visszaPerc = int.Parse(parts[5]);

                Kolcsonzes kolcsonzesSor = new(nev, jarmuId, elvitelOra, elvitelPerc, visszaOra, visszaPerc);

                kolcsonzesek.Add(kolcsonzesSor);
            }

            MessageBox.Show("A fájlbeolvasás sikeresen megtörtént!");
        }

        //6.
        private void tbNevbekeres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (kolcsonzesek.Any(x => x.Nev == tbNevbekeres.Text))
                {
                    var kolcsonzo = kolcsonzesek.Where(x => x.Nev == tbNevbekeres.Text).ToList();
                    lblFeladat6Nev.Content = $"{tbNevbekeres.Text} kölcsönzései: ";
                    kolcsonzo.ForEach(x => lbKolcsonzesek.Items.Add($"\t{x.ElvitelOra}:{x.ElvitelPerc}-{x.VisszaOra}:{x.VisszaPerc}"));

                }
                tbIdopontBekeres.Focus();
            }
        }

        //7.
        private void tbIdopontBekeres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string[] idopont = tbIdopontBekeres.Text.Split(':');
                var jarmulist = kolcsonzesek.Where(x =>
                ((x.ElvitelOra < int.Parse(idopont[0]) || (x.ElvitelOra == int.Parse(idopont[0]) && x.ElvitelPerc < int.Parse(idopont[1])))) && (x.VisszaOra > int.Parse(idopont[0]) || (x.VisszaOra == int.Parse(idopont[0]) && x.VisszaPerc > int.Parse(idopont[1])))).ToList();
                //var jarmulist = kolcsonzesek.Where(x => ((x.ElvitelOra < int.Parse(idopont[0])) || (x.ElvitelOra == int.Parse(idopont[0]) && (x.ElvitelPerc < int.Parse(idopont[1]))) && x.VisszaOra > int.Parse(idopont[0]) || (x.VisszaOra == int.Parse(idopont[0]) && x.VisszaPerc > int.Parse(idopont[1])))).ToList();

                jarmulist.ForEach(x => lbIdopont.Items.Add($"{x.ElvitelOra}:{x.ElvitelPerc}-{x.VisszaOra}:{x.VisszaPerc} : {x.Nev}"));
            }

        }

        public int KolcsonzesiIdo()
        {
            int sumLoan = 0;
            for(var index = 0; index < kolcsonzesek.Count; index++)
            {
                if (kolcsonzesek[index].VisszaPerc < kolcsonzesek[index].ElvitelPerc)
                {
                    sumLoan += (kolcsonzesek[index].VisszaOra - kolcsonzesek[index].ElvitelOra - 1) * 2;
                    if (kolcsonzesek[index].VisszaPerc - kolcsonzesek[index].ElvitelPerc + 60 > 30)
                    {
                        sumLoan += 2;
                    }
                    else
                    {
                        sumLoan += 1;
                    }
                }
                else
                {
                    sumLoan += (kolcsonzesek[index].VisszaOra - kolcsonzesek[index].ElvitelOra) * 2;
                    if (kolcsonzesek[index].VisszaPerc - kolcsonzesek[index].ElvitelPerc > 30)
                    {
                        sumLoan += 2;
                    }
                    else { sumLoan += 1;}
                }
            }


            return sumLoan;
        }

        public void RongaltJarmu()
        {

        }
    }
}
