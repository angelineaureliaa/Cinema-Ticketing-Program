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
    public partial class FormDaftarStudio : Form
    {
        public List<Studio> listStudio = new List<Studio>();
        public FormDaftarStudio()
        {
            InitializeComponent();
        }

        private void dataGridViewDaftarStudio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string kode = dataGridViewDaftarStudio.CurrentRow.Cells["Id"].Value.ToString();

            if (e.ColumnIndex == dataGridViewDaftarStudio.Columns["buttonUbahGrid"].Index)
            {
                MessageBox.Show("Ubah Data: " + " " + kode);
                FormUbahStudio frm = new FormUbahStudio();
                frm.Owner = this;
                frm.kodeUbah = kode;
                frm.ShowDialog();
            }
            else if (e.ColumnIndex == dataGridViewDaftarStudio.Columns["buttonHapusGrid"].Index)
            {
                MessageBox.Show("Hapus Data: " + " " + kode);
                Studio.HapusData(kode);
            }
        }

        private void FormDaftarStudio_Load(object sender, EventArgs e)
        {
            listStudio = Studio.BacaData("", "");
            if (listStudio.Count > 0)
            {
                //tampilkan ke datagrid view
                dataGridViewDaftarStudio.DataSource = listStudio;

                if (dataGridViewDaftarStudio.ColumnCount < 8)
                {
                    DataGridViewButtonColumn btnTambah = new DataGridViewButtonColumn();
                    btnTambah.Name = "buttonUbahGrid";//nama objek button
                    btnTambah.Text = "Ubah";//tulisan di button
                    btnTambah.UseColumnTextForButtonValue = true;//agar tulisan muncul di button
                    dataGridViewDaftarStudio.Columns.Add(btnTambah);//tambahkan button ke grid

                    DataGridViewButtonColumn btnHapus = new DataGridViewButtonColumn();
                    btnHapus.Name = "buttonHapusGrid";//nama objek button
                    btnHapus.Text = "Hapus";//tulisan di button
                    btnHapus.UseColumnTextForButtonValue = true;//agar tulisan muncul di button
                    dataGridViewDaftarStudio.Columns.Add(btnHapus);//tambahkan button ke grid
                }
                else
                {
                    dataGridViewDaftarStudio.DataSource = null;
                }
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahStudio form = new FormTambahStudio();
            form.Owner = this;
            form.Show();
        }

        private void textBoxKriteria_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxKriteria.Text == "Nama")
            {
                listStudio = Studio.BacaData("nama", textBoxKriteria.Text);
            }
            else if (comboBoxKriteria.Text == "Kapasitas")
            {
                listStudio = Studio.BacaData("kapasitas", textBoxKriteria.Text);
            }

            if (listStudio.Count > 0) //jika list kategori terisi data
            {
                dataGridViewDaftarStudio.DataSource = listStudio;
            }
            else
            {
                dataGridViewDaftarStudio.DataSource = null;
            }
        }
    }
}
