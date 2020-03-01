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
using System.Windows.Shapes;
using System.IO;
namespace Helios
{
    /// <summary>
    /// Logique d'interaction pour nbre.xaml
    /// </summary>
    public partial class nbre : Window
    {
        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();
        // string wilaya = "alger";   //wilaya par defaut
        string wilaya = File.ReadAllText(@"wilaya.txt");
        
        public nbre(InfoJour.weatherinfo.Root output)
        {
            wilaya=wilaya.Trim(new Char[] {' ', '\r', '\n','\t' });
            InitializeComponent();
            wilaya=wilaya.Replace(" ", "");

        }
        private void power_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void minimize_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        private void ComboJours_DropDownClosed(object sender, EventArgs e)
        {
            //comboJours.BorderBrush = Brushes.White;
            string str = comboJours.Text;

            switch (str)
            {
                case "1 jour":
                    output_out.nbreJours = 1;
                    break;
                case "2 jours":
                    output_out.nbreJours = 2;
                    break;
                case "3 jours":
                    output_out.nbreJours = 3;
                    break;
                case "4 jours":
                    output_out.nbreJours = 4;
                    break;
                case "5 jours":
                    output_out.nbreJours = 5;
                    break;
                case "6 jours":
                    output_out.nbreJours = 6;
                    break;

            }
        }

        private void Btn_valid_Click(object sender, RoutedEventArgs e)
        {
            if(output_out.nbreJours != 0)
            {
                StreamReader sr = new StreamReader(@"son.txt");
                string str = sr.ReadLine();
                sr.Close();
                if (str == "Activé")
                {
                    MediaPlayer player = new MediaPlayer();
                    player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                    player.Play();
                }
                accueil nvl = new accueil(wilaya, output_out);
                nvl.Show();
                this.Close();
            }
        }

        private void Btn_annul_Click(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            this.Close();
        }
        
    }
}
