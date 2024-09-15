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
    public partial class FormTambahStudio : Form
    {
        public Jenis_Studio jS;
        public Cinema cinema;
        public List<Cinema> listCinema = new List<Cinema>();
        public List<Jenis_Studio> listJStud = new List<Jenis_Studio>();
        public FormTambahStudio()
        {
            InitializeComponent();
        }

        private void FormTambahStudio_Load(object sender, EventArgs e)
        {
            listCinema = Cinema.BacaData("","");
            comboBoxCinema.DataSource = listCinema;
            comboBoxCinema.DisplayMember = "Nama_cabang";

            listJStud = Jenis_Studio.BacaData("", "");
            comboBoxJenisStud.DataSource = listJStud;
            comboBoxJenisStud.DisplayMember = "Nama";

            textBoxNama.Focus();
        }

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
                Studio s = new Studio(int.Parse(labelID.Text), textBoxNama.Text, int.Parse(textBoxKapasitas.Text), jS, cinema, int.Parse(textBoxWeekdays.Text), int.Parse(textBoxWeekend.Text));
                MessageBox.Show(s.Id.ToString());
                MessageBox.Show(s.JenisStudio.Id.ToString());
                MessageBox.Show(s.Cinema.Id.ToString());
                Studio.TambahData(s);
                MessageBox.Show("Data Anda berhasil ditambahkan");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Penyimpanan gagal. Pesan kesalahan : " + ex.Message, "Kesalahan");
            }
}

        private void buttonKosongi_Click(object sender, EventArgs e)
        {
            textBoxNama.Clear();
            textBoxKapasitas.Clear();
            textBoxWeekdays.Clear();
            textBoxWeekend.Clear();
            textBoxNama.Focus();
            comboBoxCinema.SelectedIndex = -1;
            comboBoxJenisStud.SelectedIndex = -1;
        }
    }
}
