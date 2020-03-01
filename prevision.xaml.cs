using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Helios
{
    /// <summary>
    /// Logique d'interaction pour prevision.xaml
    /// </summary>
    public partial class prevision : Window
    {

        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();
  

    
        string wilaya3 = File.ReadAllText(@"wilaya.txt");       //Lecture de la wilaya par defaut à partir du fichier
        String appid = "07c468b91d0fb941aab0ef82d2198883";     //La clé utilisée pour se connecter au site OpenWeatherMap

        int nbre_jours = 1;
        public prevision(String wilaya, InfoJour.weatherinfo.Root output)
        {
            wilaya3 = wilaya3.Trim(new Char[] { ' ', '\r', '\n', '\t' });
            InitializeComponent();
            nbre_jours = output.nbreJours;  
            System.Windows.Threading.DispatcherTimer monTimer = new System.Windows.Threading.DispatcherTimer();
            monTimer.Tick += (sender, eventArgs) =>
            {
                //on recupere la date actuelle
                DateTime date1 = DateTime.Now;
                //on choisis notre format d'affichage
                string dt;
                dt = String.Format("{0:dddd  HH:mm:ss}", date1);
                //on ecrit sur le label, et 'est actualisé chaque seconde :ccool:
            };
            monTimer.Interval = TimeSpan.FromTicks(100);
            monTimer.Start();

            recupe_man rec = new recupe_man( wilaya);
            bool how = false;

            int cnx = InfoJour.test_cnx();    //Test s'il ya connexion internet
            if (cnx == 1)     //S'il ya connexion internet, on appelle getweather qui récupère la météo du jour à partir du site OpenWeatherMap
            {
                if (wilaya == "alger") wilaya = "kolea";
                else if (wilaya == "tissemssilt") wilaya = "tissemsilt";
                else wilaya = wilaya.Replace('-', ' ');

                InfoJour.weatherinfo.getweather(wilaya, appid, ref output);
                how = true;

                if (wilaya == "kolea") wilaya = "alger";
                if (wilaya == "tissemsilt") wilaya = "tissemssilt";
                else wilaya = wilaya.Replace(' ', '-');
            }
            else if (output.used == false)
            {
                //sinon, en cas où il n'y a pas connexion internet, on passe a la récupération manuelle du l'observation du jour
                rec.ShowDialog();
                output = rec.output;
                if (output.precip!= -1) how = true;

            }
            else how = true;       
            if (how == false)
            {
                //test de contrôle
                goto end;
            }

            int[] tab_id = new int[400];
            ManipBasedeDonnees.recherche(output, tab_id, wilaya);    
            //Effectuer la recherche de l'observation du jour pour la wilaya choisie dans la BDD et mettre les ID correspondants dans une table.
            if (tab_id[0] == 0)     //Si aucune des observations météorologiques enregistrées corresponde à l'observation du jour, on quitte.
            {
                WpfMessageBox.Show("L'observation du jour n'est pas valide pour cette wilaya");
                how = false;
                goto end;
            }
            Prev[] tab_prev = new Prev[400];

            ManipBasedeDonnees.previsions(tab_id, tab_prev, nbre_jours, wilaya);   //Récuperer les prévisions correspondantes à partir de la BDD

            ElemTabproba[] tableProba = new ElemTabproba[13];
            tableProba = Traitement.Probabilite(tab_prev);          //Calculer les probabilitées de chaque prévision

            Prev b = new Prev();
            b = Traitement.Prévision_final(tableProba, nbre_jours);     //Renvoie la prévision finale qui va être affichée en premier.


            //Afficher le résultat de la prévision sur la fenêtre.
            String ch1 = DateTime.Now.Date.AddDays(1).DayOfWeek.ToString();
            String ch2 = null;
            switch (ch1)
            {
                case "Saturday":
                    ch2 = "Samedi";
                    break;
                case "Sunday":
                    ch2 = "Dimanche";
                    break;
                case "Monday":
                    ch2 = "Lundi";
                    break;
                case "Tuesday":
                    ch2 = "Mardi";
                    break;
                case "Thursday":
                    ch2 = "Jeudi";
                    break;
                case "Friday":
                    ch2 = "Vendredi";
                    break;
                case "Wednesday":
                    ch2 = "Mercredi";
                    break;
            }
            item0.Content = "  " +  ch2 + " " + DateTime.Now.Date.AddDays(1).Day.ToString() + "/" + DateTime.Now.Date.AddDays(1).Month.ToString() + "/" + DateTime.Now.Date.AddDays(1).Year.ToString();
            ch1 = DateTime.Now.Date.AddDays(2).DayOfWeek.ToString();
            ch2 = null;
            switch (ch1)  
            {

                case "Saturday":
                    ch2 = "Samedi";
                    break;
                case "Sunday":
                    ch2 = "Dimanche";
                    break;
                case "Monday":
                    ch2 = "Lundi";
                    break;           
                case "Tuesday":
                    ch2 = "Mardi";
                    break;
                case "Thursday":
                    ch2 = "Jeudi";
                    break;
                case "Friday":
                    ch2 = "Vendredi";
                    break;
                case "Wednesday":
                    ch2 = "Mercredi";
                    break;
            }
            item1.Content = "  " + ch2 + " " + DateTime.Now.Date.AddDays(2).Day.ToString() + "/" + DateTime.Now.Date.AddDays(2).Month.ToString() + "/" + DateTime.Now.Date.AddDays(2).Year.ToString();
            ch1 = DateTime.Now.Date.AddDays(3).DayOfWeek.ToString();
            ch2 = null;
            switch (ch1)
            {

                case "Saturday":
                    ch2 = "Samedi";
                    break;
                case "Sunday":
                    ch2 = "Dimanche";
                    break;
                case "Monday":
                    ch2 = "Lundi";
                    break;
                case "Tuesday":
                    ch2 = "Mardi";
                    break;
                case "Thursday":
                    ch2 = "Jeudi";
                    break;
                case "Friday":
                    ch2 = "Vendredi";
                    break;
                case "Wednesday":
                    ch2 = "Mercredi";
                    break;
            }
            item2.Content = "  " + ch2 + " " + DateTime.Now.Date.AddDays(3).Day.ToString() + "/" + DateTime.Now.Date.AddDays(3).Month.ToString() + "/" + DateTime.Now.Date.AddDays(3).Year.ToString();
            ch1 = DateTime.Now.Date.AddDays(4).DayOfWeek.ToString();
            ch2 = null;
            switch (ch1)
            {

                case "Saturday":
                    ch2 = "Samedi";
                    break;
                case "Sunday":
                    ch2 = "Dimanche";
                    break;
                case "Monday":
                    ch2 = "Lundi";
                    break;
                case "Tuesday":
                    ch2 = "Mardi";
                    break;
                case "Thursday":
                    ch2 = "Jeudi";
                    break;
                case "Friday":
                    ch2 = "Vendredi";
                    break;
                case "Wednesday":
                    ch2 = "Mercredi";
                    break;
            }
            item3.Content = "  " + ch2 + " " + DateTime.Now.Date.AddDays(4).Day.ToString() + "/" + DateTime.Now.Date.AddDays(4).Month.ToString() + "/" + DateTime.Now.Date.AddDays(4).Year.ToString();
            ch1 = DateTime.Now.Date.AddDays(5).DayOfWeek.ToString();
            ch2 = null;
            switch (ch1)
            {

                case "Saturday":
                    ch2 = "Samedi";
                    break;
                case "Sunday":
                    ch2 = "Dimanche";
                    break;
                case "Monday":
                    ch2 = "Lundi";
                    break;
                case "Tuesday":
                    ch2 = "Mardi";
                    break;
                case "Thursday":
                    ch2 = "Jeudi";
                    break;
                case "Friday":
                    ch2 = "Vendredi";
                    break;
                case "Wednesday":
                    ch2 = "Mercredi";
                    break;
            }
            item4.Content = "  " + ch2 + " " + DateTime.Now.Date.AddDays(5).Day.ToString() + "/" + DateTime.Now.Date.AddDays(5).Month.ToString() + "/" + DateTime.Now.Date.AddDays(5).Year.ToString();
            ch1 = DateTime.Now.Date.AddDays(6).DayOfWeek.ToString();
            ch2 = null;
            switch (ch1)
            {

                case "Saturday":
                    ch2 = "Samedi";
                    break;
                case "Sunday":
                    ch2 = "Dimanche";
                    break;
                case "Monday":
                    ch2 = "Lundi";
                    break;
                case "Tuesday":
                    ch2 = "Mardi";
                    break;
                case "Thursday":
                    ch2 = "Jeudi";
                    break;
                case "Friday":
                    ch2 = "Vendredi";
                    break;
                case "Wednesday":
                    ch2 = "Mercredi";
                    break;
            }
            item5.Content = "  " + ch2 + " " + DateTime.Now.Date.AddDays(6).Day.ToString() + "/" + DateTime.Now.Date.AddDays(6).Month.ToString() + "/" + DateTime.Now.Date.AddDays(6).Year.ToString();
         
            item0.Background = Brushes.Transparent;
            item1.Background = Brushes.Transparent;
            item2.Background = Brushes.Transparent;
            item3.Background = Brushes.Transparent;
            item4.Background = Brushes.Transparent;
            item5.Background = Brushes.Transparent;

            item0.Foreground = Brushes.Black;
            item1.Foreground = Brushes.Black;
            item2.Foreground = Brushes.Black;
            item3.Foreground = Brushes.Black;
            item4.Foreground = Brushes.Black;
            item5.Foreground = Brushes.Black;
            ComboBox2.Foreground = Brushes.Black;
            ComboBox2.Background = Brushes.Transparent;
           

            if (wilaya=="alger") lbl_cityName.Content = "Alger, Algérie";
            else lbl_cityName.Content = output.name+", Algérie";

            wilaya3 = wilaya;

            Btn_temperat_matin.Content = (" Matin: " + b.GetTempMorning() + "°C");
            Btn_temperat_soir.Content = (" Soir: " + b.GetTempEvening() + "°C");
            Btn_temperat.Content = (((b.GetTempMax() + b.GetTempMin()) / 2) + "°C");
            Btn_humid.Content = ("Humid: " + b.GetHumidity() + "%");
            Btn_visibility.Content = ("Visib: " + b.GetVisibility() + " miles");
            Btn_vitesse_vent.Content = ("Vent: " + b.GetWindSpeed() + "km/h");
            Btn_pressure.Content = ("Press: " + b.GetPressure() + "hpa");

            if (tableProba[5].GetLentghValeurs() >= 1) proba1_humid.Content = Math.Round(tableProba[5].GetValeurs()[0].GetValeur(),2) + "% (" + Math.Round(tableProba[5].GetValeurs()[0].GetProba() * 100,2) + "%)";
            if (tableProba[5].GetLentghValeurs() >= 2) proba2_humid.Content = Math.Round(tableProba[5].GetValeurs()[1].GetValeur(),2) + "% (" + Math.Round(tableProba[5].GetValeurs()[1].GetProba() * 100,2) + "%)";
            if (tableProba[5].GetLentghValeurs() >= 3) proba3_humid.Content = Math.Round(tableProba[5].GetValeurs()[2].GetValeur(),2) + "% (" + Math.Round(tableProba[5].GetValeurs()[2].GetProba() * 100,2) + "%)";

            if (tableProba[13].GetLentghValeurs() >= 1) proba1_vent.Content = Math.Round(tableProba[13].GetValeurs()[0].GetValeur(),2) + "Km/h (" + Math.Round(tableProba[13].GetValeurs()[0].GetProba() * 100,2) + "%)";
            if (tableProba[13].GetLentghValeurs() >= 2) proba2_vent.Content = Math.Round(tableProba[13].GetValeurs()[1].GetValeur(),2) + "Km/h (" + Math.Round(tableProba[13].GetValeurs()[1].GetProba() * 100,2) + "%)";
            if (tableProba[13].GetLentghValeurs() >= 3) proba3_vent.Content = Math.Round(tableProba[13].GetValeurs()[2].GetValeur(),2) + "Km/h (" + Math.Round(tableProba[13].GetValeurs()[2].GetProba() * 100,2) + "%)";

            if (tableProba[12].GetLentghValeurs() >= 1) proba1_visibility.Content = Math.Round(tableProba[12].GetValeurs()[0].GetValeur(),2) + "miles (" + Math.Round(tableProba[12].GetValeurs()[0].GetProba() * 100,2) + "%)";
            if (tableProba[12].GetLentghValeurs() >= 2) proba2_visibility.Content = Math.Round(tableProba[12].GetValeurs()[1].GetValeur(), 2) + "miles (" + Math.Round(tableProba[12].GetValeurs()[1].GetProba() * 100,2) + "%)";
            if (tableProba[12].GetLentghValeurs() >= 3) proba3_visibility.Content = Math.Round(tableProba[12].GetValeurs()[2].GetValeur(), 2) + "miles (" + Math.Round(tableProba[12].GetValeurs()[2].GetProba() * 100,2) + "%)";

            if (tableProba[7].GetLentghValeurs() >= 1) proba1_pressure.Content = Math.Round(tableProba[7].GetValeurs()[0].GetValeur(), 2) + "hpa (" + Math.Round(tableProba[7].GetValeurs()[0].GetProba() * 100,2) + "%)";
            if (tableProba[7].GetLentghValeurs() >= 2) proba2_pressure.Content = Math.Round(tableProba[7].GetValeurs()[1].GetValeur(), 2) + "hpa (" + Math.Round(tableProba[7].GetValeurs()[1].GetProba() * 100,2) + "%)";
            if (tableProba[7].GetLentghValeurs() >= 3) proba3_pressure.Content = Math.Round(tableProba[7].GetValeurs()[2].GetValeur(), 2) + "hpa (" + Math.Round(tableProba[7].GetValeurs()[2].GetProba() * 100,2) + "%)";



            exp1.Header = "Point de rosée";
            if (tableProba[3].GetLentghValeurs() >= 1) txt1.Text = (Math.Round(tableProba[3].GetValeurs()[0].GetValeur(),2) + "°C (" + Math.Round(tableProba[3].GetValeurs()[0].GetProba() * 100,2) + "%)");
            if (tableProba[3].GetLentghValeurs() >= 2) txt2.Text = (Math.Round(tableProba[3].GetValeurs()[1].GetValeur(),2) + "°C (" + Math.Round(tableProba[3].GetValeurs()[1].GetProba() * 100,2) + "%)");
            if (tableProba[3].GetLentghValeurs() >= 3) txt3.Text = (Math.Round(tableProba[3].GetValeurs()[2].GetValeur(),2) + "°C (" + Math.Round(tableProba[3].GetValeurs()[2].GetProba() * 100,2) + "%)");

            exp2.Header = "Indice de chaleur";
            if (tableProba[4].GetLentghValeurs() >= 1) txt4.Text = (Math.Round(tableProba[4].GetValeurs()[0].GetValeur(),2) + "°C (" + Math.Round(tableProba[4].GetValeurs()[0].GetProba(),2) * 100 + "%)");
            if (tableProba[4].GetLentghValeurs() >= 2) txt5.Text = (Math.Round(tableProba[4].GetValeurs()[1].GetValeur(),2) + "°C (" + Math.Round(tableProba[4].GetValeurs()[1].GetProba(),2) * 100 + "%)");
            if (tableProba[4].GetLentghValeurs() >= 3) txt6.Text = (Math.Round(tableProba[4].GetValeurs()[2].GetValeur(),2) + "°C (" + Math.Round(tableProba[4].GetValeurs()[2].GetProba(),2) * 100 + "%)");

            exp5.Header = "Précipitation";
            if (tableProba[6].GetLentghValeurs() >= 1) txt44.Text = (Math.Round(tableProba[6].GetValeurs()[0].GetValeur(),2) + " Mm (" + Math.Round(tableProba[6].GetValeurs()[0].GetProba(), 2) * 100 + "%)");
            if (tableProba[6].GetLentghValeurs() >= 2) txt55.Text = (Math.Round(tableProba[6].GetValeurs()[1].GetValeur(),2) + " Mm (" + Math.Round(tableProba[6].GetValeurs()[1].GetProba(), 2) * 100 + "%)");
            if (tableProba[6].GetLentghValeurs() >= 3) txt66.Text = (Math.Round(tableProba[6].GetValeurs()[2].GetValeur(),2) + " Mm (" + Math.Round(tableProba[6].GetValeurs()[2].GetProba(), 2) * 100 + "%)");

            exp6.Header = "Nuages";
            if (tableProba[2].GetLentghValeurs() >= 1) txt77.Text = (Math.Round(tableProba[2].GetValeurs()[0].GetValeur(),2) + "% (" + Math.Round(tableProba[2].GetValeurs()[0].GetProba(), 2) * 100 + "%)");
            if (tableProba[2].GetLentghValeurs() >= 2) txt88.Text = (Math.Round(tableProba[2].GetValeurs()[1].GetValeur(),2) + "% (" + Math.Round(tableProba[2].GetValeurs()[1].GetProba(), 2) * 100 + "%)");
            if (tableProba[2].GetLentghValeurs() >= 3) txt99.Text = (Math.Round(tableProba[2].GetValeurs()[2].GetValeur(),2) + "% (" + Math.Round(tableProba[2].GetValeurs()[2].GetProba(), 2) * 100 + "%)");

            exp3.Header = "Temp soir";
            if (tableProba[8].GetLentghValeurs() >= 1) txt7.Text = (Math.Round(tableProba[8].GetValeurs()[0].GetValeur(),2) + "°C (" + Math.Round(tableProba[8].GetValeurs()[0].GetProba(),2) * 100 + "%)");
            if (tableProba[8].GetLentghValeurs() >= 2) txt8.Text = (Math.Round(tableProba[8].GetValeurs()[1].GetValeur(),2) + "°C (" + Math.Round(tableProba[8].GetValeurs()[1].GetProba(), 2) * 100 + "%)");
            if (tableProba[8].GetLentghValeurs() >= 3) txt9.Text = (Math.Round(tableProba[8].GetValeurs()[2].GetValeur(),2) + "°C (" + Math.Round(tableProba[8].GetValeurs()[2].GetProba(), 2) * 100 + "%)");

            exp7.Header = "Temp matin";
            if (tableProba[9].GetLentghValeurs() >= 1) txt111.Text = (Math.Round(tableProba[9].GetValeurs()[0].GetValeur(), 2) + "°C (" + Math.Round(tableProba[9].GetValeurs()[0].GetProba(), 2) * 100 + "%)");
            if (tableProba[9].GetLentghValeurs() >= 2) txt222.Text = (Math.Round(tableProba[9].GetValeurs()[1].GetValeur(), 2) + "°C (" + Math.Round(tableProba[9].GetValeurs()[1].GetProba(), 2) * 100 + "%)");
            if (tableProba[9].GetLentghValeurs() >= 3) txt333.Text = (Math.Round(tableProba[9].GetValeurs()[2].GetValeur(), 2) + "°C (" + Math.Round(tableProba[9].GetValeurs()[2].GetProba(), 2) * 100 + "%)");

            exp4.Header = "Temp Min";
            if (tableProba[0].GetLentghValeurs() >= 1) txt11.Text = (Math.Round(tableProba[0].GetValeurs()[0].GetValeur(), 2) + "°C (" + Math.Round(tableProba[0].GetValeurs()[0].GetProba(), 2) * 100 + "%)");
            if (tableProba[0].GetLentghValeurs() >= 2) txt22.Text = (Math.Round(tableProba[0].GetValeurs()[1].GetValeur(), 2) + "°C (" + Math.Round(tableProba[0].GetValeurs()[1].GetProba(), 2) * 100 + "%)");
            if (tableProba[0].GetLentghValeurs() >= 3) txt33.Text = (Math.Round(tableProba[0].GetValeurs()[2].GetValeur(), 2) + "°C (" + Math.Round(tableProba[0].GetValeurs()[2].GetProba(), 2) * 100 + "%)");

            exp8.Header = "Temp Max";
            if (tableProba[1].GetLentghValeurs() >= 1) txt444.Text = (Math.Round(tableProba[1].GetValeurs()[0].GetValeur(), 2) + "°C (" + Math.Round(tableProba[1].GetValeurs()[0].GetProba(), 2) * 100 + "%)");
            if (tableProba[1].GetLentghValeurs() >= 2) txt555.Text = (Math.Round(tableProba[1].GetValeurs()[1].GetValeur(), 2) + "°C (" + Math.Round(tableProba[1].GetValeurs()[1].GetProba(), 2) * 100 + "%)");
            if (tableProba[1].GetLentghValeurs() >= 3) txt666.Text = (Math.Round(tableProba[1].GetValeurs()[2].GetValeur(), 2) + "°C (" + Math.Round(tableProba[1].GetValeurs()[2].GetProba(), 2) * 100 + "%)");

            string desc = "";
            if ((b.GetVisibility() == 10) && (b.GetCloudCover() == 0) && (b.GetPrecipation() == 0) && (b.GetTempMin() > 25))
            {
               
                    desc = "Ensoleillé";
                    BitmapImage imag = new BitmapImage(new Uri("clear_sky3.jpg", UriKind.Relative));
                    back.Source = imag;
                    light_rain.Source = new BitmapImage(new Uri("32.png", UriKind.Relative));
               
            }
            else if ((b.GetVisibility() <= 10) && (b.GetCloudCover() > 10) && (b.GetPrecipation() == 0) && (b.GetTempMin() > 17))
            {
                    desc = "Plutôt ensoleillé";
                    BitmapImage imag = new BitmapImage(new Uri("clear_sky3.jpg", UriKind.Relative));
                    back.Source = imag;
                    light_rain.Source = new BitmapImage(new Uri("34.png", UriKind.Relative));
            }
            else if (b.GetPrecipation() > 0)
            {
                desc = "Averses";
                light_rain.Source = new BitmapImage(new Uri("10.png", UriKind.Relative));

                BitmapImage imag = new BitmapImage(new Uri("rain1.jpg", UriKind.Relative));
                back.Source = imag;
            }
            else if (b.GetCloudCover() > 30)
            {
                desc = "Nuageux";
                BitmapImage imag = new BitmapImage(new Uri("clouds1.jpg", UriKind.Relative));
                back.Source = imag;
                light_rain.Source = new BitmapImage(new Uri("28.png", UriKind.Relative));



            }
            else if (b.GetTempMin() <= 1)
            {
                desc = "Neige";
                light_rain.Source = new BitmapImage(new Uri("14.png", UriKind.Relative));


                BitmapImage imag = new BitmapImage(new Uri("snow.jpg", UriKind.Relative));
                back.Source = imag;


            }
            else
            {
                desc = "Peu nuageux";

                BitmapImage imag = new BitmapImage(new Uri("few_clouds1.jpg", UriKind.Relative));
                back.Source = imag;
                light_rain.Source = new BitmapImage(new Uri("34.png", UriKind.Relative));


            }
            lbl_desc.Content = desc;
            output_out = output;
            output_out.nbreJours = nbre_jours;

        end:
            if(how == false)
            {
                //test dde contrôle.
                this.Visibility = Visibility.Hidden;
                if(rec.otherWindow==false)
                { 
                accueil ac = new accueil(wilaya3, output_out);
                ac.Show();}
            }
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
        private void eve(object sender, RoutedEventArgs e)
        {

        }
        private void Button_PointerExited1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            rich_humid.Opacity = 0;
        }
        private void Button_PoiterEntered1(object sender, System.Windows.Input.MouseEventArgs e)
        {
            rich_humid.Opacity = 1;
        }
        private void Button_PoiterEntered2(object sender, System.Windows.Input.MouseEventArgs e)
        {
            rich_vent.Opacity = 1;
            //Btn_vitesse_vent.Opacity = 1;
        }
        private void Button_PointerExited2(object sender, System.Windows.Input.MouseEventArgs e)
        {
            rich_vent.Opacity = 0;
        }
        private void Button_PoiterEntered3(object sender, System.Windows.Input.MouseEventArgs e)
        {
            rich_visibility.Opacity = 1;
        }
        private void Button_PointerExited3(object sender, System.Windows.Input.MouseEventArgs e)
        {
            rich_visibility.Opacity = 0;
        }
        private void Button_PoiterEntered4(object sender, System.Windows.Input.MouseEventArgs e)
        {
            rich_pressure.Opacity = 1;
        }
        private void Button_PointerExited4(object sender, System.Windows.Input.MouseEventArgs e)
        {
            rich_pressure.Opacity = 0;
        }
        private void Button_PoiterEntered5(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
        private void Button_PointerExited6(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }
        private void Button_PoiterEntered6(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }
        private void Button_PointerExited7(object sender, System.Windows.Input.MouseEventArgs e)
        {


        }
        private void Button_PoiterEntered7(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

      
        private void power_click(object sender, RoutedEventArgs e)           //Evenement pour fermeture de la fenêtre.
        {
            Close();
        }
        private void minimize_click(object sender, RoutedEventArgs e)       //Evenement pour minimiser la taille de la fenêtre.
        {
            WindowState = WindowState.Minimized;
        }

        private void Button_PointerExited5(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //if ((exp1.IsExpanded) && (exp2.IsExpanded) && (exp3.IsExpanded)) exp1.IsExpanded = false;
        }

        //Prendre une screeShot de la fenêtre
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
                using (System.IO.FileStream stream = new FileStream(file, FileMode.Create))

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
                meteo_jour nvv = new meteo_jour(wilaya3, output_out);
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
            prevision prv = new prevision(wilaya3, output_out);
            prv.Show();
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
            evolution ev = new evolution(wilaya3, output_out);
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
            parametre cr = new parametre(wilaya3, output_out);
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
            connexion mise = new connexion(wilaya3, output_out);
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
            accueil ac = new accueil(wilaya3, output_out);
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
            Contact cont = new Contact(wilaya3, output_out);
            cont.Show();
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
            apropos ap = new apropos(wilaya3, output_out);
            ap.Show();
            this.Close();
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

        private void Btn_vitesse_vent_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Txt55_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        //Choix de la wilaya à partir de la liste déroulante.
        private void ComboBox1_DropDownClosed(object sender, EventArgs e)
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

            ComboBox1.BorderBrush = Brushes.White;
             str = ComboBox1.Text;

            switch (str)
            {
                case "Adrar":
                    {
                        wilaya3 = "adrar";
                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Ain Defla":
                    {
                        wilaya3 = "ain-defla";
                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Ain Témouchent":
                    {
                        wilaya3 = "ain-temouchent";
                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Alger":
                    {
                        wilaya3 = "alger";
                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Annaba":
                    {
                        wilaya3 = "annaba";
                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Batna":
                    {
                        wilaya3 = "batna";
                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Béchar":
                    {
                        wilaya3 = "bechar";
                        MessageBox.Show("output_out.name avant le if: " + output_out.name);
                        if (this.output_out.name != wilaya3) output_out.used = false;

                        MessageBox.Show("wilaya: " + wilaya3 + "   output_out.used: " + output_out.used);
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Béjaia":
                    {
                        wilaya3 = "bejaia";
                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Biskra":
                    {
                        wilaya3 = "biskra";
                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Blida":
                    {
                        wilaya3 = "blida";
                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Bordj Bou Arréridj":
                    {
                        wilaya3 = "bordj-bou-arreridj";

                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Bouira":
                    {
                        wilaya3 = "bouira";

                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Chlef":
                    {
                        wilaya3 = "chlef";

                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Boumerdès":
                    {
                        wilaya3 = "boumerdes";

                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Constantine":
                    {
                        wilaya3 = "constantine";

                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Djelfa":
                    {
                        wilaya3 = "djelfa";

                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "El Bayadh":
                    {
                        wilaya3 = "el-bayadh";

                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "El Taref":
                    {
                        wilaya3 = "el-tarf";

                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "El-Oued":
                    {
                        wilaya3 = "el-oued";

                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Guelma":
                    {
                        wilaya3 = "guelma";

                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Illizi":
                    {
                        wilaya3 = "illizi";

                        if (this.output_out.name != wilaya3) output_out.used = false;

                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Jijel":
                    {
                        wilaya3 = "jijel";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Laghouat":
                    {
                        wilaya3 = "laghouat";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Khenchela":
                    {
                        wilaya3 = "khenchela";
                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "M’Sila":
                    {
                        wilaya3 = "msila";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Mascara":
                    {
                        wilaya3 = "mascara";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Médéa":
                    {
                        wilaya3 = "medea";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Mila":
                    {
                        wilaya3 = "mila";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Mostaganem":
                    {
                        wilaya3 = "mostaganem";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Naâma":
                    {
                        wilaya3 = "naama";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Oran":
                    {
                        wilaya3 = "oran";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Ouargla":
                    {
                        wilaya3 = "ouargla";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Oum El Bouaghi":
                    {
                        wilaya3 = "oum-el-bouaghi";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Relizane":
                    {
                        wilaya3 = "relizane";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Saida":
                    {
                        wilaya3 = "saida";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Sidi BelAbbès":
                    {
                        wilaya3 = "sidi-bel-abbes";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Skikda":
                    {
                        wilaya3 = "skikda";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Souk Ahras":
                    {
                        wilaya3 = "souk-ahras";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tamanrasset":
                    {
                        wilaya3 = "tamanrasset";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tiaret":
                    {
                        wilaya3 = "tiaret";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tindouf":
                    {
                        wilaya3 = "tindouf";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tébessa":
                    {
                        wilaya3 = "tebessa";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tipaza":
                    {
                        wilaya3 = "tipaza";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tissemsilt":
                    {
                        wilaya3 = "tissemssilt";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tizi Ouzou":
                    {
                        wilaya3 = "tizi-ouzou";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;
                case "Tlemcen":
                    {
                        wilaya3 = "tlemcen";

                        if (this.output_out.name != wilaya3) output_out.used = false;
                        prevision prv = new prevision(wilaya3, output_out);
                        prv.Show();
                        this.Close();
                    }
                    break;

            }
            // wilaya sera l'entrée de prévision en cas ou pas connex et de méteo du jour si y a de la conex
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.A:
                    {
                        accueil ev = new accueil(wilaya3, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.M:
                    {
                        meteo_jour ev = new meteo_jour(wilaya3, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.P:
                    {
                        prevision ev = new prevision(wilaya3, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.E:
                    {
                        evolution ev = new evolution(wilaya3, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.T:
                    {
                        Contact ev = new Contact(wilaya3, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.R:
                    {
                        parametre ev = new parametre(wilaya3, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.O:
                    {
                        apropos ev = new apropos(wilaya3, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.D:
                    {
                        credit ev = new credit(wilaya3, output_out);
                        ev.Show();
                        this.Close();
                    }
                    break;
                case Key.Z:
                    {
                        connexion ev = new connexion(wilaya3, output_out);
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

        //Choix de la journée à partir de la liste déroulante.
        private void ComboBox2_DropDownClosed_1(object sender, EventArgs e)
        {
            /** Le son du click **/
            StreamReader sr = new StreamReader(@"son.txt");
            string str = sr.ReadLine();
            sr.Close();
            if (str == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
          /** Recuperation de la journée dans le combox **/
            String jour_week=null ; 
            switch (DateTime.Now.Date.AddDays(nbre_jours).DayOfWeek.ToString())
            {

                case "Saturday":
                    jour_week = "Samedi";
                    break;
                case "Sunday":
                    jour_week = "Dimanche";
                    break;
                case "Monday":
                    jour_week = "Lundi";
                    break;
                case "Tuesday":
                    jour_week = "Mardi";
                    break;
                case "Thursday":
                    jour_week = "Jeudi";
                    break;
                case "Friday":
                    jour_week = "Vendredi";
                    break;
                case "Wednesday":
                    jour_week = "Mercredi";
                    break;
            }
            String lol = "  " + jour_week + " " + DateTime.Now.Date.AddDays(nbre_jours).Day.ToString() + "/" + DateTime.Now.Date.AddDays(nbre_jours).Month.ToString() + "/" + DateTime.Now.Date.AddDays(nbre_jours).Year.ToString();

            String chaine = ComboBox2.Text;
            String dd1 = null; String dd2 = null; String dd3 = null; String dd4 = null; String dd5 = null; String dd6 = null;
            if (chaine != lol)
            {
                switch (DateTime.Now.AddDays(1).DayOfWeek.ToString())
                {

                    case "Saturday":
                        dd1 = "Samedi";
                        break;
                    case "Sunday":
                        dd1 = "Dimanche";
                        break;
                    case "Monday":
                        dd1 = "Lundi";
                        break;
                    case "Tuesday":
                        dd1 = "Mardi";
                        break;
                    case "Thursday":
                        dd1 = "Jeudi";
                        break;
                    case "Friday":
                        dd1 = "Vendredi";
                        break;
                    case "Wednesday":
                        dd1 = "Mercredi";
                        break;
                }
                switch (DateTime.Now.AddDays(2).DayOfWeek.ToString())
                {

                    case "Saturday":
                        dd2 = "Samedi";
                        break;
                    case "Sunday":
                        dd2 = "Dimanche";
                        break;
                    case "Monday":
                        dd2 = "Lundi";
                        break;
                    case "Tuesday":
                        dd2 = "Mardi";
                        break;
                    case "Thursday":
                        dd2 = "Jeudi";
                        break;
                    case "Friday":
                        dd2 = "Vendredi";
                        break;
                    case "Wednesday":
                        dd2 = "Mercredi";
                        break;
                }
                switch (DateTime.Now.AddDays(3).DayOfWeek.ToString())
                {

                    case "Saturday":
                        dd3 = "Samedi";
                        break;
                    case "Sunday":
                        dd3 = "Dimanche";
                        break;
                    case "Monday":
                        dd3 = "Lundi";
                        break;
                    case "Tuesday":
                        dd3 = "Mardi";
                        break;
                    case "Thursday":
                        dd3 = "Jeudi";
                        break;
                    case "Friday":
                        dd3 = "Vendredi";
                        break;
                    case "Wednesday":
                        dd3 = "Mercredi";
                        break;
                }
                switch (DateTime.Now.AddDays(4).DayOfWeek.ToString())
                {

                    case "Saturday":
                        dd4 = "Samedi";
                        break;
                    case "Sunday":
                        dd4 = "Dimanche";
                        break;
                    case "Monday":
                        dd4 = "Lundi";
                        break;
                    case "Tuesday":
                        dd4 = "Mardi";
                        break;
                    case "Thursday":
                        dd4 = "Jeudi";
                        break;
                    case "Friday":
                        dd4 = "Vendredi";
                        break;
                    case "Wednesday":
                        dd4 = "Mercredi";
                        break;
                }
                switch (DateTime.Now.AddDays(5).DayOfWeek.ToString())
                {

                    case "Saturday":
                        dd5 = "Samedi";
                        break;
                    case "Sunday":
                        dd5 = "Dimanche";
                        break;
                    case "Monday":
                        dd5 = "Lundi";
                        break;
                    case "Tuesday":
                        dd5 = "Mardi";
                        break;
                    case "Thursday":
                        dd5 = "Jeudi";
                        break;
                    case "Friday":
                        dd5 = "Vendredi";
                        break;
                    case "Wednesday":
                        dd5 = "Mercredi";
                        break;
                }
                switch (DateTime.Now.AddDays(6).DayOfWeek.ToString())
                {

                    case "Saturday":
                        dd6 = "Samedi";
                        break;
                    case "Sunday":
                        dd6 = "Dimanche";
                        break;
                    case "Monday":
                        dd6 = "Lundi";
                        break;
                    case "Tuesday":
                        dd6 = "Mardi";
                        break;
                    case "Thursday":
                        dd6 = "Jeudi";
                        break;
                    case "Friday":
                        dd6 = "Vendredi";
                        break;
                    case "Wednesday":
                        dd6 = "Mercredi";
                        break;
                }


                if (chaine == "  " + dd1 + " " + DateTime.Now.Date.AddDays(1).Day.ToString() + "/" + DateTime.Now.Date.AddDays(1).Month.ToString() + "/" + DateTime.Now.Date.AddDays(1).Year.ToString()
    )
                {
                    output_out.nbreJours = 1;
                    prevision prv = new prevision(wilaya3, output_out);
                    prv.Show();
                    this.Close();
                }
                else if (chaine == "  " + dd2 + " " + DateTime.Now.Date.AddDays(2).Day.ToString() + "/" + DateTime.Now.Date.AddDays(2).Month.ToString() + "/" + DateTime.Now.Date.AddDays(2).Year.ToString(
    ))
                {
                    output_out.nbreJours = 2;
                    prevision prv = new prevision(wilaya3, output_out);
                    prv.Show();
                    this.Close();
                }
                else if (chaine == "  " + dd3 + " " + DateTime.Now.Date.AddDays(3).Day.ToString() + "/" + DateTime.Now.Date.AddDays(3).Month.ToString() + "/" + DateTime.Now.Date.AddDays(3).Year.ToString())
                {
                    output_out.nbreJours = 3;
                    prevision prv = new prevision(wilaya3, output_out);
                    prv.Show();
                    this.Close();
                }
                else if (chaine == "  " + dd4 + " " + DateTime.Now.Date.AddDays(4).Day.ToString() + "/" + DateTime.Now.Date.AddDays(4).Month.ToString() + "/" + DateTime.Now.Date.AddDays(4).Year.ToString())
                {
                    output_out.nbreJours = 4;
                    prevision prv = new prevision(wilaya3, output_out);
                    prv.Show();
                    this.Close();
                }
                else if (chaine == "  " + dd5 + " " + DateTime.Now.Date.AddDays(5).Day.ToString() + "/" + DateTime.Now.Date.AddDays(5).Month.ToString() + "/" + DateTime.Now.Date.AddDays(5).Year.ToString())
                {
                    output_out.nbreJours = 5;
                    prevision prv = new prevision(wilaya3, output_out);
                    prv.Show();
                    this.Close();
                }
                else if (chaine == "  " + dd6 + " " + DateTime.Now.Date.AddDays(6).Day.ToString() + "/" + DateTime.Now.Date.AddDays(6).Month.ToString() + "/" + DateTime.Now.Date.AddDays(6).Year.ToString())
                {
                    output_out.nbreJours = 6;
                    prevision prv = new prevision(wilaya3, output_out);
                    prv.Show();
                    this.Close();
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
            String ch2 = null;
            //WpfMessageBox.Show(Convert.ToString( nbre_jours));
            switch (DateTime.Now.Date.AddDays(nbre_jours).DayOfWeek.ToString())
            {

                case "Saturday":
                    ch2 = "Samedi";
                    break;
                case "Sunday":
                    ch2 = "Dimanche";
                    break;
                case "Monday":
                    ch2 = "Lundi";
                    break;
                case "Tuesday":
                    ch2 = "Mardi";
                    break;
                case "Thursday":
                    ch2 = "Jeudi";
                    break;
                case "Friday":
                    ch2 = "Vendredi";
                    break;
                case "Wednesday":
                    ch2 = "Mercredi";
                    break;
            }
            //  MessageBox.Show(ch2 + " " + DateTime.Now.Date.AddDays(nbre_jours).Day.ToString() + "/" + DateTime.Now.Date.AddDays(nbre_jours).Month.ToString() + "/" + DateTime.Now.Date.AddDays(nbre_jours).Year.ToString());
            ComboBox2.Text = "  "+ch2 + " " + DateTime.Now.Date.AddDays(nbre_jours).Day.ToString() + "/" + DateTime.Now.Date.AddDays(nbre_jours).Month.ToString() + "/" + DateTime.Now.Date.AddDays(nbre_jours).Year.ToString();

        }

       

        
    }
    }

