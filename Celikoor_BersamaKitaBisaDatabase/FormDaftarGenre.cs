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
    public partial class FormDaftarGenre : Form
    {
        List<Genre>listGenre = new List<Genre>();
        public FormDaftarGenre()
        {
            InitializeComponent();
        }

        private void FormDaftarGenre_Load(object sender, EventArgs e)
        {
            listGenre = Genre.BacaData("", "");
            dataGridViewGenre.DataSource = listGenre;
            if (listGenre.Count >= 0)
            {
                if (dataGridViewGenre.ColumnCount == 3)
                {
                    DataGridViewButtonColumn bcol2 = new DataGridViewButtonColumn();
                    bcol2.HeaderText = "Aksi";
                    bcol2.Text = "Hapus";
                    bcol2.Name = "buttonHapusGrid";
                    bcol2.UseColumnTextForButtonValue = true;
                    dataGridViewGenre.Columns.Add(bcol2);
                }
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahGenre form = new FormTambahGenre();
            form.Owner = this;
            form.Show();
        }

        private void dataGridViewGenre_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewGenre.CurrentRow.Cells["id"].Value.ToString();
            Genre g = Genre.AmbilDataByCode(id);
            if (e.ColumnIndex == dataGridViewGenre.Columns["buttonHapusGrid"].Index && e.RowIndex >= 0)
            {
                string idHapus = dataGridViewGenre.CurrentRow.Cells["id"].Value.ToString();
                string namaHapus = dataGridViewGenre.CurrentRow.Cells["nama"].Value.ToString();
                string deskripsiHapus = dataGridViewGenre.CurrentRow.Cells["deskripsi"].Value.ToString();

                DialogResult result = MessageBox.Show(this, "Yakin Hapus id =" + idHapus + " - " + namaHapus + "?"
                    , "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Genre genre = new Genre(int.Parse(idHapus), namaHapus, deskripsiHapus);
                    Boolean hapus = Genre.HapusData(genre);
                    if (hapus == true)
                    {
                        MessageBox.Show("Berhasil hapus data");
                        FormDaftarGenre_Load(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("Gagal hapus data");
                    }
                }
            }
        }
    }
}
