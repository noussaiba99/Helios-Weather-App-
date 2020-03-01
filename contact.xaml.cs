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
using System.Net.Mail;
using System.Web;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Net;
using System.Runtime.InteropServices;
using System.IO;
using Microsoft.Win32;

namespace Helios
{
    /// <summary>
    /// Logique d'interaction pour Contact.xaml
    /// </summary>

    public partial class Contact : Window
    {
        static public bool invalidEmail(string mail)
        {
            return Regex.IsMatch(mail, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$");
        }


        // string wilaya = "alger";    //wilaya par defaut: Alger
        string wilaya = File.ReadAllText(@"wilaya.txt");    //wilaya par defaut: Alger ==> récupération du fichier 

        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();

        public Contact(string city, InfoJour.weatherinfo.Root output)
        {
            wilaya = wilaya.Trim(new Char[] { ' ', '\r', '\n', '\t' });
            InitializeComponent();
            wilaya = wilaya.Replace(" ", "");

            wilaya = city;
            output_out = output;
        }
        private void CreateScreenShot(UIElement visual, string file)
            // Creation de la capture d'écran 
        {

            double width = Convert.ToDouble(visual.GetValue(FrameworkElement.WidthProperty));

            double height = Convert.ToDouble(visual.GetValue(FrameworkElement.HeightProperty));



            if (double.IsNaN(width) || double.IsNaN(height))

            {

                throw new FormatException("Erreur !!");

            }

            RenderTargetBitmap render = new RenderTargetBitmap(
            Convert.ToInt32(width),
            Convert.ToInt32(visual.GetValue(FrameworkElement.HeightProperty)), 96, 96, PixelFormats.Pbgra32);
            // Indicate which control to render in the image
            render.Render(visual);
            try
            {
                using (FileStream stream = new FileStream(file, FileMode.Create))

                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(render));
                    encoder.Save(stream);

                }

            }
            catch (Exception )
            {
                WpfMessageBox.Show("Erreur", "Echec de la sauvegarde !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);

            }
        }
        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
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
            Application.Current.Shutdown();
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
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
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
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

            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
     
