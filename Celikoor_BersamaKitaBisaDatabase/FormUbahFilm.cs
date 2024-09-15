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
    public partial class FormUbahFilm : Form
    {
        public Film f;
        public List<Kelompok> listKelompok = new List<Kelompok>();
        public List<Genre> listGenre = new List<Genre>();
        public List<Aktor> listAktor = new List<Aktor>();
        public List<AktorFilm> listPemeran = new List<AktorFilm>();
        string fileName;
        string savePath;

        public FormUbahFilm()
        {
            InitializeComponent();
        }

        private void FormUbahFilm_Load(object sender, EventArgs e)
        {
            listKelompok = Kelompok.BacaData("", "");
            listGenre = Genre.BacaData("", "");
            listAktor = Aktor.BacaData("", "");
            comboBoxAktor.DataSource = listAktor;
            comboBoxGenre.DataSource = listGenre;
            comboBoxKelompok.DataSource = listKelompok;
            comboBoxAktor.DisplayMember="NamaAktor";
            comboBoxGenre.DisplayMember = "Nama";
            comboBoxKelompok.DisplayMember = "Nama";
            dataGridVieAktorPemeran.Columns["Film"].Visible = false;
            if (dataGridVieAktorPemeran.Columns.Count == 3)
            {
                DataGridViewButtonColumn bcol = new DataGridViewButtonColumn();

                //tentukan judul header dari kolom tombol
                bcol.HeaderText = "Aksi";

                //tentukan text dari tombol yang dibuat
                bcol.Text = "Ubah";

                //tentukan nama dari tombol yang dibuat
                bcol.Name = "btnUbahGrid";
                bcol.UseColumnTextForButtonValue = true;
                dataGridVieAktorPemeran.Columns.Add(bcol);
            }
            else
            {
                dataGridVieAktorPemeran.DataSource = null;
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            try
            {
                Kelompok selectedKelompok = (Kelompok)comboBoxKelompok.SelectedItem;
                Genre selectedGenre = (Genre)comboBoxGenre.SelectedItem;
                int subIndo = 0;
                if (comboBoxSubIndo.Text == "YES")
                {
                    subIndo = 1;
                }
                else if (comboBoxSubIndo.Text == "NO")
                {
                    subIndo = 0;
                }
                Film f = new Film(int.Parse(labelID.Text), textBoxJudul.Text, textBoxSinopsis.Text,
                    int.Parse(textBoxTahun.Text), int.Parse(textBoxDurasi.Text), selectedKelompok,
                    comboBoxBahasa.Text, subIndo, savePath, double.Parse(textBoxNominalDiskon.Text));
                Film.UbahData(f);
                GenreFilm gf = new GenreFilm(f, selectedGenre);
                GenreFilm.UbahData(gf);
                for(int i = 0; i < dataGridVieAktorPemeran.RowCount; i++)
                {
                    string namaAktor = dataGridVieAktorPemeran.Rows[i].Cells["Aktor"].Value.ToString();
                    string peranAktor = dataGridVieAktorPemeran.Rows[i].Cells["Peran"].Value.ToString();
                    List<Aktor> aktorDipilih = Aktor.BacaData("nama", namaAktor);
                    AktorFilm af = new AktorFilm(aktorDipilih[0], f, peranAktor);
                    AktorFilm.UbahData(af);
                }
                MessageBox.Show("Perubahan data film berhasil", "Informasi");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Pesan kesalahan : " + ex.Message, "Kesalahan");
            }
        }

        private void buttonKosong_Click(object sender, EventArgs e)
        {
            textBoxJudul.Clear();
            textBoxSinopsis.Clear();
            comboBoxGenre.SelectedIndex = -1;
            textBoxTahun.Clear();
            textBoxDurasi.Clear();
            comboBoxKelompok.SelectedIndex = -1;
            comboBoxBahasa.SelectedIndex = -1;
            comboBoxSubIndo.SelectedIndex = -1;
            dataGridVieAktorPemeran.Rows.Clear();
            dataGridVieAktorPemeran.Columns.Clear();
            comboBoxAktor.SelectedIndex = -1;
            comboBoxPeran.SelectedIndex = -1;
            textBoxNominalDiskon.Clear();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridVieAktorPemeran_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.ColumnIndex == dataGridVieAktorPemeran.Columns["btnUbahGrid"].Index)
            {
                groupBoxAktor.Enabled = true;
            }
        }

        private void buttonUbahAktor_Click(object sender, EventArgs e)
        {
            Aktor selectedAktor = (Aktor)comboBoxAktor.SelectedItem;
            for (int i=0; i < dataGridVieAktorPemeran.Rows.Count; i++)
            {
                if (selectedAktor.NamaAktor == dataGridVieAktorPemeran.Rows[i].Cells["Aktor"].Value.ToString())
                {
                    MessageBox.Show("Aktor tersebut sudah dipilih", "Kesalahan");
                }
                else
                {
                    dataGridVieAktorPemeran.CurrentRow.Cells["Aktor"].Value = selectedAktor;
                    dataGridVieAktorPemeran.CurrentRow.Cells["Peran"].Value = comboBoxPeran.Text;
                }
            }        
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path = "C:/Users/Asus/Poster/";
                    fileName = System.IO.Path.GetFileName(ofd.FileName);
                    System.IO.File.Copy(ofd.FileName, path + fileName);
                    savePath = path + fileName;
                    pictureBoxCoverImage.Image = Image.FromFile(ofd.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Kesalahan");
            }
        }
    }
}
