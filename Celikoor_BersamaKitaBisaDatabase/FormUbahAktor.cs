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
    public partial class FormUbahAktor : Form
    {
        public Aktor a;
        public FormDaftarAktor frm;
        public FormUbahAktor()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                string gender = "";
                if (radioButtonMale.Checked)
                {
                    gender = "L";
                }
                else if (radioButtonFemale.Checked)
                {
                    gender = "P";
                }

                a = new Aktor(int.Parse(labelID.Text), textBoxNamaAktor.Text, dateTimePickerTanggalLahirAktor.Value, gender, textBoxNegaraAsal.Text);
                Aktor.UbahData(a);
                MessageBox.Show("Data Aktor berhasil diubah.", "Informasi");
                frm.dataGridViewDaftarAktor.DataSource = Aktor.BacaData("", "");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data aktor gagal diubah. Pesan kesalahan:" + ex.Message, "Kesalahan");
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxNamaAktor.Clear();
            dateTimePickerTanggalLahirAktor.Value = DateTime.Now;
            radioButtonMale.Checked = true;
            textBoxNegaraAsal.Clear();
            textBoxNamaAktor.Focus();
        }

        private void FormUbahAktor_Load(object sender, EventArgs e)
        {
            frm = (FormDaftarAktor)this.Owner;
        }
    }
}
