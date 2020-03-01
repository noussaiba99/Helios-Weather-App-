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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Controls.Primitives;
using System.Drawing;
using System.Windows.Interop;
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;
using ColorConverter = System.Windows.Media.ColorConverter;
using System.IO;
using static Helios.InfoJour.weatherinfo;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.Win32;

namespace Helios
{
    /// summary
    /// Logique d'interaction pour recupman.xaml
    /// summary
    public partial class recupe_man : Window
    {


        double tempmi;
        double tempma;
        double visib;
        double tempve;
        int humidit;

        // string wilaya = "alger";    //wilaya par defaut: Alger
        String wilaya = File.ReadAllText(@"wilaya.txt");    //wilaya par defaut: Alger
                                                            // String wilaya = (@"wilaya.txt");
        bool enregistre = false;
        public bool otherWindow = false;
        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();

        public InfoJour.weatherinfo.Root output = new InfoJour.weatherinfo.Root();


        public recupe_man(string city)
        {
            try
            {
                wilaya = wilaya.Trim(new Char[] { ' ', '\r', '\n', '\t' });
                InitializeComponent();

                wilaya = city;
                output.name = wilaya;


            }
            catch (Exception)
            {
                WpfMessageBox.Show("Erreur", "Echec de la récupération manuelle !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error); throw;
            }

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void CreateScreenShot(UIElement visual, string file)

        {

            double width = Convert.ToDouble(visual.GetValue(FrameworkElement.WidthProperty));

            double height = Convert.ToDouble(visual.GetValue(FrameworkElement.HeightProperty));



            if (double.IsNaN(width) || double.IsNaN(height))

            {

                throw new FormatException("Erreur !! ");

            }



            RenderTargetBitmap render = new RenderTargetBitmap(

               Convert.ToInt32(width),

               Convert.ToInt32(visual.GetValue(FrameworkElement.HeightProperty)),

               96,

               96,

               PixelFormats.Pbgra32);



            // Indicate which control to render in the image

            render.Render(visual);


            try
            {
                using (System.IO.FileStream stream = new FileStream(file, FileMode.Create))

                {

                    PngBitmapEncoder encoder = new PngBitmapEncoder();



                    encoder.Frames.Add(BitmapFrame.Create(render));



                    WpfMessageBox.Show("Réussi", "Capture prise avec succés", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Information);
                    encoder.Save(stream);

                }

            }
            catch (Exception)
            {
                WpfMessageBox.Show("Erreur", "Echec de la sauvegarde !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
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


        }
        private void prev(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            /* prevision prv = new prevision(wilaya, output_out);
             prv.Show();
             this.Close();*/
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
            otherWindow = true;
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
            otherWindow = true;
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
            otherWindow = true;
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
            otherWindow = true;
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
            otherWindow = true;
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
            otherWindow = true;
            Contact cont = new Contact(wilaya, output_out);
            cont.Show();
            this.Close();
        }


        //Contrôles de saisie au niveau des TextBox + Saut du curseur vers le prochain TextBox si la touche ENTRER est appuyée

        private void Tempmin_KeyDown(object sender, KeyEventArgs e)
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
            if (e.Key == Key.Enter)//Si ENTRER
            {
                double number1;
                double number2;
                if (Double.TryParse(this.tempmin.Text, out number1) == true) //Vérifier si la valeur entrée est un nombre décimal
                {
                    Double.TryParse(this.tempmax.Text, out number2); //Conversion en Double du contenu du textBox
                    if (this.tempmax.Text.Length == 0 || number1 <= number2) //Vérifier si la température minimale est inférieure à la maximale
                    {
                        if (number1 <= 100)// Ne pas entrer de valeur supérieure à 100°C
                        {
                            tempmi = number1; //Affectation de la valeur entée dans la textBox
                            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next); //Saut au prochain textBox
                            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
                            if (keyboardFocus != null)
                            {
                                keyboardFocus.MoveFocus(tRequest);
                                this.tempmin.Background = null;
                                this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            }
                        }
                        else //Message d'erreur
                        {
                            this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                            WpfMessageBox.Show("La température extérieure ne dépasse pas les 100°C", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                            this.tempmin.Clear();
                            this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                    }
                    else//Message d'erreur
                    {
                        if (number1 < number2)
                        {
                            this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                            WpfMessageBox.Show("La température minimale doit être inférieure à la maximale", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                            this.tempmin.Clear();
                            this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                    }
                }
                else//Message d'erreur
                {
                    this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                    WpfMessageBox.Show("Veuillez entrer une valeur numérique", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                    this.tempmin.Clear();
                    this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                }
            }
        }
        private void Tempmax_KeyDown(object sender, KeyEventArgs e)
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
            if (e.Key == Key.Enter)
            {
                double number1;
                double number2;
                if (Double.TryParse(this.tempmax.Text, out number2) == true)
                {
                    Double.TryParse(this.tempmin.Text, out number1);

                    if (this.tempmin.Text.Length == 0 || number1 <= number2)
                    {
                        if (number2 <= 100)
                        {
                            tempma = number2;
                            TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                            UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
                            if (keyboardFocus != null)
                            {
                                keyboardFocus.MoveFocus(tRequest);
                                this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                this.tempmax.Background = null;
                                this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            }
                        }
                        else
                        {
                            this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                            WpfMessageBox.Show("La température extérieure ne dépasse pas les 100°C", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                            this.tempmax.Clear();
                            this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                    }
                    else
                    {
                        if (number1 > number2)
                        {
                            this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                            WpfMessageBox.Show("La température minimale doit être inférieure à la maximale", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                            this.tempmax.Clear();
                            this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                        }
                    }
                }
                else
                {
                    this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                    WpfMessageBox.Show("Veuillez entrer une valeur numérique", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                    this.tempmax.Clear();
                    this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                }

            }
        }

        private void Humidite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                int number;

                if (Int32.TryParse(this.humidite.Text, out number) == true)
                {
                    if (number <= 100 && number >= 0)
                    {

                        if (this.nonhumide.IsChecked == true)
                        {
                            if (number > 29)
                            {
                                this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                                WpfMessageBox.Show("Vous avez séléctionné 'Temps sec' les valeurs sont donc comprises entre 0 % et 29 %", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                this.humidite.Clear();
                                this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                            }
                            else
                            {
                                humidit = number;

                                TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                                UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;


                                if (keyboardFocus != null)
                                {

                                    keyboardFocus.MoveFocus(tRequest);

                                    this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));

                                    this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                    this.Grid_Visib.Opacity = 0.9;

                                }

                            }
                        }
                        else
                        {
                            if (this.peuhumide.IsChecked == true)
                            {
                                if (number > 59 || number > 30)
                                {
                                    this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                                    WpfMessageBox.Show("Vous avez séléctionné 'Temps peu humide' les valeurs sont donc comprises entre 30 % et 59 %", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                    this.humidite.Clear();
                                    this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                }
                                else
                                {

                                    humidit = number;
                                    TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);


                                    UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;


                                    if (keyboardFocus != null)
                                    {

                                        keyboardFocus.MoveFocus(tRequest);

                                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));

                                        this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                        this.Grid_Visib.Opacity = 0.9;

                                    }

                                }
                            }
                            else
                            {
                                if (this.moyhumide.IsChecked == true)
                                {
                                    if (number > 89 || number < 60)
                                    {
                                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                                        WpfMessageBox.Show("Vous avez séléctionné 'Temps moyennement humide' les valeurs sont donc comprises entre 60 % et 89 %", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                        this.humidite.Clear();
                                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                    }
                                    else
                                    {
                                        humidit = number;
                                        TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);


                                        UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;


                                        if (keyboardFocus != null)
                                        {

                                            keyboardFocus.MoveFocus(tRequest);

                                            this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));

                                            this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                            this.Grid_Visib.Opacity = 0.9;

                                        }
                                    }
                                }
                                else
                                {
                                    if (number < 90)
                                    {
                                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                                        WpfMessageBox.Show("Vous avez séléctionné 'Temps très humide' les valeurs sont donc comprises entre 90 % et 100 %", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                        this.humidite.Clear();
                                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                    }
                                    else
                                    {
                                        humidit = number;
                                        TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);


                                        UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;


                                        if (keyboardFocus != null)
                                        {

                                            keyboardFocus.MoveFocus(tRequest);

                                            this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));

                                            this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                                            this.Grid_Visib.Opacity = 0.9;


                                        }

                                    }
                                }
                            }
                        }


                    }
                    else
                    {
                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                        WpfMessageBox.Show("Le taux d'humidité  est compris entre 0% et 100%", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                        this.humidite.Clear();
                        this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

                    }

                }

            }
        }
        private void Tempvent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                double number;
                if (Double.TryParse(this.tempvent.Text, out number) == true)
                {
                    if (number <= 100)
                    {
                        tempve = number;
                        TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                        UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
                        if (keyboardFocus != null)
                        {
                            keyboardFocus.MoveFocus(tRequest);
                            LinearGradientBrush back = new LinearGradientBrush();
                            this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));
                            this.GridHumide.Opacity = 0.9;
                        }
                    }
                    else
                    {
                        this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                        WpfMessageBox.Show("La température du vent ne dépasse pas les 100°C", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                        this.tempvent.Clear();
                        this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    }
                }


                else
                {
                    this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                    WpfMessageBox.Show("Veuillez entrer une valeur numérique.(Pour les valeurs décimales utilisez ',' et non pas '.')", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                    this.tempvent.Clear();
                    this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

                }
            }
        }
        private void Visibilite_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                double number;

