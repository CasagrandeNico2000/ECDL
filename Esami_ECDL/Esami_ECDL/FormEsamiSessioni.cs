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
    public partial class FormEsamiSessioni : Form
    {
        public FormEsamiSessioni()
        {
            InitializeComponent();
        }

        //Variabili per uso generale
        int numRigheSelezionate = 0, indiceFrom = 0, indiceUntil = 0;
        string IDEsameUpdate = "", IDSessioneUpdate = "";
        bool firstSearchUpdate = true, firstSearchDelete = true;

        //Oggetti
        Label lblNumeroRighe = new Label();
        Label lblDeleteFrom = new Label();
        Label lblDeleteUntil = new Label();

        private void FormEsamiSessioni_Load(object sender, EventArgs e)
        {
            Adatta_Panel_Delete_From("SELEZIONA RIGA...");
            Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            Aggiorna();
        }

        private void buttonClearEsamiSessioni_Click(object sender, EventArgs e)
        {
            textBoxIDEsame.Text = "ID Esame...";
            textBoxIDSessione.Text = "ID Sessione...";
            textBoxIDEsame.ForeColor = Color.DimGray;
            textBoxIDSessione.ForeColor = Color.DimGray;
        }

        private void buttonInserisciEsamiSessioni_Click(object sender, EventArgs e)
        {
            if (textBoxIDEsame.Text != "ID Esame..." && textBoxIDEsame.Text != "" && textBoxIDSessione.Text != "ID Sessione..." && textBoxIDSessione.Text != "")
            {
                try
                {
                    MySqlCommand InsertQuery;
                    string comando = "INSERT INTO ecdl.esami_e_sessioni(Esami_IDEsame,Sessioni_IDSessione) VALUES (" + textBoxIDEsame.Text + "," + textBoxIDSessione.Text + ")";
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

        private void buttonDeleteEsamiSessioni_Click(object sender, EventArgs e)
        {
            if (radioButtonSingleEsamiSessioni.Checked == true)
            {
                try
                {
                    string[] valPanel = lblDeleteFrom.Text.Split(':');
                    string IDEsame = valPanel[1].Trim(' ');
                    MySqlCommand DeleteQuery;
                    string comando = "DELETE FROM ecdl.esami_e_sessioni WHERE Esami_IDEsame = " + IDEsame;
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
                        listaIDEsameDelete.Add(dataGridViewEsamiSessioniDelete.Rows[i].Cells[0].Value.ToString());
                    }
                    try
                    {
                        foreach (string item in listaIDEsameDelete)
                        {
                            MySqlCommand DeleteQuery;
                            string comando = "DELETE FROM ecdl.esami_e_sessioni WHERE Esami_IDEsame = " + item;
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

        private void buttonDeleteAllEsamiSessioni_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand DeleteAllQuery;
                string comando = "DELETE FROM ecdl.esami_e_sessioni";
                DeleteAllQuery = new MySqlCommand(comando, DatabaseECDL.connessioneEcdl);
                DeleteAllQuery.ExecuteNonQuery();
                Aggiorna();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void buttonClearUpdateEsamiSessioni_Click(object sender, EventArgs e)
        {
            textBoxIDEsameUpdateEsamiSessioni.Text = IDEsameUpdate;
            textBoxIDSessioneUpdateEsamiSessioni.Text = IDSessioneUpdate;
            textBoxIDEsameUpdateEsamiSessioni.ForeColor = Color.DimGray;
            textBoxIDSessioneUpdateEsamiSessioni.ForeColor = Color.DimGray;
        }

        private void buttonUpdateEsamiSessioni_Click(object sender, EventArgs e)
        {
            if (textBoxIDEsameUpdateEsamiSessioni.Text != "" && textBoxIDSessioneUpdateEsamiSessioni.Text != "")
            {
                try
                {
                    MySqlCommand UpdateQuery;
                    string comando = "UPDATE ecdl.esami_e_sessioni SET Esami_IDEsame = " + textBoxIDEsameUpdateEsamiSessioni.Text + ", Sessioni_IDSessione = " + textBoxIDSessioneUpdateEsamiSessioni.Text + " WHERE Esami_IDEsame = " + IDEsameUpdate;
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

        private void textBoxCercaValoreDeleteEsamiSessioni_TextChanged(object sender, EventArgs e)
        {
            MySqlCommand SearchIDEsameQuery;
            SearchIDEsameQuery = new MySqlCommand("SELECT * FROM ecdl.esami_e_sessioni WHERE Esami_IDEsame LIKE '" + textBoxCercaValoreDeleteEsamiSessioni.Text + "%'", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchIDEsameQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewEsamiSessioniDelete.DataSource = ds.Tables[0];
        }

        private void Aggiorna()
        {
            MySqlCommand SelectQuery;
            SelectQuery = new MySqlCommand("SELECT * FROM ecdl.esami_e_sessioni", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SelectQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewEsamiSessioniInsert.DataSource = ds.Tables[0];
            dataGridViewEsamiSessioniDelete.DataSource = ds.Tables[0];
            dataGridViewEsamiSessioniUpdate.DataSource = ds.Tables[0];

            Select_Count();
        }

        private void Select_Count()
        {
            MySqlCommand SelectCountQuery;
            SelectCountQuery = DatabaseECDL.connessioneEcdl.CreateCommand();
            SelectCountQuery.CommandText = "SELECT COUNT(*) FROM ecdl.esami_e_sessioni";
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
            panelNumeroRigheEsamiSessioni.Controls.Add(lblNumeroRighe);
            panelNumeroRigheEsamiSessioni.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_From(string IDEsame)
        {
            lblDeleteFrom.AutoSize = false;
            lblDeleteFrom.Dock = DockStyle.Fill;
            lblDeleteFrom.Size = new Size(296, 25);
            lblDeleteFrom.Text = "ID ESAME: " + IDEsame;
            lblDeleteFrom.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteFromEsamiSessioni.Controls.Add(lblDeleteFrom);
            panelDeleteFromEsamiSessioni.BorderStyle = BorderStyle.FixedSingle;
        }

        private void Adatta_Panel_Delete_Until(string IDEsame)
        {
            lblDeleteUntil.AutoSize = false;
            lblDeleteUntil.Dock = DockStyle.Fill;
            lblDeleteUntil.Size = new Size(296, 25);
            if (radioButtonSingleEsamiSessioni.Checked == true)
                lblDeleteUntil.Text = IDEsame;
            else
                lblDeleteUntil.Text = "ID ESAME: " + IDEsame;
            lblDeleteUntil.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteUntilEsamiSessioni.Controls.Add(lblDeleteUntil);
            panelDeleteUntilEsamiSessioni.BorderStyle = BorderStyle.FixedSingle;
        }

        private void label2EsamiSessioni_MouseEnter(object sender, EventArgs e)
        {
            label2EsamiSessioni.ForeColor = Color.Black;
        }

        private void label2EsamiSessioni_MouseLeave(object sender, EventArgs e)
        {
            label2EsamiSessioni.ForeColor = Color.DimGray;
        }

        private void label2EsamiSessioni_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxIDEsame_Click(object sender, EventArgs e)
        {
            textBoxIDEsame.Clear();
            textBoxIDEsame.ForeColor = Color.Black;
        }

        private void textBoxIDEsame_Leave(object sender, EventArgs e)
        {
            if (textBoxIDEsame.Text == "")
            {
                textBoxIDEsame.Text = "ID Esame...";
                textBoxIDEsame.ForeColor = Color.DimGray;
            }
        }

        private void textBoxIDSessione_Click(object sender, EventArgs e)
        {
            textBoxIDSessione.Clear();
            textBoxIDSessione.ForeColor = Color.Black;
        }

        private void textBoxIDSessione_Leave(object sender, EventArgs e)
        {
            if (textBoxIDSessione.Text == "")
            {
                textBoxIDSessione.Text = "ID Sessione...";
                textBoxIDSessione.ForeColor = Color.DimGray;
            }
        }

        private void textBoxCercaValoreDeleteEsamiSessioni_Click(object sender, EventArgs e)
        {
            if (firstSearchDelete != false)
            {
                textBoxCercaValoreDeleteEsamiSessioni.Clear();
                textBoxCercaValoreDeleteEsamiSessioni.ForeColor = Color.Black;
                firstSearchDelete = false;
            }
        }

        private void buttonDeleteEsamiSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteEsamiSessioni.ForeColor = Color.White;
            buttonDeleteEsamiSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteEsamiSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteEsamiSessioni.ForeColor = Color.Black;
            buttonDeleteEsamiSessioni.BackColor = Color.Gainsboro;
        }

        private void buttonDeleteAllEsamiSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteAllEsamiSessioni.ForeColor = Color.White;
            buttonDeleteAllEsamiSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteAllEsamiSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteAllEsamiSessioni.ForeColor = Color.Black;
            buttonDeleteAllEsamiSessioni.BackColor = Color.Gainsboro;
        }

        private void radioButtonMultiEsamiSessioni_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMultiEsamiSessioni.Checked == false)
            {
                Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            }
            else
            {
                Adatta_Panel_Delete_Until("SELEZIONA RIGA...");
            }
        }

        private void dataGridViewEsamiSessioniDelete_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonSingleEsamiSessioni.Checked == true)
            {
                if (dataGridViewEsamiSessioniDelete.CurrentRow != null && dataGridViewEsamiSessioniDelete.CurrentRow.Index < dataGridViewEsamiSessioniDelete.Rows.Count - 1)
                {
                    string IDEsame = dataGridViewEsamiSessioniDelete.CurrentRow.Cells[0].Value.ToString();
                    Adatta_Panel_Delete_From(IDEsame);
                }
            }
            else
            {
                if (numRigheSelezionate == 2)
                {
                    if (dataGridViewEsamiSessioniDelete.CurrentRow != null && dataGridViewEsamiSessioniDelete.CurrentRow.Index < dataGridViewEsamiSessioniDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewEsamiSessioniDelete.CurrentRow.Index;
                        string IDEsame = dataGridViewEsamiSessioniDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(IDEsame);
                        numRigheSelezionate = 1;
                    }
                }
                else if (numRigheSelezionate == 1)
                {
                    if (dataGridViewEsamiSessioniDelete.CurrentRow != null && dataGridViewEsamiSessioniDelete.CurrentRow.Index < dataGridViewEsamiSessioniDelete.Rows.Count - 1)
                    {
                        indiceUntil = dataGridViewEsamiSessioniDelete.CurrentRow.Index;
                        string IDEsame = dataGridViewEsamiSessioniDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_Until(IDEsame);
                        numRigheSelezionate++;
                    }
                }
                else if (numRigheSelezionate == 0)
                {
                    if (dataGridViewEsamiSessioniDelete.CurrentRow != null && dataGridViewEsamiSessioniDelete.CurrentRow.Index < dataGridViewEsamiSessioniDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewEsamiSessioniDelete.CurrentRow.Index;
                        string IDEsame = dataGridViewEsamiSessioniDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(IDEsame);
                        numRigheSelezionate++;
                    }
                }
            }
        }

        private void textBoxCercaValoreEsamiSessioni_Click(object sender, EventArgs e)
        {
            if (firstSearchUpdate != false)
            {
                textBoxCercaValoreEsamiSessioni.Clear();
                textBoxCercaValoreEsamiSessioni.ForeColor = Color.Black;
                firstSearchUpdate = false;
            }
        }

        private void textBoxCercaValoreEsamiSessioni_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonSearchIDEsame.Checked == true)
            {
                MySqlCommand SearchIDEsameQuery;
                SearchIDEsameQuery = new MySqlCommand("SELECT * FROM ecdl.esami_e_sessioni WHERE Esami_IDEsame LIKE '" + textBoxCercaValoreEsamiSessioni.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchIDEsameQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewEsamiSessioniUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchIDSessione.Checked == true)
            {
                MySqlCommand SearchIDSessioneQuery;
                SearchIDSessioneQuery = new MySqlCommand("SELECT * FROM ecdl.esami_e_sessioni WHERE Sessioni_IDSessione LIKE '" + textBoxCercaValoreEsamiSessioni.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchIDSessioneQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewEsamiSessioniUpdate.DataSource = ds.Tables[0];
            }
        }

        private void radioButtonSearchIDEsame_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreEsamiSessioni_TextChanged(sender, e);
        }

        private void radioButtonSearchIDSessione_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreEsamiSessioni_TextChanged(sender, e);
        }

        private void textBoxIDEsameUpdateEsamiSessioni_Click(object sender, EventArgs e)
        {
            textBoxIDEsameUpdateEsamiSessioni.Clear();
            textBoxIDEsameUpdateEsamiSessioni.ForeColor = Color.Black;
        }

        private void textBoxIDEsameUpdateEsamiSessioni_Leave(object sender, EventArgs e)
        {
            if (textBoxIDEsameUpdateEsamiSessioni.Text == "")
            {
                textBoxIDEsameUpdateEsamiSessioni.Text = IDEsameUpdate;
                textBoxIDEsameUpdateEsamiSessioni.ForeColor = Color.DimGray;
            }
        }

        private void textBoxIDSessioneUpdateEsamiSessioni_Click(object sender, EventArgs e)
        {
            textBoxIDSessioneUpdateEsamiSessioni.Clear();
            textBoxIDSessioneUpdateEsamiSessioni.ForeColor = Color.Black;
        }

        private void textBoxIDSessioneUpdateEsamiSessioni_Leave(object sender, EventArgs e)
        {
            if (textBoxIDSessioneUpdateEsamiSessioni.Text == "")
            {
                textBoxIDSessioneUpdateEsamiSessioni.Text = IDSessioneUpdate;
                textBoxIDSessioneUpdateEsamiSessioni.ForeColor = Color.DimGray;
            }
        }

        private void buttonClearUpdateEsamiSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonClearUpdateEsamiSessioni.ForeColor = Color.White;
            buttonClearUpdateEsamiSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonClearUpdateEsamiSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonClearUpdateEsamiSessioni.ForeColor = Color.Black;
            buttonClearUpdateEsamiSessioni.BackColor = Color.Gainsboro;
        }

        private void buttonUpdateEsamiSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonUpdateEsamiSessioni.ForeColor = Color.White;
            buttonUpdateEsamiSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonUpdateEsamiSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonUpdateEsamiSessioni.ForeColor = Color.Black;
            buttonUpdateEsamiSessioni.BackColor = Color.Gainsboro;
        }

        private void dataGridViewEsamiSessioniUpdate_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEsamiSessioniUpdate.CurrentRow != null && dataGridViewEsamiSessioniUpdate.CurrentRow.Index < dataGridViewEsamiSessioniUpdate.Rows.Count - 1)
            {
                textBoxIDEsameUpdateEsamiSessioni.Text = dataGridViewEsamiSessioniUpdate.CurrentRow.Cells[0].Value.ToString();
                textBoxIDSessioneUpdateEsamiSessioni.Text = dataGridViewEsamiSessioniUpdate.CurrentRow.Cells[1].Value.ToString();
                textBoxIDEsameUpdateEsamiSessioni.ForeColor = Color.DimGray;
                textBoxIDSessioneUpdateEsamiSessioni.ForeColor = Color.DimGray;
                IDEsameUpdate = textBoxIDEsameUpdateEsamiSessioni.Text;
                IDSessioneUpdate = textBoxIDSessioneUpdateEsamiSessioni.Text;
            }
        }

        private void buttonClearEsamiSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonClearEsamiSessioni.ForeColor = Color.White;
            buttonClearEsamiSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonClearEsamiSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonClearEsamiSessioni.ForeColor = Color.Black;
            buttonClearEsamiSessioni.BackColor = Color.Gainsboro;
        }

        private void buttonInserisciEsamiSessioni_MouseEnter(object sender, EventArgs e)
        {
            buttonInserisciEsamiSessioni.ForeColor = Color.White;
            buttonInserisciEsamiSessioni.BackColor = Color.SteelBlue;
        }

        private void buttonInserisciEsamiSessioni_MouseLeave(object sender, EventArgs e)
        {
            buttonInserisciEsamiSessioni.ForeColor = Color.Black;
            buttonInserisciEsamiSessioni.BackColor = Color.Gainsboro;
        }
    }
}
