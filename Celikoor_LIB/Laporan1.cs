using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Laporan1
    {
        private PenjadwalanFilm bulan;
        private Film judulFilm;
        public Laporan1(PenjadwalanFilm bulan, Film judulFilm)
        {
            Bulan = bulan;
            JudulFilm = judulFilm;
        }

        public PenjadwalanFilm Bulan { get => bulan; set => bulan = value; }
        public Film JudulFilm { get => judulFilm; set => judulFilm = value; }

        public static List<Laporan1> BacaData()
        {
            string sql = "select bulan, judul_film from (select month(jf.tanggal) as bulan, f.judul as judul_film," +
                         " count(*) as jumlah_penonton from tikets t join sesi_films sf on t.jadwal_film_id = sf.jadwal_film_id and" +
                         " t.studios_id = sf.studios_id and t.films_id = sf.films_id join film_studio fs on sf.studios_id = fs.studios_id and" +
                         " sf.films_id = fs.films_id join jadwal_films jf on sf.jadwal_film_id = jf.id join films f on fs.films_id = f.id" +
                         " where year(jf.tanggal) = 2023 group by bulan, judul_film) as MonthlyFilmCount where (bulan, jumlah_penonton) in" +
                         " (select bulan, max(jumlah_penonton) from (select month(jf.tanggal) as bulan, f.judul as judul_film, count(*) as" +
                         " jumlah_penonton from tikets t join sesi_films sf on t.jadwal_film_id = sf.jadwal_film_id and t.studios_id = sf.studios_id" +
                         " and t.films_id = sf.films_id join film_studio fs on sf.studios_id = fs.studios_id and sf.films_id = fs.films_id join" +
                         " jadwal_films jf on sf.jadwal_film_id = jf.id join films f on fs.films_id = f.id where year(jf.tanggal) = 2023 group by" +
                         " bulan, judul_film) as MaxMonthlyCount group by bulan);";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Laporan1> listLaporan = new List<Laporan1>();
            while (hasil.Read() == true)
            {
                PenjadwalanFilm pj = new PenjadwalanFilm();
                pj.Id = int.Parse(hasil.GetValue(0).ToString());

                Film film = new Film();
                film.Judul = hasil.GetValue(1).ToString();

                Laporan1 laporan = new Laporan1(pj, film);

                listLaporan.Add(laporan);
            }
            return listLaporan;
        }
    }
}
