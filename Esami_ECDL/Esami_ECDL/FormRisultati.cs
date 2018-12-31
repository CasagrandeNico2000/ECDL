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

namespace Esami_ECDL
{
    public partial class FormRisultati : Form
    {
        public FormRisultati()
        {
            InitializeComponent();
        }

        //Variabili per uso generale
        int numRigheSelezionate = 0, indiceFrom = 0, indiceUntil = 0;
        string IDEsameUpdate = "", CodiceSkillCardUpdate = "", EsitoUpdate = "", PercentualeEsitoUpdate = "";
        bool firstSearchUpdate = true, firstSearchDelete = true;

        //Oggetti
        Label lblNumeroRighe = new Label();
        Label lblDeleteFrom = new Label();
        Label lblDeleteUntil = new Label();

        private void FormRisultati_Load(object sender, EventArgs e)
        {
            Adatta_Panel_Delete_From("SELEZIONA RIGA...");
            Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            Aggiorna();
        }

        private void buttonClearRisultati_Click(object sender, EventArgs e)
        {
            textBoxIDEsameRisultati.Text = "ID Esame...";
            textBoxCodiceSkillCardRisultati.Text = "Codice skill-card...";
            textBoxEsitoRisultati.Text = "Esito...";
            textBoxPercentualeEsitoRisultati.Text = "Percentuale esito...";
            textBoxIDEsameRisultati.ForeColor = Color.DimGray;
            textBoxCodiceSkillCardRisultati.ForeColor = Color.DimGray;
            textBoxEsitoRisultati.ForeColor = Color.DimGray;
            textBoxPercentualeEsitoRisultati.ForeColor = Color.DimGray;
        }

