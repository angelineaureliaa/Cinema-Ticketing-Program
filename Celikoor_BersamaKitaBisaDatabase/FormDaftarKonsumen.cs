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
    public partial class FormDaftarKonsumen : Form
    {
        public List<Konsumen> listKonsumen = new List<Konsumen>();
        DialogResult hasil;
        public FormDaftarKonsumen()
        {
            InitializeComponent();
        }

        private void FormDaftarKonsumen_Load(object sender, EventArgs e)
        {
            listKonsumen = Konsumen.BacaData("", "");
            dataGridViewDaftarKonsumen.DataSource = listKonsumen;
            comboBoxKriteria.SelectedIndex = 0;

            if (dataGridViewDaftarKonsumen.ColumnCount < 10)
            {
                //inisiasi object dengan tipe DataGridViewButtonColumn untuk membuat tombol 
                //pada kolom datagrid
                DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();

                //tentukan judul header dari kolom tombol
                bcol.HeaderText = "Aksi";

                //tentukan text dari tombol yang dibuat
                bcol.Text = "Ubah";

                //tentukan nama dari tombol yang dibuat
                bcol.Name = "btnUbahGrid";
                bcol.UseColumnTextForButtonValue = true;
                dataGridViewDaftarKonsumen.Columns.Add(bcol);

                DataGridViewButtonColumn bcol2 = new DataGridViewButtonColumn();
                bcol2.HeaderText = "Aksi";
                bcol2.Text = "Hapus";
                bcol2.Name = "buttonHapusGrid";
                bcol2.UseColumnTextForButtonValue = true;
                dataGridViewDaftarKonsumen.Columns.Add(bcol2);
            }
            else
            {
                dataGridViewDaftarKonsumen.DataSource = null;
            }
        }

        private void textBoxKriteria_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxKriteria.Text == "Nama")
                {
                    listKonsumen = Konsumen.BacaData("Nama", textBoxKriteria.Text);
                }
                else if (comboBoxKriteria.Text == "Email")
                {
                    listKonsumen = Konsumen.BacaData("Email", textBoxKriteria.Text);
                }
                else if (comboBoxKriteria.Text == "Gender")
                {
                    listKonsumen = Konsumen.BacaData("Gender", textBoxKriteria.Text);
                }
                else if (comboBoxKriteria.Text == "Username")
                {
                    listKonsumen = Konsumen.BacaData("Username", textBoxKriteria.Text);
                }

                if (listKonsumen.Count > 0)
                {
                    dataGridViewDaftarKonsumen.DataSource = listKonsumen;
                }
                else
                {
                    dataGridViewDaftarKonsumen.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pesan kesalahan : " + ex.Message, "Kesalahan!");
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahKonsumen frm = new FormTambahKonsumen();
            frm.Owner = this;
            frm.Show();
        }

        private void dataGridViewDaftarKonsumen_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //ambil kode kategori dari kolom dengan nama KodeKategori pada row yang sedang diklik
            string id = dataGridViewDaftarKonsumen.CurrentRow.Cells["id"].Value.ToString();
            //buat objek kategori baru untuk menampung hasil pengambilan data kategori sesuai kode yang dikirim
            Konsumen k = Konsumen.AmbilDataByKode(id);
            if (k != null)
            {
                if (e.ColumnIndex == dataGridViewDaftarKonsumen.Columns["btnUbahGrid"].Index)
                {
                    FormUbahKonsumen frm = new FormUbahKonsumen();
                    frm.Owner = this;
                    frm.labelID.Text = k.Id.ToString();
                    frm.textBoxNama.Text = k.Nama.ToString();
                    frm.textBoxEmail.Text = k.Email.ToString();
                    frm.textBoxNoHp.Text = k.No_hp.ToString();
                    frm.textBoxUsername.Text = k.Username.ToString();
                    frm.genderKonsumen = k.Gender.ToString();
                    frm.tanggalLahirKonsumen = k.Tgl_lahir;
                    frm.textBoxNama.Focus();
                    frm.Show();

                }
                //datagridview1.columns["nama"].index >> memeriksa tombol hapus yg di klik oleh user
                //e.rowindex >=0 >> memastikan baris yang di klik user ada datanya
                else if (e.ColumnIndex == dataGridViewDaftarKonsumen.Columns["buttonHapusGrid"].Index & e.RowIndex >= 0)
                {
                    string idHapus = dataGridViewDaftarKonsumen.CurrentRow.Cells["id"].Value.ToString();
                    string namaHapus = dataGridViewDaftarKonsumen.CurrentRow.Cells["nama"].Value.ToString();
                    string emailHapus = dataGridViewDaftarKonsumen.CurrentRow.Cells["email"].Value.ToString();
                    string noHpHapus = dataGridViewDaftarKonsumen.CurrentRow.Cells["no_hp"].Value.ToString();
                    string tglLahirHapus = dataGridViewDaftarKonsumen.CurrentRow.Cells["tgl_lahir"].Value.ToString();
                    string genderHapus = dataGridViewDaftarKonsumen.CurrentRow.Cells["gender"].Value.ToString();
                    string usernameHapus = dataGridViewDaftarKonsumen.CurrentRow.Cells["username"].Value.ToString();
                    string passwordHapus = dataGridViewDaftarKonsumen.CurrentRow.Cells["password"].Value.ToString();

                    //tampilkan konfirmasi
                    hasil = MessageBox.Show(this, "Anda yakin akan menghapus " + idHapus + "-" + namaHapus + " dengan username " +
                            usernameHapus
                        + "?", "HAPUS", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //Jika user memilih yes
                    if (hasil == DialogResult.Yes)
                    {
                        Konsumen konsumen = new Konsumen(int.Parse(idHapus), namaHapus, emailHapus, noHpHapus, genderHapus,
                            DateTime.Parse(tglLahirHapus),
                            usernameHapus, passwordHapus);
                        //panggil method hapus data
                        Boolean hapus = Konsumen.HapusData(konsumen);
                        if (hapus == true)
                        {
                            MessageBox.Show("Penghapusan data berhasil");
                            FormDaftarKonsumen_Load(sender, e);
                        }
                        else
                        {
                            MessageBox.Show("Penghapusan data gagal");
                        }
                    }
                }
            }
        }
    }
}
