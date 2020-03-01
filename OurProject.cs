using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using System.Data.SqlClient;

namespace Helios
{
    public class InfoJour
    {
        /* Role : 
         * C’est une classe qui récupère les données du jour ( la météo du jour utilisant la connexion internet 
        */
        public class weatherinfo
        {
            public class coord
            {
                private double lon;
                public double getLon() { return this.lon; }
                public void setLon(double y) { this.lon = y; }
                private double lan;
                public double getLan() { return this.lan; }
                public void setLan(double y) { this.lan = y; }
            }//end of class coord

            public class weather
            {
                public int id;
                public int getId() { return this.id; }
                public void setId(int y) { this.id = y; }
                public string main;
                public string getMain() { return this.main; }
                public void setMain(string y) { this.main = y; }
                public string description;
                public string getDescription() { return this.description; }
                public void setDescription(string y) { this.description = y; }
                public string icon;
                public string getIcon() { return this.icon; }
                public void setIcon(string y) { this.icon = y; }
            }//end of weather class

            public class main
            {
                public double temp;
                public double getTemp() { return this.temp; }
                public void setTemp(double y) { this.temp = y; }
                public double pressure;
                public double getPressure() { return this.pressure; }
                public void setPressure(double y) { this.pressure = y; }
                public double humidity;
                public double getHumidity() { return this.humidity; }
                public void setHumidity(double y) { this.humidity = y; }
                public double temp_min;
                public double getTempMin() { return this.temp_min; }
                public void setTempMin(double y) { this.temp_min = y; }
                public double temp_max;
                public double getTempMax() { return this.temp_max; }
                public void setTempMax(double y) { this.temp_max = y; }
            }//end of main class

            public class wind
            {
                public double speed;
                public double getSpeed() { return this.speed; }
                public void setSpeed(double y) { this.speed = y; }
                public double deg;
                public double getDeg() { return this.deg; }
                public void setDeg(double y) { this.deg = y; }
            }//end of wind class

            public class clouds
            {
                public double all;
                public double getAll() { return this.all; }
                public void setAll(double y) { this.all = y; }
            }

            public class sys
            {
                public string country;
                public string getCountry() { return this.country; }
                public void setCountry(string y) { this.country = y; }
                public double sunrise;
                public DateTime getSunrise() { return UnixTimeStampToDateTime(this.sunrise); }
                public double sunset;
                public DateTime getSunset() { return UnixTimeStampToDateTime(this.sunset); }
            }

            public class Root
            {
                public string name;
                public string getName() { return this.name; }
                public void setName(string name) { this.name = name; }

                public double visibility;
                public double getVisibility() { return this.visibility; }
                public void setVisibility(double visib) { this.visibility = visib; }

                public double dt;
                public DateTime getDt() { return UnixTimeStampToDateTime(this.dt); }
                public DateTime date;
                public void setDate(DateTime date) { this.date = date; }

                public double precip = -1;
                public bool used = false;
                public int nbreJours = 1;

                public sys sys = new sys();
                public wind wind = new wind();
                public main main = new main();
                public weather[] weather;
                public coord coordinate = new coord();
                public clouds clouds = new clouds();
            }

            public static void getweather(string city, string APPID, ref weatherinfo.Root outPut) //methode pour la recuperation d'observation du jour en cas de connexion internet
            {
                using (WebClient web = new WebClient())
                {
                    string url = string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&cnt=6", city, APPID);

                    var json = web.DownloadString(url);

                    var result = JsonConvert.DeserializeObject<weatherinfo.Root>(json);

                    outPut = result;
                }
            }

            public static DateTime UnixTimeStampToDateTime(double unixTimeStamp)//modifie le type de la date du jour récupérée de unixTimeStamp à DateTimes
            {
                // Unix timestamp is seconds past epoch
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
                return dtDateTime;
            }
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int conn, int val);
        static int Out;
        public static int test_cnx()    // Vérifie si il y a de la connexion internet ou pas ( elle sera utilisée dans plusieurs autres méthodes )
        {
            if ((InternetGetConnectedState(out Out, 0)) == true)
            {
                return 1; // Connecté
            }
            else
            {
                return -1; //Non connecté
            }
        }

    }