        private void buttonInserisciRisultati_Click(object sender, EventArgs e)
        {
            if (textBoxIDEsameRisultati.Text != "ID Esame..." && textBoxIDEsameRisultati.Text != "" && textBoxCodiceSkillCardRisultati.Text != "Codice skill-card..." && textBoxCodiceSkillCardRisultati.Text != "" && textBoxEsitoRisultati.Text != "Esito..." && textBoxEsitoRisultati.Text != "" && textBoxPercentualeEsitoRisultati.Text != "Percentuale esito..." && textBoxPercentualeEsitoRisultati.Text != "")
            {
                try
                {
                    MySqlCommand InsertQuery;
                    string comando = "INSERT INTO ecdl.risultati(Esami_IDEsame,Esaminandi_Codice_Skill_Card,Esito,Percentuale_Esito) VALUES (" + textBoxIDEsameRisultati.Text + "," + textBoxCodiceSkillCardRisultati.Text + ",'" + textBoxEsitoRisultati.Text + "'," + textBoxPercentualeEsitoRisultati.Text + ")";
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
            else
            {
                MessageBox.Show("Per eseguire il comando è necessario l'inserimento di tutti i dati!");
            }
        }

        private void Aggiorna()
        {
            MySqlCommand SelectQuery;
            SelectQuery = new MySqlCommand("SELECT * FROM ecdl.risultati", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SelectQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewRisultatiInsert.DataSource = ds.Tables[0];
            dataGridViewRisultatiDelete.DataSource = ds.Tables[0];
            dataGridViewRisultatiUpdate.DataSource = ds.Tables[0];

            Select_Count();
        }

        private void Select_Count()
        {
            MySqlCommand SelectCountQuery;
            SelectCountQuery = DatabaseECDL.connessioneEcdl.CreateCommand();
            SelectCountQuery.CommandText = "SELECT COUNT(*) FROM ecdl.risultati";
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
            panelNumeroRigheRisultati.Controls.Add(lblNumeroRighe);
            panelNumeroRigheRisultati.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_From(string IDEsame)
        {
            lblDeleteFrom.AutoSize = false;
            lblDeleteFrom.Dock = DockStyle.Fill;
            lblDeleteFrom.Size = new Size(296, 25);
            lblDeleteFrom.Text = "ID ESAME: " + IDEsame;
            lblDeleteFrom.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteFromRisultati.Controls.Add(lblDeleteFrom);
            panelDeleteFromRisultati.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_Until(string IDEsame)
        {
            lblDeleteUntil.AutoSize = false;
            lblDeleteUntil.Dock = DockStyle.Fill;
            lblDeleteUntil.Size = new Size(296, 25);
            if (radioButtonSingleRisultati.Checked == true)
                lblDeleteUntil.Text = IDEsame;
            else
                lblDeleteUntil.Text = "ID ESAME: " + IDEsame;
            lblDeleteUntil.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteUntilRisultati.Controls.Add(lblDeleteUntil);
            panelDeleteUntilRisultati.BorderStyle = BorderStyle.FixedSingle;
        }

        private void label2Risultati_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2Risultati_MouseEnter(object sender, EventArgs e)
        {
            label2Risultati.ForeColor = Color.Black;
        }

        private void label2Risultati_MouseLeave(object sender, EventArgs e)
        {
            label2Risultati.ForeColor = Color.DimGray;
        }

        private void textBoxIDEsameRisultati_Click(object sender, EventArgs e)
        {
            textBoxIDEsameRisultati.Clear();
            textBoxIDEsameRisultati.ForeColor = Color.Black;
        }

        private void textBoxIDEsameRisultati_Leave(object sender, EventArgs e)
        {
            if (textBoxIDEsameRisultati.Text == "")
            {
                textBoxIDEsameRisultati.Text = "ID Esame...";
                textBoxIDEsameRisultati.ForeColor = Color.DimGray;
            }
        }

        private void textBoxCodiceSkillCardRisultati_Click(object sender, EventArgs e)
        {
            textBoxCodiceSkillCardRisultati.Clear();
            textBoxCodiceSkillCardRisultati.ForeColor = Color.Black;
        }

        private void textBoxCodiceSkillCardRisultati_Leave(object sender, EventArgs e)
        {
            if (textBoxCodiceSkillCardRisultati.Text == "")
            {
                textBoxCodiceSkillCardRisultati.Text = "Codice skill-card...";
                textBoxCodiceSkillCardRisultati.ForeColor = Color.DimGray;
            }
        }

        private void textBoxEsitoRisultati_Click(object sender, EventArgs e)
        {
            textBoxEsitoRisultati.Clear();
            textBoxEsitoRisultati.ForeColor = Color.Black;
        }

        private void textBoxEsitoRisultati_Leave(object sender, EventArgs e)
        {
            if (textBoxEsitoRisultati.Text == "")
            {
                textBoxEsitoRisultati.Text = "Esito...";
                textBoxEsitoRisultati.ForeColor = Color.DimGray;
            }
        }

        private void textBoxCercaValoreDeleteRisultati_Click(object sender, EventArgs e)
        {
            if (firstSearchDelete != false)
            {
                textBoxCercaValoreDeleteRisultati.Clear();
                textBoxCercaValoreDeleteRisultati.ForeColor = Color.Black;
                firstSearchDelete = false;
            }
        }

        private void textBoxCercaValoreDeleteRisultati_TextChanged(object sender, EventArgs e)
        {
            MySqlCommand SearchIDEsameQuery;
            SearchIDEsameQuery = new MySqlCommand("SELECT * FROM ecdl.risultati WHERE Esami_IDEsame LIKE '" + textBoxCercaValoreDeleteRisultati.Text + "%'", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchIDEsameQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewRisultatiDelete.DataSource = ds.Tables[0];
        }

        private void radioButtonMultiRisultati_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMultiRisultati.Checked == false)
            {
                Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            }
            else
            {
                Adatta_Panel_Delete_Until("SELEZIONA RIGA...");
            }
        }

        private void dataGridViewRisultatiDelete_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonSingleRisultati.Checked == true)
            {
                if (dataGridViewRisultatiDelete.CurrentRow != null && dataGridViewRisultatiDelete.CurrentRow.Index < dataGridViewRisultatiDelete.Rows.Count - 1)
                {
                    string IDEsame = dataGridViewRisultatiDelete.CurrentRow.Cells[0].Value.ToString();
                    Adatta_Panel_Delete_From(IDEsame);
                }
            }
            else
            {
                if (numRigheSelezionate == 2)
                {
                    if (dataGridViewRisultatiDelete.CurrentRow != null && dataGridViewRisultatiDelete.CurrentRow.Index < dataGridViewRisultatiDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewRisultatiDelete.CurrentRow.Index;
                        string IDEsame = dataGridViewRisultatiDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(IDEsame);
                        numRigheSelezionate = 1;
                    }
                }
                else if (numRigheSelezionate == 1)
                {
                    if (dataGridViewRisultatiDelete.CurrentRow != null && dataGridViewRisultatiDelete.CurrentRow.Index < dataGridViewRisultatiDelete.Rows.Count - 1)
                    {
                        indiceUntil = dataGridViewRisultatiDelete.CurrentRow.Index;
                        string IDEsame = dataGridViewRisultatiDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_Until(IDEsame);
                        numRigheSelezionate++;
                    }
                }
                else if (numRigheSelezionate == 0)
                {
                    if (dataGridViewRisultatiDelete.CurrentRow != null && dataGridViewRisultatiDelete.CurrentRow.Index < dataGridViewRisultatiDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewRisultatiDelete.CurrentRow.Index;
                        string IDEsame = dataGridViewRisultatiDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(IDEsame);
                        numRigheSelezionate++;
                    }
                }
            }
        }

        private void buttonDeleteRisultati_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteRisultati.ForeColor = Color.White;
            buttonDeleteRisultati.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteRisultati_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteRisultati.ForeColor = Color.Black;
            buttonDeleteRisultati.BackColor = Color.Gainsboro;
        }

        private void buttonDeleteAllRisultati_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteAllRisultati.ForeColor = Color.White;
            buttonDeleteAllRisultati.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteAllRisultati_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteAllRisultati.ForeColor = Color.Black;
            buttonDeleteAllRisultati.BackColor = Color.Gainsboro;
        }

