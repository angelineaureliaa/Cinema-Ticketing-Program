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
    public partial class FormUbahKonsumen : Form
    {
        public string genderKonsumen = "";
        public DateTime tanggalLahirKonsumen = DateTime.Now.Date;
        public FormUbahKonsumen()
        {
            InitializeComponent();
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxPassword.Text == textBoxUlang.Text)
                {
                    Konsumen k = new Konsumen(int.Parse(labelID.Text), textBoxNama.Text, textBoxEmail.Text, textBoxNoHp.Text, 
                                              genderKonsumen, tanggalLahirKonsumen, textBoxUsername.Text, textBoxPassword.Text);
                    if (Konsumen.CekKonsumen("username", textBoxUsername.Text) == false)
                    {
                        MessageBox.Show("Username sudah digunakan");
                    }
                    else
                    {
                        //Konsumen.UbahData(k);
                        MessageBox.Show("Selamat, data Anda berhasil diubah");
                    }
                }
                else
                {
                    MessageBox.Show("Ulangi password. Password yang diisi berbeda dengan yang diulang", "Perhatian!");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Username sudah digunakan.");
            }
        }

        private void FormUbahKonsumen_Load(object sender, EventArgs e)
        {

        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxNama.Clear();
            textBoxEmail.Clear();
            textBoxNoHp.Clear();
            textBoxUsername.Clear();
            textBoxPassword.Clear();
            textBoxUlang.Clear();
        }
    }
}
