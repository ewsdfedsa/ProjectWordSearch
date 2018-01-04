﻿using System;
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
        public Label[] lista_status_cuvinte;
        public string[] cuvinte_in_joc;
        public int numar_cuvinte_in_joc;
        public int dificultate_in_joc;
        public string[][] coordonate_cuvinte;
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

        public string[] autovehicul_ro;
        public string[] autovehicul_en;

        // Constante selectare limba
        public static int LIMBA_ROMANA = 0;
        public static int LIMBA_ENGLEZA = 1;

        // Constante selectare categorie
        public static int CATEGORIE_STIINTA = 0;
        public static int CATEGORIE_MEDICINA = 1;
        public static int CATEGORIE_CULORI = 2;
        public static int CATEGORIE_AUTOVEHICUL = 3;

        public WordSearch()
        {
            this.dificultate_maxima = 20;
            this.dificultate_minima = 10;

            this.nr_max_cuvinte = 10;
            this.nr_min_cuvinte = 1;

            this.nr_max_caractere = 20;
            this.nr_min_caractere = 3;

            this.initializare_cuvinte();
            this.initializare_status_cuvinte();
            this.matrice_joc = new TextBox[dificultate_maxima, dificultate_maxima]; // maxim 20x20
            this.initializare_coordonate();
            InitializeComponent();
            this.initializare_interfata();
        }

        private void initializare_status_cuvinte()
        {
            int x_initial = 130;
            int y_initial = 60;
            int distanta = 22;
            int x, y; // coordonatele

            this.lista_status_cuvinte = new Label[this.nr_max_cuvinte];

            for(int i=0; i<this.nr_max_cuvinte; i++)
            {
                x = x_initial;
                y = y_initial + i * distanta;
                this.lista_status_cuvinte[i] = new Label();
                this.Controls.Add(this.lista_status_cuvinte[i]);
                this.lista_status_cuvinte[i].Text = "";
                this.lista_status_cuvinte[i].Location = new Point(x, y); /// se pozitioneaza
                this.lista_status_cuvinte[i].Show();
            }
        }

        private void reseteaza_status_cuvinte()
        {

            FontFamily font_marcare = this.lista_status_cuvinte[0].Font.FontFamily;
            Font font = new Font(
               font_marcare,
               9,
               FontStyle.Regular,
               GraphicsUnit.Pixel);

            for (int i = 0; i < this.nr_max_cuvinte; i++)
            {
                this.lista_status_cuvinte[i].Text = "";
                this.lista_status_cuvinte[i].Font = font;
                this.lista_status_cuvinte[i].ForeColor = Color.Black;
                this.lista_status_cuvinte[i].Hide();
            }
        }

        private void marcheaza_cuvantul_gasit(int index_cuvant)
        {
            FontFamily font_marcare = this.lista_status_cuvinte[index_cuvant].Font.FontFamily;
            Font font = new Font(
               font_marcare,
               9,
               FontStyle.Strikeout,
               GraphicsUnit.Pixel);

            this.lista_status_cuvinte[index_cuvant].Font = font;
            this.lista_status_cuvinte[index_cuvant].ForeColor = Color.Red;

            for (int k = 0; k < this.coordonate_cuvinte[index_cuvant].Length; k++)
            {
                if (this.coordonate_cuvinte[index_cuvant][k] != "99_99")
                {
                    for (int i = 0; i < dificultate_maxima; i++) // se creeaza matricea jocului
                    {
                        for (int j = 0; j < dificultate_maxima; j++)
                        {
                            string nume_casuta = "casuta_" + this.coordonate_cuvinte[index_cuvant][k];
                            if(this.matrice_joc[i, j].Name == nume_casuta)
                            {
                                this.matrice_joc[i, j].BackColor = Color.LightPink;
                                //Console.WriteLine("Se marcheaza casuta {0}.", this.matrice_joc[i, j].Name);
                            }
                        }
                    }
                    //Console.WriteLine("Coordonate cuvant: {0}", this.coordonate_cuvinte[index_cuvant][k]);
                }
            }
        }

        private void initializare_coordonate()
        {
            string[] coordonate_initiale0 = new string[] { "99_99", "99_99", "99_99", "99_99", "99_99", "99_99" ,"99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99" };
            string[] coordonate_initiale1 = new string[] { "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99" };
            string[] coordonate_initiale2 = new string[] { "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99" };
            string[] coordonate_initiale3 = new string[] { "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99" };
            string[] coordonate_initiale4 = new string[] { "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99" };
            string[] coordonate_initiale5 = new string[] { "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99" };
            string[] coordonate_initiale6 = new string[] { "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99" };
            string[] coordonate_initiale7 = new string[] { "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99" };
            string[] coordonate_initiale8 = new string[] { "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99" };
            string[] coordonate_initiale9 = new string[] { "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99", "99_99" };
            this.coordonate_cuvinte = new string[][] { coordonate_initiale0, coordonate_initiale1, coordonate_initiale2, coordonate_initiale3, coordonate_initiale4, coordonate_initiale5, coordonate_initiale6, coordonate_initiale7, coordonate_initiale8, coordonate_initiale9 };
        }

        private void resetare_coordonate()
        {
            Console.WriteLine("Resetare coordonate");

            for(int i=0; i<this.nr_max_cuvinte; i++)
            {
                for(int j=0; j<this.nr_max_caractere; j++)
                {
                    this.coordonate_cuvinte[i][j] = "99_99";
                }
            }
        }

        private void initializare_cuvinte()
        {
            cuvinte_in_joc = new string[] { "", "", "", "", "", "", "", "", "", "" };

            stiinta_ro = new string[] { "osciloscop", "telescop", "computer", "multimetru", "senzor", "traductor", "pendul", "resort", "gravitatie" };
            stiinta_en = new string[] { "oscilloscope", "telescope", "computer", "multimeter", "sensor", "traductor", "pendulum", "spring", "gravity" };

            medicina_ro = new string[] { "deseuri", "analiza", "docotr", "medicament", "fiola", "seringa", "stetoscop", "microscop", "ambulanta" };
            medicina_en = new string[] { "waste", "analyze", "doctor", "drug", "vial", "syringe", "stethoscope", "microscope", "ambulance" };

            culori_ro = new string[] { "bej", "rosu", "albastru", "portocaliu", "galben", "mov", "verde", "roz", "maro", "gri", "negru", "alb" };
            culori_en = new string[] { "beige", "red", "blue", "orange", "yellow", "purple", "green", "pink", "brown", "grey", "black", "white" };

            autovehicul_ro = new string[] { "volan", "frana", "roata", "claxon", "stergatoare", "parbriz", "semnalizare", "alarma", "centura", "faruri" };
            autovehicul_en = new string[] { "steering", "brake", "wheel", "horn", "wipers", "windshield", "indicator", "alarm", "belt", "headlights" };

            limba_romana = new string[][] { stiinta_ro, medicina_ro, culori_ro, autovehicul_ro };
            limba_engleza = new string[][] { stiinta_en, medicina_en, culori_en, autovehicul_en };
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
                    this.matrice_joc[i, j].Click += new EventHandler(this.casuta_apasata);
                    this.Controls.Add(this.matrice_joc[i, j]); // se adauga casuta creata la forma existenta
                    this.matrice_joc[i, j].Size = new Size(dimensiune_casuta, dimensiune_casuta); // se dimensioneaza
                    this.matrice_joc[i, j].Location = new Point(x, y); /// se pozitioneaza
                    this.matrice_joc[i, j].Text = "0"; // se scrie un text initial (optional)
                    this.matrice_joc[i, j].TextAlign = HorizontalAlignment.Center; // se aliniaza textul pe centru
                    this.matrice_joc[i, j].Enabled = true;
                    this.matrice_joc[i, j].ReadOnly = true;
                    this.matrice_joc[i, j].Name = "casuta_" + i.ToString() + "_" + j.ToString(); 
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
            comboBox2.Items.Add("Autovehicul");
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

            this.adauga_cuvintele_pregatite_in_joc();
        }

        private void adauga_cuvintele_pregatite_in_joc()
        {
            this.resetare_coordonate();
            Console.WriteLine("Se adauga cuvintele...");

            Random generator_nr_aleatoriu = new Random();
            bool cuvantul_a_fost_adaugat;
            bool cuvantul_incape;
            int directie_cuvant; // 0 = vertical, 1 = orizontal
            int x;
            int y;

            for(int i=0; i<this.numar_cuvinte_in_joc; i++)
            {
                cuvantul_a_fost_adaugat = false;
                directie_cuvant = generator_nr_aleatoriu.Next(0, 2);

                string directie_str;

                switch(directie_cuvant)
                {
                    case 0:
                        {
                            directie_str = "verticala";
                            break;
                        }
                    case 1:
                        {
                            directie_str = "orizontala";
                            break;
                        }
                    case 2:
                        {
                            directie_str = "oblica";
                            break;
                        }
                    default:
                        {
                            directie_str = "necunoscuta";
                            break;
                        }
                }

                Console.WriteLine("Adaugare cuvant: {0}, directie: {1}", this.cuvinte_in_joc[i].ToString(), directie_str);

                if(directie_cuvant == 0) // verticala
                {
                    cuvantul_incape = false;
                    x = generator_nr_aleatoriu.Next(0, this.dificultate_in_joc);
                    y = generator_nr_aleatoriu.Next(0, this.dificultate_in_joc);
                    //Console.WriteLine("Coordonata x={0}, y={1}.", x.ToString(), y.ToString());
                    if(x + this.cuvinte_in_joc[i].Length > this.dificultate_in_joc)
                    {
                        x = this.dificultate_in_joc - this.cuvinte_in_joc[i].Length;
                        //Console.WriteLine("Coordonata formatata x={0}, y={1}.", x.ToString(), y.ToString());
                    }

                    for (int litera = 0; litera < this.cuvinte_in_joc[i].Length; litera++)
                    {
                        if (this.matrice_joc[x + litera, y].Text == "" || this.matrice_joc[x + litera, y].Text == this.cuvinte_in_joc[i][litera].ToString())
                        {
                            cuvantul_incape = true;
                        }
                        else
                        {
                            cuvantul_incape = false;
                            break;
                        }
                    }

                    if(cuvantul_incape)
                    {
                        for (int litera = 0; litera < this.cuvinte_in_joc[i].Length; litera++)
                        {
                            this.matrice_joc[x + litera, y].Text = this.cuvinte_in_joc[i][litera].ToString();
                            //this.matrice_joc[x + litera, y].BackColor = Color.LightPink;
                            string coordonate = (x + litera).ToString() + "_" + y.ToString();
                            this.coordonate_cuvinte[i][litera] = coordonate;
                            //Console.WriteLine("Adaugare coordonate: {0} pt cuvantul nr {1} la coordonatele: {1}, {2}", coordonate, i.ToString(), litera.ToString());
                        }
                        cuvantul_a_fost_adaugat = true;
                        Console.WriteLine("Cuvantul {0} a fost adaugat!", this.cuvinte_in_joc[i]);
                    }
                }
                else if(directie_cuvant == 1) // orizontala
                {
                    cuvantul_incape = false;
                    x = generator_nr_aleatoriu.Next(0, this.dificultate_in_joc);
                    y = generator_nr_aleatoriu.Next(0, this.dificultate_in_joc);
                    //Console.WriteLine("Coordonata x={0}, y={1}.", x.ToString(), y.ToString());
                    if (y + this.cuvinte_in_joc[i].Length > this.dificultate_in_joc)
                    {
                        y = this.dificultate_in_joc - this.cuvinte_in_joc[i].Length;
                        //Console.WriteLine("Coordonata formatata x={0}, y={1}.", x.ToString(), y.ToString());
                    }

                    for (int litera = 0; litera < this.cuvinte_in_joc[i].Length; litera++)
                    {
                        if (this.matrice_joc[x, y + litera].Text == "" || this.matrice_joc[x, y + litera].Text == this.cuvinte_in_joc[i][litera].ToString())
                        {
                            cuvantul_incape = true;
                        }
                        else
                        {
                            cuvantul_incape = false;
                            break;
                        }
                    }

                    if(cuvantul_incape)
                    {

                        for (int litera = 0; litera < this.cuvinte_in_joc[i].Length; litera++)
                        {
                            this.matrice_joc[x, litera + y].Text = this.cuvinte_in_joc[i][litera].ToString();
                            //this.matrice_joc[x, litera + y].BackColor = Color.LightPink;
                            string coordonate = x.ToString() + "_" + (litera + y).ToString();
                            this.coordonate_cuvinte[i][litera] = coordonate;
                            //Console.WriteLine("Adaugare coordonate: {0} pt cuvantul nr {1} la coordonatele: {1}, {2}", coordonate, i.ToString(), litera.ToString());
                        }
                        cuvantul_a_fost_adaugat = true;
                        Console.WriteLine("Cuvantul {0} a fost adaugat!", this.cuvinte_in_joc[i]);
                    }
                }

                if (cuvantul_a_fost_adaugat) // incearca din nou adaugarea cuvantului
                {
                    //this.afiseaza_coordonatele();
                }
                else
                {
                    Console.WriteLine("Imposibil de adaugat cuvantul la coordonatele calculate. Recalculare...");
                    i--;
                    continue;
                }
            }

            // completare casute cu caractere aleatorii
            for (int i = 0; i < this.dificultate_in_joc; i++)
            {
                for (int j = 0; j < this.dificultate_in_joc; j++)
                {
                    if (this.matrice_joc[i, j].Text == "")
                    {
                        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                        string str_rnd = chars[generator_nr_aleatoriu.Next(chars.Length)].ToString();
                        this.matrice_joc[i, j].Text = str_rnd;
                    }
                }
            }
        }

        private int pregateste_cuvintele(int numar_cuvinte, int limba, int categoria, int nr_min_caractere, int nr_max_caractere)
        {
            Console.WriteLine("Se pregatesc cuvintele pentru joc...");

            if(numar_cuvinte > this.cuvinte[limba][categoria].Length)
            {
                Console.WriteLine("Nu exista suficiente cuvinte in limba si categoria selectata!");
                return -1;
            }

            int cuvinte_potrivite = 0; // cuvinte care au numarul de cactere cuprins intre valorile selectate
            for(int i=0; i<this.cuvinte[limba][categoria].Length; i++)
            {
                string cuvant = this.cuvinte[limba][categoria][i];
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

            this.reseteaza_status_cuvinte();

            List<int> cuvinte_alese = new List<int>();
            int nr_aleatoriu;
            Random generator_nr_aleatoriu = new Random();
            this.numar_cuvinte_in_joc = numar_cuvinte;

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
                this.cuvinte_in_joc[i] = this.cuvinte[limba][categoria][nr_aleatoriu].ToUpper(); // se adauga cuvantul in lista de cuvinte folosite in jocul curent

                this.lista_status_cuvinte[i].Text = this.cuvinte_in_joc[i];
                this.lista_status_cuvinte[i].Show();

                Console.WriteLine("Se pregateste cuvantul: {0}.", this.cuvinte_in_joc[i].ToString());
            }
            return 0;
        }

        private int preia_nr_cuvinte()
        {
            int nr_cuvinte;

            if (Int32.TryParse(textBox2.Text, out nr_cuvinte) && nr_cuvinte >= this.nr_min_cuvinte && nr_cuvinte <= this.nr_max_cuvinte)
            {
                Console.WriteLine("Se pregateste jocul cu {0} cuvinte.", nr_cuvinte.ToString());
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
                case "AUTOVEHICUL":
                    {
                        categoria = 3;
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
                    this.matrice_joc[i, j].BackColor = Color.White;
                    this.matrice_joc[i, j].ForeColor = Color.Black;
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

        private void casuta_apasata(Object sender, EventArgs e)
        {
            string nume_casuta = ((TextBox)sender).Name;

            string[] coordonate = nume_casuta.Split('_');

            int x, y;
            Int32.TryParse(coordonate[1].ToString(), out x);
            Int32.TryParse(coordonate[2].ToString(), out y);

            Point coordonate_casuta = new Point(x, y);
            this.verifica_cuvant(coordonate_casuta);
        }

        private void verifica_cuvant(Point coordonata)
        {
            string coordonata_str = coordonata.X.ToString() + "_" + coordonata.Y.ToString();
            Console.WriteLine("Se verifica casuta {0}, {1}, folosind stringul {2}.", coordonata.X, coordonata.Y, coordonata_str);
            for (int i=0; i<this.numar_cuvinte_in_joc; i++)
            {
                for(int j=0; j<this.nr_max_caractere; j++)
                {
                    if(this.coordonate_cuvinte[i][j] == coordonata_str)
                    {
                        this.marcheaza_cuvantul_gasit(i);

                        for(int k=0; k<this.coordonate_cuvinte[i].Length; k++)
                        {
                            if (this.coordonate_cuvinte[i][k] != "99_99")
                            {
                                Console.WriteLine("Coordonate cuvant: {0}", this.coordonate_cuvinte[i][k]);
                            }
                        }
                    }
                }
            }
        }

        private int preia_dificultatea()
        {
            int dificultate;
            if (Int32.TryParse(textBox1.Text, out dificultate) && dificultate >= this.dificultate_minima && dificultate <= this.dificultate_maxima)
            {
                Console.WriteLine("Se genereaza matrice patratica de {0}x{0}.", dificultate.ToString());
                this.dificultate_in_joc = dificultate;
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
            if (Int32.TryParse(textBox4.Text, out nr_max_caractere) && nr_max_caractere >= this.nr_min_caractere && nr_max_caractere <= this.nr_max_caractere && nr_max_caractere <= this.dificultate_in_joc - 1)
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

        private void afiseaza_coordonatele()
        {
            for (int i = 0; i < this.numar_cuvinte_in_joc; i++)
            {
                for (int j = 0; j < this.nr_max_caractere; j++)
                {
                    if (this.coordonate_cuvinte[i][j].ToString() != "99_99")
                    {
                        Console.WriteLine("Coordonatele cuvantului {0}: {1}", i.ToString(), this.coordonate_cuvinte[i][j].ToString());
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pregateste_joc();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.afiseaza_coordonatele();
        }
    }
}
