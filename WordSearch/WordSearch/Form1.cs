using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WordSearch
{
    public partial class WordSearch : Form
    {
        public TextBox[, ] matrice_joc;
        public int dificultate_maxima;
        public int dificultate_minima;
        public int nr_max_cuvinte;
        public int nr_min_cuvinte;
        public int nr_max_caractere;
        public int nr_min_caractere;

        public string[][][] cuvinte;
 
        public string[][] limba_romana;
        public string[][] limba_engleza;

        public string[] stiinta_ro;
        public string[] stiinta_en;

        public string[] medicina_ro;
        public string[] medicina_en;

        public string[] culori_ro;
        public string[] culori_en;

        // Constante selectare limba
        public static int LIMBA_ROMANA = 0;
        public static int LIMBA_ENGLEZA = 1;

        // Constante selectare categorie
        public static int CATEGORIE_STIINTA = 0;
        public static int CATEGORIE_MEDICINA = 1;
        public static int CATEGORIE_CULORI = 2;

        public WordSearch()
        { 
            this.dificultate_maxima = 20;
            this.dificultate_minima = 10;

            this.nr_max_cuvinte = 10;
            this.nr_min_cuvinte = 1;

            this.nr_max_caractere = 20;
            this.nr_min_caractere = 3;

            this.initializare_cuvinte();
            this.matrice_joc = new TextBox[dificultate_maxima, dificultate_maxima]; // maxim 20x20
            InitializeComponent();
            this.initializare_interfata();
        }

        private void initializare_cuvinte()
        {
            stiinta_ro = new string[] { "telescop", "computer", "multimetru", "senzor", "traductor", "pendul", "resort", "gravitatie" };
            stiinta_en = new string[] { "telescope", "computer", "multimeter", "sensor", "traductor", "pendulum", "spring", "gravity" };

            medicina_ro = new string[] { "analiza", "docotr", "medicament", "fiola", "seringa", "stetoscop", "microscop", "ambulanta" };
            medicina_en = new string[] { "analyze", "doctor", "drug", "vial", "syringe", "stethoscope", "microscope", "ambulance" };

            culori_ro = new string[] { "albastru", "portocaliu", "galben", "mov", "verde", "roz", "maro", "gri", "negru", "alb" };
            culori_en = new string[] { "blue", "orange", "yellow", "purple", "green", "pink", "brown", "grey", "black", "white" };

            limba_romana = new string[][] { stiinta_ro, medicina_ro, culori_ro };
            limba_engleza = new string[][] { stiinta_en, medicina_en, culori_en };
            cuvinte = new string[][][] { limba_romana, limba_engleza };
        }

        private void initializare_interfata()
        {
            // creare casute pentru litere
            int dificultate_maxima = this.dificultate_maxima; // numar liniii si coloane
            int dimensiune_casuta = 20; // dimensiunea fiecarei casute
            int x_initial = 300; // pozitia primei casute
            int y_initial = 50;  //
            int x;
            int y;

            for(int i=0; i< dificultate_maxima; i++) // se creeaza matricea jocului
            {
                for(int j=0; j< dificultate_maxima; j++)
                {
                    x = x_initial + (j * dimensiune_casuta); // se calculeaza coordonata x a casutei
                    y = y_initial + (i * dimensiune_casuta); // se calculeaza coordonata y a casutei
                    this.matrice_joc[i, j] = new TextBox(); // se creeaza o noua casuta
                    this.Controls.Add(this.matrice_joc[i, j]); // se adauga casuta creata la forma existenta
                    this.matrice_joc[i, j].Size = new Size(dimensiune_casuta, dimensiune_casuta); // se dimensioneaza
                    this.matrice_joc[i, j].Location = new Point(x, y); /// se pozitioneaza
                    this.matrice_joc[i, j].Text = "0"; // se scrie un text initial (optional)
                    this.matrice_joc[i, j].TextAlign = HorizontalAlignment.Center; // se aliniaza textul pe centru
                    this.matrice_joc[i, j].Enabled = false;
                    this.matrice_joc[i, j].Hide();
                }
            }

            // creare meniu selectare limba
            comboBox1.Items.Add("Romana");
            comboBox1.Items.Add("Engleza");

            //creare meniu selectare categorie
            comboBox2.Items.Add("Stiinta");
            comboBox2.Items.Add("Medicina");
            comboBox2.Items.Add("Culori");
        }

        private void pregateste_joc()
        {
            int dificultate = this.preia_dificultatea();
            if(dificultate == -1) // numar invalid de linii
            {
                return;
            }

            int nr_max_caractere = this.preia_nr_max_caractere();
            if (nr_max_caractere == -1) // numar maxim de caracatere invalid
            {
                return;
            }

            int nr_min_caractere = this.preia_nr_min_caractere();
            if (nr_min_caractere == -1) // numar minim de caracatere invalid
            {
                return;
            }

            int nr_cuvinte = this.preia_nr_cuvinte();
            if (nr_cuvinte == -1) // numar de cuvinte invalid
            {
                return;
            }

            int limba = this.preia_limba();
            if(limba == -1) // limba selectata nu este valida
            {
                return;
            }

            int categorie = this.preia_categoria();
            if (categorie == -1) // categoria selectata nu este valida
            {
                return;
            }


            this.pregateste_casutele(dificultate);
            if(this.pregateste_cuvintele(nr_cuvinte, limba, categorie, nr_min_caractere, nr_max_caractere) == -1)
            {
                return; // nu exista suficiente cuvinte in limba si categoria selectate
            }
        }

        private int pregateste_cuvintele(int numar_cuvinte, int limba, int categoria, int nr_min_caractere, int nr_max_caractere)
        {
            if(numar_cuvinte > this.cuvinte[limba][categoria].Length)
            {
                Console.WriteLine("Nu exista suficiente cuvinte in limba si categoria selectata!");
                return -1;
            }

            int cuvinte_potrivite = 0; // cuvinte care au numarul de cactere cuprins intre valorile selectate
            for(int i=0; i<this.cuvinte[limba][categoria].Length; i++)
            {
                string cuvant = this.cuvinte[limba][limba][i];
                if(cuvant.Length > nr_min_caractere && cuvant.Length < nr_max_caractere)
                {
                    cuvinte_potrivite++;
                }
            }
            if(numar_cuvinte > cuvinte_potrivite)
            {
                Console.WriteLine("Nu exista suficiente cuvinte potrivite in limba si categoria selectata!");
                return -1;
            }

            string[] cuvinte;
            List<int> cuvinte_alese = new List<int>();
            int nr_aleatoriu;
            Random generator_nr_aleatoriu = new Random();

            bool cuvant_adaugat_deja;
            bool cuvant_nepotrivit;

            for(int i=0; i<numar_cuvinte; i++)
            {
                nr_aleatoriu = generator_nr_aleatoriu.Next(0, this.cuvinte[limba][categoria].Length);
                cuvant_adaugat_deja = false;
                cuvant_nepotrivit = false;
                for(int j=0; j<cuvinte_alese.Count; j++)
                {
                    if (nr_aleatoriu == cuvinte_alese[j]) // cuvantul a mai fost selectat odata
                    {
                        cuvant_adaugat_deja = true;
                        break;
                    }
                    if (this.cuvinte[limba][categoria][nr_aleatoriu].Length < nr_min_caractere || this.cuvinte[limba][categoria][nr_aleatoriu].Length > nr_max_caractere)
                    {
                        cuvant_nepotrivit = true;
                    }
                }
                if(cuvant_adaugat_deja || cuvant_nepotrivit)
                {
                    i--; // selecteaza alt cuvant
                    continue;
                }
                cuvinte_alese.Add(nr_aleatoriu);
                Console.WriteLine("Numar aleatoriu {0}/{1}", nr_aleatoriu.ToString(), this.cuvinte[limba][categoria].Length.ToString());
            }
            return 0;
        }

        private int preia_nr_cuvinte()
        {
            int nr_cuvinte;

            if (Int32.TryParse(textBox2.Text, out nr_cuvinte) && nr_cuvinte >= this.nr_min_cuvinte && nr_cuvinte <= this.nr_max_cuvinte)
            {
                Console.WriteLine("Se genereaza jocul cu {0} cuvinte.", nr_cuvinte.ToString());
            }
            else
            {
                string mesaj = "Eroare - numar de cuvinte invalid!\nSe accepta doar valori numerice cuprinse intre " + this.nr_min_cuvinte.ToString() + " si " + this.nr_max_cuvinte.ToString() + "!";
                Console.WriteLine(mesaj);
                nr_cuvinte = -1;
            }
            
            return nr_cuvinte;
        }

        private int preia_categoria()
        {
            int categoria;
            string cfg_categoria;

            try
            {
                cfg_categoria = comboBox2.SelectedItem.ToString().ToUpper();
            }
            catch
            {
                categoria = -1;
                Console.WriteLine("Categoria selectata este invalida!");
                return categoria;
            }

            switch (cfg_categoria)
            {
                case "STIINTA":
                    {
                        categoria = 0;
                        break;
                    }
                case "MEDICINA":
                    {
                        categoria = 1;
                        break;
                    }
                case "CULORI":
                    {
                        categoria = 2;
                        break;
                    }
                default:
                    {
                        categoria = 0;
                        break;
                    }
            }
            return categoria;
        }

        private int preia_limba()
        {
            int limba;
            string cfg_limba;
            try
            {
                cfg_limba = comboBox1.SelectedItem.ToString().ToUpper();
            }
            catch
            { 
                limba = -1;
                Console.WriteLine("Limba selectata este invalida!");
                return limba;
            }

            switch (cfg_limba)
            {
                case "ROMANA":
                    {
                        limba = 0;
                        break;
                    }
                case "ENGLEZA":
                    {
                        limba = 1;
                        break;
                    }
                default:
                    {
                        limba = 0;
                        break;
                    }
            }
            return limba;
            }

        private void reseteaza_casutele()
        {
            for (int i = 0; i < dificultate_maxima; i++)
            {
                for (int j = 0; j < dificultate_maxima; j++)
                {
                    this.matrice_joc[i, j].Text = String.Empty;
                    this.matrice_joc[i, j].Hide();
                }
            }
        }

        private void pregateste_casutele(int dificultate)
        {
            this.reseteaza_casutele();
            for(int i=0; i<dificultate; i++)
            {
                for(int j=0; j<dificultate; j++)
                {
                    this.matrice_joc[i, j].Show();
                }
            }
        }

        private int preia_dificultatea()
        {
            int dificultate;
            if (Int32.TryParse(textBox1.Text, out dificultate) && dificultate >= this.dificultate_minima && dificultate <= this.dificultate_maxima)
            {
                Console.WriteLine("Se genereaza matrice patratica de {0}x{0}.", dificultate.ToString());
            }
            else
            {
                string mesaj = "Eroare - numar de linii invalid!\nSe accepta doar valori numerice cuprinse intre " + this.dificultate_minima.ToString() + " si " + this.dificultate_maxima.ToString() + "!";
                Console.WriteLine(mesaj);
                dificultate = -1;
            }
            return dificultate;
        }

        private int preia_nr_max_caractere()
        {
            int nr_max_caractere;
            if (Int32.TryParse(textBox4.Text, out nr_max_caractere) && nr_max_caractere >= this.nr_min_caractere && nr_max_caractere <= this.nr_max_caractere)
            {
                Console.WriteLine("Nr max de caractere {0}.", nr_max_caractere.ToString());
            }
            else
            {
                string mesaj = "Eroare - numar maxim de caractere invalid!\nSe accepta doar valori numerice cuprinse intre " + this.nr_min_caractere.ToString() + " si " + this.nr_max_caractere.ToString() + "!";
                Console.WriteLine(mesaj);
                nr_max_caractere = -1;
            }
            return nr_max_caractere;
        }

        private int preia_nr_min_caractere()
        {
            int nr_min_caractere;
            if (Int32.TryParse(textBox3.Text, out nr_min_caractere) && nr_min_caractere >= this.nr_min_caractere && nr_min_caractere <= this.nr_max_caractere)
            {
                Console.WriteLine("Nr min de caractere {0}.", nr_min_caractere.ToString());
            }
            else
            {
                string mesaj = "Eroare - numar minim de caractere invalid!\nSe accepta doar valori numerice cuprinse intre " + this.nr_min_caractere.ToString() + " si " + this.nr_max_caractere.ToString() + "!";
                Console.WriteLine(mesaj);
                nr_min_caractere = -1;
            }
            return nr_min_caractere;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pregateste_joc();
        }
    }
}
