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
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Windows.Controls.Primitives;
using System.Globalization;
using System.IO;
using Microsoft.Win32;

namespace Helios
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    static class DataGridViewHelper
    {
        public static DataGridRow GetRow(this DataGrid grid, int index)
        {
            DataGridRow row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            if (row == null)
            {
                // May be virtualized, bring into view and try again.
                grid.UpdateLayout();
                grid.ScrollIntoView(grid.Items[index]);
                row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
            }
            return row;
        }
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }
        public static DataGridCell GetCell(this DataGrid grid, DataGridRow row, int column)
        {
            if (row != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(row);

                if (presenter == null)
                {
                    grid.ScrollIntoView(row, grid.Columns[column]);
                    presenter = GetVisualChild<DataGridCellsPresenter>(row);
                }

                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(column);
                return cell;
            }
            else
                return null;

        }
        public static string extraire(string str)
        {
            string res = "";
            foreach (var c in str)
            {
                if (char.IsNumber(c) || (c == '/'))
                {
                    res = res + c;
                }
            }
            return (res);
        }
        public static bool IsDate(string input)
        {
            DateTime temp;
            return DateTime.TryParse(input, CultureInfo.CurrentCulture, DateTimeStyles.NoCurrentDateDefault, out temp) &&
                   temp.Hour == 0 &&
                   temp.Minute == 0 &&
                   temp.Second == 0 &&
                   temp.Millisecond == 0 &&
                   temp > DateTime.MinValue;
        }
    }

    public partial class mise_a_jour : Window
    {
       // string wilaya = "alger";    //wilaya par defaut: Alger
        string wilaya = File.ReadAllText(@"wilaya.txt");    //wilaya par defaut: Alger

        InfoJour.weatherinfo.Root output_out = new InfoJour.weatherinfo.Root();

        public mise_a_jour(string city, InfoJour.weatherinfo.Root output)
        {
            wilaya = wilaya.Trim(new Char[] { ' ', '\r', '\n', '\t' });
            InitializeComponent();
            wilaya = city;
            output_out = output;
        }


        /* Pour ajouter une données veuillez la saisir à la fin de la base de données 
         * par ailleurs vous ne pouvez pas supprimer des ou modifier les données deja insérées */

        private void Enregistrer_Click(object sender, RoutedEventArgs e)
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
           str = ComboBox1.Text;
            string wilaya = "";
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
                case "Boumerdès":
                    wilaya = "boumerdes";
                    break;
                case "Constantine":
                    wilaya = "constantine";
                    break;
                case "Chlef":
                    wilaya = "chlef";
                    break;
                case "Djelfa":
                    wilaya = "djelfa";
                    break;
                case "El Bayadh":
                    wilaya = "el-bayadh";
                    break;
                case "El Taref":
                    wilaya = "el-taref";
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
                case "Khenchela":
                    wilaya = "khenchela";
                    break;
                case "Laghouat":
                    wilaya = "laghouat";
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
            SqlConnection connection;
            SqlCommand command;
            string connectionString = @"Data Source = INFO\SQLEXPRESS ; Initial Catalog =weather1 ; User ID =sa ; password =kerem1987";

            //string connectionString = @"Data Source = PC\SQLEXPRESS ; Initial Catalog =weather ; User ID =sa ; password =esi2018";

            //string connectionString = @"Data Source = ABCOMPUTER-PC\SQLEXPRESS ; Initial Catalog = weather ; User ID =sa ; password =ayaz oyku";
            connection = new SqlConnection(connectionString);
            DataGridRow row; string ch1;

            try
            {
                row = DataGridViewHelper.GetRow(dataGrid1, dataGrid1.SelectedIndex);

                ch1 = DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 0).ToString());
            }
            catch (Exception)
            {
                //MessageBox.Show("Vous n'avez saisi aucune donnée ! ", "Erreur");

                WpfMessageBox.Show("Erreur", "Vous n'avez saisi aucune donnée !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);

                return;
            }
            if (DataGridViewHelper.IsDate(ch1))
            {

                //DateTime date = Convert.ToDateTime(ch1);
                command = new SqlCommand("Select * from[dbo].[export-" + wilaya + "] where DATE =@p1 ", connection);
                command.Parameters.AddWithValue("@p1", ch1);
                DataSet data = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(data, "export-" + wilaya);
                }
                catch (Exception)
                {
                    //MessageBox.Show("Echec de la connexion !", "Erreur");
                    WpfMessageBox.Show("Erreur", "Echec de la connexion à la base de données  !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);

                }
                command.Dispose();
                connection.Close();
                if (data.Tables[0].Rows.Count == 0)
                {
                    try
                    {
                        // MessageBox.Show("Dataset vide !");
                        command = new SqlCommand("INSERT INTO [export-" + wilaya + "] VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15)", connection);
                        command.Parameters.AddWithValue("@p1", ch1); 
                        command.Parameters.AddWithValue("@p2", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 1).ToString()));
                        command.Parameters.AddWithValue("@p3", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 2).ToString()));
                        command.Parameters.AddWithValue("@p4", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 3).ToString()));
                        command.Parameters.AddWithValue("@p5", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 4).ToString()));
                        command.Parameters.AddWithValue("@p6", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 5).ToString()));
                        command.Parameters.AddWithValue("@p7", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 6).ToString()));
                        command.Parameters.AddWithValue("@p8", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 7).ToString()));
                        command.Parameters.AddWithValue("@p9", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 8).ToString()));
                        command.Parameters.AddWithValue("@p10", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 9).ToString()));
                        command.Parameters.AddWithValue("@p11", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 10).ToString()));
                        command.Parameters.AddWithValue("@p12", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 11).ToString()));
                        command.Parameters.AddWithValue("@p13", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 12).ToString()));
                        command.Parameters.AddWithValue("@p14", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 13).ToString()));
                        command.Parameters.AddWithValue("@p15", DataGridViewHelper.extraire(DataGridViewHelper.GetCell(dataGrid1, row, 14).ToString()));
                        connection.Open();
                        command.Dispose();
                        connection.Close();
                        WpfMessageBox.Show("", "Données ajoutées avec succées", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);


                    }
                    catch (Exception)
                    {
                        //MessageBox.Show("Echech de la connexion ");         
                        WpfMessageBox.Show("Erreur", "Echec de la connexion à la base de données !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);
                    }
                }
                else
                {
                    //MessageBox.Show("Veuillez inserer une autre date ! ", "Date existante");
                    WpfMessageBox.Show("Date existante", "Veuillez inserer une autre date !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);

                }
            }
            else
            {
                //MessageBox.Show("Veuillez inserer une autre date ! ", "Date erronée");
                WpfMessageBox.Show("Date erronée", "Veuillez inserer une autre date !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);

            }

        }

        private void Actualiser_Click(object sender, RoutedEventArgs e)
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
             str = ComboBox1.Text;
            ComboBox1.BorderBrush = Brushes.White;
            string wilaya = "";
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
                case "Boumerdès":
                    wilaya = "boumerdes";
                    break;
                case "Chlef":
                    wilaya = "chlef";
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
                    wilaya = "el-taref";
                    break;
                case "El-Oued":
                    wilaya = "el-oued";
                    break;
                case "Laghouat":
                    wilaya = "laghouat";
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
            SqlConnection connection;
            SqlCommand command;
            string connectionString = @"Data Source = INFO\SQLEXPRESS ; Initial Catalog =weather1 ; User ID =sa ; password =kerem1987";

            //string connectionString = @"Data Source = PC\SQLEXPRESS ; Initial Catalog =weather ; User ID =sa ; password =esi2018";
            //string connectionString = @"Data Source = ABCOMPUTER-PC\SQLEXPRESS ; Initial Catalog = weather ; User ID =sa ; password = ayaz oyku ";
            DataSet data = new DataSet(); // contiendra les données 
            connection = new SqlConnection(connectionString);
            command = new SqlCommand("Select DATE,MAX_TEMPERATURE_C,MIN_TEMPERATURE_C,WINDSPEED_MAX_KMH,TEMPERATURE_MORNING_C,TEMPERATURE_NOON_C,TEMPERATURE_EVENING_C,PRECIP_TOTAL_DAY_MM,HUMIDITY_MAX_PERCENT,VISIBILITY_AVG_KM,HEATINDEX_MAX_C,DEWPOINT_MAX_C,WINDTEMP_MAX_C,PRESSURE_MAX_MB,CLOUDCOVER_AVG_PERCENT from[dbo].[export-" + wilaya + "] ", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataTableCollection tables;
            try
            {
                connection.Open();
                adapter.Fill(data, "export-" + wilaya);
                tables = data.Tables;
                DataView view = new DataView(tables[0]);
                dataGrid1.ItemsSource = view;
            }
            catch (Exception)
            {
                WpfMessageBox.Show("Erreur", "Echec de la connexion à la base de données  !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);
                //MessageBox.Show("Echec de la connexion !", "Erreur");
            }
            command.Dispose();
            connection.Close();
            dataGrid1.Columns[0].Header = "Date";
            dataGrid1.Columns[1].Header = "TempMax";
            dataGrid1.Columns[2].Header = "TempMin";
            dataGrid1.Columns[3].Header = "VitesseVent";
            dataGrid1.Columns[4].Header = "TempMatin";
            dataGrid1.Columns[5].Header = "TempAprésMidi";
            dataGrid1.Columns[6].Header = "TempSoir";
            dataGrid1.Columns[7].Header = "Précipitation";
            dataGrid1.Columns[8].Header = "Humdité";
            dataGrid1.Columns[9].Header = "Visibilité";
            dataGrid1.Columns[10].Header = "IndiceDeChaleur";
            dataGrid1.Columns[11].Header = "PointDeRosée";
            dataGrid1.Columns[12].Header = "TempVent";
            dataGrid1.Columns[13].Header = "Pression";
            dataGrid1.Columns[14].Header = "CouverturNuageuse";

            ComboBox1.BorderBrush = Brushes.White;

        }


        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
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




        private void ComboBox1_DropDownClosed(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"son.txt");
            string str1 = sr.ReadLine();
            sr.Close();
            if (str1 == "Activé")
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri(@"..\..\click.mp3", UriKind.RelativeOrAbsolute));
                player.Play();
            }
            
            ComboBox1.BorderBrush = Brushes.White;

            string str = ComboBox1.Text;
            string wilaya = "";
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
                    wilaya = "el-taref";
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
                case "Ghardaia":
                    wilaya = "ghardaia";
                    break;


            }


            //SqlConnection connection;
            SqlCommand command;
            //string connectionString = @"Data Source = INFO\SQLEXPRESS ; Initial Catalog =weather1 ; User ID =sa ; password =kerem1987";
            //string connectionString = @"Data Source = PC\SQLEXPRESS ; Initial Catalog =weather ; User ID =sa ; password =esi2018";
            ///string connectionString = @"Data Source = ABCOMPUTER-PC\SQLEXPRESS ; Initial Catalog = weather ; User ID =sa ; password = ayaz oyku";

            SqlConnection connection = new SqlConnection(@"Data Source = .\SQLEXPRESS;Initial Catalog=weather;Integrated Security=True");


            DataSet data = new DataSet(); // contiendra les données 
            //connection = new SqlConnection(connectionString);
            command = new SqlCommand("Select DATE,MAX_TEMPERATURE_C,MIN_TEMPERATURE_C,WINDSPEED_MAX_KMH,TEMPERATURE_MORNING_C,TEMPERATURE_NOON_C,TEMPERATURE_EVENING_C,PRECIP_TOTAL_DAY_MM,HUMIDITY_MAX_PERCENT,VISIBILITY_AVG_KM,HEATINDEX_MAX_C,DEWPOINT_MAX_C,WINDTEMP_MAX_C,PRESSURE_MAX_MB,CLOUDCOVER_AVG_PERCENT from[dbo].[export-" + wilaya + "] ", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataTableCollection tables;
            try
            {
                connection.Open();
                adapter.Fill(data, wilaya);
                tables = data.Tables;
                DataView view = new DataView(tables[0]);
                dataGrid1.ItemsSource = view;
            }
            catch (Exception)
            {
                //MessageBox.Show("Echec de la connexion !", "Erreur");
                WpfMessageBox.Show("Erreur", "Echec de la connexion à la base de données !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);
            }
            command.Dispose();
            connection.Close();
            //MessageBox.Show(str);
            
            dataGrid1.Columns[0].Header = "Date";
            dataGrid1.Columns[1].Header = "TempMax";
            dataGrid1.Columns[2].Header = "TempMin";
            dataGrid1.Columns[3].Header = "VitesseVent";
            dataGrid1.Columns[4].Header = "TempMatin";
            dataGrid1.Columns[5].Header = "TempAprésMidi";
            dataGrid1.Columns[6].Header = "TempSoir";
            dataGrid1.Columns[7].Header = "Précipitation";
            dataGrid1.Columns[8].Header = "Humdité";
            dataGrid1.Columns[9].Header = "Visibilité";
            dataGrid1.Columns[10].Header = "IndiceDeChaleur";
            dataGrid1.Columns[11].Header = "PointDeRosée";
            dataGrid1.Columns[12].Header = "TempVent";
            dataGrid1.Columns[13].Header = "Pression";
            dataGrid1.Columns[14].Header = "CouverturNuageuse";
            ComboBox1.BorderBrush = Brushes.White;

        }



        private void ComboBox1_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox1.BorderBrush = Brushes.White;
        }

        private void ComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox1.BorderBrush = Brushes.White;
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
                using (FileStream stream = new FileStream(file, FileMode.Create))

                {

                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(render));
                    encoder.Save(stream);
                   // WpfMessageBox.Show("Réussi", "Capture prise avec succées", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Information);
                }

            }
            catch (Exception )
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
            mise_a_jour mise = new mise_a_jour(wilaya, output_out);
            mise.Show();
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            SqlConnection connection;
            SqlCommand command;
            string connectionString = @"Data Source = INFO\SQLEXPRESS ; Initial Catalog =weather1 ; User ID =sa ; password =kerem1987";
            //string connectionString = @"Data Source = PC\SQLEXPRESS ; Initial Catalog =weather ; User ID =sa ; password =esi2018";
            //string connectionString = @"Data Source = ABCOMPUTER-PC\SQLEXPRESS ; Initial Catalog = weather ; User ID =sa ; password = ayaz oyku";
            DataSet data = new DataSet(); // contiendra les données 
            connection = new SqlConnection(connectionString);
            command = new SqlCommand("Select DATE,MAX_TEMPERATURE_C,MIN_TEMPERATURE_C,WINDSPEED_MAX_KMH,TEMPERATURE_MORNING_C,TEMPERATURE_NOON_C,TEMPERATURE_EVENING_C,PRECIP_TOTAL_DAY_MM,HUMIDITY_MAX_PERCENT,VISIBILITY_AVG_KM,HEATINDEX_MAX_C,DEWPOINT_MAX_C,WINDTEMP_MAX_C,PRESSURE_MAX_MB,CLOUDCOVER_AVG_PERCENT from[dbo].[export-" + wilaya + "] ", connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataTableCollection tables;
            try
            {
                connection.Open();
                adapter.Fill(data, "export-adrar");
                tables = data.Tables;
                DataView view = new DataView(tables[0]);
                dataGrid1.ItemsSource = view;
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Echec de la connexion !", "Erreur");
                WpfMessageBox.Show("Erreur", "Echec de la connexion à la base de données  !", MessageBoxButton.OK, WpfMessageBox.MessageBoxImage.Error);
            }
            command.Dispose();
            connection.Close();
            //MessageBox.Show(str);

            dataGrid1.Columns[0].Header = "Date";
            dataGrid1.Columns[1].Header = "TempMax";
            dataGrid1.Columns[2].Header = "TempMin";
            dataGrid1.Columns[3].Header = "VitesseVent";
            dataGrid1.Columns[4].Header = "TempMatin";
            dataGrid1.Columns[5].Header = "TempAprésMidi";
            dataGrid1.Columns[6].Header = "TempSoir";
            dataGrid1.Columns[7].Header = "Précipitation";
            dataGrid1.Columns[8].Header = "Humdité";
            dataGrid1.Columns[9].Header = "Visibilité";
            dataGrid1.Columns[10].Header = "IndiceDeChaleur";
            dataGrid1.Columns[11].Header = "PointDeRosée";
            dataGrid1.Columns[12].Header = "TempVent";
            dataGrid1.Columns[13].Header = "Pression";
            dataGrid1.Columns[14].Header = "CouverturNuageuse";
            ComboBox1.BorderBrush = Brushes.White;
            */
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
                       mise_a_jour ev = new mise_a_jour(wilaya, output_out);
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
