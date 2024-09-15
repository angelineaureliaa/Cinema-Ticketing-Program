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
    public partial class FormLaporan : Form
    {
        List<Laporan1> listLaporan1 = new List<Laporan1>();
        List<Laporan2> listLaporan2 = new List<Laporan2>();
        List<Laporan3> listLaporan3 = new List<Laporan3>();
        List<Laporan4> listLaporan4 = new List<Laporan4>();
        List<Laporan5> listLaporan5 = new List<Laporan5>();
        List<Laporan6> listLaporan6 = new List<Laporan6>();
        public FormLaporan()
        {
            InitializeComponent();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxKriteria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxKriteria.Text == "Laporan jumlah film terlaris per bulannya selama tahun 2023")
            {
                listLaporan1 = Laporan1.BacaData();
                dataGridViewLaporan.DataSource = listLaporan1;
            }
            else if (comboBoxKriteria.Text == "Laporan peringkat 3 cabang terbanyak yang mendapatkan pemasukkan dari penjualan tiket")
            {
                listLaporan2 = Laporan2.BacaData();
                dataGridViewLaporan.DataSource = listLaporan2;
            }
            else if (comboBoxKriteria.Text == "Laporan 3 teratas film yang paling banyak jumlah ketidakhadiran penontonnya")
            {
                listLaporan3 = Laporan3.BacaData();
                dataGridViewLaporan.DataSource = listLaporan3;
            }
            else if (comboBoxKriteria.Text == "Laporan 3 studio beserta nama cinemanya, yang memiliki tingkat utilitas terendah pada bulan tertentu")
            {
                listLaporan4 = Laporan4.BacaData();
                dataGridViewLaporan.DataSource = listLaporan4;
            }
            else if (comboBoxKriteria.Text == "Laporan 10 konsumen teratas yang paling sering menonton film bergenre comedy")
            {
                listLaporan5 = Laporan5.BacaData();
                dataGridViewLaporan.DataSource = listLaporan5;
            }
            else if (comboBoxKriteria.Text == "Laporan jumlah film per genre")
            {
                listLaporan6 = Laporan6.BacaData();
                dataGridViewLaporan.DataSource = listLaporan6;
            }
        }

        private void FormLaporan_Load(object sender, EventArgs e)
        {

        }
    }
}
