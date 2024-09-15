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
    public partial class FormTambahGenre : Form
    {
        List<Genre> listGenre = new List<Genre>();
        FormDaftarGenre frm;
        public FormTambahGenre()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (textBoxNamaGenre.Text == "")
            {
                MessageBox.Show("Silahkan isi data genre", "Informasi");
            }
            else
            {
                //cek apakah genre sudah tersimpan atau belum
                listGenre = Genre.BacaData("nama", textBoxNamaGenre.Text);
                if (listGenre.Count == 0)
                {
                    Genre g = new Genre(int.Parse(labelID.Text), textBoxNamaGenre.Text, textBoxDeskripsi.Text);
                    Genre.TambahData(g);
                    MessageBox.Show("Data genre baru berhasil ditambahkan", "Informasi");
                    //frm.dataGridViewGenre.DataSource = Genre.BacaData("", "");
                }
                else
                {
                    MessageBox.Show("Data genre sudah tersedia", "Informasi");
                }
            }
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxNamaGenre.Clear();
            textBoxDeskripsi.Clear();
            textBoxNamaGenre.Focus();
        }

        private void FormTambahGenre_Load(object sender, EventArgs e)
        {
            frm = (FormDaftarGenre)this.Owner;
        }
    }
}
