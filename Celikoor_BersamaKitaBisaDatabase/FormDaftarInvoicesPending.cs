using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Celikoor_LIB;

namespace Celikoor_BersamaKitaBisaDatabase
{
    public partial class FormDaftarInvoicesPending : Form
    {
        public List<Invoices> listInvoice = new List<Invoices>();
        public FormDaftarInvoicesPending()
        {
            InitializeComponent();
        }
        private void FormDaftarInvoicesPending_Load(object sender, EventArgs e)
        {
            listInvoice = Invoices.BacaDataPending();
            dataGridViewDaftarInvoices.DataSource = listInvoice;
            if (listInvoice.Count > 0)
            {
                if (dataGridViewDaftarInvoices.ColumnCount == 7)
                {
                    DataGridViewButtonColumn bcol2 = new DataGridViewButtonColumn();
                    bcol2.HeaderText = "Ubah Status";
                    bcol2.Text = "VALIDASI";
                    bcol2.Name = "buttonValidasiGrid";
                    bcol2.UseColumnTextForButtonValue = true;
                    dataGridViewDaftarInvoices.Columns.Add(bcol2);
                }
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewDaftarInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewDaftarInvoices.CurrentRow.Cells["Id"].Value.ToString();
            Invoices i = Invoices.AmbilDataByKode(id);
            if (i != null)
            {
                if (e.ColumnIndex == dataGridViewDaftarInvoices.Columns["buttonValidasiGrid"].Index)
                {
                    string kodeUbah = dataGridViewDaftarInvoices.CurrentRow.Cells["Id"].Value.ToString();
                    DialogResult hasil = MessageBox.Show(this, "Proses  validasi invoices" + kodeUbah + "?", "Konfirmasi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (hasil == DialogResult.Yes)
                    {
                        Invoices invoices = new Invoices();
                        Invoices.ValidasiInvoice(invoices);
                        FormDaftarInvoicesPending_Load(sender, e);
                    }
                }
            }
        }
    }
}
