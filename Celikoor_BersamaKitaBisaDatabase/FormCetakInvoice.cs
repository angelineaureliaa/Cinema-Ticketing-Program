using Celikoor_LIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Celikoor_BersamaKitaBisaDatabase
{
    public partial class FormCetakInvoice : Form
    {
        public FormCetakInvoice()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Invoices> listInvoiceTerbayar = new List<Invoices>();
            listInvoiceTerbayar = Invoices.BacaDataTerbayar();
            dataGridViewDaftarInvoices.DataSource = listInvoiceTerbayar;
            if (listInvoiceTerbayar.Count > 0)
            {
                if (dataGridViewDaftarInvoices.ColumnCount == 7)
                {
                    DataGridViewButtonColumn bcol2 = new DataGridViewButtonColumn();
                    bcol2.HeaderText = "Cetak Invoices";
                    bcol2.Text = "CETAK";
                    bcol2.Name = "buttonCetak";
                    bcol2.UseColumnTextForButtonValue = true;
                    dataGridViewDaftarInvoices.Columns.Add(bcol2);
                }
                dataGridViewDaftarInvoices.Columns["Kasir_Id"].Visible = false;
                dataGridViewDaftarInvoices.Columns["Konsumen_Id"].Visible = false;
            }
        }

        private void dataGridViewDaftarInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewDaftarInvoices.CurrentRow.Cells["Id"].Value.ToString();
            Invoices i = Invoices.AmbilDataByKode(id);
            if (i != null)
            {
                if (e.ColumnIndex == dataGridViewDaftarInvoices.Columns["buttonCetak"].Index)
                {
                    Tiket.CetakTiket(i.Id);
                }
            }
        }
    }
}
