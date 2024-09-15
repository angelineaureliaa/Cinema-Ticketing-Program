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
    public partial class FormHalamanKonsumen : Form
    {
        FormUtama frmUtama;
        public FormHalamanKonsumen()
        {
            InitializeComponent();
        }
        
        private void FormHalamanKonsumen_Load(object sender, EventArgs e)
        {
            frmUtama = (FormUtama)this.MdiParent;
            textBoxNama.Text = frmUtama.konsumenLogin.Nama;
            textBoxEmail.Text = frmUtama.konsumenLogin.Email;
            textBoxNoHp.Text = frmUtama.konsumenLogin.No_hp;
            labelGender.Text = frmUtama.konsumenLogin.Gender;
            labelTanggal.Text = frmUtama.konsumenLogin.Tgl_lahir.ToString();
            textBoxUsername.Text = frmUtama.konsumenLogin.Username;
        }
    }
}
