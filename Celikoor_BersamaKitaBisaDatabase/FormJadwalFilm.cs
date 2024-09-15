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
    public partial class FormJadwalFilm : Form
    {
        List<Cinema> listCinema = new List<Cinema>();
        List<Studio> listStudio = new List<Studio>();
        List<Film> listFilm = new List<Film>();
        List<PenjadwalanFilm> jadwal = new List<PenjadwalanFilm>();
        List<PenjadwalanFilm> jadwalDipilih = new List<PenjadwalanFilm>();
        List<Jenis_Studio> listJenisStudio = new List<Jenis_Studio>();
        PenjadwalanFilm pj;
        SesiFilm sf;
        Film selectedFilm;
        Studio selectedStudio;
        Cinema selectedCinema;
        List<SesiFilm>listSesiFilm = new List<SesiFilm>();
        FilmStudio fs;
        List<FilmStudio> listFilmStudio = new List<FilmStudio>();
        public FormJadwalFilm()
        {
            InitializeComponent();
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonTambah_Click(object sender, EventArgs e)
        {
            try
            {
                string jam = "";
                if (checkBoxI.Checked == true)
                {
                    jam = "I";
                    selectedFilm = (Film)comboBoxJudul.SelectedItem;
                    selectedStudio = (Studio)comboBoxNamaStudio.SelectedItem;
                    dataGridViewJadwal.Rows.Add(selectedFilm.Judul.ToString(), selectedCinema.Nama_cabang.ToString(), selectedStudio.Nama.ToString(),
                        dateTimePickerTanggal.Value.ToString("yyyy/MM/dd"), jam);
                }
                if (checkBoxII.Checked == true)
                {
                    jam = "II";
                    selectedFilm = (Film)comboBoxJudul.SelectedItem;
                    selectedStudio = (Studio)comboBoxNamaStudio.SelectedItem;
                    dataGridViewJadwal.Rows.Add(selectedFilm.Judul.ToString(), selectedCinema.Nama_cabang.ToString(), selectedStudio.Nama.ToString(),
                        dateTimePickerTanggal.Value.ToString("yyyy/MM/dd"), jam);
                }
                if (checkBoxIII.Checked == true)
                {
                    jam = "III";
                    selectedFilm = (Film)comboBoxJudul.SelectedItem;
                    selectedStudio = (Studio)comboBoxNamaStudio.SelectedItem;
                    dataGridViewJadwal.Rows.Add(selectedFilm.Judul.ToString(), selectedCinema.Nama_cabang.ToString(), selectedStudio.Nama.ToString(),
                        dateTimePickerTanggal.Value.ToString("yyyy/MM/dd"), jam);
                }
                if (checkBoxIV.Checked == true)
                {
                    jam = "IV";
                    selectedFilm = (Film)comboBoxJudul.SelectedItem;
                    selectedStudio = (Studio)comboBoxNamaStudio.SelectedItem;
                    dataGridViewJadwal.Rows.Add(selectedFilm.Judul.ToString(), selectedCinema.Nama_cabang.ToString(), selectedStudio.Nama.ToString(),
                        dateTimePickerTanggal.Value.ToString("yyyy/MM/dd"), jam);
                }
                MessageBox.Show("Tambah data berhasil", "Informasi");

            }
            catch(Exception ex)
            {
                MessageBox.Show("Pesan kesalahan:" + ex.Message, "Informasi");
            }
        }

        private void FormJadwalFilm_Load(object sender, EventArgs e)
        {
            listCinema = Cinema.BacaData("","");
            comboBoxCinema.DataSource = listCinema;
            comboBoxCinema.DisplayMember = "nama_cabang";
            FormatDataGrid();           
            listFilm = Film.BacaData("","");
            comboBoxJudul.DataSource = listFilm;
            comboBoxJudul.DisplayMember = "Nama";
            listFilmStudio = FilmStudio.BacaData("", "");
        }

        private void comboBoxCinema_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedCinema = (Cinema)comboBoxCinema.SelectedItem;
            listStudio = Studio.BacaData("cinemas_id", selectedCinema.Id.ToString());
            comboBoxNamaStudio.DataSource = listStudio;
            comboBoxNamaStudio.DisplayMember = "nama";
        }

        private void comboBoxNamaStudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedStudio = (Studio)comboBoxNamaStudio.SelectedItem;
            listJenisStudio = Jenis_Studio.BacaData("id", selectedStudio.JenisStudio.Id.ToString());
            labelDetailStudio.Text = listJenisStudio[0].Nama + "  " + selectedStudio.Kapasitas.ToString() + " kursi" +
                "\nWeekday:" + "  Rp." + selectedStudio.HargaWeekday + ",-" +
                "\nWeekend:" + "  Rp." + selectedStudio.HargaWeekend +",-";

            listFilm = Film.BacaData("", "");
            comboBoxJudul.DataSource = listFilm;
            comboBoxJudul.DisplayMember = "Judul";
        }

        private void FormatDataGrid()
        {
            dataGridViewJadwal.Columns.Clear();
            dataGridViewJadwal.Columns.Add("JudulFilm", "Judul Film");
            dataGridViewJadwal.Columns["JudulFilm"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJadwal.Columns["JudulFilm"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewJadwal.Columns.Add("Cinema", "Cinema");
            dataGridViewJadwal.Columns["Cinema"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJadwal.Columns["Cinema"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewJadwal.Columns.Add("Studio", "Studio");
            dataGridViewJadwal.Columns["Studio"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJadwal.Columns["Studio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewJadwal.Columns.Add("Tanggal", "Tanggal");
            dataGridViewJadwal.Columns["Tanggal"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJadwal.Columns["Tanggal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridViewJadwal.Columns.Add("Jam", "Jam");
            dataGridViewJadwal.Columns["Jam"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewJadwal.Columns["Jam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

             DataGridViewButtonColumn bcol2 = new DataGridViewButtonColumn();
             bcol2.HeaderText = "Aksi";
             bcol2.Text = "Hapus";
             bcol2.Name = "buttonHapusGrid";
             bcol2.UseColumnTextForButtonValue = true;
             dataGridViewJadwal.Columns.Add(bcol2);
        }

        private void dataGridViewJadwal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewJadwal.Columns["buttonHapusGrid"].Index)
            {
                int index = dataGridViewJadwal.CurrentRow.Index;
                dataGridViewJadwal.Rows.RemoveAt(index);
                MessageBox.Show("Hapus data berhasil!", "Informasi");
            }
        }

        private void comboBoxJudul_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                selectedFilm = (Film)comboBoxJudul.SelectedItem;
                //pictureBoxMovie.Image = Image.FromFile(selectedFilm.CoverImage);
                labelSinopsis.Text = selectedFilm.Sinopsis;
                labelDurasi.Text = selectedFilm.Durasi.ToString() + " menit";
                labelKelompok.Text = selectedFilm.Kelompok.Nama;
                int idFilm = selectedFilm.Id;
                List<GenreFilm> genreFilmDipilih = new List<GenreFilm>();
                genreFilmDipilih = GenreFilm.BacaDataGenreFilmDipilih("1", idFilm.ToString());
                labelGenre.Text = genreFilmDipilih[0].Genre.Nama;
                List<AktorFilm> listPemeran = new List<AktorFilm>();
                listPemeran = AktorFilm.AktorPemeranFilm("1", idFilm.ToString());
                if (listPemeran.Count <= 2)
                {
                    if (listPemeran.Count == 1)
                    {
                        labelDaftarAktor.Text = listPemeran[0].Aktor.NamaAktor;
                    }
                    else if (listPemeran.Count == 2)
                    {
                        labelDaftarAktor.Text = listPemeran[0].Aktor.NamaAktor + "," + listPemeran[1].Aktor.NamaAktor.ToString();
                    }
                }
                else
                {
                    labelDaftarAktor.Text = listPemeran[0].Aktor.NamaAktor + "," + listPemeran[1].Aktor.NamaAktor.ToString() + ",...";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Pesan kesalahan : " + ex.Message, "Kesalahan");
            }
        }

        private void buttonSimpan_Click(object sender, EventArgs e)
        {
            for(int i=0;i<dataGridViewJadwal.Rows.Count-1;i++)
            {
                string judulFilm = dataGridViewJadwal.Rows[i].Cells["JudulFilm"].Value.ToString();
                string namaStudio = dataGridViewJadwal.Rows[i].Cells["Studio"].Value.ToString();
                string namaCinema = dataGridViewJadwal.Rows[i].Cells["Cinema"].Value.ToString();
                string tanggal = dataGridViewJadwal.Rows[i].Cells["Tanggal"].Value.ToString();
                string jamPemutaran = dataGridViewJadwal.Rows[i].Cells["Jam"].Value.ToString();
                List<Film> filmDipilih = new List<Film>();
                filmDipilih = Film.BacaData("f.judul", judulFilm);
                List<Cinema> cinemaStudioDipilih = new List<Cinema>();
                cinemaStudioDipilih = Cinema.BacaData("nama_cabang", namaCinema);
                List<Studio> studioDipilih = new List<Studio>();
                studioDipilih = Studio.BacaData("1", "s.nama='" + namaStudio + "' and c.nama_cabang='" + namaCinema + "'");
                fs = new FilmStudio(studioDipilih[0], filmDipilih[0]);
                List<FilmStudio> FilmStudioAda = new List<FilmStudio>();
                FilmStudioAda = FilmStudio.BacaData("f.id", filmDipilih[0].Id.ToString() + "'" +
                    " and s.id='" + studioDipilih[0].Id.ToString());
                int countFS = 0;
                for (int k = 0; k < FilmStudioAda.Count; k++)
                {
                    if (fs.Studio.Id == FilmStudioAda[k].Studio.Id && fs.Film.Id == FilmStudioAda[k].Film.Id)
                    {
                        countFS++;
                    }
                }
                if (countFS == 0)
                {
                    FilmStudio.TambahData(fs);
                }

                jadwal = PenjadwalanFilm.BacaData("", "");
                PenjadwalanFilm pj = new PenjadwalanFilm(int.Parse(labelID.Text), DateTime.Parse(tanggal), jamPemutaran);
                List<PenjadwalanFilm> jadwalDipilih = new List<PenjadwalanFilm>();
                jadwalDipilih = PenjadwalanFilm.BacaData("jam_pemutaran", jamPemutaran + "' and tanggal='" + tanggal);
                if (jadwalDipilih.Count > 0)
                {
                    pj = jadwalDipilih[0];
                    sf = new SesiFilm(jadwalDipilih[0], studioDipilih[0], filmDipilih[0]);
                }
                else
                {
                    PenjadwalanFilm.TambahData(pj);
                    List<PenjadwalanFilm> pjBaru = new List<PenjadwalanFilm>();
                    pjBaru = PenjadwalanFilm.BacaData("2", "");
                    sf = new SesiFilm(pjBaru[0], studioDipilih[0], filmDipilih[0]);
                }
                SesiFilm.TambahData(sf);
            }
            MessageBox.Show("Selamat, penjadwalan film Anda berhasil!", "Informasi");
        }
    }
}