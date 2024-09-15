using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Laporan2
    {
        private Cinema namaCabang;
        private Tiket pemasukan;

        public Laporan2(Cinema namaCabang, Tiket pemasukan)
        {
            NamaCabang = namaCabang;
            Pemasukan = pemasukan;
        }

        public Cinema NamaCabang { get => namaCabang; set => namaCabang = value; }
        public Tiket Pemasukan { get => pemasukan; set => pemasukan = value; }

        public static List<Laporan2> BacaData()
        {
            string sql = "select c.nama_cabang, sum(t.harga) " +
                "from invoices i inner join tikets t on i.id = t.invoices_id inner join " +
                "studios s on t.studios_id=s.id inner join " +
                "cinemas c on s.cinemas_id = c.id group by c.id order by sum(t.harga) desc Limit 3;";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Laporan2> listLaporan = new List<Laporan2>();
            while (hasil.Read() == true)
            {
                Cinema c = new Cinema();
                c.Nama_cabang = hasil.GetValue(0).ToString();
                Tiket t = new Tiket();
                t.Harga = int.Parse(hasil.GetValue(1).ToString());
                Laporan2 laporan = new Laporan2(c, t);
                listLaporan.Add(laporan);
            }
            return listLaporan;
        }
    }
}
