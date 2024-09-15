using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class AktorFilm
    {
        #region data members
        private Aktor aktor;
        private Film film;
        private string peran;
        #endregion

        #region constructor
        public AktorFilm(Aktor aktor, Film film, string peran)
        {
            Aktor = aktor;
            Film = film;
            Peran = peran;
        }
        #endregion

        #region properties
        public Aktor Aktor { get => aktor; set => aktor = value; }
        public Film Film { get => film; set => film = value; }
        public string Peran { get => peran; set => peran = value; }
        #endregion

        #region methods
        public static List<AktorFilm> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "") //jika tidak ada kriteria yang diisikan
            {
                sql = "select * from aktor_film as af inner join films as f on f.id = af.films_id inner join aktors as a on a.id = af.aktors_id";
            }
            else if (kriteria == "f.id")
            {
                sql = "select * from aktor_film as af inner join films as f on f.id = af.films_id inner join aktors as a on a.id = af.aktors_id" +
                      " where " + kriteria + " = '" + nilaiKriteria + "'";
            }
            else
            {
                sql = "select * from aktor_film as af inner join films as f on f.id = af.films_id inner join aktors as a on a.id = af.aktors_id" +
                      " where " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<AktorFilm> listAktorFilm = new List<AktorFilm>(); //hasil list untuk menampung data

            while (hasil.Read() == true) //selama masih ada data atau masih bisa membaca data
            {
                Aktor aktor = new Aktor(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), DateTime.Parse(hasil.GetValue(2).ToString()),
                          hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString());

                Kelompok k = new Kelompok(int.Parse(hasil.GetValue(5).ToString()), hasil.GetValue(6).ToString());

                Film film = new Film(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                          int.Parse(hasil.GetValue(3).ToString()), int.Parse(hasil.GetValue(4).ToString()), k, hasil.GetValue(7).ToString(),
                          int.Parse(hasil.GetValue(8).ToString()), hasil.GetValue(9).ToString(), Double.Parse(hasil.GetValue(10).ToString()));

                AktorFilm aktorFilm = new AktorFilm(aktor, film, hasil.GetValue(3).ToString());

                listAktorFilm.Add(aktorFilm);
            }
            return listAktorFilm;
        }

        public static void TambahData(AktorFilm af)
        {
            string sql = "insert into aktor_film (aktors_id, films_id, peran) values ('" + af.Aktor.IdAktor + "','" + af.Film.Id + "','" + af.peran + "')";

            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static void UbahData(AktorFilm af)
        {
            string sql = "";
            sql = "update aktor_film set aktors_id ='" + af.Aktor.IdAktor + "', " +
                "peran='" + af.peran + "' where aktors_id='" + af.Aktor.IdAktor +
                  "' and films_id='" + af.Film.Id + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static void HapusData(AktorFilm af)
        {
            string sql = "";
            sql = "delete from aktor_film where aktors_id ='" + af.Aktor.IdAktor + "', films_id='" + af.Film.Id + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }
        public static List<AktorFilm> AktorPemeranFilm(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "1")
            {
                sql = "select a.id, a.nama, a.tgl_lahir, a.gender, a.negara_asal,f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, k.nama, " +
                    "f.bahasa, f.is_sub_indo, f.cover_image, f.diskon_nominal, af.films_id, af.aktors_id, af.peran from " +
                    "aktor_film af inner join aktors a on a.id = af.aktors_id inner join films f " +
                    "on af.films_id = f.id inner join kelompoks k on k.id = f.kelompoks_id where af.peran = 'UTAMA' and f.id='" + nilaiKriteria + "'";
            }
            else if (kriteria == "2")
            {
                sql = "select a.id, a.nama, a.tgl_lahir, a.gender, a.negara_asal,f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, k.nama, " +
                    "f.bahasa, f.is_sub_indo, f.cover_image, f.diskon_nominal, af.films_id, af.aktors_id, af.peran from " +
                    "aktor_film af inner join aktors a on a.id = af.aktors_id inner join films f " +
                    "on af.films_id = f.id inner join kelompoks k on k.id = f.kelompoks_id where af.peran = 'UTAMA'"
                   ;
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<AktorFilm> aktorPemeran = new List<AktorFilm>();
            while (hasil.Read() == true)
            {
                Aktor a = new Aktor(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), DateTime.Parse(hasil.GetValue(2).ToString()),
                    hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString());
                Kelompok k = new Kelompok(int.Parse(hasil.GetValue(10).ToString()), hasil.GetValue(11).ToString());
                Film f = new Film(int.Parse(hasil.GetValue(5).ToString()), hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString(),
                    int.Parse(hasil.GetValue(8).ToString()), int.Parse(hasil.GetValue(9).ToString()), k, hasil.GetValue(12).ToString(),
                    int.Parse(hasil.GetValue(13).ToString()), hasil.GetValue(14).ToString(), double.Parse(hasil.GetValue(15).ToString()));
                AktorFilm af = new AktorFilm(a, f, hasil.GetValue(18).ToString());
                aktorPemeran.Add(af);
            }
            return aktorPemeran;
        }

        public static bool HapusDataAktorFilm(string nilaiKriteria)
        {
            string sql = "delete from aktor_films where films_id='" + nilaiKriteria + "'";

            int jumlahDataBerubah = Koneksi.JalankanPerintahNonQuery(sql);
            if (jumlahDataBerubah == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static List<AktorFilm> BacaAktorFilm(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "1")
            {
                sql = "select a.id, a.nama, a.tgl_lahir, a.gender, a.negara_asal,f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, k.nama, " +
                    "f.bahasa, f.is_sub_indo, f.cover_image, f.diskon_nominal, af.films_id, af.aktors_id, af.peran from " +
                    "aktor_film af inner join aktors a on a.id = af.aktors_id inner join films f " +
                    "on af.films_id = f.id inner join kelompoks k on k.id = f.kelompoks_id where f.id='" + nilaiKriteria + "'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<AktorFilm> aktorPemeran = new List<AktorFilm>();
            while (hasil.Read() == true)
            {
                Aktor a = new Aktor(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), DateTime.Parse(hasil.GetValue(2).ToString()),
                    hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString());
                Kelompok k = new Kelompok(int.Parse(hasil.GetValue(10).ToString()), hasil.GetValue(11).ToString());
                Film f = new Film(int.Parse(hasil.GetValue(5).ToString()), hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString(),
                    int.Parse(hasil.GetValue(8).ToString()), int.Parse(hasil.GetValue(9).ToString()), k, hasil.GetValue(12).ToString(),
                    int.Parse(hasil.GetValue(13).ToString()), hasil.GetValue(14).ToString(), double.Parse(hasil.GetValue(15).ToString()));
                AktorFilm af = new AktorFilm(a, f, hasil.GetValue(18).ToString());
                aktorPemeran.Add(af);
            }
            return aktorPemeran;
            #endregion
        }
    }
}
