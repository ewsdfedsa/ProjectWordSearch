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

        public string[][][] cuvinte;
        public string[][] limba_romana;
        public string[] stiinta_ro;
        public string[] medicina_ro;

        public WordSearch()
        { 
            this.dificultate_maxima = 20;
            this.dificultate_minima = 5;
            this.initializare_cuvinte();
            this.matrice_joc = new TextBox[dificultate_maxima, dificultate_maxima]; // maxim 20x20
            InitializeComponent();
            this.initializare_interfata();
            
        }

        private void initializare_cuvinte()
        {
            stiinta_ro = new string[] { "telescop", "computer", "multimetru", "senzor", "traductor", "pendul", "resort", "gravitatie" };
            medicina_ro = new string[] { "analiza", "docotr", "medicament", "fiola", "seringa", "stetoscop", "microscop", "ambulanta" };
     
            limba_romana = new string[][] { stiinta_ro, medicina_ro };
            cuvinte = new string[][][] { limba_romana };

            Console.WriteLine(stiinta_ro[0].ToString());
            Console.WriteLine(medicina_ro[0].ToString());
            Console.WriteLine(limba_romana[0][2].ToString());
            Console.WriteLine(cuvinte[0][0][2].ToString());
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
        }

        private void pregateste_joc()
        {
            int dificultate = this.preia_dificultatea();
            if(dificultate == 0) // numar invalid de linii
            {
                return;
            }
            this.pregateste_casutele(dificultate);
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
                string mesaj = "Eroare - numar de linii invalid!\nSe accepta doar valori numerice cuprinse intre 5 si 15!";
                Console.WriteLine(mesaj);
                dificultate = 0;
            }
            return dificultate;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pregateste_joc();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
