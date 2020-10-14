using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JogoDaMemoria
{
    public partial class Form1 : Form
    {
        Random random = new Random();

        Label primeiroClique = null;

        Label segundoClique = null;

        List<string> icones = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };
        public Form1()
        {
            InitializeComponent();

            AssociaIconesAosCards();
        }
        private void AssociaIconesAosCards()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconeLabel = control as Label;
                if(iconeLabel != null)
                {
                    int numeroRandomico = random.Next(icones.Count);
                    iconeLabel.Text = icones[numeroRandomico];
                    iconeLabel.ForeColor = iconeLabel.BackColor;
                    icones.RemoveAt(numeroRandomico);
                }
            }
        }

        private void icone_clique(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label labelClicado = sender as Label;

            if (segundoClique != null)
                return;

            if(labelClicado != null)
            {
                if (labelClicado.ForeColor == Color.Black)
                    return;
                if (primeiroClique == null)
                {
                    primeiroClique = labelClicado;
                    primeiroClique.ForeColor = Color.Black;
                    return;
                }

                segundoClique = labelClicado;
                segundoClique.ForeColor = Color.Black;

                Vencedor();

                if (primeiroClique.Text == segundoClique.Text)
                {
                    primeiroClique = null;
                    segundoClique = null;
                    return;
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            primeiroClique.ForeColor = primeiroClique.BackColor;
            segundoClique.ForeColor = segundoClique.BackColor;
            primeiroClique = null;
            segundoClique = null;
        }

        private void Vencedor()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconeLabel = control as Label;

                if (iconeLabel != null)
                {
                    if (iconeLabel.ForeColor == iconeLabel.BackColor)
                        return;
                }
            }

            MessageBox.Show("Você achou todas as figuras!", "Parabéns!");
            Close();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
