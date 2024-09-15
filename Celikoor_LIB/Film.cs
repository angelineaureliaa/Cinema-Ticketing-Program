using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Film
    {
        #region data members
        private int id;
        private string judul;
        private string sinopsis;
        private int tahun;
        private int durasi;
        private Kelompok kelompok;
        private string bahasa;
        private int subtitleIndo;
        private string coverImage;
        private double diskonNominal;
        #endregion

        #region constructors
        public Film(int id, string judul, string sinopsis, int tahun, int durasi, Kelompok kelompok, string bahasa, 
            int subtitleIndo, string coverImage, double diskonNominal)
        {
            Id = id;
            Judul = judul;
            Sinopsis = sinopsis;
            Tahun = tahun;
            Durasi = durasi;
            Kelompok = kelompok;
            Bahasa = bahasa;
            SubtitleIndo = subtitleIndo;
            CoverImage = coverImage;
            DiskonNominal = diskonNominal;
        }

        public Film()
        {
        }
        #endregion

        #region properties
        public int Id { get => id; set => id = value; }
        public string Judul { get => judul; set => judul = value; }
        public string Sinopsis { get => sinopsis; set => sinopsis = value; }
        public int Tahun { get => tahun; set => tahun = value; }
        public int Durasi { get => durasi; set => durasi = value; }
        public Kelompok Kelompok { get => kelompok; set => kelompok = value; }
        public string Bahasa { get => bahasa; set => bahasa = value; }
        public int SubtitleIndo { get => subtitleIndo; set => subtitleIndo = value; }
        public string CoverImage { get => coverImage; set => coverImage = value; }
        public double DiskonNominal { get => diskonNominal; set => diskonNominal = value; }
        #endregion

        #region methods
        public static List<Film> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, f.bahasa, f.is_sub_indo, " +
                    "f.cover_image, f.diskon_nominal, k.nama from films f " +
                    "inner join kelompoks k on f.kelompoks_id = k.id";
            }
            else if (kriteria == "1")
            {
                sql = "select f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, f.bahasa, f.is_sub_indo, f.cover_image, " +
                    "f.diskon_nominal, k.nama from films f " +
                "inner join kelompoks k on f.kelompoks_id = k.id where f.id = (select max(id) from films)";
            }
            else if (kriteria == "f.judul")
            {
                sql = "select f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, f.bahasa, f.is_sub_indo, f.cover_image, " +
                    "f.diskon_nominal, k.nama from films f " +
                    "inner join kelompoks k on f.kelompoks_id = k.id where " + kriteria + " ='" + nilaiKriteria + "'";
            }
            else
            {
                sql = "select f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, f.bahasa, f.is_sub_indo, f.cover_image, " +
                    "f.diskon_nominal, k.nama from films f " +
                    "inner join kelompoks k on f.kelompoks_id = k.id where " + kriteria + "='" + nilaiKriteria + "'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Film> listFilm = new List<Film>();
            while (hasil.Read() == true)
            {
                Kelompok k = new Kelompok(int.Parse(hasil.GetValue(5).ToString()), hasil.GetValue(10).ToString());
                Film film = new Film(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(), 
                    int.Parse(hasil.GetValue(3).ToString()),
                    int.Parse(hasil.GetValue(4).ToString()), k, hasil.GetValue(6).ToString(), int.Parse(hasil.GetValue(7).ToString()), 
                    hasil.GetValue(8).ToString(),
                    double.Parse(hasil.GetValue(9).ToString()));
                listFilm.Add(film);

            }
            return listFilm;
        }

        public static Film BacaDataSatu(string kriteria, string nilaiKriteria)
        {
            string sql="";
            if (kriteria != "")
            {
                 sql = "select f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id,  k.nama, f.bahasa, f.is_sub_indo, " +
                "f.cover_image, f.diskon_nominal  from films as f inner join kelompoks as k on f.kelompoks_id = k.id " +
                "where f." + kriteria + " ='" + nilaiKriteria + "'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            if (hasil.Read()==true)
            {

                Kelompok k = new Kelompok(int.Parse(hasil.GetValue(5).ToString()), hasil.GetValue(6).ToString());
                Film film = new Film(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                          int.Parse(hasil.GetValue(3).ToString()), int.Parse(hasil.GetValue(4).ToString()), k, hasil.GetValue(7).ToString(),
                          int.Parse(hasil.GetValue(8).ToString()), hasil.GetValue(9).ToString(), Double.Parse(hasil.GetValue(10).ToString()));
                return film;
            }
            else throw new Exception("Data tidak ditemukan");
        }
        public static void TambahData(Film f)
        {
            string sql = "insert into films (id, judul, sinopsis, tahun, durasi, kelompoks_id, " +
                "bahasa, is_sub_indo, cover_image, diskon_nominal) " +
                "values ('" +
                         f.Id + "','" + f.Judul + "','" + f.Sinopsis + "','" + f.Tahun + "','" + f.Durasi + "','" + f.Kelompok.Id +
                         "','" + f.Bahasa + "','" + f.SubtitleIndo + "','" + 
                         f.CoverImage + "','" + f.DiskonNominal + "')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        //ubah
        public static void UbahData(Film f)
        {
            string sql = "";
            sql = "update films set id ='" + f.Id + "', judul='" + f.Judul + "', sinopsis='" + f.Sinopsis + "', tahun= '" + f.Tahun +
                  "', durasi= '" + f.Durasi + "', kelompoks_id= '" + f.Kelompok.Id + "', bahasa= '" + f.Bahasa + "', is_sub_indo= '" +
                  f.SubtitleIndo + "', cover_image= '" + f.CoverImage + "', diskon_nominal= '" + f.DiskonNominal + "'where id='" + f.Id + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        //hapus
        public static bool HapusData(Film f)
        {
            string perintah = "delete from films where id = '" + f.Id + "'";
            int jumlahDihapus = Koneksi.JalankanPerintahNonQuery(perintah);
            Boolean status;
            if (jumlahDihapus == 0)
            {
                status = false;
            }
            else
            {
                status = true;
            }
            return status;
        }

        public static Film AmbilDataByKode(string id)
        {
            string sql = "select f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, f.bahasa, f.is_sub_indo, " +
                    "f.cover_image, f.diskon_nominal, k.nama from films f " +
                    "inner join kelompoks k on f.kelompoks_id = k.id where f.id='" + id + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
               
                    Kelompok k = new Kelompok(int.Parse(hasil.GetValue(5).ToString()), hasil.GetValue(10).ToString());
                    Film film = new Film(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                        int.Parse(hasil.GetValue(3).ToString()),
                        int.Parse(hasil.GetValue(4).ToString()), k, hasil.GetValue(6).ToString(), int.Parse(hasil.GetValue(7).ToString()),
                        hasil.GetValue(8).ToString(),
                        double.Parse(hasil.GetValue(9).ToString()));
                return film;
            }
            else
            {
                return null;
            }
        }

        public static List<Film> BacaDataDua(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if(kriteria=="")
            {
                sql = "select f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, f.bahasa, f.is_sub_indo, f.cover_image, " +
                    "f.diskon_nominal, k.nama from films f " +
                    "inner join kelompoks k on f.kelompoks_id = k.id where " + kriteria + " LIKE%'" + nilaiKriteria + "'";
            }
            if (kriteria == "1")
            {
                sql = "select f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, f.bahasa, f.is_sub_indo, f.cover_image, f.diskon_nominal, k.nama from films f " +
                    "inner join kelompoks k on f.kelompoks_id = k.id";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Film> listFilm = new List<Film>();
            while (hasil.Read() == true)
            {
                Kelompok k = new Kelompok(int.Parse(hasil.GetValue(5).ToString()), hasil.GetValue(10).ToString());
                Film film = new Film(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                            int.Parse(hasil.GetValue(3).ToString()),int.Parse(hasil.GetValue(4).ToString()), k, hasil.GetValue(6).ToString(), 
                            int.Parse(hasil.GetValue(7).ToString()), hasil.GetValue(8).ToString(), double.Parse(hasil.GetValue(9).ToString()));
                listFilm.Add(film);

            }
            return listFilm;
        }

        public override string ToString()
        {
            return judul;
        }
        #endregion
    }
}
