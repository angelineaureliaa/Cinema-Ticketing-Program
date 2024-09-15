using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Laporan4
    {
        private Studio studio;
        private Cinema namaCabang;
        private Tiket jumlahKursiKosong;

        public Laporan4(Studio studio, Cinema namaCabang, Tiket jumlahKursiKosong)
        {
            Studio = studio;
            NamaCabang = namaCabang;
            JumlahKursiKosong = jumlahKursiKosong;
        }

        public Studio Studio { get => studio; set => studio = value; }
        public Cinema NamaCabang { get => namaCabang; set => namaCabang = value; }
        public Tiket JumlahKursiKosong { get => jumlahKursiKosong; set => jumlahKursiKosong = value; }

        public static List<Laporan4> BacaData()
        {
            string sql = "select Hasil.studioNama, Hasil.cinemaNama, " +
                "Hasil.studioId, sum(Hasil.totalKapasitas_Kosong)" +
                " from (select i.tanggal, s.kapasitas-count(t.nomor_kursi) as " +
                "totalKapasitas_Kosong, s.nama as studioNama, s.id as studioId, " +
                "c.nama_cabang as cinemaNama, c.id as cinemaId from tikets t inner join " +
                "invoices i on t.invoices_id=i.id inner join studios s on t.studios_id=s.id " +
                "inner join cinemas c on s.cinemas_id=c.id where month(i.tanggal)='5' " +
                "group " +
                "by i.tanggal)as Hasil " +
                "group by Hasil.studioId " +
                "order by sum(Hasil.totalKapasitas_Kosong) asc " +
                "limit 3";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Laporan4> listLaporan = new List<Laporan4>();
            while (hasil.Read() == true)
            {
                Studio s = new Studio();
                s.Nama = hasil.GetValue(0).ToString();
                s.Id = int.Parse(hasil.GetValue(2).ToString());

                Cinema c = new Cinema();
                c.Nama_cabang = hasil.GetValue(1).ToString();

                Tiket t = new Tiket();
                t.NomorKursi = hasil.GetValue(3).ToString();
                Laporan4 tampung = new Laporan4(s, c, t);
                listLaporan.Add(tampung);
            }
            return listLaporan;
        }
    }
}
