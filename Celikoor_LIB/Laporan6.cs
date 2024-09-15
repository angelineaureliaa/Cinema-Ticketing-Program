using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Laporan6
    {
        private Genre genre;
        private Film jumlahFilm;

        public Laporan6(Genre genre, Film jumlahFilm)
        {
            Genre = genre;
            JumlahFilm = jumlahFilm;
        }

        public Genre Genre { get => genre; set => genre = value; }
        public Film JumlahFilm { get => jumlahFilm; set => jumlahFilm = value; }

        public static List<Laporan6> BacaData()
        {

            string sql = "select g.nama as genre, count(*) as jumlah_film from genres g join genre_film gf on g.id = gf.genres_id " +
                         "join films f on gf.films_id = f.id group by g.nama order by jumlah_film desc";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Laporan6> listLaporan = new List<Laporan6>();
            while (hasil.Read() == true)
            {
                Genre g = new Genre();
                g.Nama = hasil.GetValue(0).ToString();

                Film jumlahFilm = new Film();
                jumlahFilm.Judul = hasil.GetValue(1).ToString();

                Laporan6 tampung = new Laporan6(g, jumlahFilm);
                listLaporan.Add(tampung);
            }
            return listLaporan;
        }
    }
}
