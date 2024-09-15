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
    public partial class FormTambahPegawai : Form
    {
        List<Pegawai> listPegawai = new List<Pegawai>();
        FormDaftarPegawai frm;
        public FormTambahPegawai()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxNama.Text == "")
                {
                    MessageBox.Show("Silahkan isi nama Anda", "Informasi");
                }
                if (textBoxEmail.Text == "")
                {
                    MessageBox.Show("Silahkan isi email Anda", "Informasi");
                }
                if (textBoxPassword.Text == "")
                {
                    MessageBox.Show("Silahkan isi password Anda", "Informasi");
                }
                if (textBoxUlang.Text == "")
                {
                    MessageBox.Show("Silahkan isi ulang password Anda", "Informasi");
                }
                if (textBoxUsername.Text == "")
                {
                    MessageBox.Show("Silahkan isi username Anda", "Informasi");
                }
                if (comboBoxRoles.Text == "")
                {
                    MessageBox.Show("Silahkan pilih roles Anda", "Informasi");
                }
                if (textBoxPassword.Text == textBoxUlang.Text)
                {
                    Pegawai p = new Pegawai(int.Parse(labelID.Text), textBoxNama.Text, textBoxEmail.Text,
                        textBoxUsername.Text, textBoxPassword.Text, comboBoxRoles.Text);
                    listPegawai = Pegawai.BacaData("username", textBoxUsername.Text);
                    if (Pegawai.CekPegawai("username", textBoxUsername.Text) == false)
                    {
                        MessageBox.Show("Username sudah digunakan");
                    }
                    else
                    {
                        Pegawai.TambahData(p);
                        MessageBox.Show("Selamat, data Anda berhasil ditambahkan", "Informasi");
                        frm.dataGridViewDaftarPegawai.DataSource = Pegawai.BacaData("", "");
                    }
                }
                else
                {
                    MessageBox.Show("Ulangi password. Password Anda tidak sama dengan yang diisikan ulang.", "Kesalahan");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Kesalahan");
            }
        }

        private void FormTambahPegawai_Load(object sender, EventArgs e)
        {
            frm = (FormDaftarPegawai)this.Owner;
        }
    }
}
