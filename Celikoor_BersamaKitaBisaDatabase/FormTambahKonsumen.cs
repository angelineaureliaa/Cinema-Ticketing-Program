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
    public partial class FormTambahKonsumen : Form
    {
        List<Konsumen> listKonsumen = new List<Konsumen>();
        string gender;
        FormDaftarKonsumen frm;
        public FormTambahKonsumen()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonFemale.Checked == true)
                {
                    gender = "P";
                }
                else if (radioButtonMale.Checked == false)
                {
                    gender = "L";
                }
                else
                {
                    MessageBox.Show("Silahkan pilih jenis kelamin Anda", "Informasi");
                }

                if (textBoxNama.Text == "")
                {
                    MessageBox.Show("Silahkan isi nama Anda", "Informasi");
                }
                if (textBoxEmail.Text == "")
                {
                    MessageBox.Show("Silahkan isi email Anda", "Informasi");
                }
                if (textBoxNoHp.Text == "")
                {
                    MessageBox.Show("Silahkan isi nomor HP Anda", "Informasi");
                }
                if (textBoxUsername.Text == "")
                {
                    MessageBox.Show("Silahkan isi username Anda", "Informasi");
                }
                if (textBoxPassword.Text == "")
                {
                    MessageBox.Show("Silahkan isi password Anda", "Informasi");
                }
                if (textBoxUlang.Text == "")
                {
                    MessageBox.Show("Silahkan isi kembali password Anda", "Informasi");
                }

                if (textBoxPassword.Text == textBoxUlang.Text)
                {
                    //Konsumen k = new Konsumen(int.Parse(labelID.Text), textBoxNama.Text, textBoxEmail.Text, textBoxNoHp.Text, gender,
                      //  dateTimePickerTglLahir.Value, textBoxUsername.Text, textBoxPassword.Text);
                    listKonsumen = Konsumen.BacaData("username", textBoxUsername.Text);
                    if (Konsumen.CekKonsumen("username", textBoxUsername.Text) == false)
                    {
                        MessageBox.Show("Username sudah digunakan", "Informasi");
                    }
                    else
                    {
                        //Konsumen.TambahData(k);
                        MessageBox.Show("Selamat, registrasi Anda berhasil", "Informasi");
                        //frm.dataGridViewDaftarKonsumen.DataSource = Konsumen.BacaData("", "");
                    }
                }
                else
                {
                    MessageBox.Show("Ulangi password. Password yang diisi berbeda dengan yang diulang", "Kesalahan");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Kesalahan");
            }
        }

        private void FormTambahKonsumen_Load(object sender, EventArgs e)
        {
            frm = (FormDaftarKonsumen)this.Owner;
        }
    }
}
