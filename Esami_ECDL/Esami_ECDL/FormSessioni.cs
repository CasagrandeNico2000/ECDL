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

        private void FormSessioni_Load(object sender, EventArgs e)
        {
            Adatta_Panel_Delete_From("SELEZIONA RIGA...");
            Adatta_Panel_Delete_Until("NO SELEZIONE MULTIPLA");
            Aggiorna();
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
