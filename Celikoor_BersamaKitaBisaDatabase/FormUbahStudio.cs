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
    public partial class FormUbahStudio : Form
    {
        public FormUbahStudio()
        {
            InitializeComponent();
        }

        public string kodeUbah;

        public Jenis_Studio jS;
        public Cinema cinema;
        public List<Cinema> listCinema = new List<Cinema>();
        public List<Jenis_Studio> listJStud = new List<Jenis_Studio>();
        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Jenis_Studio j in listJStud)
                {
                    if (j.Nama == comboBoxJenisStud.Text)
                    {
                        jS = j;
                    }
                }
                foreach (Cinema c in listCinema)
                {
                    if (c.Nama_cabang == comboBoxCinema.Text)
                    {
                        cinema = c;
                    }
                }
                Studio studioUbah = new Studio(int.Parse(labelID.Text), textBoxNama.Text, int.Parse(textBoxKapasitas.Text), jS, cinema, int.Parse(textBoxWeekdays.Text), int.Parse(textBoxWeekend.Text));
                Studio.UbahData(studioUbah, kodeUbah);

                MessageBox.Show("Data studio telah diubah.", "Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pengubahan gagal. Pesan kesalaha : " + ex.Message, "Kesalahan");
            }
        }

        private void FormUbahStudio_Load(object sender, EventArgs e)
        {
            listCinema = Cinema.BacaData("","");
            comboBoxCinema.DataSource = listCinema;
            comboBoxCinema.DisplayMember = "Nama_cabang";

            listJStud = Jenis_Studio.BacaData("", "");
            comboBoxJenisStud.DataSource = listJStud;
            comboBoxJenisStud.DisplayMember = "Nama";
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
            //FormDaftarStudio frm = (FormDaftarStudio)this.Owner;
            //frm.FormDaftarStudio_Load(this, e);
            //this.Close();
        }

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxNama.Clear();
            textBoxKapasitas.Clear();
            comboBoxJenisStud.SelectedIndex = -1;
            comboBoxCinema.SelectedIndex = -1;
            textBoxWeekdays.Clear();
            textBoxWeekend.Clear();
        }
    }
}
