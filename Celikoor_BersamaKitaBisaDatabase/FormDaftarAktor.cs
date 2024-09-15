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
    public partial class FormDaftarAktor : Form
    {
        public List<Aktor> listAktor = new List<Aktor>();
        public FormUbahAktor formUbahAktor;
        public FormDaftarAktor()
        {
            InitializeComponent();
        }

        private void textBoxKriteria_TextChanged(object sender, EventArgs e)
        {
            string kriteria = "";

            if (comboBoxKriteria.Text == "Nama")
            {
                kriteria = "nama";
            }
            else if (comboBoxKriteria.Text == "Gender")
            {
                kriteria = "gender";
            }

            listAktor = Aktor.BacaDataSatu(kriteria, textBoxKriteria.Text);

            if (listAktor.Count > 0)
            {
                dataGridViewDaftarAktor.DataSource = listAktor;
            }
            else
            {
                dataGridViewDaftarAktor.DataSource = null;
            }

            }

        private void dataGridViewDaftarAktor_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewDaftarAktor.CurrentRow.Cells["IdAktor"].Value.ToString();
            Aktor a = Aktor.AmbilDataByKode(id);
            if (a != null)
            {
                if (e.ColumnIndex == dataGridViewDaftarAktor.Columns["buttonUbahGrid"].Index)
                {
                    FormUbahAktor frm = new FormUbahAktor();
                    frm.Owner = this;
                    frm.labelID.Text = a.IdAktor.ToString();
                    frm.textBoxNamaAktor.Text = a.NamaAktor.ToString();
                    frm.dateTimePickerTanggalLahirAktor.Value = DateTime.Parse(a.TanggalLahirAktor.ToString());
                    frm.textBoxNegaraAsal.Text = a.NegaraAsalAktor.ToString();
                    frm.Show();
                }
                else if (e.ColumnIndex == dataGridViewDaftarAktor.Columns["buttonHapusGrid"].Index && e.RowIndex >= 0)
                {
                    string idHapus = dataGridViewDaftarAktor.CurrentRow.Cells["IdAktor"].Value.ToString();
                    string namaHapus = dataGridViewDaftarAktor.CurrentRow.Cells["NamaAktor"].Value.ToString();
                    string tanggalHapus = dataGridViewDaftarAktor.CurrentRow.Cells["TanggalLahirAktor"].Value.ToString();
                    string genderHapus = dataGridViewDaftarAktor.CurrentRow.Cells["GenderAktor"].Value.ToString();
                    string negaraHapus = dataGridViewDaftarAktor.CurrentRow.Cells["NegaraAsalAktor"].Value.ToString();

                    DialogResult result = MessageBox.Show(this, "Yakin Hapus id =" + idHapus + " - " + namaHapus + "?"
                        , "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        a = new Aktor(int.Parse(idHapus), namaHapus, DateTime.Parse(tanggalHapus), genderHapus, negaraHapus);
                        Boolean hapus = Aktor.HapusData(a);
                        if (hapus == true)
                        {
                            MessageBox.Show("Berhasil hapus data");
                            dataGridViewDaftarAktor.DataSource = Aktor.BacaData("", "");
                        }
                        else
                        {
                            MessageBox.Show("Gagal hapus data");
                        }
                    }
                }
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahAktor frm = new FormTambahAktor();
            frm.Owner = this;
            frm.Show();
        }

        public void FormDaftarAktor_Load(object sender, EventArgs e)
        {
            listAktor = Aktor.BacaData("", "");
            if (listAktor.Count >= 0)
            {
                dataGridViewDaftarAktor.DataSource = listAktor;
                if (dataGridViewDaftarAktor.ColumnCount == 5)
                {
                    DataGridViewButtonColumn bcol2 = new DataGridViewButtonColumn();
                    bcol2.HeaderText = "Aksi";
                    bcol2.Text = "Ubah";
                    bcol2.Name = "buttonUbahGrid";
                    bcol2.UseColumnTextForButtonValue = true;
                    dataGridViewDaftarAktor.Columns.Add(bcol2);

                    DataGridViewButtonColumn bcol3 = new DataGridViewButtonColumn();
                    bcol3.HeaderText = "Aksi";
                    bcol3.Text = "Hapus";
                    bcol3.Name = "buttonHapusGrid";
                    bcol3.UseColumnTextForButtonValue = true;
                    dataGridViewDaftarAktor.Columns.Add(bcol3);
                }
                else
                {
                    dataGridViewDaftarAktor.DataSource = null;
                }
            }
        }

        private void comboBoxKriteria_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
