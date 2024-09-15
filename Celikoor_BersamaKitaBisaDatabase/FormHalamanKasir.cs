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
    public partial class FormHalamanKasir : Form
    {
        FormUtama frmUtama;
        public FormHalamanKasir()
        {
            InitializeComponent();
        }

        private void FormHalamanKasir_Load(object sender, EventArgs e)
        {
            frmUtama = (FormUtama)this.MdiParent;
            textBoxNama.Text = frmUtama.pegawaiLogin.Nama;
            textBoxEmail.Text = frmUtama.pegawaiLogin.Email;
            textBoxUsername.Text = frmUtama.pegawaiLogin.Username;
        }
    }
}