    public class Prev
    {
        /* Role : 
         * Contient toutes les variables météorologiques : string Date ; string temp_max ; string temp_min …etc + setters/getters 
         */

        private DateTime Date = new DateTime();
        private double tempMax;
        private double tempMin;
        private double windSpeed;
        private double tempMorning;
        private double tempEvening;
        private double tempNoon;
        private double precipitation;
        private double pressure;
        private double humidity;
        private double visibility;
        private double cloudCover;
        private double dewPoint;
        private double heatIndex;
        private double tempWind;
        private int id;

        public DateTime GetDate() { return this.Date; }
        public void SetDate(DateTime date) { this.Date = date; }

        public double GetTempMax() { return this.tempMax; }
        public void SetTempMax(double tempmax) { this.tempMax = tempmax; }

        public double GetTempMin() { return this.tempMin; }
        public void SetTempMin(double tempmin) { this.tempMin = tempmin; }

        public double GetWindSpeed() { return this.windSpeed; }
        public void SetWindSpeed(double windspeed) { this.windSpeed = windspeed; }

        public double GetTempMorning() { return this.tempMorning; }
        public void SetTempMorning(double tempmorning) { this.tempMorning = tempmorning; }

        public double GetTempNoon() { return this.tempNoon; }
        public void SetTempNoon(double tempnoon) { this.tempNoon = tempnoon; }

        public double GetTempEvening() { return this.tempEvening; }
        public void SetTempEvening(double tempevening) { this.tempEvening = tempevening; }

        public double GetPrecipation() { return this.precipitation; }
        public void SetPrecipitation(double precip) { this.precipitation = precip; }

        public double GetHumidity() { return this.humidity; }
        public void SetHumidity(double humidity) { this.humidity = humidity; }

        public double GetVisibility() { return this.visibility; }
        public void SetVisibility(double visibility) { this.visibility = visibility; }

        public double GetPressure() { return this.pressure; }
        public void SetPressure(double pressure) { this.pressure = pressure; }

        public double GetCloudCover() { return this.cloudCover; }
        public void SetCloudCover(double cloudcover) { this.cloudCover = cloudcover; }

        public double GetHeatIndex() { return this.heatIndex; }
        public void SetHeatIndex(double heatindex) { this.heatIndex = heatindex; }

        public double GetDewPoint() { return this.dewPoint; }
        public void SetDewPoint(double dewpoint) { this.dewPoint = dewpoint; }

        public double GetTempWind() { return this.tempWind; }
        public void SetTempWind(double tempwind) { this.tempWind = tempwind; }

        public int GetId() { return this.id; }
        public void SetId(int id) { this.id = id; }

    }

    public class Element
    {
        /* Role : 
         * la classe contient une valeur d'une variable météorologique accompagnée de sa probabilité  + setters / getters 
         */

        private double valeur;
        private float proba;

        public void SetValeur(double valeur)
        {
            this.valeur = valeur;
        }
        public double GetValeur()
        {
            return this.valeur;
        }
        public void SetProba(float proba)
        {
            this.proba = proba;
        }
        public float GetProba()
        {
            return this.proba;
        }
        private DateTime datePlusRecente = new DateTime();
        public void SetDate(DateTime datePlusRecente)
        {
            this.datePlusRecente = datePlusRecente;
        }
        public DateTime GetDate()
        {
            return this.datePlusRecente;
        }
    }

    public class ElemTabproba
    {
        /* Role : 
         *  l'entrée de la méthodde prévision_finale sera un objet de ElemenTabProba
         */

        private String variable;
        private Element[] valeurs = new Element[400];
        private int lentghValeurs;


        public String GetVariable()
        {
            return (this.variable);
        }
        public void SetVariable(String variable)
        {
            this.variable = variable;
        }
        public Element[] GetValeurs()
        {
            return (this.valeurs);
        }
        public void SetValeurs(Element[] valeurs)
        {
            this.valeurs = valeurs;
        }

