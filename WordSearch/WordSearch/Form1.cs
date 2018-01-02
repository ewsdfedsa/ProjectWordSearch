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
        public WordSearch()
        {
            this.dificultate_maxima = 20;
            this.matrice_joc = new TextBox[dificultate_maxima, dificultate_maxima]; // maxim 20x20
            InitializeComponent();
            this.initializare_interfata();

        }

        private void initializare_interfata()
        {
            // creare casute pentru litere
            int dificultate_maxima = this.dificultate_maxima; // numar liniii si coloane
            int dimensiune_casuta = 20;
            int x_initial = 300;
            int y_initial = 50;
            int x;
            int y;

            for(int i=0; i< dificultate_maxima; i++)
            {
                for(int j=0; j< dificultate_maxima; j++)
                {
                    x = x_initial + (j * dimensiune_casuta);
                    y = y_initial + (i * dimensiune_casuta);
                    this.matrice_joc[i, j] = new TextBox();
                    this.Controls.Add(this.matrice_joc[i, j]);
                    this.matrice_joc[i, j].Size = new Size(dimensiune_casuta, dimensiune_casuta);
                    this.matrice_joc[i, j].Location = new Point(x, y);
                    this.matrice_joc[i, j].Text = "0";
                    this.matrice_joc[i, j].TextAlign = HorizontalAlignment.Center;
                    this.matrice_joc[i, j].Enabled = false;
                    this.matrice_joc[i, j].Show();
                }
            }
            /*
            foreach(TextBox casuta in this.matrice_joc)
            {
                casuta.Show();
                casuta.Location = new Point(15, 15);
                casuta.Text = "0";
            }*/
        }

        private void pregateste_joc()
        {
            int numar_de_linii = this.preia_numar_de_linii();
            if(numar_de_linii == 0) // numar invalid de linii
            {
                return;
            }
        }

        private int preia_numar_de_linii()
        {
            int numar_linii;
            if (Int32.TryParse(textBox1.Text, out numar_linii) && numar_linii >= 5 && numar_linii <= 15)
            {
                Console.WriteLine("Se genereaza matrice patratica de {0}x{0}.", numar_linii.ToString());
            }
            else
            {
                string mesaj = "Eroare - numar de linii invalid!\nSe accepta doar valori numerice cuprinse intre 5 si 15!";
                Console.WriteLine(mesaj);
                numar_linii = 0;
            }
            return numar_linii;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.pregateste_joc();
        }
    }
}
