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
    public partial class FormDaftarKelompok : Form
    {
        public List<Kelompok> listKelompok = new List<Kelompok>();
        public FormDaftarKelompok()
        {
            InitializeComponent();
        }

        private void FormDaftarKelompok_Load(object sender, EventArgs e)
        {
            listKelompok = Kelompok.BacaData("","");
            if (listKelompok.Count > 0)
            {
                dataGridViewKelompok.DataSource = listKelompok;
                if (dataGridViewKelompok.ColumnCount == 2)
                {
                    DataGridViewButtonColumn buttonDeleteColumn = new DataGridViewButtonColumn();
                    buttonDeleteColumn.HeaderText = "Aksi";
                    buttonDeleteColumn.Text = "Delete";
                    buttonDeleteColumn.Name = "buttonHapusGrid";
                    buttonDeleteColumn.UseColumnTextForButtonValue = true;
                    dataGridViewKelompok.Columns.Add(buttonDeleteColumn);
                }
            }
            else
            {
                dataGridViewKelompok.DataSource = null;
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahKelompok form = new FormTambahKelompok();
            form.Owner = this;
            form.Show();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewKelompok_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string id = dataGridViewKelompok.CurrentRow.Cells["id"].Value.ToString();
            Kelompok k = Kelompok.AmbilDataByCode(id);
            if (e.ColumnIndex == dataGridViewKelompok.Columns["buttonHapusGrid"].Index && e.RowIndex >= 0)
            {
                string idHapus = dataGridViewKelompok.CurrentRow.Cells["id"].Value.ToString();
                string namaHapus = dataGridViewKelompok.CurrentRow.Cells["nama"].Value.ToString();

                DialogResult result = MessageBox.Show(this, "Yakin Hapus id =" + idHapus + " - " + namaHapus + "?"
                    , "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    Kelompok kel = new Kelompok(int.Parse(idHapus), namaHapus);
                    Boolean hapus = Kelompok.HapusData(kel);
                    if (hapus == true)
                    {
                        MessageBox.Show("Berhasil hapus data");
                        FormDaftarKelompok_Load(sender, e);
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
