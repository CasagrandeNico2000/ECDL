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
    public partial class FormSessioni : Form
    {
        public FormSessioni()
        {
            InitializeComponent();
        }

        //Variabili per uso generale
        int numRigheSelezionate = 0, indiceFrom = 0, indiceUntil = 0;
        string IDSessioneUpdate = "", DataUpdate = "", OraUpdate = "", NomeSedeUpdate = "";
        bool firstSearchUpdate = true, firstSearchDelete = true;

        //Oggetti
        Label lblNumeroRighe = new Label();
        Label lblDeleteFrom = new Label();
        Label lblDeleteUntil = new Label();

        private void FormSessioni_Load(object sender, EventArgs e)
        {
            Adatta_Panel_Delete_From("SELEZIONA RIGA...");
            Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            ModificaDateTimePicker();
            Aggiorna();
        }

        public void ModificaDateTimePicker()
        {
            dateTimePickerDataSessioni.Format = DateTimePickerFormat.Custom;
            dateTimePickerDataSessioni.CustomFormat = "yyyy-MM-dd";
            dateTimePickerDataUpdateSessioni.Format = DateTimePickerFormat.Custom;
            dateTimePickerDataUpdateSessioni.CustomFormat = "yyyy-MM-dd";
        }

        private void Aggiorna()
        {
            MySqlCommand SelectQuery;
            SelectQuery = new MySqlCommand("SELECT * FROM ecdl.sessioni", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SelectQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewSessioniInsert.DataSource = ds.Tables[0];
            dataGridViewSessioniDelete.DataSource = ds.Tables[0];
            dataGridViewSessioniUpdate.DataSource = ds.Tables[0];

            Select_Count();
        }

        private void Select_Count()
        {
            MySqlCommand SelectCountQuery;
            SelectCountQuery = DatabaseECDL.connessioneEcdl.CreateCommand();
            SelectCountQuery.CommandText = "SELECT COUNT(*) FROM ecdl.sessioni";
            int numeroRighe = Convert.ToInt32(SelectCountQuery.ExecuteScalar());
            Adatta_Panel_Numero_Righe(numeroRighe.ToString());
        }

        private void comboBoxOreSessioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxOreSessioni.ForeColor = Color.Black;
        }

        private void comboBoxMinutiSessioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxMinutiSessioni.ForeColor = Color.Black;
        }

        private void comboBoxSecondiSessioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSecondiSessioni.ForeColor = Color.Black;
        }

        private void textBoxNomeSedeSessioni_Click(object sender, EventArgs e)
        {
            textBoxNomeSedeSessioni.Clear();
            textBoxNomeSedeSessioni.ForeColor = Color.Black;
        }

        private void textBoxNomeSedeSessioni_Leave(object sender, EventArgs e)
        {
            if (textBoxNomeSedeSessioni.Text == "")
            {
                textBoxNomeSedeSessioni.Text = "Nome sede...";
                textBoxNomeSedeSessioni.ForeColor = Color.DimGray;
            }
        }

        private void buttonClearSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonClearSessioni.ForeColor = Color.White;
            buttonClearSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonClearSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonClearSessioni.ForeColor = Color.Black;
            buttonClearSessioni.BackColor = Color.Gainsboro;
        }

        private void buttonInserisciSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonInserisciSessioni.ForeColor = Color.White;
            buttonInserisciSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonInserisciSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonInserisciSessioni.ForeColor = Color.Black;
            buttonInserisciSessioni.BackColor = Color.Gainsboro;
        }

        private void buttonClearSessioni_Click(object sender, EventArgs e)
        {
            textBoxNomeSedeSessioni.Text = "Nome sede...";
            textBoxNomeSedeSessioni.ForeColor = Color.DimGray;
            comboBoxOreSessioni.Text = "hh";
            comboBoxOreSessioni.ForeColor = Color.DimGray;
            comboBoxMinutiSessioni.Text = "mm";
            comboBoxMinutiSessioni.ForeColor = Color.DimGray;
            comboBoxSecondiSessioni.Text = "ss";
            comboBoxSecondiSessioni.ForeColor = Color.DimGray;
            dateTimePickerDataSessioni.Value = System.DateTime.Now;
        }

        private void buttonInserisciSessioni_Click(object sender, EventArgs e)
        {
            if (textBoxNomeSedeSessioni.Text != "Nome sede..." && textBoxNomeSedeSessioni.Text != "" && dateTimePickerDataSessioni.Text != "" && comboBoxOreSessioni.Text != "hh" && comboBoxOreSessioni.Text != "" && comboBoxMinutiSessioni.Text != "mm" && comboBoxMinutiSessioni.Text != "" && comboBoxSecondiSessioni.Text != "ss" && comboBoxSecondiSessioni.Text != "")
            {
                try
                {
                    MySqlCommand InsertQuery;
                    string comando = "INSERT INTO ecdl.sessioni(Data,Ora,Sede_Nome_Sede) VALUES ('" + dateTimePickerDataSessioni.Text + "','" + comboBoxOreSessioni.Text + ":" + comboBoxMinutiSessioni.Text + ":" + comboBoxSecondiSessioni.Text + "','" + textBoxNomeSedeSessioni.Text + "')";
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

        private void textBoxCercaValoreDeleteSessioni_Click(object sender, EventArgs e)
        {
            if (firstSearchDelete != false)
            {
                textBoxCercaValoreDeleteSessioni.Clear();
                textBoxCercaValoreDeleteSessioni.ForeColor = Color.Black;
                firstSearchDelete = false;
            }
        }

        private void textBoxCercaValoreDeleteSessioni_TextChanged(object sender, EventArgs e)
        {
            MySqlCommand SearchIDSessioneQuery;
            SearchIDSessioneQuery = new MySqlCommand("SELECT * FROM ecdl.sessioni WHERE IDSessione LIKE '" + textBoxCercaValoreDeleteSessioni.Text + "%'", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchIDSessioneQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewSessioniDelete.DataSource = ds.Tables[0];
        }

        private void radioButtonMultiSessioni_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMultiSessioni.Checked == false)
            {
                Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            }
            else
            {
                Adatta_Panel_Delete_Until("SELEZIONA RIGA...");
            }
        }

        private void buttonDeleteSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteSessioni.ForeColor = Color.White;
            buttonDeleteSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteSessioni.ForeColor = Color.Black;
            buttonDeleteSessioni.BackColor = Color.Gainsboro;
        }

        private void buttonDeleteAllSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteAllSessioni.ForeColor = Color.White;
            buttonDeleteAllSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteAllSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteAllSessioni.ForeColor = Color.Black;
            buttonDeleteAllSessioni.BackColor = Color.Gainsboro;
        }

        private void dataGridViewSessioniDelete_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonSingleSessioni.Checked == true)
            {
                if (dataGridViewSessioniDelete.CurrentRow != null && dataGridViewSessioniDelete.CurrentRow.Index < dataGridViewSessioniDelete.Rows.Count - 1)
                {
                    string IDSessione = dataGridViewSessioniDelete.CurrentRow.Cells[0].Value.ToString();
                    Adatta_Panel_Delete_From(IDSessione);
                }
            }
            else
            {
                if (numRigheSelezionate == 2)
                {
                    if (dataGridViewSessioniDelete.CurrentRow != null && dataGridViewSessioniDelete.CurrentRow.Index < dataGridViewSessioniDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewSessioniDelete.CurrentRow.Index;
                        string IDSessione = dataGridViewSessioniDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(IDSessione);
                        numRigheSelezionate = 1;
                    }
                }
                else if (numRigheSelezionate == 1)
                {
                    if (dataGridViewSessioniDelete.CurrentRow != null && dataGridViewSessioniDelete.CurrentRow.Index < dataGridViewSessioniDelete.Rows.Count - 1)
                    {
                        indiceUntil = dataGridViewSessioniDelete.CurrentRow.Index;
                        string IDSessione = dataGridViewSessioniDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_Until(IDSessione);
                        numRigheSelezionate++;
                    }
                }
                else if (numRigheSelezionate == 0)
                {
                    if (dataGridViewSessioniDelete.CurrentRow != null && dataGridViewSessioniDelete.CurrentRow.Index < dataGridViewSessioniDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewSessioniDelete.CurrentRow.Index;
                        string IDSessione = dataGridViewSessioniDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(IDSessione);
                        numRigheSelezionate++;
                    }
                }
            }
        }

        private void buttonDeleteSessioni_Click(object sender, EventArgs e)
        {
            if (radioButtonSingleSessioni.Checked == true)
            {
                try
                {
                    string[] valPanel = lblDeleteFrom.Text.Split(':');
                    string IDSessione = valPanel[1].Trim(' ');
                    MySqlCommand DeleteQuery;
                    string comando = "DELETE FROM ecdl.sessioni WHERE IDSessione = " + IDSessione;
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
                    List<string> listaIDSessioneDelete = new List<string>();
                    for (int i = indiceFrom; i <= indiceUntil; i++)
                    {
                        listaIDSessioneDelete.Add(dataGridViewSessioniDelete.Rows[i].Cells[0].Value.ToString());
                    }
                    try
                    {
                        foreach (string item in listaIDSessioneDelete)
                        {
                            MySqlCommand DeleteQuery;
                            string comando = "DELETE FROM ecdl.sessioni WHERE IDSessione = " + item;
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

        private void buttonDeleteAllSessioni_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand DeleteAllQuery;
                string comando = "DELETE FROM ecdl.sessioni";
                DeleteAllQuery = new MySqlCommand(comando, DatabaseECDL.connessioneEcdl);
                DeleteAllQuery.ExecuteNonQuery();
                Aggiorna();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void textBoxCercaValoreSessioni_Click(object sender, EventArgs e)
        {
            if (firstSearchUpdate != false)
            {
                textBoxCercaValoreSessioni.Clear();
                textBoxCercaValoreSessioni.ForeColor = Color.Black;
                firstSearchUpdate = false;
            }
        }

        private void textBoxCercaValoreSessioni_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonSearchIDSessioneSessioni.Checked == true)
            {
                MySqlCommand SearchIDSessioneQuery;
                SearchIDSessioneQuery = new MySqlCommand("SELECT * FROM ecdl.sessioni WHERE IDSessione LIKE '" + textBoxCercaValoreSessioni.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchIDSessioneQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewSessioniUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchDataSessioni.Checked == true)
            {
                MySqlCommand SearchDataQuery;
                SearchDataQuery = new MySqlCommand("SELECT * FROM ecdl.sessioni WHERE Data LIKE '" + textBoxCercaValoreSessioni.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchDataQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewSessioniUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchOraSessioni.Checked == true)
            {
                MySqlCommand SearchOraQuery;
                SearchOraQuery = new MySqlCommand("SELECT * FROM ecdl.sessioni WHERE Ora LIKE '" + textBoxCercaValoreSessioni.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchOraQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewSessioniUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchNomeSedeSessioni.Checked == true)
            {
                MySqlCommand SearchNomeSedeQuery;
                SearchNomeSedeQuery = new MySqlCommand("SELECT * FROM ecdl.sessioni WHERE Sede_Nome_Sede LIKE '" + textBoxCercaValoreSessioni.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchNomeSedeQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewSessioniUpdate.DataSource = ds.Tables[0];
            }
        }

        private void radioButtonSearchIDSessioneSessioni_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreSessioni_TextChanged(sender, e);
        }

        private void radioButtonSearchDataSessioni_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreSessioni_TextChanged(sender, e);
        }

        private void radioButtonSearchOraSessioni_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreSessioni_TextChanged(sender, e);
        }

        private void radioButtonSearchNomeSedeSessioni_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreSessioni_TextChanged(sender, e);
        }

        private void comboBoxOreUpdateSessioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxOreUpdateSessioni.ForeColor = Color.Black;
        }

        private void comboBoxMinutiUpdateSessioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxMinutiUpdateSessioni.ForeColor = Color.Black;
        }

        private void comboBoxSecondiUpdateSessioni_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxSecondiUpdateSessioni.ForeColor = Color.Black;
        }

        private void textBoxNomeSedeUpdateSessioni_Click(object sender, EventArgs e)
        {
            textBoxNomeSedeUpdateSessioni.Clear();
            textBoxNomeSedeUpdateSessioni.ForeColor = Color.Black;
        }

        private void textBoxNomeSedeUpdateSessioni_Leave(object sender, EventArgs e)
        {
            if (textBoxNomeSedeUpdateSessioni.Text == "")
            {
                textBoxNomeSedeUpdateSessioni.Text = NomeSedeUpdate;
                textBoxNomeSedeUpdateSessioni.ForeColor = Color.DimGray;
            }
        }

        private void buttonClearUpdateSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonClearUpdateSessioni.ForeColor = Color.White;
            buttonClearUpdateSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonClearUpdateSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonClearUpdateSessioni.ForeColor = Color.Black;
            buttonClearUpdateSessioni.BackColor = Color.Gainsboro;
        }

        private void buttonUpdateSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonUpdateSessioni.ForeColor = Color.White;
            buttonUpdateSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonUpdateSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonUpdateSessioni.ForeColor = Color.Black;
            buttonUpdateSessioni.BackColor = Color.Gainsboro;
        }

        private void dataGridViewSessioniUpdate_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewSessioniUpdate.CurrentRow != null && dataGridViewSessioniUpdate.CurrentRow.Index < dataGridViewSessioniUpdate.Rows.Count - 1)
            {
                dateTimePickerDataUpdateSessioni.Text = dataGridViewSessioniUpdate.CurrentRow.Cells[1].Value.ToString();
                string ora = dataGridViewSessioniUpdate.CurrentRow.Cells[2].Value.ToString();
                string[] oraSplit = ora.Split(':');
                comboBoxOreUpdateSessioni.Text = oraSplit[0];
                comboBoxMinutiUpdateSessioni.Text = oraSplit[1];
                comboBoxSecondiUpdateSessioni.Text = oraSplit[2];
                textBoxNomeSedeUpdateSessioni.Text = dataGridViewSessioniUpdate.CurrentRow.Cells[3].Value.ToString();
                comboBoxOreUpdateSessioni.ForeColor = Color.DimGray;
                comboBoxMinutiUpdateSessioni.ForeColor = Color.DimGray;
                comboBoxSecondiUpdateSessioni.ForeColor = Color.DimGray;
                textBoxNomeSedeUpdateSessioni.ForeColor = Color.DimGray;
                IDSessioneUpdate = dataGridViewSessioniUpdate.CurrentRow.Cells[0].Value.ToString();
                DataUpdate = dateTimePickerDataUpdateSessioni.Text;
                OraUpdate = comboBoxOreUpdateSessioni.Text + ":" + comboBoxMinutiUpdateSessioni.Text + ":" + comboBoxSecondiUpdateSessioni.Text;
                NomeSedeUpdate = textBoxNomeSedeUpdateSessioni.Text;
            }
        }

        private void buttonClearUpdateSessioni_Click(object sender, EventArgs e)
        {
            dateTimePickerDataUpdateSessioni.Text = DataUpdate;
            string[] oraSplit = OraUpdate.Split(':');
            comboBoxOreUpdateSessioni.Text = oraSplit[0];
            comboBoxMinutiUpdateSessioni.Text = oraSplit[1];
            comboBoxSecondiUpdateSessioni.Text = oraSplit[2];
            textBoxNomeSedeUpdateSessioni.Text = NomeSedeUpdate;
            comboBoxOreUpdateSessioni.ForeColor = Color.DimGray;
            comboBoxMinutiUpdateSessioni.ForeColor = Color.DimGray;
            comboBoxSecondiUpdateSessioni.ForeColor = Color.DimGray;
            textBoxNomeSedeUpdateSessioni.ForeColor = Color.DimGray;
        }

        private void buttonUpdateSessioni_Click(object sender, EventArgs e)
        {
            if (dateTimePickerDataUpdateSessioni.Text != "" && comboBoxOreUpdateSessioni.Text != "" && comboBoxMinutiUpdateSessioni.Text != "" && comboBoxSecondiUpdateSessioni.Text != "" && textBoxNomeSedeUpdateSessioni.Text != "")
            {
                try
                {
                    MySqlCommand UpdateQuery;
                    string comando = "UPDATE ecdl.sessioni SET Data = '" + dateTimePickerDataUpdateSessioni.Text + "', Ora = '" + comboBoxOreUpdateSessioni.Text + ":" + comboBoxMinutiUpdateSessioni.Text + ":" + comboBoxSecondiUpdateSessioni.Text + "', Sede_Nome_Sede = '" + textBoxNomeSedeUpdateSessioni.Text + "' WHERE IDSessione = " + IDSessioneUpdate;
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

        private void Adatta_Panel_Numero_Righe(string num)
        {
            lblNumeroRighe.AutoSize = false;
            lblNumeroRighe.Dock = DockStyle.Fill;
            lblNumeroRighe.Size = new Size(717, 29);
            lblNumeroRighe.Text = "NUMERO RIGHE: " + num;
            lblNumeroRighe.TextAlign = ContentAlignment.MiddleCenter;
            panelNumeroRigheSessioni.Controls.Add(lblNumeroRighe);
            panelNumeroRigheSessioni.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_From(string IDSessione)
        {
            lblDeleteFrom.AutoSize = false;
            lblDeleteFrom.Dock = DockStyle.Fill;
            lblDeleteFrom.Size = new Size(296, 25);
            lblDeleteFrom.Text = "ID SESSIONE: " + IDSessione;
            lblDeleteFrom.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteFromSessioni.Controls.Add(lblDeleteFrom);
            panelDeleteFromSessioni.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_Until(string IDSessione)
        {
            lblDeleteUntil.AutoSize = false;
            lblDeleteUntil.Dock = DockStyle.Fill;
            lblDeleteUntil.Size = new Size(296, 25);
            if (radioButtonSingleSessioni.Checked == true)
                lblDeleteUntil.Text = IDSessione;
            else
                lblDeleteUntil.Text = "ID SESSIONE: " + IDSessione;
            lblDeleteUntil.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteUntilSessioni.Controls.Add(lblDeleteUntil);
            panelDeleteUntilSessioni.BorderStyle = BorderStyle.FixedSingle;
        }

        private void label2Sessioni_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2Sessioni_MouseEnter(object sender, EventArgs e)
        {
            label2Sessioni.ForeColor = Color.Black;
        }

        private void label2Sessioni_MouseLeave(object sender, EventArgs e)
        {
            label2Sessioni.ForeColor = Color.DimGray;
        }
    }
}
