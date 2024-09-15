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
    public partial class FormDaftarCinema : Form
    {
        List<Cinema> listCinema = new List<Cinema>();
        public FormDaftarCinema()
        {
            InitializeComponent();
        }

        private void FormDaftarCinema_Load(object sender, EventArgs e)
        {
            listCinema = Cinema.BacaData("","");
            dataGridViewCinema.DataSource = listCinema;
            if (listCinema.Count >= 0)
            {
                if (dataGridViewCinema.ColumnCount == 5)
                {
                    DataGridViewButtonColumn bcol2 = new DataGridViewButtonColumn();
                    bcol2.HeaderText = "Aksi";
                    bcol2.Text = "Hapus";
                    bcol2.Name = "buttonHapusGrid";
                    bcol2.UseColumnTextForButtonValue = true;
                    dataGridViewCinema.Columns.Add(bcol2);
                }
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahCinema form = new FormTambahCinema();
            form.Owner = this;
            form.Show();
        }

        private void dataGridViewCinema_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewCinema.CurrentRow.Cells["id"].Value.ToString();
            Cinema c = Cinema.AmbilDataByCode(id);
            if (e.ColumnIndex == dataGridViewCinema.Columns["buttonHapusGrid"].Index && e.RowIndex >= 0)
            {
                string idHapus = dataGridViewCinema.CurrentRow.Cells["id"].Value.ToString();
                string namaHapus = dataGridViewCinema.CurrentRow.Cells["nama_cabang"].Value.ToString();
                string alamat = dataGridViewCinema.CurrentRow.Cells["alamat"].Value.ToString();
                string tanggal = dataGridViewCinema.CurrentRow.Cells["tgl_dibuka"].Value.ToString();
                string kota = dataGridViewCinema.CurrentRow.Cells["kota"].Value.ToString();

                DialogResult result = MessageBox.Show(this, "Yakin Hapus id =" + idHapus + " - " + namaHapus + "?"
                    , "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Cinema cin = new Cinema(int.Parse(idHapus), namaHapus, alamat, DateTime.Parse(tanggal), kota);
                    Boolean hapus = Cinema.HapusData(cin);
                    if (hapus == true)
                    {
                        MessageBox.Show("Berhasil hapus data");
                        dataGridViewCinema.DataSource = Cinema.BacaData("", "");
                    }
                    else
                    {
                        MessageBox.Show("Gagal hapus data");
                    }
                }
            }
        }
        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
