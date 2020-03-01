using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Drawing;
using LiveCharts;
using LiveCharts.Wpf;
using System.Timers;
using System.IO;
using System.Windows.Media;
using System.Windows;

using System.Runtime.Serialization;
using Helios;
using Brushes = System.Windows.Media.Brushes;
using Microsoft.Win32;

namespace Helios
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class evolution : Window
    {
        WeatherEntities db = new WeatherEntities();// création des entités pour déssiner les graphes
        string wilaya = File.ReadAllText(@"wilaya.txt");   //lire le contenu du fichier wilaya.txt qui contient la wilaya par défaut au début alger  

        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();

        public evolution(string city, InfoJour.weatherinfo.Root output) // la fenetre evolution 
        {
            wilaya = wilaya.Trim(new Char[] { ' ', '\r', '\n', '\t' });
            InitializeComponent();
            wilaya = wilaya.Replace(" ", "");

            wilaya = city;
            output_out = output;

        }


        LineSeries col = new LineSeries() //déclarer le types des graphes (courbes)
        {
            Title = "Temperature",
            DataLabels = true,
            Values = new ChartValues<int>(),
            LabelPoint = Point => Point.Y.ToString(),
            Foreground = Brushes.Black
                    ,
            AreaLimit = 10,
            LineSmoothness = 0,
            Stroke = Brushes.MidnightBlue,
            PointForeground = Brushes.White,
            PointGeometrySize = 20,
            Opacity = 55
        };
        LineSeries col2 = new LineSeries()
        {
            Title = "Humidité",
            DataLabels = true,
            Values = new ChartValues<int>(),
            LabelPoint = Point => Point.Y.ToString(),
            Foreground = Brushes.Black
                    ,
            AreaLimit = 10,
            LineSmoothness = 0,
            Stroke = Brushes.MidnightBlue,
            PointForeground = Brushes.White,
            PointGeometrySize = 20,
            Opacity = 55
        };
        LineSeries col3 = new LineSeries()
        {
            Title = "Précipitation",
            DataLabels = false,
            Values = new ChartValues<double>(),
            LabelPoint = Point => Point.Y.ToString(),
            Foreground = Brushes.Black
                    ,
            AreaLimit = 10,
            LineSmoothness = 0,
            Stroke = Brushes.MidnightBlue,
            PointForeground = Brushes.White,
            PointGeometrySize = 20,
            Opacity = 55
        };
        string str = "Alger";
        private void ComboBox1_DropDownClosed(object sender, EventArgs e) // pour le choix de la wilaya que l'utilisateur veut voir son evolution
        {
            str = combox1.Text;
            WeatherEntities db = new WeatherEntities();
            using (db)
            {
                col.Values.Clear(); // il faut supprimer les graphes qui sont déja déssiner , il y a 3 graphes pour la temperature l'humidité et la précipitation
                col2.Values.Clear();
                col3.Values.Clear();
                if (str == "Adrar")
                {
                    var data1 = db.GetTempAdrar();//lire les données en utilisant des procédures dans la bdd
                    var data2 = db.GetHumAdrar();
                    var data3 = db.GetPreAdrar();
                    foreach (var x in data1)
                    {
                        if (x.Year != 2019)
                        {
                            col.Values.Add(x.Total.Value / 365);//ajouter les valeurs au graphes 
                        }
                        else
                        {
                            col.Values.Add(x.Total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.Year != 2019)
                        {
                            col2.Values.Add(x.Total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.Total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.Year != 2019)
                        {
                            col3.Values.Add(x.Total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.Total.Value / 31);
                        }

                    }
                }
                if (str == "Annaba")
                {
                    var data1 = db.GetTempAnnaba();
                    var data2 = db.GetHumAnnaba();
                    var data3 = db.GetPreAnnaba();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Alger")
                {
                    var data1 = db.GetTempAlger();
                    var data2 = db.GetHumAlger();
                    var data3 = db.GetPreAlger();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Ain Defla")
                {
                    var data1 = db.GetTempaindefla();
                    var data2 = db.GetHumaindefla();
                    var data3 = db.GetPreaindefla();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Batna")
                {
                    var data1 = db.GetTempBatna();
                    var data2 = db.GetHumBatna();
                    var data3 = db.GetPreBatna();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Blida")
                {
                    var data1 = db.GetTempBlida();
                    var data2 = db.GetHumblida();
                    var data3 = db.GetPreblida();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Ain Témouchent")
                {
                    var data1 = db.GetTempAintemouchent();
                    var data2 = db.GetHumAintemouchent();
                    var data3 = db.GetPreAintemouchent();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Béchar")
                {
                    var data1 = db.GetTempBechar();
                    var data2 = db.GetHumBechar();
                    var data3 = db.GetPrebechar();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Béjaia")
                {
                    var data1 = db.GetTempBejaia();
                    var data2 = db.GetHumbejaia();
                    var data3 = db.GetPrebejaia();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Biskra")
                {
                    var data1 = db.GetTempBiskra();
                    var data2 = db.GetHumbiskra();
                    var data3 = db.GetPrebiskra();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Bordj Bou Arréridj")
                {
                    var data1 = db.GetTempBordj();
                    var data2 = db.GetHumbordj();
                    var data3 = db.GetPrebordj();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Bouira")
                {
                    var data1 = db.GetTempBouira();
                    var data2 = db.GetHumbouira();
                    var data3 = db.GetPrebouira();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "El Bayadh")
                {
                    var data1 = db.GetTempByadh();
                    var data2 = db.GetHumbayadh();
                    var data3 = db.GetPrebayadh();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Chlef")
                {
                    var data1 = db.GetTempChlef();
                    var data2 = db.GetHumchlef();
                    var data3 = db.GetPrechlef();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Constantine")
                {
                    var data1 = db.GetTempConsto();
                    var data2 = db.GetHumconsto();
                    var data3 = db.GetPreconsto();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Djelfa")
                {
                    var data1 = db.GetTempDjelfa();
                    var data2 = db.GetHumdjelfa();
                    var data3 = db.GetPredjelfa();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Ghardaia")
                {
                    var data1 = db.GetTempGhardaia();
                    var data2 = db.GetHumghardaia();
                    var data3 = db.GetPreghardaia();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Guelma")
                {
                    var data1 = db.GetTempGuelma();
                    var data2 = db.GetHumguelma();
                    var data3 = db.GetPreguelma();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Illizi")
                {
                    var data1 = db.GetTempIllizi();
                    var data2 = db.GetHumillizi();
                    var data3 = db.GetPreillizi();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Jijel")
                {
                    var data1 = db.GetTempJijel();
                    var data2 = db.GetHumjijel();
                    var data3 = db.GetPrejijel();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Khenchela")
                {
                    var data1 = db.GetTempKhench();
                    var data2 = db.GetHumkhench();
                    var data3 = db.GetPrekhench();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Laghouat")
                {
                    var data1 = db.GetTempLaghouat();
                    var data2 = db.GetHumlaghouat();
                    var data3 = db.GetPrelaghouat();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Mascara")
                {
                    var data1 = db.GetTempMascara();
                    var data2 = db.GetHummascara();
                    var data3 = db.GetPremascara();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Médéa")
                {
                    var data1 = db.GetTempMedea();
                    var data2 = db.GetHummedea();
                    var data3 = db.GetPremedea();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Mila")
                {
                    var data1 = db.GetTempMila();
                    var data2 = db.GetHummila();
                    var data3 = db.GetPremila();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Mostaganem")
                {
                    var data1 = db.GetTempMosta();
                    var data2 = db.GetHummosta();
                    var data3 = db.GetPremosta();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "M’Sila")
                {
                    var data1 = db.GetTempMsila();
                    var data2 = db.GetHummsila();
                    var data3 = db.GetPremsila();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Naâma")
                {
                    var data1 = db.GetTempNaama();
                    var data2 = db.GetHumnaama();
                    var data3 = db.GetPrenaama();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Oran")
                {
                    var data1 = db.GetTempOran();
                    var data2 = db.GetHumoran();
                    var data3 = db.GetPreoran();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "El-Oued")
                {
                    var data1 = db.GetTempOued();
                    var data2 = db.GetHumoued();
                    var data3 = db.GetPreoued();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Ouargla")
                {
                    var data2 = db.GetTempOuerg();
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Oum El Bouaghi")
                {
                    var data1 = db.GetTempOum();
                    var data2 = db.GetHumoum();
                    var data3 = db.GetPreoum();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Relizane")
                {
                    var data1 = db.GetTempReliz();
                    var data2 = db.GetHumrelizane();
                    var data3 = db.GetPrerelizane();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Saida")
                {
                    var data1 = db.GetTempSaida();
                    var data2 = db.GetHumsaida();
                    var data3 = db.GetPresaida();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Sétif")
                {
                    var data1 = db.GetTempSetif();
                    var data2 = db.GetHumsetif();
                    var data3 = db.GetPresetif();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Sidi BelAbbès")
                {
                    var data1 = db.GetTempSidiabbes();
                    var data2 = db.GetHumsidiabbes();
                    var data3 = db.GetPresidiabbas();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Skikda")
                {
                    var data1 = db.GetTempSkikda();
                    var data2 = db.GetHumskikda();
                    var data3 = db.GetPreskikda();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Souk Ahras")
                {
                    var data1 = db.GetTempSouk();
                    var data2 = db.GetHumsouk();
                    var data3 = db.GetPresouk();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Tamanrasset")
                {
                    var data1 = db.GetTemptamen();
                    var data2 = db.GetHumtamm();
                    var data3 = db.GetPretam();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "El Taref")
                {
                    var data1 = db.GetTempTarf();
                    var data2 = db.GetHumtaref();
                    var data3 = db.GetPretaref();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Tebessa")
                {
                    var data1 = db.GetTempTebessa();
                    var data2 = db.GetHumtebessa();
                    var data3 = db.GetPretebessa();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Tiaret")
                {
                    var data1 = db.GetTempTiaret();
                    var data2 = db.GetHumtiaret();
                    var data3 = db.GetPretiaret();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Tindouf")
                {
                    var data1 = db.GetTempTindouf();
                    var data2 = db.GetHumtindouf();
                    var data3 = db.GetPretindouf();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Tipaza")
                {
                    var data1 = db.GetTempTipaza();
                    var data2 = db.GetHumtipaza();
                    var data3 = db.GetPretipaza();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Tissemsilt")
                {
                    var data1 = db.GetTempTisse();
                    var data2 = db.GetHumtissem();
                    var data3 = db.GetPretiss();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Tizi-Ouzou")
                {
                    var data1 = db.GetTempTizi();
                    var data2 = db.GetHumtizi();
                    var data3 = db.GetPretizi();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Tlemcen")
                {
                    var data1 = db.GetTempTlemcen();
                    var data2 = db.GetHumtlemcen();
                    var data3 = db.GetPretlemcen();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
                if (str == "Boumerdès")
                {
                    var data1 = db.GetTempBoumerdes();
                    var data2 = db.GetHumboumerdes();
                    var data3 = db.GetPreboumerdes();
                    foreach (var x in data1)
                    {
                        if (x.year != 2019)
                        {
                            col.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data2)
                    {
                        if (x.year != 2019)
                        {
                            col2.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col2.Values.Add(x.total.Value / 31);
                        }

                    }
                    foreach (var x in data3)
                    {
                        if (x.year != 2019)
                        {
                            col3.Values.Add(x.total.Value / 365);
                        }
                        else
                        {
                            col3.Values.Add(x.total.Value / 31);
                        }

                    }
                }
            }
        }
        private void MainWindow_Load(object sender, EventArgs e)
        {
            //lorsque cette fenetre s'affiche le graphe de l'evolution de la temperature d'alger par defaut

            using (db)
            {
                var data2 = db.GetHumAlger();
                foreach (var x in data2)
                {
                    if (x.year != 2019)
                    {
                        col2.Values.Add(x.total.Value / 365);
                    }
                    else
                    {
                        col2.Values.Add(x.total.Value / 31);
                    }

                }
                var data3 = db.GetPreAdrar();
                foreach (var x in data3)
                {
                    if (x.Year != 2019)
                    {
                        col3.Values.Add(x.Total.Value / 365);
                    }
                    else
                    {
                        col3.Values.Add(x.Total.Value / 31);
                    }

                }
                var data1 = db.GetTempAlger();
                //initialiser les axes 
                Axis ax = new Axis() { Foreground = Brushes.White, Separator = new LiveCharts.Wpf.Separator() { Step = 1, IsEnabled = false } };
                ax.Labels = new List<String>();
                foreach (var x in data1)
                {
                    if (x.year != 2019)
                    { col.Values.Add(x.total.Value / 365); }
                    else
                    { col.Values.Add(x.total.Value / 31); }
                    ax.Labels.Add(x.year.ToString());

                }
                cartesianchart1.Series.Add(col);
                cartesianchart1.AxisX.Add(ax);

                cartesianchart1.AxisY.Add(new Axis()
                {
                    LabelFormatter = value => value.ToString(),
                    Separator = new LiveCharts.Wpf.Separator(),
                    Foreground = Brushes.White
                });
                cartesianchart1.ToolTip = new System.Windows.Controls.ToolTip()
                {
                    Content = "Evolution Climatique pendant dix ans 2009-2019",
                    Background = Brushes.DarkSlateGray,
                    Foreground = Brushes.SkyBlue
                };


            }
        }


        private void temp_Click(object sender, RoutedEventArgs e)//clique sur l'icone de l'évolution de la temperature 
        {
            while (cartesianchart1.Series.Count > 0) { cartesianchart1.Series.RemoveAt(0); }

            cartesianchart1.Series.Add(col);
        }

        private void Hum_Click(object sender, RoutedEventArgs e)//clique sur l'icone de l'évolution de l'humidité
        {

            while (cartesianchart1.Series.Count > 0) { cartesianchart1.Series.RemoveAt(0); }


            cartesianchart1.Series.Add(col2);

        }
        private void Precip_Click(object sender, RoutedEventArgs e)//clique sur l'icone de l'évolution de la prrécipitation
        {

            while (cartesianchart1.Series.Count > 0) { cartesianchart1.Series.RemoveAt(0); }


            cartesianchart1.Series.Add(col3);

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
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


        private void btnPinPanel_Click(object sender, RoutedEventArgs e)
        {
            /*SolidColorBrush Sopa = new SolidColorBrush();
            Sopa.Color = Colors.Red;
            Sopa.Opacity = 50;
            rainn.OpacityMask = Sopa;
            rainn.Opacity = 0;*/
        }



        private void power_click(object sender, RoutedEventArgs e)//fermer la fenetre
        {
            Close();
        }

        private void minimize_click(object sender, RoutedEventArgs e)//reduire la fenetre
        {
            WindowState = WindowState.Minimized;
        }

        private void Combox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CreateScreenShot(UIElement visual, string file)//pour prendre des captures d'écran

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
                    //   WpfMessageBox.Show("Réussi", "Capture prise avec succés", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Information);
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

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
            prevision prv = new prevision(wilaya, output_out);
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

