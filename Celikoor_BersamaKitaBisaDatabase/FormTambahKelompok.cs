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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Celikoor_BersamaKitaBisaDatabase
{
    public partial class FormTambahKelompok : Form
    {
        List<Kelompok> listKelompok = new List<Kelompok>();
        FormDaftarKelompok frm;
        public FormTambahKelompok()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxNamaKelompok.Text == "")
                {
                    MessageBox.Show("Silahkan isi kelompok umur", "Informasi");
                }
                else
                {
                    //CEK APAKAH DATA KELOMPOK SUDAH ADA
                    listKelompok = Kelompok.BacaData("nama", textBoxNamaKelompok.Text);
                    if (listKelompok.Count > 0)
                    {
                        MessageBox.Show("Kelompok sudah tersedia", "Informasi");
                    }
                    else
                    {
                        Kelompok k = new Kelompok(int.Parse(labelID.Text), textBoxNamaKelompok.Text);
                        Kelompok.TambahData(k);
                        MessageBox.Show("Data kelompok baru berhasil ditambahkan.",
                            "Informasi");
                        //frm.dataGridViewKelompok.DataSource = Kelompok.BacaData("", "");
                    }
                }          
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxNamaKelompok.Clear();
            textBoxNamaKelompok.Focus();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormTambahKelompok_Load(object sender, EventArgs e)
        {
            frm = (FormDaftarKelompok)this.Owner;
        }
    }
}