                if (Double.TryParse(this.visibilite.Text, out number) == true)
                {
                    if (number <= 10 && number >= 0)
                    {

                        visib = number;
                        TraversalRequest tRequest = new TraversalRequest(FocusNavigationDirection.Next);
                        UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
                        if (keyboardFocus != null)
                        {
                            keyboardFocus.MoveFocus(tRequest);
                            this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77000000"));
                            this.Grid_Visib.Opacity = 0.7;
                            this.GridPrecip.Opacity = 0.9;
                        }

                    }
                    else
                    {
                        this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                        WpfMessageBox.Show("La visibilité est comprise entre 0 et 10", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                        this.visibilite.Clear();
                        this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                    }

                }
                else
                {
                    this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFF10B0B"));
                    WpfMessageBox.Show("Veuillez entrer une valeur numérique", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                    this.visibilite.Clear();
                    this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));
                }
            }
        }
        //CheckBox humidité : un seul champs ne peut être choisi à la fois 
        private void Moyhumide_Checked(object sender, RoutedEventArgs e)
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
            this.nonhumide.IsEnabled = false;
            this.peuhumide.IsEnabled = false;
            this.treshumide.IsEnabled = false;
            this.humidite.IsEnabled = true;  // lorsqu'un champs est choisi le textBox humitie est activé      
            this.Mhumide.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78")); //Changement de background 

        }
        private void Treshumide_Checked(object sender, RoutedEventArgs e)
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
            this.nonhumide.IsEnabled = false;
            this.moyhumide.IsEnabled = false;
            this.peuhumide.IsEnabled = false;
            this.humidite.IsEnabled = true;

            this.Thumide.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));
        }
        private void Peuhumide_Checked(object sender, RoutedEventArgs e)
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

            this.nonhumide.IsEnabled = false;
            this.moyhumide.IsEnabled = false;
            this.treshumide.IsEnabled = false;
            this.humidite.IsEnabled = true;
            this.Phumide.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));

        }
        private void Nonhumide_Checked(object sender, RoutedEventArgs e)
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
            this.peuhumide.IsEnabled = false;
            this.moyhumide.IsEnabled = false;
            this.treshumide.IsEnabled = false;
            this.humidite.IsEnabled = true;

            this.Nhumide.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));



        }

        //CheckBox humidité : Pour modifier son choix l'utilisateur uncheck le checkbox qu'il avait choisit
        private void Moyhumide_Unchecked(object sender, RoutedEventArgs e)
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

            this.peuhumide.IsEnabled = true;
            this.nonhumide.IsEnabled = true;
            this.treshumide.IsEnabled = true;
            this.humidite.IsEnabled = false; //Le textBlock humidité est désactivé
            this.Mhumide.Background = null;
        }
        private void Peuhumide_Unchecked(object sender, RoutedEventArgs e)
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

            this.nonhumide.IsEnabled = true;
            this.moyhumide.IsEnabled = true;
            this.treshumide.IsEnabled = true;
            this.humidite.IsEnabled = false;
            this.Phumide.Background = null;


        }
        private void Nonhumide_Unchecked(object sender, RoutedEventArgs e)
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
            this.peuhumide.IsEnabled = true;
            this.moyhumide.IsEnabled = true;
            this.treshumide.IsEnabled = true;
            this.humidite.IsEnabled = false;
            this.Nhumide.Background = null;


        }
        private void Treshumide_Unchecked(object sender, RoutedEventArgs e)
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
            this.peuhumide.IsEnabled = true;
            this.moyhumide.IsEnabled = true;
            this.nonhumide.IsEnabled = true;
            this.humidite.IsEnabled = false;

            this.Thumide.Background = null;
        }
        //CheckBox Pluie: un seul champs ne peut être choisi à la fois 
        private void Pluieintense_Checked(object sender, RoutedEventArgs e)
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
            LinearGradientBrush back = new LinearGradientBrush(); //Changement de background et désactivation des autres champs
            this.pasdepluie.IsEnabled = false;
            this.pluieimportante.IsEnabled = false;
            this.pluiemoderee.IsEnabled = false;
            back.StartPoint = new Point(0.5, 1);
            back.EndPoint = new Point(0.5, 0);
            GradientStop orangeGS = new GradientStop();
            GradientStop whiteGS = new GradientStop();
            orangeGS.Color = Color.FromRgb(199, 112, 137);
            orangeGS.Offset = 1;
            whiteGS.Color = Colors.White;
            back.GradientStops.Add(orangeGS);
            back.GradientStops.Add(whiteGS);
            back.Opacity = 100;
            this.Pin.FontWeight = FontWeights.Bold;
            this.Pin.Background = back;
            this.Pin.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF2F3C6C"));
            this.GridPrecip.Opacity = 0.7;
            this.GridVent.Opacity = 0.9;
        }
        private void Pluieimportante_Checked(object sender, RoutedEventArgs e)
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
            // LinearGradientBrush back = new LinearGradientBrush();
            this.pasdepluie.IsEnabled = false;
            this.pluieimportante.IsEnabled = false;
            this.pluiemoderee.IsEnabled = false;


            this.GridPrecip.Opacity = 0.7;
            this.GridVent.Opacity = 0.9;
            this.Pin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));

        }
        private void Pluiemoderee_Checked(object sender, RoutedEventArgs e)
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

            // LinearGradientBrush back = new LinearGradientBrush();
            this.pasdepluie.IsEnabled = false;
            this.pluieimportante.IsEnabled = false;
            this.pluieintense.IsEnabled = false;


            this.GridPrecip.Opacity = 0.7;
            this.GridVent.Opacity = 0.9;
            this.Pil.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));

        }
        private void Pasdepluie_Checked(object sender, RoutedEventArgs e)
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
            // LinearGradientBrush back = new LinearGradientBrush();
            this.pluieintense.IsEnabled = false;
            this.pluieimportante.IsEnabled = false;
            this.pluiemoderee.IsEnabled = false;
            this.GridPrecip.Opacity = 0.7;
            this.GridVent.Opacity = 0.9;
            this.Pdp.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));

        }
        private void Pluieintense_Unchecked(object sender, RoutedEventArgs e)
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
            // LinearGradientBrush back = new LinearGradientBrush();
            this.pluieimportante.IsEnabled = true;
            this.pluiemoderee.IsEnabled = true;
            this.pasdepluie.IsEnabled = true;

            this.Pin.Background = null;
            this.GridPrecip.Opacity = 0.9;
            this.GridVent.Opacity = 0.7;
        }
        private void Pluieimportante_Unchecked(object sender, RoutedEventArgs e)
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

            // LinearGradientBrush back = new LinearGradientBrush();
            this.pluieintense.IsEnabled = true;
            this.pluiemoderee.IsEnabled = true;
            this.pasdepluie.IsEnabled = true;

            this.Pim.Background = null;
            this.GridPrecip.Opacity = 0.9;
            this.GridVent.Opacity = 0.7;
        }
        private void Pluiemoderee_Unchecked(object sender, RoutedEventArgs e)
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
            // LinearGradientBrush back = new LinearGradientBrush();
            this.pluieimportante.IsEnabled = true;
            this.pluieintense.IsEnabled = true;
            this.pasdepluie.IsEnabled = true;
            this.Pil.Background = null;
            this.GridPrecip.Opacity = 0.9;
            this.GridVent.Opacity = 0.7;
        }
        private void Pasdepluie_Unchecked(object sender, RoutedEventArgs e)
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
            //LinearGradientBrush back = new LinearGradientBrush();
            this.pluieimportante.IsEnabled = true;
            this.pluiemoderee.IsEnabled = true;
            this.pluieintense.IsEnabled = true;

            this.Pdp.Background = null;

            this.GridPrecip.Opacity = 0.9;
            this.GridVent.Opacity = 0.7;
        }

        //Changement d'opacité des Grid au passage de la souris
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            this.GridHumide.Opacity = 0.9;
        }
        private void GridHumide_MouseLeave(object sender, MouseEventArgs e)
        {
            this.GridHumide.Opacity = 0.7;
        }
        private void GridTemp_MouseMove(object sender, MouseEventArgs e)
        {
            this.GridTemp.Opacity = 0.9;
        }
        private void GridTemp_MouseLeave(object sender, MouseEventArgs e)
        {
            this.GridTemp.Opacity = 0.7;
        }
        private void GridPrecip_MouseMove(object sender, MouseEventArgs e)
        {
            this.GridPrecip.Opacity = 0.9;
        }
        private void GridPrecip_MouseLeave(object sender, MouseEventArgs e)
        {
            this.GridPrecip.Opacity = 0.7;
        }
        //CheckBox vitesse du vent: un seul champs ne peut être choisi à la fois 
        private void Case1_Checked(object sender, RoutedEventArgs e)
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


            // LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = false;
            this.case3.IsEnabled = false;
            this.case4.IsEnabled = false;

            this.case1T.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));

        }
        private void Case1_Unchecked(object sender, RoutedEventArgs e)
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
            //  LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = true;
            this.case3.IsEnabled = true;
            this.case4.IsEnabled = true;

            this.case1T.Background = null;
        }
        private void Case2_Checked(object sender, RoutedEventArgs e)
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
            // LinearGradientBrush back = new LinearGradientBrush();
            this.case1.IsEnabled = false;
            this.case3.IsEnabled = false;
            this.case4.IsEnabled = false;

            this.case2T.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));

        }
        private void Case2_Unchecked(object sender, RoutedEventArgs e)
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
            //LinearGradientBrush back = new LinearGradientBrush();
            this.case1.IsEnabled = true;
            this.case3.IsEnabled = true;
            this.case4.IsEnabled = true;

            this.case2T.Background = null;
        }
        private void Case3_Checked(object sender, RoutedEventArgs e)
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
            //LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = false;
            this.case1.IsEnabled = false;
            this.case4.IsEnabled = false;
            this.case3T.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));

        }
        private void Case3_Unchecked(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            // LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = true;
            this.case1.IsEnabled = true;
            this.case4.IsEnabled = true;

            this.case3T.Background = null;
        }
        private void Case4_Checked(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            //LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = false;
            this.case3.IsEnabled = false;
            this.case1.IsEnabled = false;

            this.case4T.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));

        }
        private void Case4_Unchecked(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            //LinearGradientBrush back = new LinearGradientBrush();
            this.case2.IsEnabled = true;
            this.case3.IsEnabled = true;
            this.case1.IsEnabled = true;

            this.case4T.Background = null;
        }
        //Changement opacité des Grid au passage de la souris
        private void GridVent_MouseMove_1(object sender, MouseEventArgs e)
        {
            this.GridVent.Opacity = 0.9;
        }
        private void GridVent_MouseLeave_1(object sender, MouseEventArgs e)
        {
            this.GridVent.Opacity = 0.7;
        }

        private void Grid_Visib_MouseMove(object sender, MouseEventArgs e)
        {
            this.Grid_Visib.Opacity = 0.9;
        }
        private void Grid_Visib_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Grid_Visib.Opacity = 0.7;
        }
        private void Reinitialiser_MouseMove(object sender, MouseEventArgs e)
        {
            this.Reinitialiser.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFBAA4B"));
        }
        private void Enregistrer_MouseMove(object sender, MouseEventArgs e)
        {
            this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFBAA4B"));
        }
        private void Reinitialiser_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Reinitialiser.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));

        }
        private void Enregistrer_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#77D56C78"));

        }
        //Réunitialisation des données entrées 
        private void Reinitialiser_Click(object sender, RoutedEventArgs e)
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
            this.Reinitialiser.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFBAA4B"));

            Case1_Unchecked(case1, null); // Chackbox réinitialisées
            Case2_Unchecked(case2, null);
            Case3_Unchecked(case3, null);
            Case4_Unchecked(case4, null);
            Peuhumide_Unchecked(peuhumide, null);
            Moyhumide_Unchecked(moyhumide, null);
            Treshumide_Unchecked(treshumide, null);
            Nonhumide_Unchecked(nonhumide, null);
            Pluieimportante_Unchecked(pluieimportante, null);
            Pluieintense_Unchecked(pluieintense, null);
            Pluiemoderee_Unchecked(pluiemoderee, null);
            Pasdepluie_Unchecked(pasdepluie, null);

            this.tempmax.Clear(); //TextBox réinitialisées
            this.tempmin.Clear();
            this.tempvent.Clear();
            this.visibilite.Clear();
            this.humidite.Clear();
            this.humidite.IsEnabled = false;
            this.case1.IsChecked = false;
            this.case2.IsChecked = false;
            this.case3.IsChecked = false;
            this.case4.IsChecked = false;
            this.peuhumide.IsChecked = false;
            this.moyhumide.IsChecked = false;
            this.treshumide.IsChecked = false;
            this.nonhumide.IsChecked = false;
            this.pluieimportante.IsChecked = false;
            this.pluieintense.IsChecked = false;
            this.pluiemoderee.IsChecked = false;
            this.pasdepluie.IsChecked = false;
            /* LinearGradientBrush back = new LinearGradientBrush();
             back.StartPoint = new Point(0.5, 0);
             back.EndPoint = new System.Windows.Point(0.5, 1);
             GradientStop grayGS = new GradientStop();
             GradientStop whiteGS = new GradientStop();
             grayGS.Color = Color.FromRgb(176, 176, 176);
             grayGS.Offset = 1;
             whiteGS.Color = Colors.White;
             back.GradientStops.Add(grayGS);
             back.GradientStops.Add(whiteGS);
             back.Opacity = 100;
             this.visibilite.FontWeight = FontWeights.Normal;
             this.visibilite.Background = back;
             this.tempmax.Background = back;
             this.tempvent.Background = back;
             this.tempmin.Background = back;
             this.humidite.Background = back;*/
            GridHumide_MouseLeave(GridHumide, null);
            GridPrecip_MouseLeave(GridPrecip, null);
            GridTemp_MouseLeave(GridTemp, null);
            GridVent_MouseLeave_1(GridVent, null);
        }
        //Enregistrement des données + tests
        private void Enregistrer_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            //Si un champs n'a pas été rempli : message d'erreur
            if (this.tempmax.Text.Length == 0 || this.humidite.Text.Length == 0 || this.tempmin.Text.Length == 0 || this.visibilite.Text.Length == 0 || this.tempvent.Text.Length == 0 || (this.case1.IsChecked == false && this.case2.IsChecked == false && this.case3.IsChecked == false && this.case4.IsChecked == false) || (this.peuhumide.IsChecked == false && this.treshumide.IsChecked == false && this.moyhumide.IsChecked == false && this.nonhumide.IsChecked == false) || (this.pluieimportante.IsChecked == false && this.pasdepluie.IsChecked == false && this.pluieintense.IsChecked == false && this.pluiemoderee.IsChecked == false))
            {
                WpfMessageBox.Show("Veuillez remplir tous les champs.", " ", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

            }
            else
            {
                //Si un textBox ne contient pas de valeur décimale : message d'erreur
                if (Double.TryParse(tempmin.Text, out tempmi) == false || Double.TryParse(tempmax.Text, out tempma) == false || Double.TryParse(tempvent.Text, out tempve) == false || Double.TryParse(visibilite.Text, out visib) == false || Int32.TryParse(humidite.Text, out humidit) == false)
                {
                    WpfMessageBox.Show("", "Veuillez entrer des valeurs numériques.(Pour les valeurs décimales utilisez ',' et non pas '.')", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                    this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
                }
                else
                {
                    //Si la température minimale est supérieure à la maximale : message d'erreur
                    if (tempmi > tempma)
                    {
                        WpfMessageBox.Show("Erreur", "La température minimale est inférieure à la maximale", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);
                        this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
                        GridTemp_MouseMove(GridTemp, null);

                    }
                    else
                    {
                        //Si les valeurs entrées dépasse la valeur 100
                        if (tempmi > 100 || tempma > 100 || tempve > 100)
                        {
                            WpfMessageBox.Show("", "Les valeurs de la température sont inférieures à 100°C", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                            this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
                            GridTemp_MouseMove(GridTemp, null);
                        }

                        else
                        {
                            // Si la visibilité entrée n'est pas dans l'intervalle 0-10
                            if (visib > 10 || visib < 0)
                            {
                                WpfMessageBox.Show("", "La visibilité est comprise entre 0 et 10", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));
                                Grid_Visib_MouseMove(Grid_Visib, null);
                            }
                            else
                            {
                                if (humidit > 100 || humidit < 0)
                                {
                                    WpfMessageBox.Show("", "L'humidité est comprise entre 0% et 100%", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);

                                    this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                                }
                                //Tests de cohérence entre la description choisie concernant l'humidité et la valeur entrée
                                else
                                {
                                    if (peuhumide.IsChecked == true && (humidit > 59 || humidit < 30))
                                    {
                                        WpfMessageBox.Show("", "Vous avez séléctionné 'peu humide' le taux d'humidité est compris entre 30% et 59%", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                        this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                                    }
                                    else
                                    {
                                        if (moyhumide.IsChecked == true && (humidit > 89 || humidit < 60))
                                        {
                                            WpfMessageBox.Show("", "Vous avez séléctionné 'moyennement humide' le taux d'humidité est compris entre 60% et 89%", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                            this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                                        }
                                        else
                                        {
                                            if (treshumide.IsChecked == true && (humidit < 90))
                                            {
                                                WpfMessageBox.Show("", "Vous avez séléctionné 'très humide' le taux d'humidité est compris entre 90% et 100%", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                                this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                                            }
                                            else
                                            {
                                                if (nonhumide.IsChecked == true && (humidit > 29))
                                                {
                                                    WpfMessageBox.Show("", "Vous avez séléctionné 'temps sec' le taux d'humidité est compris entre 0% et 29%", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Warning);
                                                    this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF38779E"));

                                                }
                                                else
                                                {
                                                    this.Enregistrer.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFBAA4B"));
                                                    WpfMessageBox.Show("", "Vos données ont été enregistrées!", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Information);
                                                    enregistre = true;
                                                    output.main.temp_max = Convert.ToDouble(this.tempmax.Text);
                                                    output.main.temp_min = Convert.ToDouble(this.tempmin.Text);
                                                    output.wind.deg = Convert.ToDouble(this.tempvent.Text);
                                                    output.main.humidity = Convert.ToDouble(this.humidite.Text);
                                                    output.visibility = Convert.ToDouble(this.visibilite.Text);

                                                    double rain = 0;
                                                    if (this.pluieintense.IsChecked == true) rain = 10;
                                                    else if (this.pluieimportante.IsChecked == true) rain = 7;
                                                    else if (this.pluiemoderee.IsChecked == true) rain = 2;
                                                    else rain = 0;
                                                    output.precip = Convert.ToDouble(rain);
                                                    output.used = true;

                                                    double windspeed = 0;
                                                    if (this.case1.IsChecked == true) windspeed = 8;
                                                    if (this.case2.IsChecked == true) windspeed = 23;
                                                    if (this.case3.IsChecked == true) windspeed = 32;
                                                    if (this.case4.IsChecked == true) windspeed = 50;
                                                    output.wind.speed = Convert.ToDouble(windspeed);

                                                    this.Reinitialiser.IsEnabled = false;
                                                    this.tempmax.IsEnabled = false;
                                                    this.tempmin.IsEnabled = false;
                                                    this.tempvent.IsEnabled = false;
                                                    this.humidite.IsEnabled = false;
                                                    this.visibilite.IsEnabled = false;
                                                    this.case1.IsEnabled = false;
                                                    this.case2.IsEnabled = false;
                                                    this.case3.IsEnabled = false;
                                                    this.case4.IsEnabled = false;
                                                    this.pluieimportante.IsEnabled = false;
                                                    this.pluiemoderee.IsEnabled = false;
                                                    this.pluieintense.IsEnabled = false;
                                                    this.pasdepluie.IsEnabled = false;
                                                    this.peuhumide.IsEnabled = false;
                                                    this.moyhumide.IsEnabled = false;
                                                    this.nonhumide.IsEnabled = false;
                                                    this.treshumide.IsEnabled = false;

                                                    this.Close();
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Tempmin_MouseEnter(object sender, MouseEventArgs e)
        {
            //this.tempmin.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }
        private void Tempmax_MouseEnter(object sender, MouseEventArgs e)
        {
            // this.tempmax.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));


        }
        private void Tempmax_MouseLeave(object sender, MouseEventArgs e)
        {

            this.tempmax.Background = null;
        }
        private void Tempmin_MouseLeave(object sender, MouseEventArgs e)
        {

            this.tempmin.Background = null;
        }
        private void Tempvent_MouseLeave(object sender, MouseEventArgs e)
        {

            this.tempvent.Background = null;
        }
        private void Tempvent_MouseEnter(object sender, MouseEventArgs e)
        {
            // this.tempvent.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }
        private void Visibilite_MouseEnter(object sender, MouseEventArgs e)
        {
            //this.visibilite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }
        private void Visibilite_MouseLeave(object sender, MouseEventArgs e)
        {

            this.visibilite.Background = null;
        }

        private void Humidite_MouseEnter(object sender, MouseEventArgs e)
        {
            //this.humidite.Background = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFFFF"));

        }
        private void Humidite_MouseLeave(object sender, MouseEventArgs e)
        {

            this.humidite.Background = null;
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
                        recupe_man ev = new recupe_man(wilaya);
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
