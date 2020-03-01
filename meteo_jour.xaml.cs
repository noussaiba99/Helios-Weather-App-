using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Runtime.Serialization;
using System.Windows.Shapes;
using Helios;
using Microsoft.Win32;
using System.Runtime;

namespace Helios
{
    /// <summary>
    /// Logique d'interaction pour meteo_jour.xaml
    /// </summary>
    public partial class meteo_jour : Window
    {

    
        const string APPID = "3310e252d2c293fd401e73dbc9eda3bc";
       // string wilaya = "alger"; 
       string wilaya = File.ReadAllText(@"wilaya.txt");    //wilaya par defaut: Alger
      
        //wilaya par defaut: Alger
        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();


        public meteo_jour(string city, InfoJour.weatherinfo.Root output)
        {
            wilaya = wilaya.Trim(new Char[] { ' ', '\r', '\n', '\t' });
            InitializeComponent();
           wilaya=wilaya.Replace(" ", "");
       
            int nb = output.nbreJours;

            if (city == "alger") city = "kolea";
            else if (city == "tissemssilt") city = "tissemsilt";
            else city = city.Replace('-', ' ');

            //InfoJour.weatherinfo.Root output = new InfoJour.weatherinfo.Root();
            InfoJour.weatherinfo.getweather(city, APPID, ref output);   // one day weather
                                                                        //output.weatherList[0].description;

            if (city == "kolea")
            {
                city = "alger";
                lbl_cityName.Content = "Alger, Algérie";
            }
            else lbl_cityName.Content = string.Format("{0}", output.name) + " , Algérie";
            lbl_temp.Content = string.Format("{0} \u00B0" + "C", output.main.temp);
            lbl_hum.Content = string.Format("{0}", output.main.humidity) + " %";
            lbl_pres.Content = string.Format("{0}", output.main.pressure) + " hpa";
            lbl_visib.Content = string.Format("{0}", output.visibility) + " miles";
            lbl_vent.Content = string.Format("{0}", output.wind.speed) + " Km/h";
            lbl_sunset.Content = string.Format("{0}", InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset).Hour) + ":" + string.Format("{0}", InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset).Minute) + ":" + string.Format("{0}", InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset).Second);

            lbl_sunrise.Content = string.Format("{0}", InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise).Hour) + ":" + string.Format("{0}", InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise).Minute) + ":" + string.Format("{0}", InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise).Second);
             string desc = string.Format("{0}", output.weather[0].description);
             lbl_desc.Content = desc;
            /******************** date dynamique ******************************/
            System.Windows.Threading.DispatcherTimer monTimer = new System.Windows.Threading.DispatcherTimer();
            monTimer.Tick += (sender, eventArgs) =>
            {
                //on recupere la date actuelle
                DateTime date1 = DateTime.Now;
                //on chois notre format d'affichage
                string dt, dtt;
                dt = String.Format("{0:dddd ,dd MMMM yyyy}", date1);
                dtt = String.Format("{0: HH:mm: ss}", date1);
                // on ecrit sur le label, et 'est actualisé chaque seconde :ccool:
                date_meteo.Content = dt.ToString();
                date_meteo1.Content = dtt.ToString();
            };
            monTimer.Interval = TimeSpan.FromTicks(100);
            monTimer.Start();
            /*******************************fin date dynamique************************/
            switch (desc)
            {
                case "light rain":
                case "moderate rain":
                case "heavy intensity rain":
                case "very heavy rain":
                case "extreme rain":
                case "light intensity drizzle":
                case "drizzle":
                case "heavy intensity drizzle":
                case "light intensity drizzle rain":
                case "drizzle rain":
                case "heavy intensity drizzle rain":
                case "shower rain and drizzle":
                case "heavy shower rain and drizzle":
                case "shower drizzle":



                    {

                        lbl_desc.Content = "Pluie fine";
                        if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) < 0) && (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) > 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("lightrain1.jpg", UriKind.Relative));
                            back.Source = imag;
                            light_rain.Source = new BitmapImage(new Uri("41.png", UriKind.Relative));

                        }
                        
                        else if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) > 0)|| (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) <0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("lightrain2.jpg", UriKind.Relative));
                            back.Source = imag;
                            light_rain.Source = new BitmapImage(new Uri("46.png", UriKind.Relative));

                        }




                    }
                    break;
                case "few clouds":
                case "scattered clouds":
                case "broken clouds":
                case "overcast clouds":

                    {


                        lbl_desc.Content = "Nuageux";

                        if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) < 0) && (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) > 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("clouds1.jpg", UriKind.Relative));
                            back.Source = imag;
                            light_rain.Source = new BitmapImage(new Uri("28.png", UriKind.Relative));

                        }
                        if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) > 0) || (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) < 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("clouds2.jpg", UriKind.Relative));
                            back.Source = imag;
                            light_rain.Source = new BitmapImage(new Uri("27.png", UriKind.Relative));


                        }

                    }
                    break;
                case "clear sky":

                    {
                        if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) < 0) && (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) > 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("clear_sky3.jpg", UriKind.Relative));
                            back.Source = imag;
                            light_rain.Source = new BitmapImage(new Uri("32.png", UriKind.Relative));
                            lbl_desc.Content = "Ensoleillé";

                        }
                        else if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) > 0) || (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) < 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("clear_sky2.jpg", UriKind.Relative));
                            back.Source = imag;
                            light_rain.Source = new BitmapImage(new Uri("31.png", UriKind.Relative));
                            lbl_desc.Content = "Clair";

                        }

                        
                    }
                    break;
                case "rain":
                case "freezing rain":
                case "light intensity shower rain":
                case "shower rain":
                case "heavy intensity shower rain":
                case "ragged shower rain":
                    {
                        light_rain.Source = new BitmapImage(new Uri("10.png", UriKind.Relative));

                        if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) < 0) && (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) > 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("rain1.jpg", UriKind.Relative));
                            back.Source = imag;

                        }
                        else if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) > 0) || (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) < 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("rain2.jpg", UriKind.Relative));
                            back.Source = imag;
                        }

                        lbl_desc.Content = "Pluie";
                    }
                    break;
                case "thunderstorm with light rain":
                case "thunderstorm with rain":
                case "thunderstorm with heavy rain":
                case "light thunderstorm":
                case "thunderstorm":
                case "heavy thunderstorm":
                case "ragged thunderstorm":
                case "thunderstorm with light drizzle":
                case "thunderstorm with drizzle":
                case "thunderstorm with heavy drizzle":


                    {
                        light_rain.Source = new BitmapImage(new Uri("4.png", UriKind.Relative));

                        if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) < 0) && (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) > 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("orage1.jpg", UriKind.Relative));
                            back.Source = imag;
                        }
                        else if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) > 0) || (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) < 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("orage2.jpg", UriKind.Relative));
                            back.Source = imag;
                        }


                        lbl_desc.Content = "Orage";


                    }
                    break;

                case "mist":
                case "smoke":
                case "haze":
                case "sand, dust whirls":
                case "fog":
                case "sand":
                case "dust":
                case "volcanic ash":
                case "squalls":
                case "tornado":

                    {
                        light_rain.Source = new BitmapImage(new Uri("24.png", UriKind.Relative));

                        if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) < 0) && (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) > 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("tornade1.jpg", UriKind.Relative));
                            back.Source = imag;
                        }
                        else if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) > 0) || (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) < 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("tornade2.jpg", UriKind.Relative));
                            back.Source = imag;
                        }

                        lbl_desc.Content = "Tournade";


                    }
                    break;

                case "light snow":
                case "Heavy snow":
                case "Light shower sleet":
                case "Shower sleet":
                case "Light rain and snow":
                case "Rain and snow":
                case "Light shower snow":
                case "Shower snow":
                case "Heavy shower snow":


                    {
                        light_rain.Source = new BitmapImage(new Uri("14.png", UriKind.Relative));


                        lbl_desc.Content = "Neige";
                        if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) < 0) && (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) > 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("snow.jpg", UriKind.Relative));
                            back.Source = imag;
                        }
                        else if ((DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunset)) > 0) || (DateTime.Compare(InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt), InfoJour.weatherinfo.UnixTimeStampToDateTime(output.sys.sunrise)) < 0))
                        {
                            BitmapImage imag = new BitmapImage(new Uri("snow.jpg", UriKind.Relative));
                            back.Source = imag;
                        }

                    }
                    break;

                default:
                    MessageBox.Show("Erreur", "Aucune condition trouvée", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }

            city = city.Replace(' ', '-');
            if (city == "tissemsilt") city = "tissemssilt";
            wilaya = city;
            output_out = output;
            output_out.nbreJours = nb;

            //ManipBasedeDonnees.add_BDD(output, wilaya);       //ajouter l'obseevation du jour au dataset
        }

        private ToolTip newToolTip()
        {
            throw new NotImplementedException();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            Application.Current.Shutdown();
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }

        private void ComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();

            ComboBox1.BorderBrush = Brushes.White;
            string str = ComboBox1.Text;

            switch (str)
            {
                case "Adrar":
                    wilaya = "adrar";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }

                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Ain Defla":
                    wilaya = "ain-defla";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Ain Témouchent":
                    wilaya = "ain-temouchent";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }

                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Alger":
                    wilaya = "alger";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Ghardaia":
                    wilaya = "ghardaia";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Annaba":
                    wilaya = "annaba";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Batna":
                    wilaya = "batna";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Béchar":
                    wilaya = "bechar";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Béjaia":
                    wilaya = "bejaia";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Biskra":
                    wilaya = "biskra";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Blida":
                    wilaya = "blida";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Bordj Bou Arréridj":
                    wilaya = "bordj-bou-arreridj";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Bouira":
                    wilaya = "bouira";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Chlef":
                    wilaya = "chlef";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Boumerdès":
                    wilaya = "boumerdes";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Constantine":
                    wilaya = "constantine";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Djelfa":
                    wilaya = "djelfa";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "El Bayadh":
                    wilaya = "el-bayadh";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "El Taref":
                    wilaya = "el-tarf";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "El-Oued":
                    wilaya = "el-oued";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Guelma":
                    wilaya = "guelma";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Illizi":
                    wilaya = "illizi";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Jijel":
                    wilaya = "jijel";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Laghouat":
                    wilaya = "laghouat";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Khenchela":
                    wilaya = "khenchela";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "M’Sila":
                    wilaya = "msila";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Mascara":
                    wilaya = "mascara";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Médéa":
                    wilaya = "medea";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Mila":
                    wilaya = "mila";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Mostaganem":
                    wilaya = "mostaganem";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Naâma":
                    wilaya = "naama";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Oran":
                    wilaya = "oran";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Ouargla":
                    wilaya = "ouargla";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Oum El Bouaghi":
                    wilaya = "oum-el-bouaghi";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Relizane":
                    wilaya = "relizane";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Saida":
                    wilaya = "saida";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Sidi BelAbbès":
                    wilaya = "sidi-bel-abbes";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Skikda":
                    wilaya = "skikda";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Souk Ahras":
                    wilaya = "souk-ahras";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tamanrasset":
                    wilaya = "tamanrasset";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tiaret":
                    wilaya = "tiaret";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tindouf":
                    wilaya = "tindouf";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tébessa":
                    wilaya = "tebessa";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tipaza":
                    wilaya = "tipaza";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tissemsilt":
                    wilaya = "tissemssilt";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tizi-Ouzou":
                    wilaya = "tizi-ouzou";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tlemcen":
                    wilaya = "tlemcen";
                    if (this.output_out.name != wilaya) { InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root(); }
                    if (InfoJour.test_cnx() == 1)
                    {
                        meteo_jour nvv = new meteo_jour(wilaya, output_out);
                        nvv.Show();
                        this.Close();
                    }
                    else
                    {
                        prevision prv = new prevision(wilaya, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;

            }
            // wilaya sera l'entrée de prévision en cas ou pas connex et de méteo du jour si y a de la conex
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
            /*if (InfoJour.test_cnx() == 1)
            {
                meteo_jour nvv = new meteo_jour(wilaya);
                nvv.Show();
                this.Close();
            }
            else
            {
                prevision prv = new prevision(wilaya);
                prv.Show();
                this.Close();
            }*/
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
        
        private void evolut(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
            evolution ev = new evolution(wilaya, output_out);
            ev.Show();
            this.Close();
        }
        private void generall(object sender, RoutedEventArgs e)
        {
            MediaPlayer player = new MediaPlayer();
            player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
            player.Play();
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
     


        private void CreateScreenShot(UIElement visual, string file)

        {
          
            double width = Convert.ToDouble(visual.GetValue(FrameworkElement.WidthProperty));

            double height = Convert.ToDouble(visual.GetValue(FrameworkElement.HeightProperty));



            if (double.IsNaN(width) || double.IsNaN(height))

            {

                throw new FormatException("Erreur !!");

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
                using (FileStream stream = new FileStream(file, FileMode.Create))

                {

                    PngBitmapEncoder encoder = new PngBitmapEncoder();



                    encoder.Frames.Add(BitmapFrame.Create(render));

                   // WpfMessageBox.Show("Réussi", "Capture prise avec succés", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Information);
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
        private void power_click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void minimize_click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }


        /****************************************************/

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.Winapi)]
        internal static extern IntPtr GetFocus();
        private Control GetFocusedControl()
        {
            Control focusedControl = null;
            // To get hold of the focused control:
            IntPtr focusedHandle = GetFocus();
            /* if (focusedHandle != IntPtr.Zero)
                 // Note that if the focused Control is not a .Net control, then this will return null.
               //  focusedControl = Control.(focusedHandle);*/
            return focusedControl;
        }


        private void CreateAndLoadRichTextBox()
        {
            // Create a FlowDocument  
            FlowDocument mcFlowDoc = new FlowDocument();
            // Create a paragraph with text  
            Paragraph para = new Paragraph();
            Paragraph para1 = new Paragraph();
            para1.Inlines.Add(new Bold(new Run("Infos sur Oran :")));
            para.Inlines.Add(new Run("Oran est une ville portuaire au nord-ouest de l'Algérie. Elle est considérée comme le berceau de la musique raï. Le fort de Santa Cruz, une citadelle ottomane reconstruite par les Espagnols, se trouve au sommet du mont Murdjadjo ; il offre une vue sur la baie en contrebas. La chapelle Notre-Dame de Santa Cruz, aux murs blanchis, se trouve à proximité. Elle fut érigée en l'honneur de son homonyme après une épidémie de choléra. À la Blanca, la vieille ville turque, se situe la mosquée Hassan Pacha. Cette dernière est dotée d'un minaret octogonal. "));

            para.FontFamily = new FontFamily("Roboto");
            para.FontSize = 18;
            para.Foreground = Brushes.White;
            para1.FontFamily = new FontFamily("Roboto");
            para1.FontSize = 18;
            para1.Foreground = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFEAC72D");
            // Add the paragraph to blocks of paragraph  

            mcFlowDoc.Blocks.Add(para1);
            mcFlowDoc.Blocks.Add(para);
            // Create RichTextBox, set its hegith and width  
            RichTextBox mcRTB = new RichTextBox();
            mcRTB.Width = 350;
            mcRTB.Height = 455;
            mcRTB.Margin = new Thickness(272, 75, 628, 70);




            // Set contents  
            mcRTB.Background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#7F1D3340");
            mcRTB.Document = mcFlowDoc;

            // Add RichTextbox to the container  
            gridfull.Children.Add(mcRTB);
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
        /*******************************************************/
    }

}
