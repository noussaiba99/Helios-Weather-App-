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
using Microsoft.Win32;
using AudioSwitcher.AudioApi.CoreAudio;
using System.Diagnostics;

namespace Helios
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class parametre : Window
    {

        string wilaya = File.ReadAllText(@"wilaya.txt");  //lire à partir d'un fichier wilaya.txt la wilaya par defaut 

        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();
        public parametre(string city, InfoJour.weatherinfo.Root output)//la fenetre parametres
        {
            wilaya = wilaya.Trim(new Char[] { ' ', '\r', '\n', '\t' });
            InitializeComponent();
            wilaya = wilaya.Replace(" ", "");

            wilaya = city;
            output_out = output;
        }
        private void CreateScreenShot(UIElement visual, string file)//pour prendre des captures d'écran

        {

            double width = Convert.ToDouble(visual.GetValue(FrameworkElement.WidthProperty));

            double height = Convert.ToDouble(visual.GetValue(FrameworkElement.HeightProperty));



            if (double.IsNaN(width) || double.IsNaN(height))

            {

                throw new FormatException("Erreur !! ");

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
                    //  WpfMessageBox.Show("Réussi", "Capture prise avec succés", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Information);

                }

            }
            catch (Exception)
            {
                WpfMessageBox.Show("Erreur", "Echec de la sauvegarde !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);
                // WpfMessageBox.Show("Erreur", "Echec de la sauvegarde !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);

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
                WpfMessageBox.Show("Vous ne disposez pas de connexion internet");
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
            prevision prv = new prevision(wilaya, output_out);
            prv.Show();
            this.Close();
        }
        private void evolut(object sender, RoutedEventArgs e) //pour activer ou désactiver le son
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
        private void apropo(object sender, RoutedEventArgs e)
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
        private void credits(object sender, RoutedEventArgs e)
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
            credit cr = new credit(wilaya, output_out);
            cr.Show();
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




        private void btnPinPanel_Click(object sender, RoutedEventArgs e)
        {
            /*SolidColorBrush Sopa = new SolidColorBrush();
            Sopa.Color = Colors.Red;
            Sopa.Opacity = 50;
            rainn.OpacityMask = Sopa;
            rainn.Opacity = 0;*/


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
        private void power_click(object sender, RoutedEventArgs e)//fermer la fenetre
        {

            Close();
        }

        private void minimize_click(object sender, RoutedEventArgs e)//réduire la fenetre
        {
            WindowState = WindowState.Minimized;
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }



        private void confirmer_click(object sender, RoutedEventArgs e)//pour confirmer le nouveau mot de passe 
        {
            if ((passeaconfirmed.Password == "") || (passeactuel.Password == "") || (passenouv.Password == ""))
            {
                textbk.Visibility = Visibility.Visible;
                textbk.Text = "   Vous devez remplir tous les champs";//si un champ est vide un message d'alerte sera afficher
            }
            else
            {
                if (icon1.Visibility == Visibility.Visible)
                { pass1.Text = passeactuel.Password; }
                else
                { passeactuel.Password = pass1.Text; }
                if (icon3.Visibility == Visibility.Visible)
                { pass2.Text = passenouv.Password; }
                else
                { passenouv.Password = pass2.Text; }
                if (icon5.Visibility == Visibility.Visible)
                { pass3.Text = passeaconfirmed.Password; }
                else
                { passeaconfirmed.Password = pass3.Text; }
                StreamReader rd = new StreamReader(@"passwd.txt");//lire le mot de passe à partir d'un fichier passwd.txt 
                string essay = rd.ReadLine();
                if (passeactuel.Password != essay)
                {
                    textbk.Visibility = Visibility.Visible;
                    textbk.Text = "    Mot de Passe incorrect";
                    rd.Close();
                }
                else
                {
                    rd.Close();
                    if (passenouv.Password != passeaconfirmed.Password)
                    {
                        textbk.Visibility = Visibility.Visible;
                        textbk.Text = "   Mot de Passe n'est pas confirmé";
                        passeaconfirmed.Clear();
                        passenouv.Clear();
                        pass2.Text = "";
                        pass3.Text = "";
                    }
                    else
                    {
                        if (passenouv.Password.Length <= 5)
                        {
                            textbk.Visibility = Visibility.Visible;
                            textbk.Text = "Mot de Passe Faible,Il faut qu'il contient au mois 6 caractères";
                            passeaconfirmed.Clear();
                            passenouv.Clear();
                            pass2.Text = "";
                            pass3.Text = "";
                        }
                        else
                        {
                            textbk.Visibility = Visibility.Hidden;
                            StreamWriter sr = new StreamWriter(@"passwd.txt");
                            sr.Close();
                            StreamWriter sr2 = new StreamWriter(@"passwd.txt");
                            sr2.WriteLine(passenouv.Password);
                            sr2.Close();
                            passeaconfirmed.Clear();
                            passeactuel.Clear();
                            passenouv.Clear();
                            pass1.Text = "";
                            pass2.Text = "";
                            pass3.Text = "";
                            WpfMessageBox.Show("Réussi", "Mot de passe modifié avec succées", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Information);

                        }
                    }
                }
            }
        }

        private void annuler_click(object sender, RoutedEventArgs e)
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


            passeactuel.Clear();
            passenouv.Clear();
            passeaconfirmed.Clear();
            pass1.Text = "";
            pass2.Text = "";
            pass3.Text = "";
        }
        public string str = "Adrar";
        private void main_load(object sender, RoutedEventArgs e)
        {

            // CoreAudioDevice defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
            // Debug.WriteLine("Current Volume:" + defaultPlaybackDevice.Volume);

            StreamReader dw = new StreamReader(@"son.txt");
            combox2.Text = dw.ReadLine();
            dw.Close();
            /* slide.Text = Convert.ToString(dw.ReadLine());
             defaultPlaybackDevice.Volume = Convert.ToDouble(slide.Text);*/
            // Slider.Value = Convert.ToDouble(dw.ReadLine());

            icon2.Visibility = Visibility.Hidden;
            icon1.Visibility = Visibility.Visible;
            icon4.Visibility = Visibility.Hidden;
            icon6.Visibility = Visibility.Hidden;
            pass1.Visibility = Visibility.Hidden;
            pass2.Visibility = Visibility.Hidden;
            pass3.Visibility = Visibility.Hidden;
            textbk.Visibility = Visibility.Hidden;
            StreamReader sr = new StreamReader(@"wilaya.txt");
            str = sr.ReadLine();
            switch (str)
            {
                case "adrar":
                    combox1.Text = "Adrar";
                    break;
                case "ain-defla":
                    combox1.Text = "Ain Defla";
                    break;
                case "ain-temouchent":
                    combox1.Text = "Ain Témouchent";
                    break;
                case "alger":
                    combox1.Text = "Alger";
                    break;
                case "annaba":
                    combox1.Text = "Annaba";

                    break;
                case "batna":
                    combox1.Text = "Batna";


                    break;
                case "bechar":
                    combox1.Text = "Béchar";

                    break;
                case "bejaia":
                    combox1.Text = "Béjaia";

                    break;
                case "Biskra":
                    combox1.Text = "biskra";

                    break;
                case "Blida":
                    combox1.Text = "blida";

                    break;
                case "bordj-bou-arreridj":
                    combox1.Text = "Bordj Bou Arréridj";

                    break;
                case "bouira":
                    combox1.Text = "Bouira";

                    break;
                case "chlef":
                    combox1.Text = "Chlef";

                    break;
                case "boumerdes":
                    combox1.Text = "Boumerdès";

                    break;
                case "constantine":
                    combox1.Text = "Constantine";

                    break;
                case "djelfa":
                    combox1.Text = "Djelfa";

                    break;
                case "el-bayadh":
                    combox1.Text = "El Bayadh";

                    break;
                case "el-Tarf":
                    combox1.Text = "El Taref";

                    break;
                case "el-oued":
                    combox1.Text = "El-Oued";

                    break;
                case "guelma":
                    combox1.Text = "Guelma";

                    break;
                case "illizi":
                    combox1.Text = "Illizi";

                    break;
                case "jijel":
                    combox1.Text = "Jijel";

                    break;
                case "laghouat":
                    combox1.Text = "Laghouat";

                    break;
                case "khenchela":
                    combox1.Text = "Khenchela";

                    break;
                case "msila":
                    combox1.Text = "M’Sila";

                    break;
                case "mascara":
                    combox1.Text = "Mascara";

                    break;
                case "medea":
                    combox1.Text = "Médéa";

                    break;
                case "mila":
                    combox1.Text = "Mila";
                    break;
                case "mostaganem":
                    combox1.Text = "Mostaganem";

                    break;
                case "naama":
                    combox1.Text = "Naâma";

                    break;
                case "oran":
                    combox1.Text = "Oran";
                    break;
                case "ouargla":
                    combox1.Text = "Ouargla";

                    break;
                case "oum-el-bouaghi":
                    combox1.Text = "Oum El Bouaghi";

                    break;
                case "relizane":
                    combox1.Text = "Relizane";

                    break;
                case "saida":
                    combox1.Text = "Saida";

                    break;
                case "sidi-bel-abbes":
                    combox1.Text = "Sidi BelAbbès";

                    break;
                case "skikda":
                    combox1.Text = "Skikda";

                    break;
                case "souk-ahras":
                    combox1.Text = "Souk Ahras";

                    break;
                case "tamanrasset":
                    combox1.Text = "Tamanrasset";

                    break;
                case "tiaret":
                    combox1.Text = "Tiaret";

                    break;
                case "tindouf":
                    combox1.Text = "Tindouf";

                    break;
                case "tebessa":
                    combox1.Text = "Tébessa";

                    break;
                case "tipaza":
                    combox1.Text = "Tipaza";

                    break;
                case "tissemsilt":
                    combox1.Text = "Tissemssilt";

                    break;
                case "tizi-ouzou":
                    combox1.Text = "Tizi-Ouzou";

                    break;
                case "tlemcen":
                    combox1.Text = "Tlemcen";
                    break;
            }

            sr.Close();
        }


        private void Combox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //string wilaya;
            string str = combox1.Text;

            switch (str)
            {
                case "Adrar":
                    wilaya = "adrar";
                    break;
                case "Ain Defla":
                    wilaya = "ain-defla";
                    break;
                case "Ain Témouchent":
                    wilaya = "ain-temouchent";
                    break;
                case "Alger":

                    wilaya = "alger";
                    break;
                case "Annaba":
                    wilaya = "annaba";

                    break;
                case "Batna":
                    wilaya = "batna";


                    break;
                case "Béchar":
                    wilaya = "bechar";

                    break;
                case "Béjaia":
                    wilaya = "bejaia";

                    break;
                case "Biskra":
                    wilaya = "biskra";

                    break;
                case "Blida":
                    wilaya = "blida";

                    break;
                case "Bordj Bou Arréridj":
                    wilaya = "bordj-bou-arreridj";

                    break;
                case "Bouira":
                    wilaya = "bouira";

                    break;
                case "Chlef":
                    wilaya = "chlef";

                    break;
                case "Boumerdès":
                    wilaya = "boumerdes";

                    break;
                case "Constantine":
                    wilaya = "constantine";

                    break;
                case "Djelfa":
                    wilaya = "djelfa";

                    break;
                case "El Bayadh":
                    wilaya = "el-bayadh";

                    break;
                case "El Taref":
                    wilaya = "el-tarf";

                    break;
                case "El-Oued":
                    wilaya = "el-oued";

                    break;
                case "Guelma":
                    wilaya = "guelma";

                    break;
                case "Illizi":
                    wilaya = "illizi";

                    break;
                case "Jijel":
                    wilaya = "jijel";

                    break;
                case "Laghouat":
                    wilaya = "laghouat";

                    break;
                case "Khenchela":
                    wilaya = "khenchela";

                    break;
                case "M’Sila":
                    wilaya = "msila";

                    break;
                case "Mascara":
                    wilaya = "mascara";

                    break;
                case "Médéa":
                    wilaya = "medea";

                    break;
                case "Mila":
                    wilaya = "mila";
                    break;
                case "Mostaganem":
                    wilaya = "mostaganem";

                    break;
                case "Naâma":
                    wilaya = "naama";

                    break;
                case "Oran":
                    wilaya = "oran";
                    break;
                case "Ouargla":
                    wilaya = "ouargla";

                    break;
                case "Oum El Bouaghi":
                    wilaya = "oum-el-bouaghi";

                    break;
                case "Relizane":
                    wilaya = "relizane";

                    break;
                case "Saida":
                    wilaya = "saida";

                    break;
                case "Sidi BelAbbès":
                    wilaya = "sidi-bel-abbes";

                    break;
                case "Skikda":
                    wilaya = "skikda";

                    break;
                case "Souk Ahras":
                    wilaya = "souk-ahras";

                    break;
                case "Tamanrasset":
                    wilaya = "tamanrasset";

                    break;
                case "Tiaret":
                    wilaya = "tiaret";

                    break;
                case "Tindouf":
                    wilaya = "tindouf";

                    break;
                case "Tébessa":
                    wilaya = "tebessa";

                    break;
                case "Tipaza":
                    wilaya = "tipaza";

                    break;
                case "Tissemsilt":
                    wilaya = "tissemssilt";

                    break;
                case "Tizi-Ouzou":
                    wilaya = "tizi-ouzou";

                    break;
                case "Tlemcen":
                    wilaya = "tlemcen";
                    break;
            }
        }
        private void ComboBox1_DropDownClosed(object sender, EventArgs e)
        {

            StreamWriter sw2 = new StreamWriter(@"wilaya.txt");
            sw2.Close();
            StreamWriter sw = new StreamWriter(@"wilaya.txt");
            //str = combox1.Text;
            string str = combox1.Text;
            switch (str)
            {
                case "Adrar":
                    wilaya = "adrar";
                    break;
                case "Ain Defla":
                    wilaya = "ain-defla";
                    break;
                case "Ain Témouchent":
                    wilaya = "ain-temouchent";
                    break;
                case "Alger":

                    wilaya = "alger";
                    break;
                case "Annaba":
                    wilaya = "annaba";

                    break;
                case "Batna":
                    wilaya = "batna";


                    break;
                case "Béchar":
                    wilaya = "bechar";

                    break;
                case "Béjaia":
                    wilaya = "bejaia";

                    break;
                case "Biskra":
                    wilaya = "biskra";

                    break;
                case "Blida":
                    wilaya = "blida";

                    break;
                case "Bordj Bou Arréridj":
                    wilaya = "bordj-bou-arreridj";

                    break;
                case "Bouira":
                    wilaya = "bouira";

                    break;
                case "Chlef":
                    wilaya = "chlef";

                    break;
                case "Boumerdès":
                    wilaya = "boumerdes";

                    break;
                case "Constantine":
                    wilaya = "constantine";

                    break;
                case "Djelfa":
                    wilaya = "djelfa";

                    break;
                case "El Bayadh":
                    wilaya = "el-bayadh";

                    break;
                case "El Taref":
                    wilaya = "el-tarf";

                    break;
                case "El-Oued":
                    wilaya = "el-oued";

                    break;
                case "Guelma":
                    wilaya = "guelma";

                    break;
                case "Illizi":
                    wilaya = "illizi";

                    break;
                case "Jijel":
                    wilaya = "jijel";

                    break;
                case "Laghouat":
                    wilaya = "laghouat";

                    break;
                case "Khenchela":
                    wilaya = "khenchela";

                    break;
                case "M’Sila":
                    wilaya = "msila";

                    break;
                case "Mascara":
                    wilaya = "mascara";

                    break;
                case "Médéa":
                    wilaya = "medea";

                    break;
                case "Mila":
                    wilaya = "mila";
                    break;
                case "Mostaganem":
                    wilaya = "mostaganem";

                    break;
                case "Naâma":
                    wilaya = "naama";

                    break;
                case "Oran":
                    wilaya = "oran";
                    break;
                case "Ouargla":
                    wilaya = "ouargla";

                    break;
                case "Oum El Bouaghi":
                    wilaya = "oum-el-bouaghi";

                    break;
                case "Relizane":
                    wilaya = "relizane";

                    break;
                case "Saida":
                    wilaya = "saida";

                    break;
                case "Sidi BelAbbès":
                    wilaya = "sidi-bel-abbes";

                    break;
                case "Skikda":
                    wilaya = "skikda";

                    break;
                case "Souk Ahras":
                    wilaya = "souk-ahras";

                    break;
                case "Tamanrasset":
                    wilaya = "tamanrasset";

                    break;
                case "Tiaret":
                    wilaya = "tiaret";

                    break;
                case "Tindouf":
                    wilaya = "tindouf";

                    break;
                case "Tébessa":
                    wilaya = "tebessa";

                    break;
                case "Tipaza":
                    wilaya = "tipaza";

                    break;
                case "Tissemsilt":
                    wilaya = "tissemssilt";

                    break;
                case "Tizi-Ouzou":
                    wilaya = "tizi-ouzou";

                    break;
                case "Tlemcen":
                    wilaya = "tlemcen";
                    break;
            }
            sw.WriteLine(wilaya);
            sw.Close();

        }

        private void Visibility_Click(object sender, RoutedEventArgs e)
        {

            if (icon1.Visibility == Visibility.Visible)
            {
                pass1.Visibility = Visibility.Visible;
                passeactuel.Visibility = Visibility.Hidden;
                pass1.Text = passeactuel.Password;
                icon1.Visibility = Visibility.Hidden;
                icon2.Visibility = Visibility.Visible;
            }
            else
            {
                passeactuel.Visibility = Visibility.Visible;
                pass1.Visibility = Visibility.Hidden;
                passeactuel.Password = pass1.Text;
                icon2.Visibility = Visibility.Hidden;
                icon1.Visibility = Visibility.Visible;
            }
        }
        private void Visibility2_Click(object sender, RoutedEventArgs e)
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
            if (icon5.Visibility == Visibility.Visible)
            {
                pass2.Visibility = Visibility.Visible;
                passenouv.Visibility = Visibility.Hidden;
                pass2.Text = passenouv.Password;
                icon5.Visibility = Visibility.Hidden;
                icon6.Visibility = Visibility.Visible;
            }
            else
            {
                passenouv.Visibility = Visibility.Visible;
                pass2.Visibility = Visibility.Hidden;
                passenouv.Password = pass2.Text;
                icon6.Visibility = Visibility.Hidden;
                icon5.Visibility = Visibility.Visible;
            }
        }
        private void Visibility3_Click(object sender, RoutedEventArgs e)
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
            if (icon3.Visibility == Visibility.Visible)
            {
                pass3.Visibility = Visibility.Visible;
                passeaconfirmed.Visibility = Visibility.Hidden;
                pass3.Text = passeaconfirmed.Password;
                icon3.Visibility = Visibility.Hidden;
                icon4.Visibility = Visibility.Visible;
            }
            else
            {
                passeaconfirmed.Visibility = Visibility.Visible;
                pass3.Visibility = Visibility.Hidden;
                passeaconfirmed.Password = pass3.Text;
                icon4.Visibility = Visibility.Hidden;
                icon3.Visibility = Visibility.Visible;
            }
        }

        private void Combox2_Copy_DropDownClosed(object sender, EventArgs e)
        {



            StreamWriter sw = new StreamWriter(@"son.txt");
            str = combox2.Text;
            sw.WriteLine(str);
            sw.Close();

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