        public int GetLentghValeurs()
        {
            return (this.lentghValeurs);
        }
        public void SetLentghValeurs(int lentghValeurs)
        {
            this.lentghValeurs = lentghValeurs;
        }
    }

    public class Valeur
    {

        private DateTime date = new DateTime();
        private double value;


        public DateTime GetDate()
        {
            return (this.date);
        }
        public void SetDate(DateTime date)
        {
            this.date = date;
        }
        public double GetValue()
        {
            return (this.value);
        }
        public void SetValue(double value)
        {
            this.value = value;
        }
    }

    public class Prevproba
    {
        public static int lentgh; // la taille de tous les tableaux valeurs
        private String variable;
        private Valeur[] valeurs = new Valeur[400];

        public String GetVariable()
        {
            return (this.variable);
        }
        public void SetVariable(String variable)
        {
            this.variable = variable;
        }
        public Valeur[] GetValeurs()
        {
            return (this.valeurs);
        }
        public void SetValeurs(Valeur[] valeurs)
        {
            this.valeurs = valeurs;
        }
    }

    public class Traitement
    {
        /* Les trois classes : Prev , Element , TabProba  permettront l’interaction entre les méthodes de la classe traitement. */

        //de mail du mel
        static public ElemTabproba[] Probabilite(Prev[] previsioninit)
        {
            //Transformation de Prev à Prevproba

            Prevproba[] prevision = new Prevproba[14];
            for (int m = 0; m < 14; m++)
            {
                prevision[m] = new Prevproba();
            }
            int k = 0;
            prevision[0].SetVariable("TempMin");
            prevision[1].SetVariable("TempMax");
            prevision[2].SetVariable("CloudCover");
            prevision[3].SetVariable("DewPoint");
            prevision[4].SetVariable("HeatIndex");
            prevision[5].SetVariable("Humidity");
            prevision[6].SetVariable("Precipitation");
            prevision[7].SetVariable("Pressure");
            prevision[8].SetVariable("TempEvening");
            prevision[9].SetVariable("TempMornong");
            prevision[10].SetVariable("TempNoon");
            prevision[11].SetVariable("TempWind");
            prevision[12].SetVariable("Visibility");
            prevision[13].SetVariable("WindSpeed");



            foreach (Prev elem2 in previsioninit)
            {
                if (elem2 != null)
                {
                    prevision[0].GetValeurs()[k] = new Valeur();
                    prevision[0].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[0].GetValeurs()[k].SetValue(elem2.GetTempMin());
                    prevision[1].GetValeurs()[k] = new Valeur();
                    prevision[1].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[1].GetValeurs()[k].SetValue(elem2.GetTempMax());
                    prevision[2].GetValeurs()[k] = new Valeur();
                    prevision[2].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[2].GetValeurs()[k].SetValue(elem2.GetCloudCover());
                    prevision[3].GetValeurs()[k] = new Valeur();
                    prevision[3].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[3].GetValeurs()[k].SetValue(elem2.GetDewPoint());
                    prevision[4].GetValeurs()[k] = new Valeur();
                    prevision[4].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[4].GetValeurs()[k].SetValue(elem2.GetHeatIndex());
                    prevision[5].GetValeurs()[k] = new Valeur();
                    prevision[5].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[5].GetValeurs()[k].SetValue(elem2.GetHumidity());
                    prevision[6].GetValeurs()[k] = new Valeur();
                    prevision[6].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[6].GetValeurs()[k].SetValue(elem2.GetPrecipation());
                    prevision[7].GetValeurs()[k] = new Valeur();
                    prevision[7].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[7].GetValeurs()[k].SetValue(elem2.GetPressure());
                    prevision[8].GetValeurs()[k] = new Valeur();
                    prevision[8].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[8].GetValeurs()[k].SetValue(elem2.GetTempEvening());
                    prevision[9].GetValeurs()[k] = new Valeur();
                    prevision[9].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[9].GetValeurs()[k].SetValue(elem2.GetTempMorning());
                    prevision[10].GetValeurs()[k] = new Valeur();
                    prevision[10].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[10].GetValeurs()[k].SetValue(elem2.GetTempNoon());
                    prevision[11].GetValeurs()[k] = new Valeur();
                    prevision[11].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[11].GetValeurs()[k].SetValue(elem2.GetTempWind());
                    prevision[12].GetValeurs()[k] = new Valeur();
                    prevision[12].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[12].GetValeurs()[k].SetValue(elem2.GetVisibility());
                    prevision[13].GetValeurs()[k] = new Valeur();
                    prevision[13].GetValeurs()[k].SetDate(elem2.GetDate());
                    prevision[13].GetValeurs()[k].SetValue(elem2.GetWindSpeed());
                    k++;
                }
            }
            Prevproba.lentgh = k;



            //Initialisation des éléments de tabProba
            ElemTabproba[] tabProba = new ElemTabproba[14];

            for (int l = 0; l < 14; l++)
            {
                tabProba[l] = new ElemTabproba();
            }
            tabProba[0].SetVariable("TempMin");
            tabProba[1].SetVariable("TempMax");
            tabProba[2].SetVariable("CloudCover");
            tabProba[3].SetVariable("DewPoint");
            tabProba[4].SetVariable("HeatIndex");
            tabProba[5].SetVariable("Humidity");
            tabProba[6].SetVariable("Precipitation");
            tabProba[7].SetVariable("Pressure");
            tabProba[8].SetVariable("TempEvening");
            tabProba[9].SetVariable("TempMorning");
            tabProba[10].SetVariable("TempNoon");
            tabProba[11].SetVariable("TempWind");
            tabProba[12].SetVariable("Visibility");
            tabProba[13].SetVariable("WindSpeed");




            //Création de tabProba
            int j;
            int i = 0;
            for (int l = 0; l < 14; l++)
            {

                IList<Valeur> Listprevision = prevision[i].GetValeurs().ToList();
                IEnumerable<Valeur> List = Listprevision.Where(x => x != null);
                Listprevision = List.ToList();


                IEnumerable<IGrouping<double, Valeur>> ListprevisionGroupee1 = from s in Listprevision
                                                                               group s by s.GetValue();
                IEnumerable<IGrouping<double, Valeur>> ListprevisionGroupee = ListprevisionGroupee1.OrderBy(o => o.Key).ToList();


                j = 0;
                foreach (IGrouping<double, Valeur> val in ListprevisionGroupee)
                {

                    tabProba[l].GetValeurs()[j] = new Element();

                    tabProba[l].GetValeurs()[j].SetValeur((double)val.Key);
                    DateTime date = new DateTime();
                    date = DateTime.MinValue;
                    foreach (Valeur valeur in val)
                    {

                        tabProba[l].GetValeurs()[j].SetProba(tabProba[l].GetValeurs()[j].GetProba() + 1);
                        if (valeur.GetDate() > date)
                        {
                            date = valeur.GetDate();
                        }
                    }

                    tabProba[l].GetValeurs()[j].SetProba(tabProba[l].GetValeurs()[j].GetProba() / Listprevision.Count);

                    tabProba[l].GetValeurs()[j].SetDate(date);
                    j++;

                }

                tabProba[l].SetLentghValeurs(j);
                i++;



            }
            for (int l = 0; l < 14; l++)
            {
                Element[] sorted = tabProba[l].GetValeurs().Where(x => x != null).OrderByDescending(c => c.GetProba()).ToArray();

                tabProba[l].SetValeurs(sorted);

            }


            return tabProba;

        }

