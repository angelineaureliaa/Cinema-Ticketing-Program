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
    public partial class FormTambahJenisStudio : Form
    {
        List<Jenis_Studio> listJenisStudio = new List<Jenis_Studio>();
        FormDaftarJenisStudio frm;
        public FormTambahJenisStudio()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            if (textBoxNama.Text == "")
            {
                MessageBox.Show("Silahkan isi nama jenis studio!", "Informasi");
            }
            else
            {
                Jenis_Studio js = new Jenis_Studio(int.Parse(labelID.Text), textBoxNama.Text, textBoxdesk.Text);
                //cek apakah data jenis_studio sudah ada
                listJenisStudio = Jenis_Studio.BacaData("nama", textBoxNama.Text);

                if (listJenisStudio.Count == 0)
                {
                    Jenis_Studio.TambahData(js);
                    MessageBox.Show("Data jenis studio baru berhasil ditambahkan", "Informasi");
                    //frm.dataGridViewJenisStudio.DataSource = Jenis_Studio.BacaData("", "");

                }
                else
                {
                    MessageBox.Show("Data jenis studio sudah tersedia");
                }
            }   
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxNama.Clear();
            textBoxdesk.Clear();
            textBoxNama.Focus();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTambahJenisStudio_Load(object sender, EventArgs e)
        {
            frm = (FormDaftarJenisStudio)this.Owner;
        }
    }
}
