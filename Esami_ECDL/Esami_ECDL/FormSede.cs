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
    public partial class FormSede : Form
    {
        public FormSede()
        {
            InitializeComponent();
        }

        //Variabili per uso generale
        List<string> listaRegioni, listaProvince;
        int numRigheSelezionate = 0, indiceFrom = 0, indiceUntil = 0;
        string NomeSedeUpdate = "", AulaUpdate = "", NomeCittàUpdate = "", ViaUpdate = "", RegioneUpdate = "", NumeroCivicoUpdate = "", ProvinciaUpdate = "";
        bool firstSearch = true, firstSearchDelete = true;

        //Oggetti
        Label lblNumeroRighe = new Label();
        Label lblDeleteFrom = new Label();
        Label lblDeleteUntil = new Label();

        private void textBoxNomeSedeSede_Click(object sender, EventArgs e)
        {
            textBoxNomeSedeSede.Clear();
            textBoxNomeSedeSede.ForeColor = Color.Black;
        }

        private void textBoxAulaSede_Click(object sender, EventArgs e)
        {
            textBoxAulaSede.Clear();
            textBoxAulaSede.ForeColor = Color.Black;
        }

        private void textBoxAulaSede_Leave(object sender, EventArgs e)
        {
            if (textBoxAulaSede.Text == "")
            {
                textBoxAulaSede.Text = "Aula...";
                textBoxAulaSede.ForeColor = Color.DimGray;
            }
        }

        private void textBoxNomeCittàSede_Click(object sender, EventArgs e)
        {
            textBoxNomeCittàSede.Clear();
            textBoxNomeCittàSede.ForeColor = Color.Black;
        }

        private void textBoxNomeCittàSede_Leave(object sender, EventArgs e)
        {
            if (textBoxNomeCittàSede.Text == "")
            {
                textBoxNomeCittàSede.Text = "Nome città...";
                textBoxNomeCittàSede.ForeColor = Color.DimGray;
            }
        }

        private void textBoxViaSede_Click(object sender, EventArgs e)
        {
            textBoxViaSede.Clear();
            textBoxViaSede.ForeColor = Color.Black;
        }

        private void textBoxViaSede_Leave(object sender, EventArgs e)
        {
            if (textBoxViaSede.Text == "")
            {
                textBoxViaSede.Text = "Via...";
                textBoxViaSede.ForeColor = Color.DimGray;
            }
        }

        private void comboBoxRegioneSede_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxRegioneSede.ForeColor = Color.Black;
            int indice = 0;
            string regione = comboBoxRegioneSede.Text;
            foreach (string item in listaRegioni)
            {
                if (regione == item)
                {
                    comboBoxProvinciaSede.Items.Clear();
                    string[] province = listaProvince.ElementAt(indice).Split(',');
                    foreach (string item2 in province)
                    {
                        comboBoxProvinciaSede.Items.Add(item2);
                    }
                }
                indice++;
            }
        }

        private void textBoxNumeroCivicoSede_Click(object sender, EventArgs e)
        {
            textBoxNumeroCivicoSede.Clear();
            textBoxNumeroCivicoSede.ForeColor = Color.Black;
        }

        private void textBoxNumeroCivicoSede_Leave(object sender, EventArgs e)
        {
            if (textBoxNumeroCivicoSede.Text == "")
            {
                textBoxNumeroCivicoSede.Text = "Numero civico...";
                textBoxNumeroCivicoSede.ForeColor = Color.DimGray;
            }
        }

        private void comboBoxProvinciaSede_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxProvinciaSede.ForeColor = Color.Black;
            int indice = 0;
            string provincia = comboBoxProvinciaSede.Text;
            foreach (string item in listaProvince)
            {
                if (item.Contains(provincia))
                {
                    comboBoxRegioneSede.Items.Clear();
                    comboBoxRegioneSede.Items.Add(listaRegioni.ElementAt(indice));
                }
                indice++;
            }
        }

        private void buttonClearSede_MouseEnter(object sender, EventArgs e)
        {
            buttonClearSede.ForeColor = Color.White;
            buttonClearSede.BackColor = Color.SteelBlue;
        }

        private void buttonClearSede_MouseLeave(object sender, EventArgs e)
        {
            buttonClearSede.ForeColor = Color.Black;
            buttonClearSede.BackColor = Color.Gainsboro;
        }

        private void buttonInserisciSede_MouseEnter(object sender, EventArgs e)
        {
            buttonInserisciSede.ForeColor = Color.White;
            buttonInserisciSede.BackColor = Color.SteelBlue;
        }

        private void buttonInserisciSede_MouseLeave(object sender, EventArgs e)
        {
            buttonInserisciSede.ForeColor = Color.Black;
            buttonInserisciSede.BackColor = Color.Gainsboro;
        }

        private void buttonClearSede_Click(object sender, EventArgs e)
        {
            textBoxNomeSedeSede.Text = "Nome sede...";
            textBoxAulaSede.Text = "Aula...";
            textBoxNomeCittàSede.Text = "Nome città...";
            textBoxViaSede.Text = "Via...";
            comboBoxRegioneSede.Text = "Regione...";
            textBoxNumeroCivicoSede.Text = "Numero civico...";
            comboBoxProvinciaSede.Text = "Provincia...";
            textBoxNomeSedeSede.ForeColor = Color.DimGray;
            textBoxAulaSede.ForeColor = Color.DimGray;
            textBoxNomeCittàSede.ForeColor = Color.DimGray;
            textBoxViaSede.ForeColor = Color.DimGray;
            comboBoxRegioneSede.ForeColor = Color.DimGray;
            textBoxNumeroCivicoSede.ForeColor = Color.DimGray;
            comboBoxProvinciaSede.ForeColor = Color.DimGray;
            comboBoxProvinciaSede.Items.Clear();
            comboBoxRegioneSede.Items.Clear();
            foreach (string item in listaProvince)
            {
                string[] p = item.Split(',');
                foreach (string item2 in p)
                {
                    comboBoxProvinciaSede.Items.Add(item2);
                }
            }
            foreach (string item in listaRegioni)
            {
                comboBoxRegioneSede.Items.Add(item);
            }
        }

        private void buttonInserisciSede_Click(object sender, EventArgs e)
        {
            if (textBoxNomeSedeSede.Text != "Nome sede..." && textBoxNomeSedeSede.Text != "" && textBoxAulaSede.Text != "Aula..." && textBoxAulaSede.Text != "" && comboBoxProvinciaSede.Text != "Provincia..." && comboBoxProvinciaSede.Text != "" && comboBoxRegioneSede.Text != "Regione..." && comboBoxRegioneSede.Text != "" && textBoxNomeCittàSede.Text != "Nome città..." && textBoxNomeCittàSede.Text != "" && textBoxViaSede.Text != "Via..." && textBoxViaSede.Text != "" && textBoxNumeroCivicoSede.Text != "Numero civico..." && textBoxNumeroCivicoSede.Text != "")
            {
                bool verProvincia = false, verRegione = false;
                string nomesede = textBoxNomeSedeSede.Text;
                string aula = textBoxAulaSede.Text;
                string nomecittà = textBoxNomeCittàSede.Text;
                string via = textBoxViaSede.Text;
                string provincia = comboBoxProvinciaSede.Text;
                string numcivico = textBoxNumeroCivicoSede.Text;
                string regione = comboBoxRegioneSede.Text;
                foreach (string item in listaRegioni)
                {
                    if (regione == item)
                    {
                        verRegione = true;
                    }
                }
                foreach (string item in listaProvince)
                {
                    if (item.Contains(provincia))
                    {
                        verProvincia = true;
                    }
                }
                if (verProvincia == true && verRegione == true)
                {
                    try
                    {
                        MySqlCommand InsertQuery;
                        string comando = "INSERT INTO ecdl.sede(Nome_Sede,Aula,Nome_Città,Via,Regione,Numero_Civico,Provincia) VALUES ('" + nomesede + "','" + aula + "','" + nomecittà + "','" + via + "','" + regione + "'," + numcivico + ",'" + provincia + "')";
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

        private void textBoxCercaValoreDeleteSede_Click(object sender, EventArgs e)
        {
            if (firstSearchDelete != false)
            {
                textBoxCercaValoreDeleteSede.Clear();
                textBoxCercaValoreDeleteSede.ForeColor = Color.Black;
                firstSearchDelete = false;
            }
        }

        private void textBoxCercaValoreDeleteSede_TextChanged(object sender, EventArgs e)
        {
            MySqlCommand SearchNomeSedeQuery;
            SearchNomeSedeQuery = new MySqlCommand("SELECT * FROM ecdl.sede WHERE Nome_Sede LIKE '" + textBoxCercaValoreDeleteSede.Text + "%'", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchNomeSedeQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewSedeDelete.DataSource = ds.Tables[0];
        }

        private void radioButtonMultiSede_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMultiSede.Checked == false)
            {
                Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            }
            else
            {
                Adatta_Panel_Delete_Until("SELEZIONA RIGA...");
            }
        }

        private void buttonDeleteSede_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteSede.ForeColor = Color.White;
            buttonDeleteSede.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteSede_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteSede.ForeColor = Color.Black;
            buttonDeleteSede.BackColor = Color.Gainsboro;
        }

        private void buttonDeleteAllSede_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteAllSede.ForeColor = Color.White;
            buttonDeleteAllSede.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteAllSede_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteAllSede.ForeColor = Color.Black;
            buttonDeleteAllSede.BackColor = Color.Gainsboro;
        }

        private void dataGridViewSedeDelete_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonSingleSede.Checked == true)
            {
                if (dataGridViewSedeDelete.CurrentRow != null && dataGridViewSedeDelete.CurrentRow.Index < dataGridViewSedeDelete.Rows.Count - 1)
                {
                    string nomesede = dataGridViewSedeDelete.CurrentRow.Cells[0].Value.ToString();
                    Adatta_Panel_Delete_From(nomesede);
                }
            }
            else
            {
                if (numRigheSelezionate == 2)
                {
                    if (dataGridViewSedeDelete.CurrentRow != null && dataGridViewSedeDelete.CurrentRow.Index < dataGridViewSedeDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewSedeDelete.CurrentRow.Index;
                        string nomesede = dataGridViewSedeDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(nomesede);
                        numRigheSelezionate = 1;
                    }
                }
                else if (numRigheSelezionate == 1)
                {
                    if (dataGridViewSedeDelete.CurrentRow != null && dataGridViewSedeDelete.CurrentRow.Index < dataGridViewSedeDelete.Rows.Count - 1)
                    {
                        indiceUntil = dataGridViewSedeDelete.CurrentRow.Index;
                        string nomesede = dataGridViewSedeDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_Until(nomesede);
                        numRigheSelezionate++;
                    }
                }
                else if (numRigheSelezionate == 0)
                {
                    if (dataGridViewSedeDelete.CurrentRow != null && dataGridViewSedeDelete.CurrentRow.Index < dataGridViewSedeDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewSedeDelete.CurrentRow.Index;
                        string nomesede = dataGridViewSedeDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(nomesede);
                        numRigheSelezionate++;
                    }
                }
            }
        }

        private void buttonDeleteSede_Click(object sender, EventArgs e)
        {
            if (radioButtonSingleSede.Checked == true)
            {
                try
                {
                    string[] valPanel = lblDeleteFrom.Text.Split(':');
                    string nomesede = valPanel[1].Trim(' ');
                    MySqlCommand DeleteQuery;
                    string comando = "DELETE FROM ecdl.sede WHERE Nome_Sede = " + nomesede;
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
                    List<string> listaNomeSedeDelete = new List<string>();
                    for (int i = indiceFrom; i <= indiceUntil; i++)
                    {
                        listaNomeSedeDelete.Add(dataGridViewSedeDelete.Rows[i].Cells[0].Value.ToString());
                    }
                    try
                    {
                        foreach (string item in listaNomeSedeDelete)
                        {
                            MySqlCommand DeleteQuery;
                            string comando = "DELETE FROM ecdl.sede WHERE Nome_Sede = " + item;
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

        private void buttonDeleteAllSede_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand DeleteAllQuery;
                string comando = "DELETE FROM ecdl.sede";
                DeleteAllQuery = new MySqlCommand(comando, DatabaseECDL.connessioneEcdl);
                DeleteAllQuery.ExecuteNonQuery();
                Aggiorna();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void textBoxCercaValoreSede_Click(object sender, EventArgs e)
        {
            if (firstSearch != false)
            {
                textBoxCercaValoreSede.Clear();
                textBoxCercaValoreSede.ForeColor = Color.Black;
                firstSearch = false;
            }
        }

        private void textBoxCercaValoreSede_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonSearchNomeSedeSede.Checked == true)
            {
                MySqlCommand SearchCAPQuery;
                SearchCAPQuery = new MySqlCommand("SELECT * FROM ecdl.sede WHERE Nome_Sede LIKE '" + textBoxCercaValoreSede.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchCAPQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewSedeUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchAulaSede.Checked == true)
            {
                MySqlCommand SearchCittàQuery;
                SearchCittàQuery = new MySqlCommand("SELECT * FROM ecdl.sede WHERE Aula LIKE '" + textBoxCercaValoreSede.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchCittàQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewSedeUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchNomeCittàSede.Checked == true)
            {
                MySqlCommand SearchProvinciaQuery;
                SearchProvinciaQuery = new MySqlCommand("SELECT * FROM ecdl.sede WHERE Nome_Città LIKE '" + textBoxCercaValoreSede.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchProvinciaQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewSedeUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchViaSede.Checked == true)
            {
                MySqlCommand SearchRegioneQuery;
                SearchRegioneQuery = new MySqlCommand("SELECT * FROM ecdl.sede WHERE Via LIKE '" + textBoxCercaValoreSede.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchRegioneQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewSedeUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchRegioneSede.Checked == true)
            {
                MySqlCommand SearchCAPQuery;
                SearchCAPQuery = new MySqlCommand("SELECT * FROM ecdl.sede WHERE Regione LIKE '" + textBoxCercaValoreSede.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchCAPQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewSedeUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchNumeroCivicoSede.Checked == true)
            {
                MySqlCommand SearchCittàQuery;
                SearchCittàQuery = new MySqlCommand("SELECT * FROM ecdl.sede WHERE Numero_Civico LIKE '" + textBoxCercaValoreSede.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchCittàQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewSedeUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchProvinciaSede.Checked == true)
            {
                MySqlCommand SearchProvinciaQuery;
                SearchProvinciaQuery = new MySqlCommand("SELECT * FROM ecdl.sede WHERE Provincia LIKE '" + textBoxCercaValoreSede.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchProvinciaQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewSedeUpdate.DataSource = ds.Tables[0];
            }
        }

        private void radioButtonSearchNomeSedeSede_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreSede_TextChanged(sender, e);
        }

        private void radioButtonSearchAulaSede_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreSede_TextChanged(sender, e);
        }

        private void radioButtonSearchNomeCittàSede_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreSede_TextChanged(sender, e);
        }

        private void radioButtonSearchViaSede_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreSede_TextChanged(sender, e);
        }

        private void radioButtonSearchRegioneSede_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreSede_TextChanged(sender, e);
        }

        private void radioButtonSearchNumeroCivicoSede_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreSede_TextChanged(sender, e);
        }

        private void radioButtonSearchProvinciaSede_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreSede_TextChanged(sender, e);
        }

        private void textBoxNomeSedeUpdateSede_Click(object sender, EventArgs e)
        {
            textBoxNomeSedeUpdateSede.Clear();
            textBoxNomeSedeUpdateSede.ForeColor = Color.Black;
        }

        private void textBoxNomeSedeUpdateSede_Leave(object sender, EventArgs e)
        {
            if (textBoxNomeSedeUpdateSede.Text == "")
            {
                textBoxNomeSedeUpdateSede.Text = NomeSedeUpdate;
                textBoxNomeSedeUpdateSede.ForeColor = Color.DimGray;
            }
        }

        private void textBoxAulaUpdateSede_Click(object sender, EventArgs e)
        {
            textBoxAulaUpdateSede.Clear();
            textBoxAulaUpdateSede.ForeColor = Color.Black;
        }

        private void textBoxAulaUpdateSede_Leave(object sender, EventArgs e)
        {
            if (textBoxAulaUpdateSede.Text == "")
            {
                textBoxAulaUpdateSede.Text = AulaUpdate;
                textBoxAulaUpdateSede.ForeColor = Color.DimGray;
            }
        }

        private void textBoxNomeCittàUpdateSede_Click(object sender, EventArgs e)
        {
            textBoxNomeCittàUpdateSede.Clear();
            textBoxNomeCittàUpdateSede.ForeColor = Color.Black;
        }

        private void textBoxNomeCittàUpdateSede_Leave(object sender, EventArgs e)
        {
            if (textBoxNomeCittàUpdateSede.Text == "")
            {
                textBoxNomeCittàUpdateSede.Text = NomeCittàUpdate;
                textBoxNomeCittàUpdateSede.ForeColor = Color.DimGray;
            }
        }

        private void textBoxViaUpdateSede_Click(object sender, EventArgs e)
        {
            textBoxViaUpdateSede.Clear();
            textBoxViaUpdateSede.ForeColor = Color.Black;
        }

        private void textBoxViaUpdateSede_Leave(object sender, EventArgs e)
        {
            if (textBoxViaUpdateSede.Text == "")
            {
                textBoxViaUpdateSede.Text = ViaUpdate;
                textBoxViaUpdateSede.ForeColor = Color.DimGray;
            }
        }

        private void textBoxNumeroCivicoUpdateSede_Click(object sender, EventArgs e)
        {
            textBoxNumeroCivicoUpdateSede.Clear();
            textBoxNumeroCivicoUpdateSede.ForeColor = Color.Black;
        }

        private void textBoxNumeroCivicoUpdateSede_Leave(object sender, EventArgs e)
        {
            if (textBoxNumeroCivicoUpdateSede.Text == "")
            {
                textBoxNumeroCivicoUpdateSede.Text = NumeroCivicoUpdate;
                textBoxNumeroCivicoUpdateSede.ForeColor = Color.DimGray;
            }
        }

        private void comboBoxRegioneUpdateSede_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxRegioneUpdateSede.ForeColor = Color.Black;
            int indice = 0;
            string regione = comboBoxRegioneUpdateSede.Text;
            foreach (string item in listaRegioni)
            {
                if (regione == item)
                {
                    comboBoxProvinciaUpdateSede.Items.Clear();
                    string[] province = listaProvince.ElementAt(indice).Split(',');
                    foreach (string item2 in province)
                    {
                        comboBoxProvinciaUpdateSede.Items.Add(item2);
                    }
                }
                indice++;
            }
        }

        private void comboBoxProvinciaUpdateSede_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxProvinciaUpdateSede.ForeColor = Color.Black;
            int indice = 0;
            string provincia = comboBoxProvinciaUpdateSede.Text;
            foreach (string item in listaProvince)
            {
                if (item.Contains(provincia))
                {
                    comboBoxRegioneUpdateSede.Items.Clear();
                    comboBoxRegioneUpdateSede.Items.Add(listaRegioni.ElementAt(indice));
                }
                indice++;
            }
        }

        private void buttonClearUpdateSede_MouseEnter(object sender, EventArgs e)
        {
            buttonClearUpdateSede.ForeColor = Color.White;
            buttonClearUpdateSede.BackColor = Color.SteelBlue;
        }

        private void buttonClearUpdateSede_MouseLeave(object sender, EventArgs e)
        {
            buttonClearUpdateSede.ForeColor = Color.Black;
            buttonClearUpdateSede.BackColor = Color.Gainsboro;
        }

        private void buttonUpdateSede_MouseEnter(object sender, EventArgs e)
        {
            buttonUpdateSede.ForeColor = Color.White;
            buttonUpdateSede.BackColor = Color.SteelBlue;
        }

        private void buttonUpdateSede_MouseLeave(object sender, EventArgs e)
        {
            buttonUpdateSede.ForeColor = Color.Black;
            buttonUpdateSede.BackColor = Color.Gainsboro;
        }

        private void dataGridViewSedeUpdate_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewSedeUpdate.CurrentRow != null && dataGridViewSedeUpdate.CurrentRow.Index < dataGridViewSedeUpdate.Rows.Count - 1)
            {
                textBoxNomeSedeUpdateSede.Text = dataGridViewSedeUpdate.CurrentRow.Cells[0].Value.ToString();
                textBoxAulaUpdateSede.Text = dataGridViewSedeUpdate.CurrentRow.Cells[1].Value.ToString();
                textBoxNomeCittàUpdateSede.Text = dataGridViewSedeUpdate.CurrentRow.Cells[2].Value.ToString();
                textBoxViaUpdateSede.Text = dataGridViewSedeUpdate.CurrentRow.Cells[3].Value.ToString();
                comboBoxRegioneUpdateSede.Text = dataGridViewSedeUpdate.CurrentRow.Cells[4].Value.ToString();
                textBoxNumeroCivicoUpdateSede.Text = dataGridViewSedeUpdate.CurrentRow.Cells[5].Value.ToString();
                comboBoxProvinciaUpdateSede.Text = dataGridViewSedeUpdate.CurrentRow.Cells[6].Value.ToString();
                textBoxNomeSedeUpdateSede.ForeColor = Color.DimGray;
                textBoxAulaUpdateSede.ForeColor = Color.DimGray;
                textBoxNomeCittàUpdateSede.ForeColor = Color.DimGray;
                textBoxViaUpdateSede.ForeColor = Color.DimGray;
                comboBoxRegioneUpdateSede.ForeColor = Color.DimGray;
                textBoxNumeroCivicoUpdateSede.ForeColor = Color.DimGray;
                comboBoxProvinciaUpdateSede.ForeColor = Color.DimGray;
                NomeSedeUpdate = textBoxNomeSedeUpdateSede.Text;
                AulaUpdate = textBoxAulaUpdateSede.Text;
                NomeCittàUpdate = textBoxNomeCittàUpdateSede.Text;
                ViaUpdate = textBoxViaUpdateSede.Text;
                RegioneUpdate = comboBoxRegioneUpdateSede.Text;
                NumeroCivicoUpdate = textBoxNumeroCivicoUpdateSede.Text;
                ProvinciaUpdate = comboBoxProvinciaUpdateSede.Text;
                comboBoxProvinciaUpdateSede.Items.Clear();
                comboBoxRegioneUpdateSede.Items.Clear();
                foreach (string item in listaProvince)
                {
                    string[] p = item.Split(',');
                    foreach (string item2 in p)
                    {
                        comboBoxProvinciaUpdateSede.Items.Add(item2);
                    }
                }
                foreach (string item in listaRegioni)
                {
                    comboBoxRegioneUpdateSede.Items.Add(item);
                }
            }
        }

        private void buttonClearUpdateSede_Click(object sender, EventArgs e)
        {
            textBoxNomeSedeUpdateSede.Text = NomeSedeUpdate;
            textBoxAulaUpdateSede.Text = AulaUpdate;
            textBoxNomeCittàUpdateSede.Text = NomeCittàUpdate;
            textBoxViaUpdateSede.Text = ViaUpdate;
            comboBoxRegioneUpdateSede.Text = RegioneUpdate;
            textBoxNumeroCivicoUpdateSede.Text = NumeroCivicoUpdate;
            comboBoxProvinciaUpdateSede.Text = ProvinciaUpdate;
            textBoxNomeSedeUpdateSede.ForeColor = Color.DimGray;
            textBoxAulaUpdateSede.ForeColor = Color.DimGray;
            textBoxNomeCittàUpdateSede.ForeColor = Color.DimGray;
            textBoxViaUpdateSede.ForeColor = Color.DimGray;
            comboBoxRegioneUpdateSede.ForeColor = Color.DimGray;
            textBoxNumeroCivicoUpdateSede.ForeColor = Color.DimGray;
            comboBoxProvinciaUpdateSede.ForeColor = Color.DimGray;
            comboBoxProvinciaUpdateSede.Items.Clear();
            comboBoxRegioneUpdateSede.Items.Clear();
            foreach (string item in listaProvince)
            {
                string[] p = item.Split(',');
                foreach (string item2 in p)
                {
                    comboBoxProvinciaUpdateSede.Items.Add(item2);
                }
            }
            foreach (string item in listaRegioni)
            {
                comboBoxRegioneUpdateSede.Items.Add(item);
            }
        }

        private void buttonUpdateSede_Click(object sender, EventArgs e)
        {
            if (textBoxNomeSedeUpdateSede.Text != "" && textBoxAulaUpdateSede.Text != "" && textBoxNomeCittàUpdateSede.Text != "" && textBoxViaUpdateSede.Text != "" && comboBoxRegioneUpdateSede.Text != "" && textBoxNumeroCivicoUpdateSede.Text != "" && comboBoxProvinciaUpdateSede.Text != "")
            {
                bool verProvincia = false, verRegione = false;
                string nomesede = textBoxNomeSedeSede.Text;
                string aula = textBoxAulaSede.Text;
                string nomecittà = textBoxNomeCittàSede.Text;
                string via = textBoxViaSede.Text;
                string provincia = comboBoxProvinciaSede.Text;
                string numcivico = textBoxNumeroCivicoSede.Text;
                string regione = comboBoxRegioneSede.Text;
                foreach (string item in listaRegioni)
                {
                    if (regione == item)
                    {
                        verRegione = true;
                    }
                }
                foreach (string item in listaProvince)
                {
                    if (item.Contains(provincia))
                    {
                        verProvincia = true;
                    }
                }
                if (verProvincia == true && verRegione == true)
                {
                    try
                    {
                        MySqlCommand UpdateQuery;
                        string comando = "UPDATE ecdl.sede SET Nome_Sede = '" + nomesede + "', Aula = '" + aula + "', Nome_Città = '" + nomecittà + "', Via = '" + ViaUpdate + "', Regione = '" + regione + "', Numero_Civico = " + numcivico + ", Provincia = '" + provincia + "' WHERE Nome_Sede = " + NomeSedeUpdate;
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

        private void textBoxNomeSedeSede_Leave(object sender, EventArgs e)
        {
            if (textBoxNomeSedeSede.Text == "")
            {
                textBoxNomeSedeSede.Text = "Nome sede...";
                textBoxNomeSedeSede.ForeColor = Color.DimGray;
            }
        }

        private void label2Sede_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2Sede_MouseEnter(object sender, EventArgs e)
        {
            label2Sede.ForeColor = Color.Black;
        }

        private void label2Sede_MouseLeave(object sender, EventArgs e)
        {
            label2Sede.ForeColor = Color.DimGray;
        }

        private void FormSede_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader("Luogo di nascita.txt");
            listaRegioni = new List<string>();
            listaProvince = new List<string>();
            string riga = "";
            while ((riga = sr.ReadLine()) != null)
            {
                string[] r = riga.Split(';');
                listaRegioni.Add(r[0]);
                listaProvince.Add(r[2]);
            }
            sr.Close();
            foreach (string item in listaProvince)
            {
                string[] p = item.Split(',');
                foreach (string item2 in p)
                {
                    comboBoxProvinciaSede.Items.Add(item2);
                    comboBoxProvinciaUpdateSede.Items.Add(item2);
                }
            }
            foreach (string item in listaRegioni)
            {
                comboBoxRegioneSede.Items.Add(item);
                comboBoxRegioneUpdateSede.Items.Add(item);
            }
            Adatta_Panel_Delete_From("SELEZIONA RIGA...");
            Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            Aggiorna();
        }

        private void Aggiorna()
        {
            MySqlCommand SelectQuery;
            SelectQuery = new MySqlCommand("SELECT * FROM ecdl.sede", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SelectQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewSedeInsert.DataSource = ds.Tables[0];
            dataGridViewSedeDelete.DataSource = ds.Tables[0];
            dataGridViewSedeUpdate.DataSource = ds.Tables[0];

            Select_Count();
        }

        private void Select_Count()
        {
            MySqlCommand SelectCountQuery;
            SelectCountQuery = DatabaseECDL.connessioneEcdl.CreateCommand();
            SelectCountQuery.CommandText = "SELECT COUNT(*) FROM ecdl.sede";
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
            panelNumeroRigheSede.Controls.Add(lblNumeroRighe);
            panelNumeroRigheSede.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_From(string NomeSede)
        {
            lblDeleteFrom.AutoSize = false;
            lblDeleteFrom.Dock = DockStyle.Fill;
            lblDeleteFrom.Size = new Size(296, 25);
            lblDeleteFrom.Text = "NOME SEDE: " + NomeSede;
            lblDeleteFrom.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteFromSede.Controls.Add(lblDeleteFrom);
            panelDeleteFromSede.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_Until(string NomeSede)
        {
            lblDeleteUntil.AutoSize = false;
            lblDeleteUntil.Dock = DockStyle.Fill;
            lblDeleteUntil.Size = new Size(296, 25);
            if (radioButtonSingleSede.Checked == true)
                lblDeleteUntil.Text = NomeSede;
            else
                lblDeleteUntil.Text = "NOME SEDE: " + NomeSede;
            lblDeleteUntil.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteUntilSede.Controls.Add(lblDeleteUntil);
            panelDeleteUntilSede.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
