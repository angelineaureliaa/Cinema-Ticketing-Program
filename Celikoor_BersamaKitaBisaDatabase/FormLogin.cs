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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Koneksi koneksi = new Koneksi();
                if (comboBoxPengguna.Text == "Konsumen")
                {
                    Konsumen k = Konsumen.CekLogin(textBoxUsn.Text, textBoxPwd.Text);
                    if (!(k is null))
                    {
                        FormUtama frmUtama = (FormUtama)this.Owner;
                        frmUtama.konsumenLogin = k;
                        MessageBox.Show("Koneksi berhasil");
                        MessageBox.Show("Selamat Datang " + k.Nama);
                        this.DialogResult = DialogResult.OK;
                        this.Close();//tutup form login
                    }
                    else
                    {
                        MessageBox.Show(this, "Username atau password Anda salah");
                    }
                }

                else
                {
                    if (comboBoxPengguna.Text == "Pegawai")
                    {
                        Pegawai p = Pegawai.CekLogin(textBoxUsn.Text, textBoxPwd.Text);
                        if (!(p is null))
                        {
                            FormUtama frmUtama = (FormUtama)this.Owner;
                            frmUtama.pegawaiLogin = p;
                            MessageBox.Show("Koneksi berhasil");
                            MessageBox.Show("Selamat Datang " + p.Nama);
                            this.DialogResult = DialogResult.OK;
                            this.Close();//tutup form login
                        }
                        else
                        {
                            MessageBox.Show(this, "Username atau password Anda salah");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Koneksi gagal. Pesan kesalahan : " + ex.Message, "Kesalahan");
            }
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
