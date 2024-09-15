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
    public partial class FormScanTiket : Form
    {
        public FormScanTiket()
        {
            InitializeComponent();
        }
        string invoiceId;
        string kodeKursi;
        private void textBoxNamaGenre_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string barcode = textBoxBarcode.Text;
                if (barcode.Length == 6)
                {
                    invoiceId = barcode.Substring(0,3);
                    kodeKursi = barcode.Substring(3,3);
                    int invoice = int.Parse(invoiceId.TrimStart('0'));
                    string kKursi = kodeKursi[1] == '0' ? kodeKursi.Substring(0, 1)+kodeKursi.Substring(2,1) : kodeKursi;
                    //MessageBox.Show(invoice.ToString());
                    //MessageBox.Show(kKursi);
                    Tiket.UbahStatusTiket(invoice, kKursi);
                    MessageBox.Show("Done");
                    textBoxBarcode.Clear();
                }
            }
            catch(Exception m)
            {
                MessageBox.Show("Gagal update, Error :" + m.Message);
            }
        }

        private void FormScanTiket_Load(object sender, EventArgs e)
        {

        }
    }
}
