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
    public partial class FormEsami : Form
    {
        public FormEsami()
        {
            InitializeComponent();
        }

        //Variabili per uso generale
        int numRigheSelezionate = 0, indiceFrom = 0, indiceUntil = 0;
        string IDEsameUpdate = "", TipoUpdate = "", PercentualeMinimaUpdate = "";
        bool firstSearchUpdate = true, firstSearchDelete = true;

        //Oggetti
        Label lblNumeroRighe = new Label();
        Label lblDeleteFrom = new Label();
        Label lblDeleteUntil = new Label();

        private void FormEsami_Load(object sender, EventArgs e)
        {
            Adatta_Panel_Delete_From("SELEZIONA RIGA...");
            Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            Aggiorna();
        }

        private void Aggiorna()
        {
            MySqlCommand SelectQuery;
            SelectQuery = new MySqlCommand("SELECT * FROM ecdl.esami", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SelectQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewEsamiInsert.DataSource = ds.Tables[0];
            dataGridViewEsamiDelete.DataSource = ds.Tables[0];
            dataGridViewEsamiUpdate.DataSource = ds.Tables[0];

            Select_Count();
        }

        private void Select_Count()
        {
            MySqlCommand SelectCountQuery;
            SelectCountQuery = DatabaseECDL.connessioneEcdl.CreateCommand();
            SelectCountQuery.CommandText = "SELECT COUNT(*) FROM ecdl.esami";
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
            panelNumeroRigheEsami.Controls.Add(lblNumeroRighe);
            panelNumeroRigheEsami.BorderStyle = BorderStyle.FixedSingle;
        }

        private void textBoxTipo_Click(object sender, EventArgs e)
        {
            textBoxTipo.Clear();
            textBoxTipo.ForeColor = Color.Black;
        }

        private void textBoxTipo_Leave(object sender, EventArgs e)
        {
            if (textBoxTipo.Text == "")
            {
                textBoxTipo.Text = "Tipo...";
                textBoxTipo.ForeColor = Color.DimGray;
            }
        }

        private void textBoxPercentualeMinima_Click(object sender, EventArgs e)
        {
            textBoxPercentualeMinima.Clear();
            textBoxPercentualeMinima.ForeColor = Color.Black;
        }

        private void textBoxPercentualeMinima_Leave(object sender, EventArgs e)
        {
            if (textBoxPercentualeMinima.Text == "")
            {
                textBoxPercentualeMinima.Text = "Percentuale minima...";
                textBoxPercentualeMinima.ForeColor = Color.DimGray;
            }
        }

        private void buttonClearEsami_MouseEnter(object sender, EventArgs e)
        {
            buttonClearEsami.ForeColor = Color.White;
            buttonClearEsami.BackColor = Color.SteelBlue;
        }

        private void buttonClearEsami_MouseLeave(object sender, EventArgs e)
        {
            buttonClearEsami.ForeColor = Color.Black;
            buttonClearEsami.BackColor = Color.Gainsboro;
        }

        private void buttonInserisciEsami_MouseEnter(object sender, EventArgs e)
        {
            buttonInserisciEsami.ForeColor = Color.White;
            buttonInserisciEsami.BackColor = Color.SteelBlue;
        }

        private void buttonInserisciEsami_MouseLeave(object sender, EventArgs e)
        {
            buttonInserisciEsami.ForeColor = Color.Black;
            buttonInserisciEsami.BackColor = Color.Gainsboro;
        }

        private void buttonClearEsami_Click(object sender, EventArgs e)
        {
            textBoxTipo.Text = "Tipo...";
            textBoxPercentualeMinima.Text = "Percentuale minima...";
            textBoxTipo.ForeColor = Color.DimGray;
            textBoxPercentualeMinima.ForeColor = Color.DimGray;
        }

        private void buttonInserisciEsami_Click(object sender, EventArgs e)
        {
            if (textBoxTipo.Text != "Tipo..." && textBoxTipo.Text != "" && textBoxPercentualeMinima.Text != "Percentuale minima..." && textBoxPercentualeMinima.Text != "")
            {
                try
                {
                    MySqlCommand InsertQuery;
                    string comando = "INSERT INTO ecdl.esami(Tipo,Percentuale_Minima) VALUES ('" + textBoxTipo.Text + "'," + textBoxPercentualeMinima.Text + ")";
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

        private void textBoxCercaValoreDeleteEsami_Click(object sender, EventArgs e)
        {
            if (firstSearchDelete != false)
            {
                textBoxCercaValoreDeleteEsami.Clear();
                textBoxCercaValoreDeleteEsami.ForeColor = Color.Black;
                firstSearchDelete = false;
            }
        }

        private void textBoxCercaValoreDeleteEsami_TextChanged(object sender, EventArgs e)
        {
            MySqlCommand SearchIDEsameQuery;
            SearchIDEsameQuery = new MySqlCommand("SELECT * FROM ecdl.esami WHERE IDEsame LIKE '" + textBoxCercaValoreDeleteEsami.Text + "%'", DatabaseECDL.connessioneEcdl);
            MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchIDEsameQuery);
            DataSet ds = new DataSet();

            MSQLAdapter.Fill(ds);
            dataGridViewEsamiDelete.DataSource = ds.Tables[0];
        }

        private void radioButtonMultiEsami_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMultiEsami.Checked == false)
            {
                Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            }
            else
            {
                Adatta_Panel_Delete_Until("SELEZIONA RIGA...");
            }
        }

        private void buttonDeleteEsami_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteEsami.ForeColor = Color.White;
            buttonDeleteEsami.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteEsami_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteEsami.ForeColor = Color.Black;
            buttonDeleteEsami.BackColor = Color.Gainsboro;
        }

        private void buttonDeleteAllEsami_MouseEnter(object sender, EventArgs e)
        {
            buttonDeleteAllEsami.ForeColor = Color.White;
            buttonDeleteAllEsami.BackColor = Color.SteelBlue;
        }

        private void buttonDeleteAllEsami_MouseLeave(object sender, EventArgs e)
        {
            buttonDeleteAllEsami.ForeColor = Color.Black;
            buttonDeleteAllEsami.BackColor = Color.Gainsboro;
        }

        private void dataGridViewEsamiDelete_SelectionChanged(object sender, EventArgs e)
        {
            if (radioButtonSingleEsami.Checked == true)
            {
                if (dataGridViewEsamiDelete.CurrentRow != null && dataGridViewEsamiDelete.CurrentRow.Index < dataGridViewEsamiDelete.Rows.Count - 1)
                {
                    string IDEsame = dataGridViewEsamiDelete.CurrentRow.Cells[0].Value.ToString();
                    Adatta_Panel_Delete_From(IDEsame);
                }
            }
            else
            {
                if (numRigheSelezionate == 2)
                {
                    if (dataGridViewEsamiDelete.CurrentRow != null && dataGridViewEsamiDelete.CurrentRow.Index < dataGridViewEsamiDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewEsamiDelete.CurrentRow.Index;
                        string IDEsame = dataGridViewEsamiDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(IDEsame);
                        numRigheSelezionate = 1;
                    }
                }
                else if (numRigheSelezionate == 1)
                {
                    if (dataGridViewEsamiDelete.CurrentRow != null && dataGridViewEsamiDelete.CurrentRow.Index < dataGridViewEsamiDelete.Rows.Count - 1)
                    {
                        indiceUntil = dataGridViewEsamiDelete.CurrentRow.Index;
                        string IDEsame = dataGridViewEsamiDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_Until(IDEsame);
                        numRigheSelezionate++;
                    }
                }
                else if (numRigheSelezionate == 0)
                {
                    if (dataGridViewEsamiDelete.CurrentRow != null && dataGridViewEsamiDelete.CurrentRow.Index < dataGridViewEsamiDelete.Rows.Count - 1)
                    {
                        indiceFrom = dataGridViewEsamiDelete.CurrentRow.Index;
                        string IDEsame = dataGridViewEsamiDelete.CurrentRow.Cells[0].Value.ToString();
                        Adatta_Panel_Delete_From(IDEsame);
                        numRigheSelezionate++;
                    }
                }
            }
        }

        private void buttonDeleteEsami_Click(object sender, EventArgs e)
        {
            if (radioButtonSingleEsami.Checked == true)
            {
                try
                {
                    string[] valPanel = lblDeleteFrom.Text.Split(':');
                    string IDEsame = valPanel[1].Trim(' ');
                    MySqlCommand DeleteQuery;
                    string comando = "DELETE FROM ecdl.esami WHERE IDEsame = " + IDEsame;
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
                        listaIDEsameDelete.Add(dataGridViewEsamiDelete.Rows[i].Cells[0].Value.ToString());
                    }
                    try
                    {
                        foreach (string item in listaIDEsameDelete)
                        {
                            MySqlCommand DeleteQuery;
                            string comando = "DELETE FROM ecdl.esami WHERE IDEsame = " + item;
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

        private void buttonDeleteAllEsami_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand DeleteAllQuery;
                string comando = "DELETE FROM ecdl.esami";
                DeleteAllQuery = new MySqlCommand(comando, DatabaseECDL.connessioneEcdl);
                DeleteAllQuery.ExecuteNonQuery();
                Aggiorna();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void textBoxCercaValoreEsami_Click(object sender, EventArgs e)
        {
            if (firstSearchUpdate != false)
            {
                textBoxCercaValoreEsami.Clear();
                textBoxCercaValoreEsami.ForeColor = Color.Black;
                firstSearchUpdate = false;
            }
        }

        private void textBoxCercaValoreEsami_TextChanged(object sender, EventArgs e)
        {
            if (radioButtonSearchIDEsameEsami.Checked == true)
            {
                MySqlCommand SearchIDEsameQuery;
                SearchIDEsameQuery = new MySqlCommand("SELECT * FROM ecdl.esami WHERE IDEsame LIKE '" + textBoxCercaValoreEsami.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchIDEsameQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewEsamiUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchTipoEsami.Checked == true)
            {
                MySqlCommand SearchTipoQuery;
                SearchTipoQuery = new MySqlCommand("SELECT * FROM ecdl.esami WHERE Tipo LIKE '" + textBoxCercaValoreEsami.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchTipoQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewEsamiUpdate.DataSource = ds.Tables[0];
            }
            else if (radioButtonSearchPercentualeMinimaEsami.Checked == true)
            {
                MySqlCommand SearchPercentualeMinimaQuery;
                SearchPercentualeMinimaQuery = new MySqlCommand("SELECT * FROM ecdl.esami WHERE Percentuale_Minima LIKE '" + textBoxCercaValoreEsami.Text + "%'", DatabaseECDL.connessioneEcdl);
                MySqlDataAdapter MSQLAdapter = new MySqlDataAdapter(SearchPercentualeMinimaQuery);
                DataSet ds = new DataSet();

                MSQLAdapter.Fill(ds);
                dataGridViewEsamiUpdate.DataSource = ds.Tables[0];
            }
        }

        private void radioButtonSearchIDEsameEsami_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreEsami_TextChanged(sender, e);
        }

        private void radioButtonSearchTipoEsami_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreEsami_TextChanged(sender, e);
        }

        private void radioButtonSearchPercentualeMinimaEsami_CheckedChanged(object sender, EventArgs e)
        {
            textBoxCercaValoreEsami_TextChanged(sender, e);
        }

        private void textBoxTipoUpdateEsami_Click(object sender, EventArgs e)
        {
            textBoxTipoUpdateEsami.Clear();
            textBoxTipoUpdateEsami.ForeColor = Color.Black;
        }

        private void textBoxTipoUpdateEsami_Leave(object sender, EventArgs e)
        {
            if (textBoxTipoUpdateEsami.Text == "")
            {
                textBoxTipoUpdateEsami.Text = TipoUpdate;
                textBoxTipoUpdateEsami.ForeColor = Color.DimGray;
            }
        }

        private void textBoxPercentualeMinimaUpdateEsami_Click(object sender, EventArgs e)
        {
            textBoxPercentualeMinimaUpdateEsami.Clear();
            textBoxPercentualeMinimaUpdateEsami.ForeColor = Color.Black;
        }

        private void textBoxPercentualeMinimaUpdateEsami_Leave(object sender, EventArgs e)
        {
            if (textBoxPercentualeMinimaUpdateEsami.Text == "")
            {
                textBoxPercentualeMinimaUpdateEsami.Text = PercentualeMinimaUpdate;
                textBoxPercentualeMinimaUpdateEsami.ForeColor = Color.DimGray;
            }
        }

        private void Adatta_Panel_Delete_From(string IDEsame)
        {
            lblDeleteFrom.AutoSize = false;
            lblDeleteFrom.Dock = DockStyle.Fill;
            lblDeleteFrom.Size = new Size(296, 25);
            lblDeleteFrom.Text = "ID ESAME: " + IDEsame;
            lblDeleteFrom.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteFromEsami.Controls.Add(lblDeleteFrom);
            panelDeleteFromEsami.BorderStyle = BorderStyle.FixedSingle;
        }

        private void buttonClearUpdateEsami_MouseEnter(object sender, EventArgs e)
        {
            buttonClearUpdateEsami.ForeColor = Color.White;
            buttonClearUpdateEsami.BackColor = Color.SteelBlue;
        }

        private void buttonClearUpdateEsami_MouseLeave(object sender, EventArgs e)
        {
            buttonClearUpdateEsami.ForeColor = Color.Black;
            buttonClearUpdateEsami.BackColor = Color.Gainsboro;
        }

        private void buttonUpdateEsami_MouseEnter(object sender, EventArgs e)
        {
            buttonUpdateEsami.ForeColor = Color.White;
            buttonUpdateEsami.BackColor = Color.SteelBlue;
        }

        private void buttonUpdateEsami_MouseLeave(object sender, EventArgs e)
        {
            buttonUpdateEsami.ForeColor = Color.Black;
            buttonUpdateEsami.BackColor = Color.Gainsboro;
        }

        private void buttonClearUpdateEsami_Click(object sender, EventArgs e)
        {
            textBoxTipoUpdateEsami.Text = TipoUpdate;
            textBoxPercentualeMinimaUpdateEsami.Text = PercentualeMinimaUpdate;
            textBoxTipoUpdateEsami.ForeColor = Color.DimGray;
            textBoxPercentualeMinimaUpdateEsami.ForeColor = Color.DimGray;
        }

        private void buttonUpdateEsami_Click(object sender, EventArgs e)
        {
            if (textBoxTipoUpdateEsami.Text != "" && textBoxPercentualeMinimaUpdateEsami.Text != "")
            {
                try
                {
                    MySqlCommand UpdateQuery;
                    string comando = "UPDATE ecdl.esami SET Tipo = '" + textBoxTipoUpdateEsami.Text + "', Percentuale_Minima = " + textBoxPercentualeMinimaUpdateEsami.Text + " WHERE IDEsame = " + IDEsameUpdate;
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

        private void dataGridViewEsamiUpdate_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewEsamiUpdate.CurrentRow != null && dataGridViewEsamiUpdate.CurrentRow.Index < dataGridViewEsamiUpdate.Rows.Count - 1)
            {
                textBoxTipoUpdateEsami.Text = dataGridViewEsamiUpdate.CurrentRow.Cells[1].Value.ToString();
                textBoxPercentualeMinimaUpdateEsami.Text = dataGridViewEsamiUpdate.CurrentRow.Cells[2].Value.ToString();
                textBoxTipoUpdateEsami.ForeColor = Color.DimGray;
                textBoxPercentualeMinimaUpdateEsami.ForeColor = Color.DimGray;
                IDEsameUpdate = dataGridViewEsamiUpdate.CurrentRow.Cells[0].Value.ToString();
                TipoUpdate = textBoxTipoUpdateEsami.Text;
                PercentualeMinimaUpdate = textBoxPercentualeMinimaUpdateEsami.Text;
            }
        }

        private void Adatta_Panel_Delete_Until(string IDEsame)
        {
            lblDeleteUntil.AutoSize = false;
            lblDeleteUntil.Dock = DockStyle.Fill;
            lblDeleteUntil.Size = new Size(296, 25);
            if (radioButtonSingleEsami.Checked == true)
                lblDeleteUntil.Text = IDEsame;
            else
                lblDeleteUntil.Text = "ID ESAME: " + IDEsame;
            lblDeleteUntil.TextAlign = ContentAlignment.MiddleCenter;
            panelDeleteUntilEsami.Controls.Add(lblDeleteUntil);
            panelDeleteUntilEsami.BorderStyle = BorderStyle.FixedSingle;
        }

        private void label2Esami_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2Esami_MouseEnter(object sender, EventArgs e)
        {
            label2Esami.ForeColor = Color.Black;
        }

        private void label2Esami_MouseLeave(object sender, EventArgs e)
        {
            label2Esami.ForeColor = Color.DimGray;
        }
    }
}
