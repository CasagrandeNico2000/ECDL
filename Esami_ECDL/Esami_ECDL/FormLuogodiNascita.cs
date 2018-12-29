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
    public partial class FormLuogodiNascita : Form
    {
        public FormLuogodiNascita()
        {
            InitializeComponent();
        }

        //Variabili per uso generale
        List<string> listaRegioni, listaCAP, listaProvince;
        int numRigheSelezionate = 0, indiceFrom = 0, indiceUntil = 0;
        string capUpdate = "", cittàUpdate = "", provinciaUpdate = "", regioneUpdate = "";
        bool firstSearch = true, firstSearchDelete = true;

        //Oggetti
        Label lblNumeroRighe = new Label();
        Label lblDeleteFrom = new Label();
        Label lblDeleteUntil = new Label();

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Black;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label2.ForeColor = Color.DimGray;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBoxCAP.Text = "CAP...";
            textBoxCittà.Text = "Città...";
            comboBoxProvincia.Text = "Provincia...";
            comboBoxRegione.Text = "Regione...";
            textBoxCAP.ForeColor = Color.DimGray;
            textBoxCittà.ForeColor = Color.DimGray;
            comboBoxProvincia.ForeColor = Color.DimGray;
            comboBoxRegione.ForeColor = Color.DimGray;
            comboBoxProvincia.Items.Clear();
            comboBoxRegione.Items.Clear();
            foreach (string item in listaProvince)
            {
                string[] p = item.Split(',');
                foreach (string item2 in p)
                {
                    comboBoxProvincia.Items.Add(item2);
                }
            }
            foreach (string item in listaRegioni)
            {
                comboBoxRegione.Items.Add(item);
            }
        }

        private void buttonInserisci_Click(object sender, EventArgs e)
        {
            if (textBoxCAP.Text != "CAP..." && textBoxCAP.Text != "" && textBoxCittà.Text != "Città..." && textBoxCittà.Text != "" && comboBoxProvincia.Text != "Provincia..." && comboBoxProvincia.Text != "" && comboBoxRegione.Text != "Regione..." && comboBoxRegione.Text != "")
            {
                int indice = 0;
                bool verCap = false, verProvincia = false, verRegione = false;
                int cap = Convert.ToInt32(textBoxCAP.Text);
                string città = textBoxCittà.Text;
                string provincia = comboBoxProvincia.Text;
                string regione = comboBoxRegione.Text;
                foreach (string item in listaRegioni)
                {
                    if (regione == item)
                    {
                        string[] capCorretti = listaCAP.ElementAt(indice).Split('-');
                        int minimo = Convert.ToInt32(capCorretti[0]);
                        int massimo = Convert.ToInt32(capCorretti[1]);
                        if (cap >= minimo && cap <= massimo)
                        {
                            verCap = true;
                        }
                        verRegione = true;
                    }
                    indice++;
                }
                foreach (string item in listaProvince)
                {
                    if (item.Contains(provincia))
                    {
                        verProvincia = true;
                    }
                }
                if (verCap == true && verProvincia == true && verRegione == true)
                {
                    try
                    {
                        MySqlCommand InsertQuery;
                        string comando = "INSERT INTO ecdl.luogo_di_nascita(CAP,Città,Provincia,Regione) VALUES (" + cap + ",'" + città + "','" + provincia + "','" + regione + "')";
                        InsertQuery = new MySqlCommand(comando, DatabaseECDL.connessioneEcdl);
                        InsertQuery.ExecuteNonQuery();
                        MessageBox.Show("Inserimento riga tabella effettuato!");
                        Aggiorna();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                }
                else if (verCap == false)
                {
                    MessageBox.Show("CAP non corretto!");
                }
                else if (verProvincia == false)
                {
                    MessageBox.Show("Provincia non corretta!");
                }
                else if (verRegione == false)
                {
                    MessageBox.Show("Regione non corretta!");
                }
            }
            else
            {
                MessageBox.Show("Per eseguire il comando è necessario l'inserimento di tutti i dati!");
            }
        }

        private void Aggiorna()
        {
            MySqlCommand SelectQuery;
            SelectQuery = new MySqlCommand("SELECT * FROM ecdl.luogo_di_nascita", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SelectQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewLuogodiNascitaInsert.DataSource = ds.Tables[0];
            dataGridViewLuogodiNascitaDelete.DataSource = ds.Tables[0];
            dataGridViewLuogodiNascitaUpdate.DataSource = ds.Tables[0];

            Select_Count();
        }

        private void Select_Count()
        {
            MySqlCommand SelectCountQuery;
            SelectCountQuery = DatabaseECDL.connessioneEcdl.CreateCommand();
            SelectCountQuery.CommandText = "SELECT COUNT(*) FROM ecdl.luogo_di_nascita";
            int numeroRighe = Convert.ToInt32(SelectCountQuery.ExecuteScalar());
            Adatta_Panel_Numero_Righe(numeroRighe.ToString());
        }

        private void Adatta_Panel_Numero_Righe(string num)
        {
            lblNumeroRighe.AutoSize = false;
            lblNumeroRighe.Dock = DockStyle.Fill;
            lblNumeroRighe.Size = new Size(717, 29);
            lblNumeroRighe.Text = "NUMERO RIGHE: " + num;
            lblNumeroRighe.TextAlign = ContentAlignment.MiddleCenter;
            panelNumeroRighe.Controls.Add(lblNumeroRighe);
            panelNumeroRighe.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_From(string cap)
        {
            lblDeleteFrom.AutoSize = false;
            lblDeleteFrom.Dock = DockStyle.Fill;
            lblDeleteFrom.Size = new Size(296, 25);
            lblDeleteFrom.Text = "CAP: " + cap;
            lblDeleteFrom.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteFrom.Controls.Add(lblDeleteFrom);
            panelDeleteFrom.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_Until(string cap)
        {
            lblDeleteUntil.AutoSize = false;
            lblDeleteUntil.Dock = DockStyle.Fill;
            lblDeleteUntil.Size = new Size(296, 25);
            if (radioButtonSingle.Checked == true)
                lblDeleteUntil.Text = cap;
            else
                lblDeleteUntil.Text = "CAP: " + cap;
            lblDeleteUntil.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteUntil.Controls.Add(lblDeleteUntil);
            panelDeleteUntil.BorderStyle = BorderStyle.FixedSingle;
        }

        private void buttonInserisci_MouseEnter(object sender, EventArgs e)
        {
            buttonInserisci.ForeColor = Color.White;
            buttonInserisci.BackColor = Color.SteelBlue;
        }

        private void buttonInserisci_MouseLeave(object sender, EventArgs e)
        {
            buttonInserisci.ForeColor = Color.Black;
            buttonInserisci.BackColor = Color.Gainsboro;
        }

        private void buttonClear_MouseEnter(object sender, EventArgs e)
        {
            buttonClear.ForeColor = Color.White;
            buttonClear.BackColor = Color.SteelBlue;
        }

        private void buttonClear_MouseLeave(object sender, EventArgs e)
        {
            buttonClear.ForeColor = Color.Black;
            buttonClear.BackColor = Color.Gainsboro;
        }

        private void textBoxCAP_Click(object sender, EventArgs e)
        {
            textBoxCAP.Clear();
            textBoxCAP.ForeColor = Color.Black;
        }

        private void textBoxCAP_Leave(object sender, EventArgs e)
        {
            if (textBoxCAP.Text == "")
            {
                textBoxCAP.Text = "CAP...";
                textBoxCAP.ForeColor = Color.DimGray;
            }
        }

        private void textBoxCittà_Click(object sender, EventArgs e)
        {
            textBoxCittà.Clear();
            textBoxCittà.ForeColor = Color.Black;
        }

        private void textBoxCittà_Leave(object sender, EventArgs e)
        {
            if (textBoxCittà.Text == "")
            {
                textBoxCittà.Text = "Città...";
                textBoxCittà.ForeColor = Color.DimGray;
            }
        }

        private void comboBoxProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxProvincia.ForeColor = Color.Black;
            int indice = 0;
            string provincia = comboBoxProvincia.Text;
            foreach (string item in listaProvince)
            {
                if (item.Contains(provincia))
                {
                    comboBoxRegione.Items.Clear();
                    comboBoxRegione.Items.Add(listaRegioni.ElementAt(indice));
                }
                indice++;
            }
        }

        private void radioButtonMulti_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMulti.Checked == false)
            {
                Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            }
            else
            {
                Adatta_Panel_Delete_Until("SELEZIONA RIGA...");
            }
        }

        private void dataGridViewLuogodiNascitaDelete_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonSingle.Checked == true)
            {
                if (dataGridViewLuogodiNascitaDelete.CurrentRow != null && dataGridViewLuogodiNascitaDelete.CurrentRow.Index < dataGridViewLuogodiNascitaDelete.Rows.Count - 1)
                {
                    string cap = dataGridViewLuogodiNascitaDelete.CurrentRow.Cells[0].Value.ToString();
                    Adatta_Panel_Delete_From(cap);
                }
            }
            else
            {
                if (numRigheSelezionate == 2)
                {
                    if (dataGridViewLuogodiNascitaDelete.CurrentRow != null && dataGridViewLuogodiNascitaDelete.CurrentRow.Index < dataGridViewLuogodiNascitaDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewLuogodiNascitaDelete.CurrentRow.Index;
                        string cap = dataGridViewLuogodiNascitaDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(cap);
                        numRigheSelezionate = 1;
                    }
                }
                else if (numRigheSelezionate == 1)
                {
                    if (dataGridViewLuogodiNascitaDelete.CurrentRow != null && dataGridViewLuogodiNascitaDelete.CurrentRow.Index < dataGridViewLuogodiNascitaDelete.Rows.Count - 1)
                    {
                        indiceUntil = dataGridViewLuogodiNascitaDelete.CurrentRow.Index;
                        string cap = dataGridViewLuogodiNascitaDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_Until(cap);
                        numRigheSelezionate++;
                    }
                }
                else if (numRigheSelezionate == 0)
                {
                    if (dataGridViewLuogodiNascitaDelete.CurrentRow != null && dataGridViewLuogodiNascitaDelete.CurrentRow.Index < dataGridViewLuogodiNascitaDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewLuogodiNascitaDelete.CurrentRow.Index;
                        string cap = dataGridViewLuogodiNascitaDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(cap);
                        numRigheSelezionate++;
                    }
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (radioButtonSingle.Checked == true)
            {
                try
                {
                    string[] valPanel = lblDeleteFrom.Text.Split(':');
                    string cap = valPanel[1].Trim(' ');
                    MySqlCommand DeleteQuery;
                    string comando = "DELETE FROM ecdl.luogo_di_nascita WHERE CAP = " + cap;
                    DeleteQuery = new MySqlCommand(comando, DatabaseECDL.connessioneEcdl);
                    DeleteQuery.ExecuteNonQuery();
                    MessageBox.Show("Rimozione riga tabella effettuata!");
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString());
                }
            }
            else
            {
                if (indiceFrom < indiceUntil)
                {
                    List<string> listaCapDelete = new List<string>();
                    for (int i = indiceFrom; i <= indiceUntil; i++)
                    {
                        listaCapDelete.Add(dataGridViewLuogodiNascitaDelete.Rows[i].Cells[0].Value.ToString());
                    }
                    try
                    {
                        foreach (string item in listaCapDelete)
                        {
                            MySqlCommand DeleteQuery;
                            string comando = "DELETE FROM ecdl.luogo_di_nascita WHERE CAP = " + item;
                            DeleteQuery = new MySqlCommand(comando, DatabaseECDL.connessioneEcdl);
                            DeleteQuery.ExecuteNonQuery();
                        }
                        MessageBox.Show("Rimozione righe tabella effettuata!");
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                }
                else
                {
                    MessageBox.Show("Multiselezione non corretta!");
                }
            }
            Aggiorna();
        }

        private void buttonDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand DeleteAllQuery;
                string comando = "DELETE FROM ecdl.luogo_di_nascita";
                DeleteAllQuery = new MySqlCommand(comando, DatabaseECDL.connessioneEcdl);
                DeleteAllQuery.ExecuteNonQuery();
                Aggiorna();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void textBoxCercaValore_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonSearchCAP.Checked == true)
            {
                MySqlCommand SearchCAPQuery;
                SearchCAPQuery = new MySqlCommand("SELECT * FROM ecdl.luogo_di_nascita WHERE CAP LIKE '" + textBoxCercaValore.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchCAPQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewLuogodiNascitaUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchCittà.Checked == true)
            {
                MySqlCommand SearchCittàQuery;
                SearchCittàQuery = new MySqlCommand("SELECT * FROM ecdl.luogo_di_nascita WHERE Città LIKE '" + textBoxCercaValore.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchCittàQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewLuogodiNascitaUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchProvincia.Checked == true)
            {
                MySqlCommand SearchProvinciaQuery;
                SearchProvinciaQuery = new MySqlCommand("SELECT * FROM ecdl.luogo_di_nascita WHERE Provincia LIKE '" + textBoxCercaValore.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchProvinciaQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewLuogodiNascitaUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchRegione.Checked == true)
            {
                MySqlCommand SearchRegioneQuery;
                SearchRegioneQuery = new MySqlCommand("SELECT * FROM ecdl.luogo_di_nascita WHERE Regione LIKE '" + textBoxCercaValore.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchRegioneQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewLuogodiNascitaUpdate.DataSource = ds.Tables[0];
            }
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            buttonDelete.ForeColor = Color.White;
            buttonDelete.BackColor = Color.SteelBlue;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            buttonDelete.ForeColor = Color.Black;
            buttonDelete.BackColor = Color.Gainsboro;
        }

        private void textBoxCercaValore_Click(object sender, EventArgs e)
        {
            if (firstSearch != false)
            {
                textBoxCercaValore.Clear();
                textBoxCercaValore.ForeColor = Color.Black;
                firstSearch = false;
            }
        }

        private void radioButtonSearchCAP_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValore_TextChanged(sender, e);
        }

        private void radioButtonSearchCittà_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValore_TextChanged(sender, e);
        }

        private void radioButtonSearchProvincia_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValore_TextChanged(sender, e);
        }

        private void radioButtonSearchRegione_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValore_TextChanged(sender, e);
        }

        private void dataGridViewLuogodiNascitaUpdate_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridViewLuogodiNascitaUpdate.CurrentRow != null && dataGridViewLuogodiNascitaUpdate.CurrentRow.Index < dataGridViewLuogodiNascitaUpdate.Rows.Count - 1)
            {
                textBoxCAPUpdate.Text = dataGridViewLuogodiNascitaUpdate.CurrentRow.Cells[0].Value.ToString();
                textBoxCittàUpdate.Text = dataGridViewLuogodiNascitaUpdate.CurrentRow.Cells[1].Value.ToString();
                comboBoxProvinciaUpdate.Text = dataGridViewLuogodiNascitaUpdate.CurrentRow.Cells[2].Value.ToString();
                comboBoxRegioneUpdate.Text = dataGridViewLuogodiNascitaUpdate.CurrentRow.Cells[3].Value.ToString();
                textBoxCAPUpdate.ForeColor = Color.DimGray;
                textBoxCittàUpdate.ForeColor = Color.DimGray;
                comboBoxProvinciaUpdate.ForeColor = Color.DimGray;
                comboBoxRegioneUpdate.ForeColor = Color.DimGray;
                capUpdate = textBoxCAPUpdate.Text;
                cittàUpdate = textBoxCittàUpdate.Text;
                provinciaUpdate = comboBoxProvinciaUpdate.Text;
                regioneUpdate = comboBoxRegioneUpdate.Text;
                comboBoxProvinciaUpdate.Items.Clear();
                comboBoxRegioneUpdate.Items.Clear();
                foreach (string item in listaProvince)
                {
                    string[] p = item.Split(',');
                    foreach (string item2 in p)
                    {
                        comboBoxProvinciaUpdate.Items.Add(item2);
                    }
                }
                foreach (string item in listaRegioni)
                {
                    comboBoxRegioneUpdate.Items.Add(item);
                }
            }
        }

        private void textBoxCAPUpdate_Click_1(object sender, EventArgs e)
        {
            textBoxCAPUpdate.Clear();
            textBoxCAPUpdate.ForeColor = Color.Black;
        }

        private void comboBoxProvinciaUpdate_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBoxProvinciaUpdate.ForeColor = Color.Black;
            int indice = 0;
            string provincia = comboBoxProvinciaUpdate.Text;
            foreach (string item in listaProvince)
            {
                if (item.Contains(provincia))
                {
                    comboBoxRegioneUpdate.Items.Clear();
                    comboBoxRegioneUpdate.Items.Add(listaRegioni.ElementAt(indice));
                }
                indice++;
            }
        }

        private void comboBoxRegioneUpdate_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            comboBoxRegioneUpdate.ForeColor = Color.Black;
            int indice = 0;
            string regione = comboBoxRegioneUpdate.Text;
            foreach (string item in listaRegioni)
            {
                if (regione == item)
                {
                    comboBoxProvinciaUpdate.Items.Clear();
                    string[] province = listaProvince.ElementAt(indice).Split(',');
                    foreach (string item2 in province)
                    {
                        comboBoxProvinciaUpdate.Items.Add(item2);
                    }
                }
                indice++;
            }
        }

        private void buttonClearUpdate_Click_1(object sender, EventArgs e)
        {
            textBoxCAPUpdate.Text = capUpdate;
            textBoxCittàUpdate.Text = cittàUpdate;
            comboBoxProvinciaUpdate.Text = provinciaUpdate;
            comboBoxRegioneUpdate.Text = regioneUpdate;
            textBoxCAPUpdate.ForeColor = Color.DimGray;
            textBoxCittàUpdate.ForeColor = Color.DimGray;
            comboBoxProvinciaUpdate.ForeColor = Color.DimGray;
            comboBoxRegioneUpdate.ForeColor = Color.DimGray;
            comboBoxProvinciaUpdate.Items.Clear();
            comboBoxRegioneUpdate.Items.Clear();
            foreach (string item in listaProvince)
            {
                string[] p = item.Split(',');
                foreach (string item2 in p)
                {
                    comboBoxProvinciaUpdate.Items.Add(item2);
                }
            }
            foreach (string item in listaRegioni)
            {
                comboBoxRegioneUpdate.Items.Add(item);
            }
        }

        private void buttonUpdate_Click_1(object sender, EventArgs e)
        {
            if (textBoxCAPUpdate.Text != "" && textBoxCittàUpdate.Text != "" && comboBoxProvinciaUpdate.Text != "" && comboBoxRegioneUpdate.Text != "")
            {
                int indice = 0;
                bool verCap = false, verProvincia = false, verRegione = false;
                int cap = Convert.ToInt32(textBoxCAPUpdate.Text);
                string città = textBoxCittàUpdate.Text;
                string provincia = comboBoxProvinciaUpdate.Text;
                string regione = comboBoxRegioneUpdate.Text;
                foreach (string item in listaRegioni)
                {
                    if (regione == item)
                    {
                        string[] capCorretti = listaCAP.ElementAt(indice).Split('-');
                        int minimo = Convert.ToInt32(capCorretti[0]);
                        int massimo = Convert.ToInt32(capCorretti[1]);
                        if (cap >= minimo && cap <= massimo)
                        {
                            verCap = true;
                        }
                        verRegione = true;
                    }
                    indice++;
                }
                foreach (string item in listaProvince)
                {
                    if (item.Contains(provincia))
                    {
                        verProvincia = true;
                    }
                }
                if (verCap == true && verProvincia == true && verRegione == true)
                {
                    try
                    {
                        MySqlCommand UpdateQuery;
                        string comando = "UPDATE ecdl.luogo_di_nascita SET CAP = " + cap + ", Città = '" + città + "', Provincia = '" + provincia + "', Regione = '" + regione + "' WHERE CAP = " + capUpdate;
                        UpdateQuery = new MySqlCommand(comando, DatabaseECDL.connessioneEcdl);
                        UpdateQuery.ExecuteNonQuery();
                        MessageBox.Show("Modifica riga tabella effettuata!");
                        Aggiorna();
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show(exception.ToString());
                    }
                }
                else if (verCap == false)
                {
                    MessageBox.Show("CAP non corretto!");
                }
                else if (verProvincia == false)
                {
                    MessageBox.Show("Provincia non corretta!");
                }
                else if (verRegione == false)
                {
                    MessageBox.Show("Regione non corretta!");
                }
            }
            else
            {
                MessageBox.Show("Per eseguire il comando è necessario l'inserimento di tutti i dati!");
            }
        }

        private void buttonClearUpdate_MouseEnter_1(object sender, EventArgs e)
        {
            buttonClearUpdate.ForeColor = Color.White;
            buttonClearUpdate.BackColor = Color.SteelBlue;
        }

        private void buttonClearUpdate_MouseLeave_1(object sender, EventArgs e)
        {
            buttonClearUpdate.ForeColor = Color.Black;
            buttonClearUpdate.BackColor = Color.Gainsboro;
        }

        private void buttonUpdate_MouseEnter_1(object sender, EventArgs e)
        {
            buttonUpdate.ForeColor = Color.White;
            buttonUpdate.BackColor = Color.SteelBlue;
        }

        private void buttonUpdate_MouseLeave_1(object sender, EventArgs e)
        {
            buttonUpdate.ForeColor = Color.Black;
            buttonUpdate.BackColor = Color.Gainsboro;
        }

        private void textBoxCercaValoreDelete_TextChanged(object sender, EventArgs e)
        {
            MySqlCommand SearchCAPQuery;
            SearchCAPQuery = new MySqlCommand("SELECT * FROM ecdl.luogo_di_nascita WHERE CAP LIKE '" + textBoxCercaValoreDelete.Text + "%'", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchCAPQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewLuogodiNascitaDelete.DataSource = ds.Tables[0];
        }

        private void textBoxCercaValoreDelete_Click(object sender, EventArgs e)
        {
            if (firstSearchDelete != false)
            {
                textBoxCercaValoreDelete.Clear();
                textBoxCercaValoreDelete.ForeColor = Color.Black;
                firstSearchDelete = false;
            }
        }

        private void textBoxCittàUpdate_Leave_1(object sender, EventArgs e)
        {
            if (textBoxCittàUpdate.Text == "")
            {
                textBoxCittàUpdate.Text = cittàUpdate;
                textBoxCittàUpdate.ForeColor = Color.DimGray;
            }
        }

        private void textBoxCittàUpdate_Click_1(object sender, EventArgs e)
        {
            textBoxCittàUpdate.Clear();
            textBoxCittàUpdate.ForeColor = Color.Black;
        }

        private void textBoxCAPUpdate_Leave_1(object sender, EventArgs e)
        {
            if (textBoxCAPUpdate.Text == "")
            {
                textBoxCAPUpdate.Text = capUpdate;
                textBoxCAPUpdate.ForeColor = Color.DimGray;
            }
        }

        private void buttonDeleteAll_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteAll.ForeColor = Color.White;
            buttonDeleteAll.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteAll_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteAll.ForeColor = Color.Black;
            buttonDeleteAll.BackColor = Color.Gainsboro;
        }

        private void comboBoxRegione_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxRegione.ForeColor = Color.Black;
            int indice = 0;
            string regione = comboBoxRegione.Text;
            foreach (string item in listaRegioni)
            {
                if (regione == item)
                {
                    comboBoxProvincia.Items.Clear();
                    string[] province = listaProvince.ElementAt(indice).Split(',');
                    foreach (string item2 in province)
                    {
                        comboBoxProvincia.Items.Add(item2);
                    }
                }
                indice++;
            }
        }

        private void FormLuogodiNascita_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("Luogo di nascita.txt");
            listaRegioni = new List<string>();
            listaCAP = new List<string>();
            listaProvince = new List<string>();
            string riga = "";
            while ((riga = sr.ReadLine()) != null)
            {
                string[] r = riga.Split(';');
                listaRegioni.Add(r[0]);
                listaCAP.Add(r[1]);
                listaProvince.Add(r[2]);
            }
            sr.Close();
            foreach (string item in listaProvince)
            {
                string[] p = item.Split(',');
                foreach (string item2 in p)
                {
                    comboBoxProvincia.Items.Add(item2);
                    comboBoxProvinciaUpdate.Items.Add(item2);
                }
            }
            foreach (string item in listaRegioni)
            {
                comboBoxRegione.Items.Add(item);
                comboBoxRegioneUpdate.Items.Add(item);
            }
            Adatta_Panel_Delete_From("SELEZIONA RIGA...");
            Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            Aggiorna();
        }
    }
}