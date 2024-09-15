using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Laporan3
    {
        private Film namafilm;
        private Tiket jumlahKetidakhadiran;

        public Laporan3(Film namafilm, Tiket jumlahKetidakhadiran)
        {
            Namafilm = namafilm;
            JumlahKetidakhadiran = jumlahKetidakhadiran;
        }

        public Film Namafilm { get => namafilm; set => namafilm = value; }
        public Tiket JumlahKetidakhadiran { get => jumlahKetidakhadiran; set => jumlahKetidakhadiran = value; }

        public static List<Laporan3> BacaData()
        {
            string sql = "select f.judul, count(t.status_hadir) as jumlah_ketidakhadiran from films f inner " +
            "join tikets t on f.id = t.films_id inner join studios s on s.id = t.studios_id inner " +
            "join jadwal_films jf on t.jadwal_film_id = jf.id inner join pegawais p on p.id = t.operator_id inner " +
            "join invoices i on i.id = t.invoices_id where t.status_hadir = '0' group by f.judul " +
            "order by jumlah_ketidakhadiran desc LIMIT 3; ";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Laporan3> listLaporan3 = new List<Laporan3>();
            while (hasil.Read() == true)
            {
                Film film = new Film();
                Tiket tiket = new Tiket();
                film.Judul = hasil.GetValue(0).ToString();
                tiket.StatusHadir = int.Parse(hasil.GetValue(1).ToString());
                Laporan3 tampung = new Laporan3(film, tiket);
                listLaporan3.Add(tampung);
            }
            return listLaporan3;
        }
    }
}
