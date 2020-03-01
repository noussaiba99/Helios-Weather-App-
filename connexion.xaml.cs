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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using LiveCharts.WinForms;
using LiveCharts;
using LiveCharts.Wpf;
using Brushes = System.Windows.Media.Brushes;
using System.IO;

namespace Helios
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class connexion: Window
    {
        string wilaya = File.ReadAllText(@"wilaya.txt");    //wilaya par defaut: Alger

        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();
        public connexion(string city, InfoJour.weatherinfo.Root output)
        {
            wilaya = wilaya.Trim(new Char[] { ' ', '\r', '\n', '\t' }); // suppression des caractéres bizarres 
            InitializeComponent();
        }

        private void main_load(object sender, RoutedEventArgs e)
        {
            // string str;
            icon2.Visibility = Visibility.Hidden;
            icon1.Visibility = Visibility.Visible;
            text2.Visibility = Visibility.Hidden;
            text3.Visibility = Visibility.Hidden;

        }


        private void Fermer_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Mini_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        // Evenement de la confirmation 
        private void confirmer_Click(object sender, RoutedEventArgs e)
        {
            StreamReader sr1 = new StreamReader(@"son.txt");
            string str = sr1.ReadLine();
            sr1.Close();
            if (str == "Activé")// si le son est acitvé ===> le click provoquera un son 
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            /***********************************************************************************************************
            *               Controles de saisi
             * *********************************************************************************************************/
            if ((passwd.Password == ""))  // si l'admin n'a saisi aucun mot de passe 
            {
                text3.Visibility = Visibility.Visible;
                text3.Text = "Champ obligatoire "; // affichage de ce texte 
            }
            else
            {
                text3.Text = "";

                // recupération du mot de passe  saisi dans un chaine 
                if (icon1.Visibility == Visibility.Visible)
                {
                    text2.Text = passwd.Password;
                }
                else
                {
                    passwd.Password = text2.Text;
                }
                 
                StreamReader sr = new StreamReader("passwd.txt"); // recuperation du mot de passe du fichier 
                str = sr.ReadLine();
                if (str == passwd.Password) // comparaison entre les deux chains si c'est la bonne : 
                {
                    /// =====> Mise a jour 
                    text2.Clear();
                    text3.Text = "";
                    passwd.Clear();
                    mise_a_jour mise = new mise_a_jour(wilaya, output_out);
                    mise.Show();
                    this.Close();
                }
                else //sinn 
                {
                    /// ===> resaisi du mot de passe 
                    text3.Text = "Le mot de passe est incorrect";
                    text3.Visibility = Visibility.Visible;
                }
            }
        }
        private void annuler_Click(object sender, RoutedEventArgs e)
        {

            text2.Clear();
            text3.Text = "";
            passwd.Clear();
        }

        private void visibility_click(object sender, RoutedEventArgs e)
        {
            if (icon1.Visibility == Visibility.Visible)
            {
                text2.Visibility = Visibility.Visible;
                passwd.Visibility = Visibility.Hidden;
                text2.Text = passwd.Password;
                icon1.Visibility = Visibility.Hidden;
                icon2.Visibility = Visibility.Visible;
            }
            else
            {
                passwd.Visibility = Visibility.Visible;
                text2.Visibility = Visibility.Hidden;
                passwd.Password = text2.Text;
                icon2.Visibility = Visibility.Hidden;
                icon1.Visibility = Visibility.Visible;
            }
        }
    }
}