        //de helios
        public static Prev Prévision_final(ElemTabproba[] table, int jour_a_predire)
        {//la méthode qui donne le resultat final qui sera affiché ( les valeurs de variables en prenant les probabilités les plus grandes )
            Prev a = new Prev();
            Element[] interm = new Element[14];

            for (int i = 0; i < interm.Length; i++)
            {
                interm[i] = new Element();

            }
            for (int i = 0; i < 14; i++) // Pour chaque variables ( precip , tempMin , tempMax..... )
            {
                interm[i] = table[i].GetValeurs()[0]; // reçoit la valeur ayant la plus grande probabilité qui sera par la suite affiché
                // Comme les valeurs de varaibles venant de probabilité sont ordonnée donc on prend la premiere valeur et on l'affiche comme elle a 
                // la plus grand probabilité mais si il en existe deux ayant la meme probabilité on prend la plus récente 
                if (table[i].GetValeurs()[0].GetProba() == table[i].GetValeurs()[1].GetProba())  // même probailité
                {
                    if (DateTime.Compare(table[i].GetValeurs()[0].GetDate(), table[i].GetValeurs()[1].GetDate()) > 0)  // ==> Date plus récente 
                    {
                        interm[i] = table[i].GetValeurs()[0];
                    }
                    else
                        interm[i] = table[i].GetValeurs()[1];
                }

            }
            // c'est la date de jour de la prévision c'est a dire dans nb jour d'aujourd'hui

            DateTime date = DateTime.Now.Date;
            date = date.AddDays(jour_a_predire);


            //remplir la sortie avec les données qu'on a obtenu ==> ces derniers représente les valeurs de variables à afficher
            a.SetDate(date);
            a.SetTempMin(interm[0].GetValeur());
            a.SetTempMax(interm[1].GetValeur());
            a.SetCloudCover(interm[2].GetValeur());
            a.SetDewPoint(interm[3].GetValeur());
            a.SetHeatIndex(interm[4].GetValeur());
            a.SetHumidity(interm[5].GetValeur());
            a.SetPrecipitation(interm[6].GetValeur());
            a.SetPressure(interm[7].GetValeur());
            a.SetTempEvening(interm[8].GetValeur());
            a.SetTempMorning(interm[9].GetValeur());
            a.SetTempNoon(interm[10].GetValeur());
            a.SetTempWind(interm[11].GetValeur());
            a.SetVisibility(interm[12].GetValeur());
            a.SetWindSpeed(interm[13].GetValeur());

            return a;  //Nous rend les valeurs des variables ayant la plus grande probabilité :)

        }

    }

