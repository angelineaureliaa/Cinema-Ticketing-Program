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
    public partial class FormDaftarJenisStudio : Form
    {
        List<Jenis_Studio> listJenisStudio = new List<Jenis_Studio>();
        public FormDaftarJenisStudio()
        {
            InitializeComponent();
        }

        private void FormDaftarJenisStudio_Load(object sender, EventArgs e)
        {
            listJenisStudio = Jenis_Studio.BacaData("", "");
            dataGridViewJenisStudio.DataSource = listJenisStudio;
            if (listJenisStudio.Count > 0)
            {
                if (dataGridViewJenisStudio.ColumnCount == 3)
                {
                    DataGridViewButtonColumn bcol2 = new DataGridViewButtonColumn();
                    bcol2.HeaderText = "Aksi";
                    bcol2.Text = "Hapus";
                    bcol2.Name = "buttonHapusGrid";
                    bcol2.UseColumnTextForButtonValue = true;
                    dataGridViewJenisStudio.Columns.Add(bcol2);
                }
            }
            else
            {
                dataGridViewJenisStudio.DataSource = null;
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahJenisStudio form = new FormTambahJenisStudio();
            form.Owner = this;
            form.Show();
        }

        private void dataGridViewJenisStudio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewJenisStudio.CurrentRow.Cells["id"].Value.ToString();
            if (e.ColumnIndex == dataGridViewJenisStudio.Columns["buttonHapusGrid"].Index && e.RowIndex >= 0)
            {
                string idHapus = dataGridViewJenisStudio.CurrentRow.Cells["id"].Value.ToString();
                string namaHapus = dataGridViewJenisStudio.CurrentRow.Cells["nama"].Value.ToString();
                string deskripsiHapus = dataGridViewJenisStudio.CurrentRow.Cells["deskripsi"].Value.ToString();

                DialogResult result = MessageBox.Show(this, "Yakin Hapus id =" + idHapus + " - " + namaHapus + "?"
                    , "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Jenis_Studio js = new Jenis_Studio(int.Parse(idHapus), namaHapus, deskripsiHapus);
                    Boolean hapus = Jenis_Studio.HapusData(js);
                    if (hapus == true)
                    {
                        MessageBox.Show("Berhasil hapus data");
                        FormDaftarJenisStudio_Load(sender, e);
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
