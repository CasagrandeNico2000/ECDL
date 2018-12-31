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
    public partial class FormEsaminandi : Form
    {
        public FormEsaminandi()
        {
            InitializeComponent();
        }

        //Variabili per uso generale
        int numRigheSelezionate = 0, indiceFrom = 0, indiceUntil = 0;
        string CodiceSkillCardUpdate = "", NomeUpdate = "", SessoUpdate = "", DatadiNascitaUpdate = "", ProfessioneUpdate = "", CAPUpdate = "";
        bool firstSearchUpdate = true, firstSearchDelete = true;

        //Oggetti
        Label lblNumeroRighe = new Label();
        Label lblDeleteFrom = new Label();
        Label lblDeleteUntil = new Label();

        private void FormEsaminandi_Load(object sender, EventArgs e)
        {
            Adatta_Panel_Delete_From("SELEZIONA RIGA...");
            Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            ModificaDateTimePicker();
            Aggiorna();
        }

        public void ModificaDateTimePicker()
        {
            dateTimePickerDatadiNascitaEsaminandi.Format = DateTimePickerFormat.Custom;
            dateTimePickerDatadiNascitaEsaminandi.CustomFormat = "yyyy-MM-dd";
            dateTimePickerDatadiNascitaUpdateEsaminandi.Format = DateTimePickerFormat.Custom;
            dateTimePickerDatadiNascitaUpdateEsaminandi.CustomFormat = "yyyy-MM-dd";
        }

        private void textBoxCodiceSkillCardEsaminandi_Click(object sender, EventArgs e)
        {
            textBoxCodiceSkillCardEsaminandi.Clear();
            textBoxCodiceSkillCardEsaminandi.ForeColor = Color.Black;
        }

        private void textBoxCodiceSkillCardEsaminandi_Leave(object sender, EventArgs e)
        {
            if (textBoxCodiceSkillCardEsaminandi.Text == "")
            {
                textBoxCodiceSkillCardEsaminandi.Text = "Codice skill-card...";
                textBoxCodiceSkillCardEsaminandi.ForeColor = Color.DimGray;
            }
        }

        private void textBoxNomeEsaminandi_Click(object sender, EventArgs e)
        {
            textBoxNomeEsaminandi.Clear();
            textBoxNomeEsaminandi.ForeColor = Color.Black;
        }

        private void textBoxNomeEsaminandi_Leave(object sender, EventArgs e)
        {
            if (textBoxNomeEsaminandi.Text == "")
            {
                textBoxNomeEsaminandi.Text = "Nome...";
                textBoxNomeEsaminandi.ForeColor = Color.DimGray;
            }
        }

        private void textBoxProfessioneEsaminandi_Click(object sender, EventArgs e)
        {
            textBoxProfessioneEsaminandi.Clear();
            textBoxProfessioneEsaminandi.ForeColor = Color.Black;
        }

        private void textBoxProfessioneEsaminandi_Leave(object sender, EventArgs e)
        {
            if (textBoxProfessioneEsaminandi.Text == "")
            {
                textBoxProfessioneEsaminandi.Text = "Professione...";
                textBoxProfessioneEsaminandi.ForeColor = Color.DimGray;
            }
        }

        private void textBoxCAPLuogodiNascitaEsaminandi_Click(object sender, EventArgs e)
        {
            textBoxCAPLuogodiNascitaEsaminandi.Clear();
            textBoxCAPLuogodiNascitaEsaminandi.ForeColor = Color.Black;
        }

        private void textBoxCAPLuogodiNascitaEsaminandi_Leave(object sender, EventArgs e)
        {
            if (textBoxCAPLuogodiNascitaEsaminandi.Text == "")
            {
                textBoxCAPLuogodiNascitaEsaminandi.Text = "CAP luogo di nascita...";
                textBoxCAPLuogodiNascitaEsaminandi.ForeColor = Color.DimGray;
            }
        }

        private void buttonClearEsaminandi_MouseEnter(object sender, EventArgs e)
        {
            buttonClearEsaminandi.ForeColor = Color.White;
            buttonClearEsaminandi.BackColor = Color.SteelBlue;
        }

        private void buttonClearEsaminandi_MouseLeave(object sender, EventArgs e)
        {
            buttonClearEsaminandi.ForeColor = Color.Black;
            buttonClearEsaminandi.BackColor = Color.Gainsboro;
        }

        private void buttonInserisciEsaminandi_MouseEnter(object sender, EventArgs e)
        {
            buttonInserisciEsaminandi.ForeColor = Color.White;
            buttonInserisciEsaminandi.BackColor = Color.SteelBlue;
        }

        private void buttonInserisciEsaminandi_MouseLeave(object sender, EventArgs e)
        {
            buttonInserisciEsaminandi.ForeColor = Color.Black;
            buttonInserisciEsaminandi.BackColor = Color.Gainsboro;
        }

        private void buttonClearEsaminandi_Click(object sender, EventArgs e)
        {
            textBoxCodiceSkillCardEsaminandi.Text = "Codice skill-card...";
            textBoxCodiceSkillCardEsaminandi.ForeColor = Color.DimGray;
            textBoxNomeEsaminandi.Text = "Nome...";
            textBoxNomeEsaminandi.ForeColor = Color.DimGray;
            textBoxProfessioneEsaminandi.Text = "Professione...";
            textBoxProfessioneEsaminandi.ForeColor = Color.DimGray;
            textBoxCAPLuogodiNascitaEsaminandi.Text = "CAP luogo di nascita...";
            textBoxCAPLuogodiNascitaEsaminandi.ForeColor = Color.DimGray;
            radioButtonMaschioEsaminandi.Checked = false;
            radioButtonFemminaEsaminandi.Checked = false;
            dateTimePickerDatadiNascitaEsaminandi.Value = System.DateTime.Now;
        }

        private void buttonInserisciEsaminandi_Click(object sender, EventArgs e)
        {
            if (textBoxCodiceSkillCardEsaminandi.Text != "Codice skill-card..." && textBoxCodiceSkillCardEsaminandi.Text != "" && dateTimePickerDatadiNascitaEsaminandi.Text != "" && textBoxNomeEsaminandi.Text != "Nome..." && textBoxNomeEsaminandi.Text != "" && textBoxProfessioneEsaminandi.Text != "Professione..." && textBoxProfessioneEsaminandi.Text != "" && textBoxCAPLuogodiNascitaEsaminandi.Text != "CAP luogo di nascita..." && textBoxCAPLuogodiNascitaEsaminandi.Text != "" && radioButtonMaschioEsaminandi.Checked != false || radioButtonFemminaEsaminandi.Checked != false)
            {
                try
                {
                    string sesso = "";
                    MySqlCommand InsertQuery;
                    if (radioButtonMaschioEsaminandi.Checked == true)
                        sesso = "Maschio";
                    else
                        sesso = "Femmina";
                    string comando = "INSERT INTO ecdl.esaminandi(Codice_Skill_Card,Nome,Sesso,Data_di_Nascita,Professione,Luogo_di_Nascita_CAP) VALUES (" + textBoxCodiceSkillCardEsaminandi.Text + ",'" + textBoxNomeEsaminandi.Text + "','" + sesso + "','" + dateTimePickerDatadiNascitaEsaminandi.Text + "','" + textBoxProfessioneEsaminandi.Text + "'," + textBoxCAPLuogodiNascitaEsaminandi.Text + ")";
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

        private void textBoxCercaValoreDeleteEsaminandi_Click(object sender, EventArgs e)
        {
            if (firstSearchDelete != false)
            {
                textBoxCercaValoreDeleteEsaminandi.Clear();
                textBoxCercaValoreDeleteEsaminandi.ForeColor = Color.Black;
                firstSearchDelete = false;
            }
        }

        private void textBoxCercaValoreDeleteEsaminandi_TextChanged(object sender, EventArgs e)
        {
            MySqlCommand SearchCodiceSkillCardQuery;
            SearchCodiceSkillCardQuery = new MySqlCommand("SELECT * FROM ecdl.esaminandi WHERE Codice_Skill_Card LIKE '" + textBoxCercaValoreDeleteEsaminandi.Text + "%'", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchCodiceSkillCardQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewEsaminandiDelete.DataSource = ds.Tables[0];
        }

        private void radioButtonMultiEsaminandi_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMultiEsaminandi.Checked == false)
            {
                Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            }
            else
            {
                Adatta_Panel_Delete_Until("SELEZIONA RIGA...");
            }
        }

        private void buttonDeleteEsaminandi_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteEsaminandi.ForeColor = Color.White;
            buttonDeleteEsaminandi.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteEsaminandi_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteEsaminandi.ForeColor = Color.Black;
            buttonDeleteEsaminandi.BackColor = Color.Gainsboro;
        }

        private void buttonDeleteAllEsaminandi_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteAllEsaminandi.ForeColor = Color.White;
            buttonDeleteAllEsaminandi.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteAllEsaminandi_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteAllEsaminandi.ForeColor = Color.Black;
            buttonDeleteAllEsaminandi.BackColor = Color.Gainsboro;
        }

        private void dataGridViewEsaminandiDelete_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonSingleEsaminandi.Checked == true)
            {
                if (dataGridViewEsaminandiDelete.CurrentRow != null && dataGridViewEsaminandiDelete.CurrentRow.Index < dataGridViewEsaminandiDelete.Rows.Count - 1)
                {
                    string CodiceSkillCard = dataGridViewEsaminandiDelete.CurrentRow.Cells[0].Value.ToString();
                    Adatta_Panel_Delete_From(CodiceSkillCard);
                }
            }
            else
            {
                if (numRigheSelezionate == 2)
                {
                    if (dataGridViewEsaminandiDelete.CurrentRow != null && dataGridViewEsaminandiDelete.CurrentRow.Index < dataGridViewEsaminandiDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewEsaminandiDelete.CurrentRow.Index;
                        string CodiceSkillCard = dataGridViewEsaminandiDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(CodiceSkillCard);
                        numRigheSelezionate = 1;
                    }
                }
                else if (numRigheSelezionate == 1)
                {
                    if (dataGridViewEsaminandiDelete.CurrentRow != null && dataGridViewEsaminandiDelete.CurrentRow.Index < dataGridViewEsaminandiDelete.Rows.Count - 1)
                    {
                        indiceUntil = dataGridViewEsaminandiDelete.CurrentRow.Index;
                        string CodiceSkillCard = dataGridViewEsaminandiDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_Until(CodiceSkillCard);
                        numRigheSelezionate++;
                    }
                }
                else if (numRigheSelezionate == 0)
                {
                    if (dataGridViewEsaminandiDelete.CurrentRow != null && dataGridViewEsaminandiDelete.CurrentRow.Index < dataGridViewEsaminandiDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewEsaminandiDelete.CurrentRow.Index;
                        string CodiceSkillCard = dataGridViewEsaminandiDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(CodiceSkillCard);
                        numRigheSelezionate++;
                    }
                }
            }
        }

        private void buttonDeleteEsaminandi_Click(object sender, EventArgs e)
        {
            if (radioButtonSingleEsaminandi.Checked == true)
            {
                try
                {
                    string[] valPanel = lblDeleteFrom.Text.Split(':');
                    string CodiceSkillCard = valPanel[1].Trim(' ');
                    MySqlCommand DeleteQuery;
                    string comando = "DELETE FROM ecdl.esaminandi WHERE Codice_Skill_Card = " + CodiceSkillCard;
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
                    List<string> listaCodiceSkillCardDelete = new List<string>();
                    for (int i = indiceFrom; i <= indiceUntil; i++)
                    {
                        listaCodiceSkillCardDelete.Add(dataGridViewEsaminandiDelete.Rows[i].Cells[0].Value.ToString());
                    }
                    try
                    {
                        foreach (string item in listaCodiceSkillCardDelete)
                        {
                            MySqlCommand DeleteQuery;
                            string comando = "DELETE FROM ecdl.esaminandi WHERE Codice_Skill_Card = " + item;
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

        private void buttonDeleteAllEsaminandi_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand DeleteAllQuery;
                string comando = "DELETE FROM ecdl.esaminandi";
                DeleteAllQuery = new MySqlCommand(comando, DatabaseECDL.connessioneEcdl);
                DeleteAllQuery.ExecuteNonQuery();
                Aggiorna();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void textBoxCercaValoreEsaminandi_Click(object sender, EventArgs e)
        {
            if (firstSearchUpdate != false)
            {
                textBoxCercaValoreEsaminandi.Clear();
                textBoxCercaValoreEsaminandi.ForeColor = Color.Black;
                firstSearchUpdate = false;
            }
        }

        private void textBoxCercaValoreEsaminandi_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonSearchCodiceSkillCardEsaminandi.Checked == true)
            {
                MySqlCommand SearchIDSessioneQuery;
                SearchIDSessioneQuery = new MySqlCommand("SELECT * FROM ecdl.esaminandi WHERE Codice_Skill_Card LIKE '" + textBoxCercaValoreEsaminandi.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchIDSessioneQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewEsaminandiUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchNomeEsaminandi.Checked == true)
            {
                MySqlCommand SearchDataQuery;
                SearchDataQuery = new MySqlCommand("SELECT * FROM ecdl.esaminandi WHERE Nome LIKE '" + textBoxCercaValoreEsaminandi.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchDataQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewEsaminandiUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchSessoEsaminandi.Checked == true)
            {
                MySqlCommand SearchOraQuery;
                SearchOraQuery = new MySqlCommand("SELECT * FROM ecdl.esaminandi WHERE Sesso LIKE '" + textBoxCercaValoreEsaminandi.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchOraQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewEsaminandiUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchDatadiNascitaEsaminandi.Checked == true)
            {
                MySqlCommand SearchNomeSedeQuery;
                SearchNomeSedeQuery = new MySqlCommand("SELECT * FROM ecdl.esaminandi WHERE Data_di_Nascita LIKE '" + textBoxCercaValoreEsaminandi.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchNomeSedeQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewEsaminandiUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchProfessioneEsaminandi.Checked == true)
            {
                MySqlCommand SearchNomeSedeQuery;
                SearchNomeSedeQuery = new MySqlCommand("SELECT * FROM ecdl.esaminandi WHERE Professione LIKE '" + textBoxCercaValoreEsaminandi.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchNomeSedeQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewEsaminandiUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchCAPEsaminandi.Checked == true)
            {
                MySqlCommand SearchNomeSedeQuery;
                SearchNomeSedeQuery = new MySqlCommand("SELECT * FROM ecdl.esaminandi WHERE Luogo_di_Nascita_CAP LIKE '" + textBoxCercaValoreEsaminandi.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchNomeSedeQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewEsaminandiUpdate.DataSource = ds.Tables[0];
            }
        }

        private void radioButtonSearchCodiceSkillCardEsaminandi_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreEsaminandi_TextChanged(sender, e);
        }

        private void radioButtonSearchNomeEsaminandi_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreEsaminandi_TextChanged(sender, e);
        }

        private void radioButtonSearchSessoEsaminandi_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreEsaminandi_TextChanged(sender, e);
        }

        private void radioButtonSearchDatadiNascitaEsaminandi_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreEsaminandi_TextChanged(sender, e);
        }

        private void radioButtonSearchProfessioneEsaminandi_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreEsaminandi_TextChanged(sender, e);
        }

        private void radioButtonSearchCAPEsaminandi_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreEsaminandi_TextChanged(sender, e);
        }

        private void textBoxCodiceSkillCardUpdateEsaminandi_Click(object sender, EventArgs e)
        {
            textBoxCodiceSkillCardUpdateEsaminandi.Clear();
            textBoxCodiceSkillCardUpdateEsaminandi.ForeColor = Color.Black;
        }

        private void textBoxCodiceSkillCardUpdateEsaminandi_Leave(object sender, EventArgs e)
        {
            if (textBoxCodiceSkillCardUpdateEsaminandi.Text == "")
            {
                textBoxCodiceSkillCardUpdateEsaminandi.Text = CodiceSkillCardUpdate;
                textBoxCodiceSkillCardUpdateEsaminandi.ForeColor = Color.DimGray;
            }
        }

        private void textBoxNomeUpdateEsaminandi_Click(object sender, EventArgs e)
        {
            textBoxNomeUpdateEsaminandi.Clear();
            textBoxNomeUpdateEsaminandi.ForeColor = Color.Black;
        }

        private void textBoxNomeUpdateEsaminandi_Leave(object sender, EventArgs e)
        {
            if (textBoxNomeUpdateEsaminandi.Text == "")
            {
                textBoxNomeUpdateEsaminandi.Text = NomeUpdate;
                textBoxNomeUpdateEsaminandi.ForeColor = Color.DimGray;
            }
        }

        private void textBoxProfessioneUpdateEsaminandi_Click(object sender, EventArgs e)
        {
            textBoxProfessioneUpdateEsaminandi.Clear();
            textBoxProfessioneUpdateEsaminandi.ForeColor = Color.Black;
        }

        private void textBoxProfessioneUpdateEsaminandi_Leave(object sender, EventArgs e)
        {
            if (textBoxProfessioneUpdateEsaminandi.Text == "")
            {
                textBoxProfessioneUpdateEsaminandi.Text = ProfessioneUpdate;
                textBoxProfessioneUpdateEsaminandi.ForeColor = Color.DimGray;
            }
        }

        private void textBoxCAPLuogodiNascitaUpdateEsaminandi_Click(object sender, EventArgs e)
        {
            textBoxCAPLuogodiNascitaUpdateEsaminandi.Clear();
            textBoxCAPLuogodiNascitaUpdateEsaminandi.ForeColor = Color.Black;
        }

        private void textBoxCAPLuogodiNascitaUpdateEsaminandi_Leave(object sender, EventArgs e)
        {
            if (textBoxCAPLuogodiNascitaUpdateEsaminandi.Text == "")
            {
                textBoxCAPLuogodiNascitaUpdateEsaminandi.Text = CAPUpdate;
                textBoxCAPLuogodiNascitaUpdateEsaminandi.ForeColor = Color.DimGray;
            }
        }

        private void comboBoxSessoUpdateEsaminandi_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSessoUpdateEsaminandi.ForeColor = Color.Black;
        }

        private void buttonClearUpdateEsaminandi_MouseEnter(object sender, EventArgs e)
        {
            buttonClearUpdateEsaminandi.ForeColor = Color.White;
            buttonClearUpdateEsaminandi.BackColor = Color.SteelBlue;
        }

        private void buttonClearUpdateEsaminandi_MouseLeave(object sender, EventArgs e)
        {
            buttonClearUpdateEsaminandi.ForeColor = Color.Black;
            buttonClearUpdateEsaminandi.BackColor = Color.Gainsboro;
        }

        private void buttonUpdateEsaminandi_MouseEnter(object sender, EventArgs e)
        {
            buttonUpdateEsaminandi.ForeColor = Color.White;
            buttonUpdateEsaminandi.BackColor = Color.SteelBlue;
        }

        private void buttonUpdateEsaminandi_MouseLeave(object sender, EventArgs e)
        {
            buttonUpdateEsaminandi.ForeColor = Color.Black;
            buttonUpdateEsaminandi.BackColor = Color.Gainsboro;
        }

        private void dataGridViewEsaminandiUpdate_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEsaminandiUpdate.CurrentRow != null && dataGridViewEsaminandiUpdate.CurrentRow.Index < dataGridViewEsaminandiUpdate.Rows.Count - 1)
            {
                textBoxCodiceSkillCardUpdateEsaminandi.Text = dataGridViewEsaminandiUpdate.CurrentRow.Cells[0].Value.ToString();
                textBoxNomeUpdateEsaminandi.Text = dataGridViewEsaminandiUpdate.CurrentRow.Cells[1].Value.ToString();
                comboBoxSessoUpdateEsaminandi.Text = dataGridViewEsaminandiUpdate.CurrentRow.Cells[2].Value.ToString();
                dateTimePickerDatadiNascitaUpdateEsaminandi.Text = dataGridViewEsaminandiUpdate.CurrentRow.Cells[3].Value.ToString();
                textBoxProfessioneUpdateEsaminandi.Text = dataGridViewEsaminandiUpdate.CurrentRow.Cells[4].Value.ToString();
                textBoxCAPLuogodiNascitaUpdateEsaminandi.Text = dataGridViewEsaminandiUpdate.CurrentRow.Cells[5].Value.ToString();
                textBoxCodiceSkillCardUpdateEsaminandi.ForeColor = Color.DimGray;
                textBoxNomeUpdateEsaminandi.ForeColor = Color.DimGray;
                comboBoxSessoUpdateEsaminandi.ForeColor = Color.DimGray;
                textBoxProfessioneUpdateEsaminandi.ForeColor = Color.DimGray;
                textBoxCAPLuogodiNascitaUpdateEsaminandi.ForeColor = Color.DimGray;
                CodiceSkillCardUpdate = textBoxCodiceSkillCardUpdateEsaminandi.Text;
                NomeUpdate = textBoxNomeUpdateEsaminandi.Text;
                SessoUpdate = comboBoxSessoUpdateEsaminandi.Text;
                DatadiNascitaUpdate = dateTimePickerDatadiNascitaUpdateEsaminandi.Text;
                ProfessioneUpdate = textBoxProfessioneUpdateEsaminandi.Text;
                CAPUpdate = textBoxCAPLuogodiNascitaUpdateEsaminandi.Text;
            }
        }

        private void buttonClearUpdateEsaminandi_Click(object sender, EventArgs e)
        {
            textBoxCodiceSkillCardUpdateEsaminandi.Text = CodiceSkillCardUpdate;
            textBoxNomeUpdateEsaminandi.Text = NomeUpdate;
            comboBoxSessoUpdateEsaminandi.Text = SessoUpdate;
            dateTimePickerDatadiNascitaUpdateEsaminandi.Text = DatadiNascitaUpdate;
            textBoxProfessioneUpdateEsaminandi.Text = ProfessioneUpdate;
            textBoxCAPLuogodiNascitaUpdateEsaminandi.Text = CAPUpdate;
            textBoxCodiceSkillCardUpdateEsaminandi.ForeColor = Color.DimGray;
            textBoxNomeUpdateEsaminandi.ForeColor = Color.DimGray;
            comboBoxSessoUpdateEsaminandi.ForeColor = Color.DimGray;
            textBoxProfessioneUpdateEsaminandi.ForeColor = Color.DimGray;
            textBoxCAPLuogodiNascitaUpdateEsaminandi.ForeColor = Color.DimGray;
        }

        private void buttonUpdateEsaminandi_Click(object sender, EventArgs e)
        {
            if (dateTimePickerDatadiNascitaUpdateEsaminandi.Text != "" && comboBoxSessoUpdateEsaminandi.Text != "" && textBoxCodiceSkillCardUpdateEsaminandi.Text != "" && textBoxNomeUpdateEsaminandi.Text != "" && textBoxProfessioneUpdateEsaminandi.Text != "" && textBoxCAPLuogodiNascitaUpdateEsaminandi.Text != "")
            {
                try
                {
                    MySqlCommand UpdateQuery;
                    string comando = "UPDATE ecdl.esaminandi SET Codice_Skill_Card = " + textBoxCodiceSkillCardUpdateEsaminandi.Text + ", Nome = '" + textBoxNomeUpdateEsaminandi.Text + "', Sesso = '" + comboBoxSessoUpdateEsaminandi.Text + "', Data_di_Nascita = '" + dateTimePickerDatadiNascitaUpdateEsaminandi.Text + "', Professione = '" + textBoxProfessioneUpdateEsaminandi.Text + "', Luogo_di_Nascita_CAP = " + textBoxCAPLuogodiNascitaUpdateEsaminandi.Text + " WHERE Codice_Skill_Card = " + CodiceSkillCardUpdate;
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

        private void Aggiorna()
        {
            MySqlCommand SelectQuery;
            SelectQuery = new MySqlCommand("SELECT * FROM ecdl.esaminandi", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SelectQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewEsaminandiInsert.DataSource = ds.Tables[0];
            dataGridViewEsaminandiDelete.DataSource = ds.Tables[0];
            dataGridViewEsaminandiUpdate.DataSource = ds.Tables[0];

            Select_Count();
        }

        private void Select_Count()
        {
            MySqlCommand SelectCountQuery;
            SelectCountQuery = DatabaseECDL.connessioneEcdl.CreateCommand();
            SelectCountQuery.CommandText = "SELECT COUNT(*) FROM ecdl.esaminandi";
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
            panelNumeroRigheEsaminandi.Controls.Add(lblNumeroRighe);
            panelNumeroRigheEsaminandi.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_From(string CodiceSkillCard)
        {
            lblDeleteFrom.AutoSize = false;
            lblDeleteFrom.Dock = DockStyle.Fill;
            lblDeleteFrom.Size = new Size(296, 25);
            lblDeleteFrom.Text = "CODICE SKILL-CARD: " + CodiceSkillCard;
            lblDeleteFrom.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteFromEsaminandi.Controls.Add(lblDeleteFrom);
            panelDeleteFromEsaminandi.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_Until(string CodiceSkillCard)
        {
            lblDeleteUntil.AutoSize = false;
            lblDeleteUntil.Dock = DockStyle.Fill;
            lblDeleteUntil.Size = new Size(296, 25);
            if (radioButtonSingleEsaminandi.Checked == true)
                lblDeleteUntil.Text = CodiceSkillCard;
            else
                lblDeleteUntil.Text = "CODICE SKILL-CARD: " + CodiceSkillCard;
            lblDeleteUntil.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteUntilEsaminandi.Controls.Add(lblDeleteUntil);
            panelDeleteUntilEsaminandi.BorderStyle = BorderStyle.FixedSingle;
        }

        private void label2Esaminandi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2Esaminandi_MouseEnter(object sender, EventArgs e)
        {
            label2Esaminandi.ForeColor = Color.Black;
        }

        private void label2Esaminandi_MouseLeave(object sender, EventArgs e)
        {
            label2Esaminandi.ForeColor = Color.DimGray;
        }
    }
}