    public class ManipBasedeDonnees
    {
        /* Role : La manipulation de la base de donnée */
        public static void connection(string cmd, bool conn, ref DataSet data, string wilaya)
        {
            /* Cette méthode permet la connexion avec la base de données tout en exécutant a commande cmd sur la table de wilaya " wilaya" et rend con un booleen qui à vrai en cas de 
             * connexion avec succés Faux sinn , et met le contenu de l'éxécution de la commande dans data
             */
            ///string connectionstring = null;
            ///SqlConnection connect;
            SqlCommand command;
            string sql = null;
            // Cette chaine permet de connecter visual studio avec notre bdd et cecei en entrant ces caracteristiques 
            //connectionstring = @"Data Source = INFO\SQLEXPRESS ; Initial Catalog =weather1 ; User ID =sa ; password =kerem1987";
            //connectionstring = @"Data Source = INFO\SQLEXPRESS ; Initial Catalog =weather1 ; User ID =sa ; password =kerem1987";
            //connectionstring = @"Data Source = ABCOMPUTER-PC\SQLEXPRESS ; Initial Catalog =weather ; User ID =sa ; password =ayaz oyku";

            sql = cmd;
            //connect = new SqlConnection(connectionstring);
            SqlConnection connect = new SqlConnection(@"Data Source = .\SQLEXPRESS;Initial Catalog=weather;Integrated Security=True");

            command = new SqlCommand(sql, connect);
            /* Utilisation du mode deconnecté : ce mode récupère les données en mode connectés, mais ne t'y donne accès que lorsque tout le résultat de la requête a été traité, 
            * une fois le résultat totalement récupéré et placé dans le DataSet, 
            * la requête ouverte et le résultat associé sont détruits au niveau de la base, mais la connexion à celle-ci n'est pas close.
            */
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            try
            {
                //int i = 1;
                connect.Open();
                adapter.Fill(data, wilaya);
                conn = true;
                // Fermer la connexion 
                connect.Close();
            }
            catch (Exception)
            {
                conn = false;
            }
        }

