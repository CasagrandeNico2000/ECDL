using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace Esami_ECDL
{
    public partial class FormAccount : Form
    {
        public FormAccount()
        {
            InitializeComponent();
        }

        //Variabili per criptazione
        SymmetricAlgorithm algoritmo;
        string nuovapwcriptata;
        string pwdecriptata;
        byte[] pwcriptata;
        byte[] bytespassword;
        byte[] bytespassworddecriptata;
        byte[] chiavepassword;
        string apppwdecriptata;
        bool criptazEffettuata = false;

        private void FormAccount_Load(object sender, EventArgs e)
        {
            algoritmo = Rijndael.Create();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            string esistefiletxt = "Credenziali utenti.txt";
            if (File.Exists(esistefiletxt) == true)
            {
                int numerorighefiletxt = File.ReadAllLines("Credenziali utenti.txt").Length;
                if (numerorighefiletxt > 0)
                    System.IO.File.WriteAllText("Credenziali utenti.txt", string.Empty);
            }
        }

        private void labelCreaAccount_Click(object sender, EventArgs e)
        {
            if (textBoxCreaUsername.Text != "" && textBoxCreaUsername.Text != "Username..." && textBoxCreaPassword.Text != "" && textBoxCreaPassword.Text != "Password...")
            {
                StreamWriter sw = new StreamWriter("Credenziali utenti.txt", true);
                bool criptaz = checkBoxCriptazionePW.Checked;
                string pwCriptataString = "", bytesPasswordString = "", chiavePasswordString = "";
                for (int i = 0; i < pwcriptata.Length; i++)
                {
                    pwCriptataString += pwcriptata[i].ToString();
                    if (i != pwcriptata.Length - 1)
                        pwCriptataString += ",";
                }
                for (int i = 0; i < bytespassword.Length; i++)
                {
                    bytesPasswordString += bytespassword[i].ToString();
                    if (i != bytespassword.Length - 1)
                        bytesPasswordString += ",";
                }
                for (int i = 0; i < chiavepassword.Length; i++)
                {
                    chiavePasswordString += chiavepassword[i].ToString();
                    if (i != chiavepassword.Length - 1)
                        chiavePasswordString += ",";
                }
                if (criptaz != false)
                    sw.WriteLine(textBoxCreaUsername.Text + ";" + textBoxCreaPassword.Text + ";" + pwCriptataString + ";" + bytesPasswordString + ";" + chiavePasswordString);
                else
                    sw.WriteLine(textBoxCreaUsername.Text + ";" + textBoxCreaPassword.Text);
                sw.Close();
                this.Close();
            }
            else
            {
                MessageBox.Show("Credenziali utente non corrette!");
            }
        }

        private void checkBoxCriptazionePW_CheckedChanged(object sender, EventArgs e)
        {
            if (textBoxCreaPassword.Text != "" && textBoxCreaPassword.Text != "Password...")
            {
                if (checkBoxCriptazionePW.Checked != false)
                {
                    string appPW = CriptazionePW(textBoxCreaPassword.Text);
                    textBoxCreaPassword.Text = appPW;
                    criptazEffettuata = true;
                }
                else
                {
                    if (criptazEffettuata != false)
                    {
                        string appPW = DecriptazionePW();
                        textBoxCreaPassword.Text = appPW;
                        criptazEffettuata = false;
                    }
                    else
                    {
                        MessageBox.Show("Criptazione necessaria per decriptazione!");
                    }
                }
            }
            else
            {
                if (checkBoxCriptazionePW.Checked != false)
                    MessageBox.Show("Criptazione non valida!");
                else
                    MessageBox.Show("Decriptazione non valida!");
            }
        }

        private string CriptazionePW(string pw)
        {
            bytespassword = Encoding.ASCII.GetBytes(pw);
            chiavepassword = Encoding.ASCII.GetBytes("0123456789abcdef");
            algoritmo.Key = chiavepassword;
            algoritmo.Mode = CipherMode.CBC;
            algoritmo.Padding = PaddingMode.PKCS7;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, algoritmo.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(bytespassword, 0, bytespassword.Length);
            cs.Close();
            pwcriptata = ms.ToArray();
            ms.Close();
            nuovapwcriptata = Encoding.ASCII.GetString(pwcriptata);
            return nuovapwcriptata;
        }

        private string DecriptazionePW()
        {
            System.IO.MemoryStream ms1 = new System.IO.MemoryStream(pwcriptata);
            CryptoStream cs1 = new CryptoStream(ms1, algoritmo.CreateDecryptor(), CryptoStreamMode.Read);
            cs1.Read(pwcriptata, 0, pwcriptata.Length);
            bytespassworddecriptata = ms1.ToArray();
            cs1.Close();
            ms1.Close();
            pwdecriptata = Encoding.ASCII.GetString(bytespassworddecriptata);
            apppwdecriptata = pwdecriptata.Substring(0, bytespassword.Length);
            return apppwdecriptata;
        }

        private void labelCreaAccount_MouseEnter(object sender, EventArgs e)
        {
            labelCreaAccount.ForeColor = Color.Black;
        }

        private void labelCreaAccount_MouseLeave(object sender, EventArgs e)
        {
            labelCreaAccount.ForeColor = Color.DimGray;
        }

        private void panel9_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Inserire nome utente per Account");
        }

        private void panel13_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Inserire password utente per Account");
        }

        private void textBoxCreaUsername_Click(object sender, EventArgs e)
        {
            textBoxCreaUsername.Clear();
            textBoxCreaUsername.ForeColor = Color.Black;
        }

        private void textBoxCreaUsername_Leave(object sender, EventArgs e)
        {
            if (textBoxCreaUsername.Text == "")
            {
                textBoxCreaUsername.Text = "Username...";
                textBoxCreaUsername.ForeColor = Color.DimGray;
            }
        }

        private void textBoxCreaPassword_Click(object sender, EventArgs e)
        {
            textBoxCreaPassword.Clear();
            textBoxCreaPassword.ForeColor = Color.Black;
        }

        private void textBoxCreaPassword_Leave(object sender, EventArgs e)
        {
            if (textBoxCreaPassword.Text == "")
            {
                textBoxCreaPassword.Text = "Password...";
                textBoxCreaPassword.ForeColor = Color.DimGray;
            }
        }

        private void panel18_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Scegliere se effettuare criptazione password(opzionale)");
        }
    }
}