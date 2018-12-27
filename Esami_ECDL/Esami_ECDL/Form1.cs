using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Esami_ECDL
{
    public partial class FormUtente : Form
    {
        public FormUtente()
        {
            InitializeComponent();
        }

        // Esci
        public string messaggio = "Sei sicuro di voler uscire?";
        public string avvertenza = "ATTENZIONE";
        MessageBoxButtons bottoni = MessageBoxButtons.YesNo;
        DialogResult risultato;

        private void FormUtente_Load(object sender, EventArgs e)
        {
            Recupero.RecuperoUN = false;
            Recupero.RecuperoPW = false;
        }

        private void buttonLocalLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string StringaConnessione = "Server=127.0.0.1;Database=ecdl;Uid=root;Password=root;";

                DatabaseECDL.connessioneEcdl = new MySqlConnection(StringaConnessione);
                DatabaseECDL.connessioneEcdl.Open();

                MessageBox.Show("Connessione al database ecdl effettuata!");

                FormMenù menù = new FormMenù();
                menù.ShowDialog();

                DatabaseECDL.connessioneEcdl.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (textBoxUsername.Text != "" && textBoxUsername.Text != "Username..." && textBoxPassword.Text != "" && textBoxPassword.Text != "Password...")
            {
                string Username = textBoxUsername.Text;
                string Password = textBoxPassword.Text;

            }
            else
            {
                MessageBox.Show("Credenziali utente non corrette!");
            }
        }

        private void linkLabelCreaAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormAccount CreazioneAccount = new FormAccount();
            CreazioneAccount.ShowDialog();
        }

        private void linkLabelRecuperoPW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Recupero.RecuperoPW = false;
            Recupero.RecuperoUN = true;

            Recupero.RecuperoUN = false;
        }

        private void linkLabelRecuperoUN_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Recupero.RecuperoPW = true;
            Recupero.RecuperoUN = false;

            Recupero.RecuperoPW = false;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            risultato = MessageBox.Show(messaggio, avvertenza, bottoni);
            if (risultato == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }

        private void textBoxUsername_Leave(object sender, EventArgs e)
        {
            if (textBoxUsername.Text == "")
            {
                textBoxUsername.Text = "Username...";
                textBoxUsername.ForeColor = Color.DimGray;
            }
        }

        private void textBoxUsername_Click(object sender, EventArgs e)
        {
            textBoxUsername.Clear();
            textBoxUsername.ForeColor = Color.Black;
        }

        private void textBoxPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxPassword.Text == "")
            {
                textBoxPassword.Text = "Password...";
                textBoxPassword.ForeColor = Color.DimGray;
            }
        }

        private void textBoxPassword_Click(object sender, EventArgs e)
        {
            textBoxPassword.Clear();
            textBoxPassword.ForeColor = Color.Black;
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Inserire nome utente per login");
        }

        private void panel13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Inserire password utente per login");
        }

        private void buttonLogin_MouseEnter(object sender, EventArgs e)
        {
            buttonLogin.BackColor = Color.SteelBlue;
            buttonLogin.ForeColor = Color.White;
        }

        private void buttonLogin_MouseLeave(object sender, EventArgs e)
        {
            buttonLogin.BackColor = Color.Gainsboro;
            buttonLogin.ForeColor = Color.Black;
        }

        private void buttonLocalLogin_MouseEnter(object sender, EventArgs e)
        {
            buttonLocalLogin.BackColor = Color.SteelBlue;
            buttonLocalLogin.ForeColor = Color.White;
        }

        private void buttonLocalLogin_MouseLeave(object sender, EventArgs e)
        {
            buttonLocalLogin.BackColor = Color.Gainsboro;
            buttonLocalLogin.ForeColor = Color.Black;
        }
    }

    public static class DatabaseECDL
    {
        public static MySqlConnection connessioneEcdl;
    }

    public static class Recupero
    {
        public static bool RecuperoPW;
        public static bool RecuperoUN;
    }
}