        public static double similarite_Bray_Curtis(double a, double b)
        {
            /* Role : 
             * La  distance  de  Bray-Curtis,  ou  indice  de  dissimilarité de  Bray-Curtis  utilisé  en  pour  évaluer  la dissimilarité entre deux échantillons donnés.
             * Cette méthode nous calcule la similarité entre deux doubles selon la distance de Bray-Curtis.  */
            if (a < 0)
            {
                b = b - a + 1;
                a = 1;
            }
            else
            {
                if (b < 0)
                {
                    a = a - b + 1;
                    b = 1;
                }
                else
                {
                    if ((a == 0) || (b == 0))
                    {
                        if (a == 0) a = 1;
                        if (b == 0) b = 1;
                    }
                }
            }
            if (a <= 8)
            {
                if ((a - 2 <= b) && (b <= a + 2))
                {
                    return 1;
                }
            }
            double c = Math.Min(a, b);
            return (2 * c) / (a + b);
        }

        public static void recherche(InfoJour.weatherinfo.Root output, int[] tab_id, String wilaya)
        {
            /* Role : 
             *  Rechercher une donnée "output " dans la table de la bdd de la wilaya "wilaya" et nous rend un table d'id qui défini les jours dont l'observation est similaire à output
             */
            bool b = false; int cpt = 1;
            DateTime today = DateTime.Now;
            string mnth = "'" + today.Month % 12 + "'";
            string mnth2 = "'" + (today.Month - 1) % 12 + "'";
            string mnth3 = "'" + (today.Month + 1) % 12 + "'";

            string table = "[dbo].[" + wilaya + "]";
            // Commande de la recherche */
            string cmmd = "SELECT * FROM " + table + " WHERE Month(DATE) = " + mnth + " or Month(DATE) = " + mnth2 + " or Month(DATE) = " + mnth3;
        retour:
            DataSet data = new DataSet();
            connection(cmmd, b, ref data, wilaya); int j = 0;
            double seuil = 0.92;
            double seuil2 = 0.92;
            if ((-8 <= output.main.getTempMin()) && (output.main.getTempMin() <= 8)) seuil = 0.66;
            else { seuil = 0.92; }
            if ((-8 <= output.main.getTempMax()) && (output.main.getTempMax() <= 8)) seuil2 = 0.66;
            else { seuil2 = 0.92; }

            foreach (DataRow row in data.Tables[wilaya].Rows)
            {
                string txt = Convert.ToString(row["CLOUDCOVER_AVG_PERCENT"]);
                string txt2 = Convert.ToString(row["HUMIDITY_MAX_PERCENT"]);
                txt = txt.Replace('.', ',');
                txt2 = txt2.Replace('.', ',');
                double a = Convert.ToDouble(txt);


                if ((similarite_Bray_Curtis(Convert.ToDouble(row["MAX_TEMPERATURE_C"]), output.main.getTempMax()) > seuil2) && (similarite_Bray_Curtis(Convert.ToDouble(row["MIN_TEMPERATURE_C"]), output.main.getTempMin()) > seuil) && (similarite_Bray_Curtis(Convert.ToDouble(row["HUMIDITY_MAX_PERCENT"]), output.main.humidity) > 0.75))
                {
                    tab_id[j] = Convert.ToInt32(row["id"]); j++;
                }
            }
            if (tab_id[0] == 0)
            {
                cmmd = cmmd + " or Month(DATE) = " + "'" + (today.Month + 2) % 12 + "'"; cpt += 1;
                if (cpt < 10)
                    goto retour;
            }
        }

