using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class GenreFilm
    {
        #region data member
        private Film film;
        private Genre genre;
        #endregion

        #region constructor

        public GenreFilm(Film film, Genre genre)
        {
            this.film = film;
            this.genre = genre;
        }
        #endregion

        #region properties

        public Film Film { get => film; set => film = value; }
        public Genre Genre { get => genre; set => genre = value; }

        #endregion

        #region methods
        public static void TambahData(GenreFilm gf)
        {
            string sql = "insert into genre_film (films_id, genres_id) values ('" + gf.film.Id + "','" + gf.genre.Id + "')";

            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static void UbahData(GenreFilm gf)
        {
            string sql = "";
            sql = "update genre_film set genres_id='" + gf.genre.Id + "' where films_id='" + gf.film.Id + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static bool HapusData(string nilaiKriteria)
        {
            string sql = "delete from genre_film where films_id='" + nilaiKriteria + "'";

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
        public static List<GenreFilm> BacaDataGenreFilmDipilih(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, k.nama, f.bahasa, f.is_sub_indo, f.cover_image, f.diskon_nominal," +
                               "g.id, g.nama, g.deskripsi from films f inner join genre_film gf on f.id = gf.films_id inner join genres g\r\non gf.genres_id = g.id" +
                               " inner join kelompoks k on k.id = f.kelompoks_id";
            }
            else if (kriteria == "1")
            {
                sql = "select f.id, f.judul, f.sinopsis, f.tahun, f.durasi, k.id, k.nama, f.bahasa, f.is_sub_indo, f.cover_image, f.diskon_nominal," +
                                "g.id, g.nama, g.deskripsi from films f inner join genre_film gf on f.id = gf.films_id inner join genres g\r\non gf.genres_id = g.id" +
                                " inner join kelompoks k on k.id = f.kelompoks_id where f.id='" + nilaiKriteria + "'";

            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<GenreFilm> list = new List<GenreFilm>();
            while (hasil.Read() == true)
            {
                Kelompok k = new Kelompok(int.Parse(hasil.GetValue(5).ToString()), hasil.GetValue(6).ToString());
                Film f = new Film(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(), int.Parse(hasil.GetValue(3).ToString()),
                    int.Parse(hasil.GetValue(4).ToString()), k, hasil.GetValue(7).ToString(), int.Parse(hasil.GetValue(8).ToString()), hasil.GetValue(9).ToString(),
                    double.Parse(hasil.GetValue(10).ToString()));
                Genre g = new Genre(int.Parse(hasil.GetValue(11).ToString()), hasil.GetValue(12).ToString(), hasil.GetValue(13).ToString());
                GenreFilm gf = new GenreFilm(f, g);
                list.Add(gf);
            }
            return list;
            
        }

        public static List<GenreFilm> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "") //jika tidak ada kriteria yang diisikan
            {
                sql = "select f.*, g.*, k.* from films f inner join genre_film gf on f.id = gf.films_id inner join genres g " +
                      "on g.id = gf.genres_id inner join kelompoks k on k.id = f.id ";

            }
            else if (kriteria == "1")
            {
                sql = "select f.*, g.*, k.* from films f inner join genre_film gf on f.id = gf.films_id inner join genres g " +
                      "on g.id = gf.genres_id inner join kelompoks k on k.id = f.id " +
                      "where " + kriteria + " = '" + nilaiKriteria + "'";
            }
            else
            {
                sql = "select f.*, g.*, k.* from films f inner join genre_film gf on f.id = gf.films_id inner join genres g " +
                      "on g.id = gf.genres_id inner join kelompoks k on k.id = f.id " +
                      " where " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<GenreFilm> listGenreFilm = new List<GenreFilm>(); //hasil list untuk menampung data

            while (hasil.Read() == true) //selama masih ada data atau masih bisa membaca data
            {
                Genre genre = new Genre(int.Parse(hasil.GetValue(10).ToString()), hasil.GetValue(11).ToString(), hasil.GetValue(12).ToString());

                Kelompok k = new Kelompok(int.Parse(hasil.GetValue(13).ToString()), hasil.GetValue(14).ToString());

                Film film = new Film(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                          int.Parse(hasil.GetValue(3).ToString()), int.Parse(hasil.GetValue(4).ToString()), k, hasil.GetValue(6).ToString(),
                          int.Parse(hasil.GetValue(7).ToString()), hasil.GetValue(8).ToString(), Double.Parse(hasil.GetValue(9).ToString()));

                GenreFilm genreFilm = new GenreFilm(film, genre);

                listGenreFilm.Add(genreFilm);
            }

            return listGenreFilm;
        }

        public override string ToString()
        {
            return Genre.Nama;
        }
        #endregion
    }
}
