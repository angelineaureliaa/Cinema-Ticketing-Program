using Celikoor_LIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Celikoor_BersamaKitaBisaDatabase
{
    public partial class FormDaftarPegawai : Form
    {
        public List<Pegawai> listPegawai = new List<Pegawai>();
        public FormDaftarPegawai()
        {
            InitializeComponent();
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormRegisterPegawai"];
            if (form == null)
            {
                FormTambahPegawai frm = new FormTambahPegawai();
                frm.Owner = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        public void FormDaftarPegawai_Load(object sender, EventArgs e)
        {
            listPegawai = Pegawai.BacaData("", "");
            if (listPegawai.Count > 0)
            {
                //tampilkan ke datagrid view
                dataGridViewDaftarPegawai.DataSource = listPegawai;

                if (dataGridViewDaftarPegawai.ColumnCount < 7)
                {
                    //inisiasi object dengan tipe DataGridViewButtonColumn untuk membuat tombol
                    //pada kolom datagrid
                    DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();

                    // tentukan judul header dari kolom tombol
                    bcol.HeaderText = "Aksi";

                    DataGridViewButtonColumn bcol2 = new DataGridViewButtonColumn();
                    bcol2.HeaderText = "Aksi";
                    bcol2.Text = "Hapus";
                    bcol2.Name = "buttonHapusGrid";
                    bcol2.UseColumnTextForButtonValue = true;
                    dataGridViewDaftarPegawai.Columns.Add(bcol2);
                }
            }
            else
            {
                dataGridViewDaftarPegawai.DataSource = null;
            }
        }

        private void dataGridViewDaftarPegawai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string pKodePegawai = dataGridViewDaftarPegawai.CurrentRow.Cells["ID"].Value.ToString();

            Pegawai p = Pegawai.AmbilDataByKode(pKodePegawai);
            if (p != null)
            {
                if (e.ColumnIndex == dataGridViewDaftarPegawai.Columns["buttonHapusGrid"].Index & e.RowIndex >= 0)
                {
                    int kodeHapus = int.Parse(dataGridViewDaftarPegawai.CurrentRow.Cells["Id"].Value.ToString());
                    string namaHapus = dataGridViewDaftarPegawai.CurrentRow.Cells["Nama"].Value.ToString();
                    string emailHapus = dataGridViewDaftarPegawai.CurrentRow.Cells["Email"].Value.ToString();
                    string usernameHapus = dataGridViewDaftarPegawai.CurrentRow.Cells["Username"].Value.ToString();
                    string password = dataGridViewDaftarPegawai.CurrentRow.Cells["Password"].Value.ToString();
                    string rolesHapus = dataGridViewDaftarPegawai.CurrentRow.Cells["Roles"].Value.ToString();

                    //tampilkan konfirmasi
                    DialogResult hasil = MessageBox.Show(this, "Anda yakin akan menghapus " + kodeHapus + "-" + namaHapus
                        + "?", "HAPUS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //Jika user memilih yes
                    if (hasil == DialogResult.Yes)
                    {
                        Pegawai pegawaiHps = new Pegawai(kodeHapus, namaHapus, emailHapus, usernameHapus, password, rolesHapus);
                        //panggil method hapus data
                        Boolean hapus = Pegawai.HapusData(pegawaiHps);
                        if (hapus == true)
                        {
                            MessageBox.Show("Data pegawai berhasil dihapus");
                            
                        }
                        else
                        {
                            MessageBox.Show("Data pegawai gagal terhapus. Coba lagi.");
                        }
                    }
                    FormDaftarPegawai_Load(sender, e);
                }


            }
            else
            {
                MessageBox.Show("Terdapat kesalahan pada data");
            }
        }

        private void textBoxKriteria_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxKriteria.Text == "Kode")
            {
                listPegawai = Pegawai.BacaData("id", textBoxKriteria.Text);
            }
            else if (comboBoxKriteria.Text == "Nama")
            {
                listPegawai = Pegawai.BacaData("Nama", textBoxKriteria.Text);
            }
            else if (comboBoxKriteria.Text == "Username")
            {
                listPegawai = Pegawai.BacaData("username", textBoxKriteria.Text);
            }

            if (listPegawai.Count > 0)
            {
                dataGridViewDaftarPegawai.DataSource = listPegawai;
            }
            else
            {
                dataGridViewDaftarPegawai.DataSource = null;
            }
        }
    }
}