        private void buttonDeleteRisultati_Click(object sender, EventArgs e)
        {
            if (radioButtonSingleRisultati.Checked == true)
            {
                try
                {
                    string[] valPanel = lblDeleteFrom.Text.Split(':');
                    string IDEsame = valPanel[1].Trim(' ');
                    MySqlCommand DeleteQuery;
                    string comando = "DELETE FROM ecdl.risultati WHERE Esami_IDEsame = " + IDEsame;
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
                    List<string> listaIDEsameDelete = new List<string>();
                    for (int i = indiceFrom; i <= indiceUntil; i++)
                    {
                        listaIDEsameDelete.Add(dataGridViewRisultatiDelete.Rows[i].Cells[0].Value.ToString());
                    }
                    try
                    {
                        foreach (string item in listaIDEsameDelete)
                        {
                            MySqlCommand DeleteQuery;
                            string comando = "DELETE FROM ecdl.risultati WHERE Esami_IDEsame = " + item;
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

        private void buttonDeleteAllRisultati_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand DeleteAllQuery;
                string comando = "DELETE FROM ecdl.risultati";
                DeleteAllQuery = new MySqlCommand(comando, DatabaseECDL.connessioneEcdl);
                DeleteAllQuery.ExecuteNonQuery();
                Aggiorna();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void textBoxCercaValoreRisultati_Click(object sender, EventArgs e)
        {
            if (firstSearchUpdate != false)
            {
                textBoxCercaValoreRisultati.Clear();
                textBoxCercaValoreRisultati.ForeColor = Color.Black;
                firstSearchUpdate = false;
            }
        }

        private void textBoxCercaValoreRisultati_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonSearchIDEsame.Checked == true)
            {
                MySqlCommand SearchIDEsameQuery;
                SearchIDEsameQuery = new MySqlCommand("SELECT * FROM ecdl.risultati WHERE Esami_IDEsame LIKE '" + textBoxCercaValoreRisultati.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchIDEsameQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewRisultatiUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchCodiceSkillCard.Checked == true)
            {
                MySqlCommand SearchCodiceSkillCardQuery;
                SearchCodiceSkillCardQuery = new MySqlCommand("SELECT * FROM ecdl.risultati WHERE Esaminandi_Codice_Skill_Card LIKE '" + textBoxCercaValoreRisultati.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchCodiceSkillCardQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewRisultatiUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchEsito.Checked == true)
            {
                MySqlCommand SearchEsitoQuery;
                SearchEsitoQuery = new MySqlCommand("SELECT * FROM ecdl.risultati WHERE Esito LIKE '" + textBoxCercaValoreRisultati.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchEsitoQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewRisultatiUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchPercentualeEsito.Checked == true)
            {
                MySqlCommand SearchPercentualeEsitoQuery;
                SearchPercentualeEsitoQuery = new MySqlCommand("SELECT * FROM ecdl.risultati WHERE Percentuale_Esito LIKE '" + textBoxCercaValoreRisultati.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchPercentualeEsitoQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewRisultatiUpdate.DataSource = ds.Tables[0];
            }
        }

        private void radioButtonSearchIDEsame_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreRisultati_TextChanged(sender, e);
        }

        private void radioButtonSearchCodiceSkillCard_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreRisultati_TextChanged(sender, e);
        }

        private void radioButtonSearchEsito_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreRisultati_TextChanged(sender, e);
        }

        private void radioButtonSearchPercentualeEsito_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreRisultati_TextChanged(sender, e);
        }

        private void textBoxIDEsameUpdateRisultati_Click(object sender, EventArgs e)
        {
            textBoxIDEsameUpdateRisultati.Clear();
            textBoxIDEsameUpdateRisultati.ForeColor = Color.Black;
        }

        private void textBoxIDEsameUpdateRisultati_Leave(object sender, EventArgs e)
        {
            if (textBoxIDEsameUpdateRisultati.Text == "")
            {
                textBoxIDEsameUpdateRisultati.Text = IDEsameUpdate;
                textBoxIDEsameUpdateRisultati.ForeColor = Color.DimGray;
            }
        }

        private void textBoxCodiceSkillCardUpdateRisultati_Click(object sender, EventArgs e)
        {
            textBoxCodiceSkillCardUpdateRisultati.Clear();
            textBoxCodiceSkillCardUpdateRisultati.ForeColor = Color.Black;
        }

        private void textBoxCodiceSkillCardUpdateRisultati_Leave(object sender, EventArgs e)
        {
            if (textBoxCodiceSkillCardUpdateRisultati.Text == "")
            {
                textBoxCodiceSkillCardUpdateRisultati.Text = CodiceSkillCardUpdate;
                textBoxCodiceSkillCardUpdateRisultati.ForeColor = Color.DimGray;
            }
        }

        private void textBoxEsitoUpdateRisultati_Click(object sender, EventArgs e)
        {
            textBoxEsitoUpdateRisultati.Clear();
            textBoxEsitoUpdateRisultati.ForeColor = Color.Black;
        }

        private void textBoxEsitoUpdateRisultati_Leave(object sender, EventArgs e)
        {
            if (textBoxEsitoUpdateRisultati.Text == "")
            {
                textBoxEsitoUpdateRisultati.Text = EsitoUpdate;
                textBoxEsitoUpdateRisultati.ForeColor = Color.DimGray;
            }
        }

        private void textBoxPercentualeEsitoUpdateRisultati_Click(object sender, EventArgs e)
        {
            textBoxPercentualeEsitoUpdateRisultati.Clear();
            textBoxPercentualeEsitoUpdateRisultati.ForeColor = Color.Black;
        }

        private void textBoxPercentualeEsitoUpdateRisultati_Leave(object sender, EventArgs e)
        {
            if (textBoxPercentualeEsitoUpdateRisultati.Text == "")
            {
                textBoxPercentualeEsitoUpdateRisultati.Text = PercentualeEsitoUpdate;
                textBoxPercentualeEsitoUpdateRisultati.ForeColor = Color.DimGray;
            }
        }

        private void buttonClearUpdateRisultati_MouseEnter(object sender, EventArgs e)
        {
            buttonClearUpdateRisultati.ForeColor = Color.White;
            buttonClearUpdateRisultati.BackColor = Color.SteelBlue;
        }

        private void buttonClearUpdateRisultati_MouseLeave(object sender, EventArgs e)
        {
            buttonClearUpdateRisultati.ForeColor = Color.Black;
            buttonClearUpdateRisultati.BackColor = Color.Gainsboro;
        }

        private void buttonUpdateRisultati_MouseEnter(object sender, EventArgs e)
        {
            buttonUpdateRisultati.ForeColor = Color.White;
            buttonUpdateRisultati.BackColor = Color.SteelBlue;
        }

        private void buttonUpdateRisultati_MouseLeave(object sender, EventArgs e)
        {
            buttonUpdateRisultati.ForeColor = Color.Black;
            buttonUpdateRisultati.BackColor = Color.Gainsboro;
        }

        private void dataGridViewRisultatiUpdate_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewRisultatiUpdate.CurrentRow != null && dataGridViewRisultatiUpdate.CurrentRow.Index < dataGridViewRisultatiUpdate.Rows.Count - 1)
            {
                textBoxIDEsameUpdateRisultati.Text = dataGridViewRisultatiUpdate.CurrentRow.Cells[0].Value.ToString();
                textBoxCodiceSkillCardUpdateRisultati.Text = dataGridViewRisultatiUpdate.CurrentRow.Cells[1].Value.ToString();
                textBoxEsitoUpdateRisultati.Text = dataGridViewRisultatiUpdate.CurrentRow.Cells[2].Value.ToString();
                textBoxPercentualeEsitoUpdateRisultati.Text = dataGridViewRisultatiUpdate.CurrentRow.Cells[3].Value.ToString();
                textBoxIDEsameUpdateRisultati.ForeColor = Color.DimGray;
                textBoxCodiceSkillCardUpdateRisultati.ForeColor = Color.DimGray;
                textBoxEsitoUpdateRisultati.ForeColor = Color.DimGray;
                textBoxPercentualeEsitoUpdateRisultati.ForeColor = Color.DimGray;
                IDEsameUpdate = textBoxIDEsameUpdateRisultati.Text;
                CodiceSkillCardUpdate = textBoxCodiceSkillCardUpdateRisultati.Text;
                EsitoUpdate = textBoxEsitoUpdateRisultati.Text;
                PercentualeEsitoUpdate = textBoxPercentualeEsitoUpdateRisultati.Text;
            }
        }

        private void buttonClearUpdateRisultati_Click(object sender, EventArgs e)
        {
            textBoxIDEsameUpdateRisultati.Text = IDEsameUpdate;
            textBoxCodiceSkillCardUpdateRisultati.Text = CodiceSkillCardUpdate;
            textBoxEsitoUpdateRisultati.Text = EsitoUpdate;
            textBoxPercentualeEsitoUpdateRisultati.Text = PercentualeEsitoUpdate;
            textBoxIDEsameUpdateRisultati.ForeColor = Color.DimGray;
            textBoxCodiceSkillCardUpdateRisultati.ForeColor = Color.DimGray;
            textBoxEsitoUpdateRisultati.ForeColor = Color.DimGray;
            textBoxPercentualeEsitoUpdateRisultati.ForeColor = Color.DimGray;
        }

        private void buttonUpdateRisultati_Click(object sender, EventArgs e)
        {
            if (textBoxIDEsameUpdateRisultati.Text != "" && textBoxCodiceSkillCardUpdateRisultati.Text != "" && textBoxEsitoUpdateRisultati.Text != "" && textBoxPercentualeEsitoUpdateRisultati.Text != "")
            {
                try
                {
                    MySqlCommand UpdateQuery;
                    string comando = "UPDATE ecdl.risultati SET Esami_IDEsame = " + textBoxIDEsameUpdateRisultati.Text + ", Esaminandi_Codice_Skill_Card = " + textBoxCodiceSkillCardUpdateRisultati.Text + ", Esito = '" + textBoxEsitoUpdateRisultati.Text + "', Percentuale_Esito = " + textBoxPercentualeEsitoUpdateRisultati.Text + " WHERE Esami_IDEsame = " + IDEsameUpdate;
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
            else
            {
                MessageBox.Show("Per eseguire il comando è necessario l'inserimento di tutti i dati!");
            }
        }

        private void textBoxPercentualeEsitoRisultati_Click(object sender, EventArgs e)
        {
            textBoxPercentualeEsitoRisultati.Clear();
            textBoxPercentualeEsitoRisultati.ForeColor = Color.Black;
        }

        private void textBoxPercentualeEsitoRisultati_Leave(object sender, EventArgs e)
        {
            if (textBoxPercentualeEsitoRisultati.Text == "")
            {
                textBoxPercentualeEsitoRisultati.Text = "Percentuale esito...";
                textBoxPercentualeEsitoRisultati.ForeColor = Color.DimGray;
            }
        }

        private void buttonClearRisultati_MouseEnter(object sender, EventArgs e)
        {
            buttonClearRisultati.ForeColor = Color.White;
            buttonClearRisultati.BackColor = Color.SteelBlue;
        }

        private void buttonClearRisultati_MouseLeave(object sender, EventArgs e)
        {
            buttonClearRisultati.ForeColor = Color.Black;
            buttonClearRisultati.BackColor = Color.Gainsboro;
        }

        private void buttonInserisciRisultati_MouseEnter(object sender, EventArgs e)
        {
            buttonInserisciRisultati.ForeColor = Color.White;
            buttonInserisciRisultati.BackColor = Color.SteelBlue;
        }

        private void buttonInserisciRisultati_MouseLeave(object sender, EventArgs e)
        {
            buttonInserisciRisultati.ForeColor = Color.Black;
            buttonInserisciRisultati.BackColor = Color.Gainsboro;
        }
    }
}
