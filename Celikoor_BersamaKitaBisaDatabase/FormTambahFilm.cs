using Celikoor_BersamaKitaBisaDatabase.Properties;
using Celikoor_LIB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Celikoor_BersamaKitaBisaDatabase
{
    public partial class FormTambahFilm : Form
    {
        string fileName;
        string savePath;
        List<Kelompok> listKelompok = new List<Kelompok>();
        List<Aktor> listAktor = new List<Aktor>();
        List<Genre> listGenre = new List<Genre>();
        Kelompok selectedKelompok;
        GenreFilm genreFilm;
        List<Film> filmBaru = new List<Film>();
        Genre selectedGenre;
        List<Aktor> listAktorFilm = new List<Aktor>();
        Aktor selectedAktor;
        AktorFilm af;
        List<string> listPeran = new List<string>();
        public FormTambahFilm()
        {
            InitializeComponent();
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "jpg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All Files (*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string path = "D:/LINE/DATBES TERBARU/Database/";
                    fileName = System.IO.Path.GetFileName(ofd.FileName);
                    System.IO.File.Copy(ofd.FileName, path + fileName);
                    savePath = path + fileName;

                    pictureBoxCoverImage.Image = Image.FromFile(ofd.FileName);
                }
                else
                {
                    savePath = "";
                    pictureBoxCoverImage.Image = null;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Kesalahan");
            }
        }

        private void FormTambahFilm_Load(object sender, EventArgs e)
        {
            listGenre = Genre.BacaData("", "");
            comboBoxGenre.DataSource = listGenre;
            comboBoxGenre.DisplayMember = "Nama";

            listKelompok = Kelompok.BacaData("", "");
            comboBoxKelompok.DataSource = listKelompok;
            comboBoxKelompok.DisplayMember = "Nama";

            listAktor = Aktor.BacaData("", "");
            comboBoxAktor.DataSource = listAktor;
            comboBoxAktor.DisplayMember = "Nama";
        }

        private void buttonTambahAktor_Click(object sender, EventArgs e)
        {
            selectedAktor = (Aktor)comboBoxAktor.SelectedItem;
            listAktorFilm.Add(selectedAktor);
            listPeran.Add(comboBoxPeran.Text);
            MessageBox.Show("Aktor film berhasil ditambahkan","Informasi");
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            try
            {
                int subIndo;
                double diskon;
                if (comboBoxSubIndo.Text == "YES")
                {
                    subIndo = 1;
                }
                else
                {
                    subIndo = 0;
                }
                selectedKelompok = (Kelompok)comboBoxKelompok.SelectedItem;
                if(textBoxNominalDiskon.Text == null)
                {
                    diskon = 0;
                }
                else
                {
                    diskon = double.Parse(textBoxNominalDiskon.Text);
                }
                Film f = new Film(int.Parse(labelID.Text), textBoxJudul.Text, textBoxSinopsis.Text, int.Parse(textBoxTahun.Text),
                                  int.Parse(textBoxDurasi.Text), selectedKelompok, comboBoxBahasa.Text, subIndo,
                                 savePath, diskon);

                Film.TambahData(f);
                filmBaru = Film.BacaData("1", "");
                //isi aktorfilm
                for (int i =0; i<listAktorFilm.Count;i++)
                {
                    af = new AktorFilm(listAktorFilm[i], filmBaru[0], listPeran[i]);
                    AktorFilm.TambahData(af);
                }
                //isi genre_film
                selectedGenre = (Genre)comboBoxGenre.SelectedItem;
                filmBaru = Film.BacaData("1", "");
                genreFilm = new GenreFilm(filmBaru[0], selectedGenre);
                GenreFilm.TambahData(genreFilm);
                //pesan berhasil
                MessageBox.Show("Data film berhasil ditambahkan.", "Informasi");
                listAktorFilm.Clear();
                listPeran.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data film gagal ditambahkan. Pesan kesalahan:" + ex.Message, "Kesalahan");
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