        private void btnPinPanel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EmailTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailTextBox.BorderBrush = Brushes.Transparent;
            MesBorder.BorderBrush = Brushes.SteelBlue;
            EmailBorder.BorderBrush = Brushes.SteelBlue;
        }

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailTextBox.BorderBrush = Brushes.Transparent;
            MesBorder.BorderBrush = Brushes.SteelBlue;
            EmailBorder.BorderBrush = Brushes.SteelBlue;
        }

        private void MesTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            EmailTextBox.BorderBrush = Brushes.Transparent;
            MesBorder.BorderBrush = Brushes.SteelBlue;
            EmailBorder.BorderBrush = Brushes.SteelBlue;
        }

        private void Envoyer_Click(object sender, RoutedEventArgs e)
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
            string name = NameTextBox.Text;
            string email = EmailTextBox.Text;
            string mes = MesTextBox.Text;
            if (email.Length == 0)
            {
                
                EmailBorder.BorderBrush = Brushes.Red;
            }
            else
            {
                if (!invalidEmail(email))
                {
         
                    EmailBorder.BorderBrush = Brushes.Red;
                }
                else
                {
                    if (mes.Length == 0)
                    {

                        MesBorder.BorderBrush = Brushes.Red;
                        Tooltip1_Opened(this.tooltip1, null);
                    }
                    else // Envoie du mail 
                    {


                        if (InfoJour.test_cnx() == 1)
                        {

                            string Emailexped = "weatherhelios@gmail.com";
                            string Emaildest = "hn_aboutaleb@esi.dz";
                            string Sujet = "Commentaire";
                            string Mess = "Emailsender :" + email + Environment.NewLine +
                                    "Commentaire : " + mes;

                            MailMessage mail = new MailMessage(Emailexped, Emaildest, Sujet, Mess);

                            SmtpClient stpc = new SmtpClient("smtp.gmail.com", 587);

                            stpc.Credentials = new System.Net.NetworkCredential("weatherhelios@gmail.com", "kyuxpdmsiukmcdax");
                            stpc.EnableSsl = true;
                            try
                            {
                                stpc.Send(mail);
                                WpfMessageBox.Show("Réussi", "Commentaire envoyé avec succés !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Information);
                            }
                            catch (Exception )
                            {

                                WpfMessageBox.Show("Erreur", "Echec de l'envoi !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            WpfMessageBox.Show("Erreur", "Vous n'êtes pas connectés ! ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);
                        }
                    }
                }

            }

        }

        private void Tooltip1_Opened(object sender, RoutedEventArgs e)
        {
            ToolTip tooltip = (ToolTip)sender;
            ToolTipService.SetPlacement(EmailTextBox, System.Windows.Controls.Primitives.PlacementMode.Relative);
            tooltip.IsOpen = true;
            tooltip.Visibility = Visibility.Visible;


        }
        private void Screen_Click(object sender, RoutedEventArgs e)
        {
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\screen.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            //déclaration et instanciation de la fenêtre parcourir

            SaveFileDialog parcourir = new SaveFileDialog();
            parcourir.DefaultExt = "png";

            //je spécifie que seul les images .png sont selectionnables
            parcourir.Filter = " Fichier PNG (*.PNG)|*.png";

            //ouverture de la fenêtre parcourir
            parcourir.ShowDialog();

            CreateScreenShot(this, parcourir.FileName);
        }

        private void meteo(object sender, RoutedEventArgs e)
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
            if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya, output_out);
                nvv.Show();
                this.Close();
            }
            else
            {
                WpfMessageBox.Show("Vous ne disposez pas de connexion internet!");
            }
        }
        private void prev(object sender, RoutedEventArgs e)
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
            prevision prv = new prevision(wilaya,output_out);
            prv.Show();
            this.Close();
        }
        private void aprop(object sender, RoutedEventArgs e)
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
            apropos ap = new apropos(wilaya, output_out);
            ap.Show();
            this.Close();
        }
        private void evolut(object sender, RoutedEventArgs e)
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
            evolution ev = new evolution(wilaya, output_out);
            ev.Show();
            this.Close();
        }
        private void generall(object sender, RoutedEventArgs e)
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
            parametre cr = new parametre(wilaya, output_out);
            cr.Show();
            this.Close();
        }
        private void mise_ajr(object sender, RoutedEventArgs e)
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
            connexion mise = new connexion(wilaya, output_out);
            mise.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            mise.ShowDialog();
            this.Close();
        }
        private void carte(object sender, RoutedEventArgs e)
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
            accueil ac = new accueil(wilaya, output_out);
            ac.Show();
            this.Close();
        }
        private void conta(object sender, RoutedEventArgs e)
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
            Contact cont = new Contact(wilaya, output_out);
            cont.Show();
            this.Close();
        }
        private void power_click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void minimize_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    {
                        accueil ev = new accueil(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.M:
                    {
                        meteo_jour ev = new meteo_jour(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.P:
                    {
                        prevision ev = new prevision(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.E:
                    {
                        evolution ev = new evolution(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.T:
                    {
                        Contact ev = new Contact(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.R:
                    {
                        parametre ev = new parametre(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.O:
                    {
                        apropos ev = new apropos(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.D:
                    {
                        credit ev = new credit(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.Z:
                    {
                        connexion ev = new connexion(wilaya, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.S:
                    {
                        StreamReader sr = new StreamReader(@"son.txt");
                        string str = sr.ReadLine();
                        sr.Close();
                        if (str == "Activé")
                        {
                            MediaPlayer player = new MediaPlayer();
                            player.Open(new Uri(@"..\..\screen.mp3", UriKind.RelativeOrAbsolute));
                            player.Play();
                        }
                        //déclaration et instanciation de la fenêtre parcourir
                        SaveFileDialog parcourir = new SaveFileDialog();
                        parcourir.DefaultExt = "png";
                        //je spécifie que seul les images .png sont selectionnables
                        parcourir.Filter = " Fichier PNG (*.PNG)|*.png";
                        //ouverture de la fenêtre parcourir
                        parcourir.ShowDialog();
                        CreateScreenShot(this, parcourir.FileName);
                    }
                    break;
            }

        }
    }
}

