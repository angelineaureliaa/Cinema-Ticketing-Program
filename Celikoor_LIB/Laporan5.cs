using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Laporan5
    {
        private Konsumen namaKonsumen;
        private Tiket frekuensiNonton;

        public Laporan5()
        {
            this.NamaKonsumen = namaKonsumen;
            this.FrekuensiNonton = frekuensiNonton;
        }

        public Konsumen NamaKonsumen { get => namaKonsumen; set => namaKonsumen = value; }
        public Tiket FrekuensiNonton { get => frekuensiNonton; set => frekuensiNonton = value; }

        public static List<Laporan5> BacaData()
        {
            string sql = "select k.nama, count(t.nomor_kursi) as frekuensi_nonton " +
                "from films f inner join tikets t on f.id = t.films_id inner join studios s " +
                "on s.id = t.studios_id inner join jadwal_films jf on t.jadwal_film_id = jf.id " +
                "inner join pegawais p on p.id = t.operator_id inner join invoices i on i.id = " +
                "t.invoices_id " +
                "inner join konsumens k on k.id = i.konsumens_id inner join genre_film gf on " +
                "gf.films_id = f.id inner join genres g on g.id = gf.genres_id " +
                "where g.nama ='komedi' group by k.id " +
                "order by frekuensi_nonton desc " +
                "limit 10";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Laporan5> listLaporan = new List<Laporan5>();
            while (hasil.Read() == true)
            {
                Laporan5 tampung = new Laporan5();
                Konsumen namaKonsumen = new Konsumen();
                namaKonsumen.Nama = hasil.GetValue(0).ToString();
                Tiket t = new Tiket();
                t.NomorKursi = hasil.GetValue(1).ToString();
                tampung.NamaKonsumen = namaKonsumen;
                tampung.FrekuensiNonton = t;
                listLaporan.Add(tampung);
            }
            return listLaporan;
        }
    }
}
