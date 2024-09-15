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
    public partial class FormDaftarFilm : Form
    {
        List<Film> listFilm = new List<Film>();
        List<GenreFilm> listGenreFilm = new List<GenreFilm>();
        List<AktorFilm> listAktorFilm = new List<AktorFilm>();

        public FormDaftarFilm()
        {
            InitializeComponent();
        }

        private void FormDaftarFilm_Load(object sender, EventArgs e)
        {
            listFilm = Film.BacaData("", "");
            listGenreFilm = GenreFilm.BacaDataGenreFilmDipilih("", "");
            listAktorFilm = AktorFilm.AktorPemeranFilm("2", "");
            dataGridViewDaftarFilm.DataSource = listFilm;

            if (listFilm.Count > 0)
            {

                FormatDataGrid();
            }
            else
            {
                dataGridViewDaftarFilm.DataSource = null;
            }

            for (int i = 0; i < dataGridViewDaftarFilm.RowCount; i++)
            {
                dataGridViewDaftarFilm.Rows[i].Cells["IdFilm"].Value = listFilm[i].Id;
                int idFilm = listFilm[i].Id;
                dataGridViewDaftarFilm.Rows[i].Cells["JudulFilm"].Value = listFilm[i].Judul;
                dataGridViewDaftarFilm.Rows[i].Cells["Sinopsis"].Value = listFilm[i].Sinopsis;
                ////dapatkan genre dari setiap film
                listGenreFilm = GenreFilm.BacaDataGenreFilmDipilih("1", idFilm.ToString());

                dataGridViewDaftarFilm.Rows[i].Cells["Tahun"].Value = listFilm[i].Tahun;
                dataGridViewDaftarFilm.Rows[i].Cells["Durasi"].Value = listFilm[i].Durasi;
                dataGridViewDaftarFilm.Rows[i].Cells["KelompokUsia"].Value = listFilm[i].Kelompok.Nama;

                for (int j = 0; j < listGenreFilm.Count; j++)
                {
                    if (listGenreFilm[j].Film.Id == idFilm)
                    {
                        dataGridViewDaftarFilm.Rows[i].Cells["Genre"].Value = listGenreFilm[j].Genre.Nama;
                    }
                }

                for (int k = 0; k < listAktorFilm.Count; k++)
                {
                    if (listAktorFilm[k].Film.Id == idFilm)
                    {
                        dataGridViewDaftarFilm.Rows[i].Cells["Aktor"].Value = listAktorFilm[k].Aktor.NamaAktor;
                    }
                }

                dataGridViewDaftarFilm.Rows[i].Cells["Bahasa"].Value = listFilm[i].Bahasa;
                if (listFilm[i].SubtitleIndo == 0)
                {
                    dataGridViewDaftarFilm.Rows[i].Cells["SubIndo"].Value = "Tidak";
                }
                else
                {
                    dataGridViewDaftarFilm.Rows[i].Cells["SubIndo"].Value = "Ya";
                }

                dataGridViewDaftarFilm.Rows[i].Cells["CoverImage"].Value = listFilm[i].CoverImage;
                dataGridViewDaftarFilm.Rows[i].Cells["Diskon"].Value = listFilm[i].DiskonNominal;
            }
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            FormTambahFilm frm = new FormTambahFilm();
            frm.Owner = this;
            frm.Show();
        }

        private void FormatDataGrid()
        {
            dataGridViewDaftarFilm.Columns.Clear();
            dataGridViewDaftarFilm.Columns.Add("IdFilm", "ID Film");
            dataGridViewDaftarFilm.Columns["IdFilm"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["IdFilm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarFilm.Columns.Add("JudulFilm", "Judul Film");
            dataGridViewDaftarFilm.Columns["JudulFilm"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["JudulFilm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;


            dataGridViewDaftarFilm.Columns.Add("Sinopsis", "Sinopsis");
            dataGridViewDaftarFilm.Columns["Sinopsis"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["Sinopsis"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarFilm.Columns.Add("Tahun", "Tahun Rilis");
            dataGridViewDaftarFilm.Columns["Tahun"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["Tahun"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarFilm.Columns.Add("Durasi", "Durasi (menit)");
            dataGridViewDaftarFilm.Columns["Durasi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["Durasi"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarFilm.Columns.Add("KelompokUsia", "Kelompok Usia");
            dataGridViewDaftarFilm.Columns["KelompokUsia"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["KelompokUsia"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarFilm.Columns.Add("Genre", "Genre");
            dataGridViewDaftarFilm.Columns["Genre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["Genre"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarFilm.Columns.Add("Aktor", "Aktor Utama");
            dataGridViewDaftarFilm.Columns["Aktor"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["Aktor"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarFilm.Columns.Add("Bahasa", "Bahasa");
            dataGridViewDaftarFilm.Columns["Bahasa"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["Bahasa"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarFilm.Columns.Add("SubIndo", "Subtitle Indonesia");
            dataGridViewDaftarFilm.Columns["SubIndo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["SubIndo"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarFilm.Columns.Add("CoverImage", "Cover Image");
            dataGridViewDaftarFilm.Columns["CoverImage"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["CoverImage"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewDaftarFilm.Columns.Add("Diskon", "Nominal Diskon");
            dataGridViewDaftarFilm.Columns["Diskon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewDaftarFilm.Columns["Diskon"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            DataGridViewButtonColumn bcol1 = new DataGridViewButtonColumn();
            bcol1.HeaderText = "Aksi";
            bcol1.Text = "Ubah";
            bcol1.Name = "buttonUbahGrid";
            bcol1.UseColumnTextForButtonValue = true;
            dataGridViewDaftarFilm.Columns.Add(bcol1);

            DataGridViewButtonColumn bcol2 = new DataGridViewButtonColumn();
            bcol2.HeaderText = "Aksi";
            bcol2.Text = "Hapus";
            bcol2.Name = "buttonHapusGrid";
            bcol2.UseColumnTextForButtonValue = true;
            dataGridViewDaftarFilm.Columns.Add(bcol2);
        }

        private void textBoxKriteria_TextChanged(object sender, EventArgs e)
        {
            string kriteria = "";

            if (comboBoxKriteria.Text == "Judul")
            {
                kriteria = "f.judul";
            }
            else if (comboBoxKriteria.Text == "Kelompok")
            {
                kriteria = "k.nama";
            }

            listFilm = Film.BacaDataDua(kriteria, textBoxKriteria.Text);
            listGenreFilm = GenreFilm.BacaDataGenreFilmDipilih("", "");
            listAktorFilm = AktorFilm.AktorPemeranFilm("2", "");
            dataGridViewDaftarFilm.DataSource = listFilm;

            if (listFilm.Count > 0)
            {
                FormatDataGrid();
            }
            else
            {
                dataGridViewDaftarFilm.DataSource = null;
            }

            for (int i = 0; i < dataGridViewDaftarFilm.RowCount; i++)
            {
                dataGridViewDaftarFilm.Rows[i].Cells["IdFilm"].Value = listFilm[i].Id;
                int idFilm = listFilm[i].Id;
                dataGridViewDaftarFilm.Rows[i].Cells["JudulFilm"].Value = listFilm[i].Judul;
                dataGridViewDaftarFilm.Rows[i].Cells["Sinopsis"].Value = listFilm[i].Sinopsis;
                ////dapatkan genre dari setiap film
                listGenreFilm = GenreFilm.BacaDataGenreFilmDipilih("1", idFilm.ToString());

                dataGridViewDaftarFilm.Rows[i].Cells["Tahun"].Value = listFilm[i].Tahun;
                dataGridViewDaftarFilm.Rows[i].Cells["Durasi"].Value = listFilm[i].Durasi;
                dataGridViewDaftarFilm.Rows[i].Cells["KelompokUsia"].Value = listFilm[i].Kelompok.Nama;

                for (int j = 0; j < listGenreFilm.Count; j++)
                {
                    if (listGenreFilm[j].Film.Id == idFilm)
                    {
                        dataGridViewDaftarFilm.Rows[i].Cells["Genre"].Value = listGenreFilm[j].Genre.Nama;
                    }
                }

                for (int k = 0; k < listAktorFilm.Count; k++)
                {
                    if (listAktorFilm[k].Film.Id == idFilm)
                    {
                        dataGridViewDaftarFilm.Rows[i].Cells["Aktor"].Value = listAktorFilm[k].Aktor.NamaAktor;
                    }
                }

                dataGridViewDaftarFilm.Rows[i].Cells["Bahasa"].Value = listFilm[i].Bahasa;
                if (listFilm[i].SubtitleIndo == 0)
                {
                    dataGridViewDaftarFilm.Rows[i].Cells["SubIndo"].Value = "Tidak";
                }
                else
                {
                    dataGridViewDaftarFilm.Rows[i].Cells["SubIndo"].Value = "Ya";
                }

                dataGridViewDaftarFilm.Rows[i].Cells["CoverImage"].Value = listFilm[i].CoverImage;
                dataGridViewDaftarFilm.Rows[i].Cells["Diskon"].Value = listFilm[i].DiskonNominal;
            }
        }

        private void buttonTambah_Click_1(object sender, EventArgs e)
        {
            FormTambahFilm frmTambahFilm = new FormTambahFilm();
            frmTambahFilm.Owner = this;
            frmTambahFilm.Show();
        }

        private void dataGridViewDaftarFilm_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string id = dataGridViewDaftarFilm.CurrentRow.Cells["IdFilm"].Value.ToString();
                Film f = Film.AmbilDataByKode(id);
                if (f != null)
                {
                    if (e.ColumnIndex == dataGridViewDaftarFilm.Columns["buttonUbahGrid"].Index)
                    {
                        FormUbahFilm frm = new FormUbahFilm();
                        List<AktorFilm> aktorFilm = new List<AktorFilm>();
                        aktorFilm = AktorFilm.BacaAktorFilm("1", f.Id.ToString());
                        frm.Owner = this;
                        frm.labelID.Text = f.Id.ToString();
                        frm.textBoxJudul.Text = f.Judul;
                        frm.textBoxSinopsis.Text = f.Sinopsis;
                        frm.textBoxTahun.Text = f.Tahun.ToString();
                        frm.textBoxDurasi.Text = f.Durasi.ToString();
                        frm.comboBoxKelompok.Text = f.Kelompok.Nama.ToString();
                        frm.comboBoxBahasa.Text = f.Bahasa;
                        if (f.SubtitleIndo.ToString() == "1")
                        {
                            frm.comboBoxSubIndo.Text = "YES";
                        }
                        else
                        {
                            frm.comboBoxSubIndo.Text = "NO";
                        }
                        frm.pictureBoxCoverImage.Image = Image.FromFile(f.CoverImage);
                        frm.textBoxNominalDiskon.Text = f.DiskonNominal.ToString();
                        frm.dataGridVieAktorPemeran.DataSource = aktorFilm;
                        frm.Show();
                    }
                    else if (e.ColumnIndex == dataGridViewDaftarFilm.Columns["buttonHapusGrid"].Index && e.RowIndex >= 0)
                    {
                        string idHapus = dataGridViewDaftarFilm.CurrentRow.Cells["IdFilm"].Value.ToString();
                        string judulHapus = dataGridViewDaftarFilm.CurrentRow.Cells["JudulFilm"].Value.ToString();
                        string sinopsisHapus = dataGridViewDaftarFilm.CurrentRow.Cells["Sinopsis"].Value.ToString();
                        string tahunHapus = dataGridViewDaftarFilm.CurrentRow.Cells["Tahun"].Value.ToString();
                        string durasiHapus = dataGridViewDaftarFilm.CurrentRow.Cells["Durasi"].Value.ToString();
                        string namaKelompokHapus = dataGridViewDaftarFilm.CurrentRow.Cells["KelompokUsia"].Value.ToString();
                        string bahasaHapus = dataGridViewDaftarFilm.CurrentRow.Cells["Bahasa"].Value.ToString();
                        string subtitleHapus = dataGridViewDaftarFilm.CurrentRow.Cells["SubIndo"].Value.ToString();
                        string coverImageHapus = dataGridViewDaftarFilm.CurrentRow.Cells["CoverImage"].Value.ToString();
                        string nominalDiskonHapus = dataGridViewDaftarFilm.CurrentRow.Cells["Diskon"].Value.ToString();

                        //cari id dari kelompok yang dihapus
                        List<Kelompok> kelompokHapus = new List<Kelompok>();
                        kelompokHapus = Kelompok.BacaData("nama", namaKelompokHapus);
                        DialogResult result = MessageBox.Show(this, "Yakin Hapus id =" + idHapus + " - " + judulHapus + "?"
                            , "Hapus", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        int subHapus = 0;
                        if (result == DialogResult.Yes)
                        {
                            if (subtitleHapus == "Ya")
                            {
                                subHapus = 1;
                            }
                            else
                            {
                                subHapus = 0;
                            }
                            Film film = new Film(int.Parse(idHapus), judulHapus, sinopsisHapus, int.Parse(tahunHapus),
                                int.Parse(durasiHapus), kelompokHapus[0],
                                     bahasaHapus, subHapus, coverImageHapus, Double.Parse(nominalDiskonHapus));
                            Boolean hapusSesiFilm = SesiFilm.HapusData(idHapus.ToString());
                            Boolean hapusGenreFilm = GenreFilm.HapusData(idHapus.ToString());
                            Boolean hapusAktorFilm = AktorFilm.HapusDataAktorFilm(idHapus.ToString());
                            Boolean hapusFilmStudio = FilmStudio.HapusData(idHapus);
                            Boolean hapus = Film.HapusData(film);

                            if (hapusGenreFilm == true && hapusAktorFilm == true &&
                                hapus == true)
                            {
                                MessageBox.Show("Berhasil hapus data");

                                //refresh datagrid
                                FormDaftarFilm_Load(sender, e);
                            }
                            else
                            {
                                MessageBox.Show("Gagal hapus data");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Pesan kesalahan : " + ex.Message, "Kesalahan");
            }
        }
    }
}
