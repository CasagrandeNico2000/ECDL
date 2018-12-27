using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Esami_ECDL
{
    public partial class FormMenù : Form
    {
        public FormMenù()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonEsaminandi_Click(object sender, EventArgs e)
        {
            FormEsaminandi esaminandi = new FormEsaminandi();
            esaminandi.ShowDialog();
        }

        private void buttonEsami_Click(object sender, EventArgs e)
        {
            FormEsami esami = new FormEsami();
            esami.ShowDialog();
        }

        private void buttonSessioni_Click(object sender, EventArgs e)
        {
            FormSessioni sessioni = new FormSessioni();
            sessioni.ShowDialog();
        }

        private void buttonEsamiSessioni_Click(object sender, EventArgs e)
        {
            FormEsamiSessioni esamiSessioni = new FormEsamiSessioni();
            esamiSessioni.ShowDialog();
        }

        private void buttonLuogodiNascita_Click(object sender, EventArgs e)
        {
            FormLuogodiNascita luogodiNascita = new FormLuogodiNascita();
            luogodiNascita.ShowDialog();
        }

        private void buttonSede_Click(object sender, EventArgs e)
        {
            FormSede sede = new FormSede();
            sede.ShowDialog();
        }

        private void buttonRisultati_Click(object sender, EventArgs e)
        {
            FormRisultati risultati = new FormRisultati();
            risultati.ShowDialog();
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Black;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            label3.ForeColor = Color.DimGray;
        }

        private void buttonEsaminandi_MouseEnter(object sender, EventArgs e)
        {
            buttonEsaminandi.BackColor = Color.SteelBlue;
            buttonEsaminandi.ForeColor = Color.White;
        }

        private void buttonEsaminandi_MouseLeave(object sender, EventArgs e)
        {
            buttonEsaminandi.BackColor = Color.Gainsboro;
            buttonEsaminandi.ForeColor = Color.Black;
        }

        private void buttonEsami_MouseEnter(object sender, EventArgs e)
        {
            buttonEsami.BackColor = Color.SteelBlue;
            buttonEsami.ForeColor = Color.White;
        }

        private void buttonEsami_MouseLeave(object sender, EventArgs e)
        {
            buttonEsami.BackColor = Color.Gainsboro;
            buttonEsami.ForeColor = Color.Black;
        }

        private void buttonSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonSessioni.BackColor = Color.SteelBlue;
            buttonSessioni.ForeColor = Color.White;
        }

        private void buttonSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonSessioni.BackColor = Color.Gainsboro;
            buttonSessioni.ForeColor = Color.Black;
        }

        private void buttonEsamiSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonEsamiSessioni.BackColor = Color.SteelBlue;
            buttonEsamiSessioni.ForeColor = Color.White;
        }

        private void buttonEsamiSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonEsamiSessioni.BackColor = Color.Gainsboro;
            buttonEsamiSessioni.ForeColor = Color.Black;
        }

        private void buttonLuogodiNascita_MouseEnter(object sender, EventArgs e)
        {
            buttonLuogodiNascita.BackColor = Color.SteelBlue;
            buttonLuogodiNascita.ForeColor = Color.White;
        }

        private void buttonLuogodiNascita_MouseLeave(object sender, EventArgs e)
        {
            buttonLuogodiNascita.BackColor = Color.Gainsboro;
            buttonLuogodiNascita.ForeColor = Color.Black;
        }

        private void buttonSede_MouseEnter(object sender, EventArgs e)
        {
            buttonSede.BackColor = Color.SteelBlue;
            buttonSede.ForeColor = Color.White;
        }

        private void buttonSede_MouseLeave(object sender, EventArgs e)
        {
            buttonSede.BackColor = Color.Gainsboro;
            buttonSede.ForeColor = Color.Black;
        }

        private void buttonRisultati_MouseEnter(object sender, EventArgs e)
        {
            buttonRisultati.BackColor = Color.SteelBlue;
            buttonRisultati.ForeColor = Color.White;
        }

        private void buttonRisultati_MouseLeave(object sender, EventArgs e)
        {
            buttonRisultati.BackColor = Color.Gainsboro;
            buttonRisultati.ForeColor = Color.Black;
        }
    }
}
