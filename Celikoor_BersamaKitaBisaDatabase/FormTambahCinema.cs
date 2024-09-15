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
    public partial class FormTambahCinema : Form
    {
        List<Cinema> listCinema = new List<Cinema>();
        FormDaftarCinema frm;
        public FormTambahCinema()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if(textBoxNamaCabang.Text!="" && textBoxAlamat.Text!=""&&
                textBoxKota.Text != "")
            {
                listCinema = Cinema.BacaData("nama_cabang", textBoxNamaCabang.Text);
                if (listCinema.Count > 0)
                {
                    MessageBox.Show("Data cinema sudah tersedia", "Informasi");
                }
                else
                {
                    Cinema c = new Cinema(int.Parse(labelID.Text), textBoxNamaCabang.Text, textBoxAlamat.Text,
                                     dateTimePickerTanggalDibuka.Value, textBoxKota.Text);

                    Cinema.TambahData(c);

                    MessageBox.Show("Data cinema telah tersimpan. ", "Info");
                    //frm.dataGridViewCinema.DataSource = Cinema.BacaData("", "");
                    
                }
            }
            else
            {
                MessageBox.Show("Silahkan isi data yang belum diisi!", "Informasi");
            }
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxAlamat.Clear();
            textBoxKota.Clear();
            textBoxNamaCabang.Clear();
            textBoxNamaCabang.Focus();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTambahCinema_Load(object sender, EventArgs e)
        {
            frm = (FormDaftarCinema)this.Owner;
        }
    }
}
