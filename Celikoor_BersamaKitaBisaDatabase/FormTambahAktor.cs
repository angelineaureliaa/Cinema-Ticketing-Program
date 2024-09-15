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
    
    public partial class FormTambahAktor : Form
    {
        List<Aktor> listAktor = new List<Aktor>();
        string gender;
        FormDaftarAktor frm;
        public FormTambahAktor()
        {
            InitializeComponent();
        }

        private void FormTambahAktor_Load(object sender, EventArgs e)
        {
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxNamaAktor.Clear();
            textBoxNegaraAsal.Clear();
            radioButtonFemale.Checked = false;
            radioButtonMale.Checked = false;
            textBoxNamaAktor.Focus();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonMale.Checked == true)
                {
                    gender = "L";
                }
                else if (radioButtonFemale.Checked == true)
                {
                    gender = "P";
                }
                else
                {
                    MessageBox.Show("Silahkan pilih jenis kelamin Aktor", "Informasi");
                }

                //PENGECEKAN DATA
                if (textBoxNamaAktor.Text != "" && textBoxNegaraAsal.Text != "" && gender != "")
                {
                    listAktor = Aktor.BacaData("nama", textBoxNamaAktor.Text + "' and gender='" + gender + "' and " +
                        "tgl_lahir='" + dateTimePickerTanggalLahirAktor.Value.ToString("yyyy/MM/dd"));
                    if (listAktor.Count > 0)
                    {
                        MessageBox.Show("Data Aktor sudah tersimpan.", "Informasi");
                    }
                    else
                    {
                        Aktor a = new Aktor(int.Parse(labelID.Text), textBoxNamaAktor.Text, dateTimePickerTanggalLahirAktor.Value, gender,
                            textBoxNegaraAsal.Text);
                        Aktor.TambahData(a);
                        MessageBox.Show("Data aktor berhasil ditambahkan.", "Informasi");
                        //frm.dataGridViewDaftarAktor.DataSource = Aktor.BacaData("", "");
                    }
                }
                else
                {
                    MessageBox.Show("Silahkan lengkapi data yang belum diisi.", "Informasi");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Informasi");
            }
        }

        private void FormTambahAktor_Load_1(object sender, EventArgs e)
        {
            frm = (FormDaftarAktor)this.Owner;
        }
    }
}