        public static void previsions(int[] tab_id, Prev[] tab_prev, int nb_jours, string wilaya)
        {

            for (int i = 0; i < tab_id.Length; i++)
            {
                if (tab_id[i] != 0)
                {
                    int id = tab_id[i] + nb_jours;

                    bool b = false;
                    string table = "[dbo].[" + wilaya + "]";
                    string cmmd = "SELECT * FROM " + table + " WHERE id = " + id;

                    DataSet data = new DataSet();

                    connection(cmmd, b, ref data, wilaya);
                    foreach (DataRow row in data.Tables[wilaya].Rows)
                    {
                        tab_prev[i] = new Prev();

                        tab_prev[i].SetTempMax(Convert.ToDouble(row["MAX_TEMPERATURE_C"]));
                        tab_prev[i].SetTempMin(Convert.ToDouble(row["MIN_TEMPERATURE_C"]));
                        tab_prev[i].SetWindSpeed(Convert.ToDouble(row["WINDSPEED_MAX_KMH"]));
                        tab_prev[i].SetTempMorning(Convert.ToDouble(row["TEMPERATURE_MORNING_C"]));
                        tab_prev[i].SetTempNoon(Convert.ToDouble(row["TEMPERATURE_NOON_C"]));
                        tab_prev[i].SetTempEvening(Convert.ToDouble(row["TEMPERATURE_EVENING_C"]));

                        string text = Convert.ToString(row["PRECIP_TOTAL_DAY_MM"]);
                        text = text.Replace('.', ',');
                        tab_prev[i].SetPrecipitation(Convert.ToDouble(text));

                        text = Convert.ToString(row["VISIBILITY_AVG_KM"]);
                        text = text.Replace('.', ',');
                        tab_prev[i].SetVisibility(Convert.ToDouble(text));

                        tab_prev[i].SetHumidity(Convert.ToDouble(row["HUMIDITY_MAX_PERCENT"]));

                        tab_prev[i].SetPressure(Convert.ToDouble(row["PRESSURE_MAX_MB"]));

                        text = Convert.ToString(row["CLOUDCOVER_AVG_PERCENT"]);
                        text = text.Replace('.', ',');
                        tab_prev[i].SetCloudCover(Convert.ToDouble(text));

                        tab_prev[i].SetHeatIndex(Convert.ToDouble(row["HEATINDEX_MAX_C"]));
                        tab_prev[i].SetDewPoint(Convert.ToDouble(row["DEWPOINT_MAX_C"]));
                        tab_prev[i].SetTempWind(Convert.ToDouble(row["WINDTEMP_MAX_C"]));

                        tab_prev[i].SetId(Convert.ToInt32(row["id"]));
                        tab_prev[i].SetDate(Convert.ToDateTime(row["Date"]));
                    }
                }

            }
        }

        public static void add_BDD(InfoJour.weatherinfo.Root output, string wilaya)
        {
            // Role : Ajout d'une donnée dans la bdd
            ///string connectionstring = null;
            ///SqlConnection connect;
            //connectionstring = @"Data Source = INFO\SQLEXPRESS ; Initial Catalog =weather1 ; User ID =sa ; password =kerem1987";
            //connectionstring = @"Data Source=ABCOMPUTER-PC\SQLEXPRESS;Initial Catalog=weather;User ID=sa;Password=ayaz oyku";

            SqlConnection connect = new SqlConnection(@"Data Source = .\SQLEXPRESS;Initial Catalog=weather;Integrated Security=True");

            ///connect = new SqlConnection(connectionstring);
            // Commanda d'insertion dans un bdd 
            SqlCommand command = new SqlCommand("INSERT INTO [" + wilaya + "]VALUES(" + "'" + InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt).Day + "/" + InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt).Month + "/" + InfoJour.weatherinfo.UnixTimeStampToDateTime(output.dt).Year + "'" + "," + output.main.temp_max + "," + output.main.temp_min + "," + output.wind.speed + ",' ',' ',' ',' '," + output.main.humidity + "," + output.visibility + "," + output.main.pressure + "," + output.clouds.all + ",' ',' '," + output.wind.deg + ")", connect);
            // Instancier un objet datasert ==> Utilisation du mode deconnecté ==> comme la connexion 
            DataSet data = new DataSet();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            try
            {

                connect.Open(); // Ouverture de la connexion 
                adapter.Fill(data, wilaya); // recuperation du resultat de la commande 
                connect.Close(); // fermeture de la connexion
            }
            catch (Exception)
            {

            }
        }
    }

    public class InterfaceGraphique
    {
    }


}
