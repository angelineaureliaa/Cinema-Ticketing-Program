using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Cinema
    {
        #region data members
        private int id;
        private string nama_cabang;
        private string alamat;
        private DateTime tgl_dibuka;
        private string kota;
        #endregion

        #region constructor
        public Cinema(int id, string nama_cabang, string alamat, DateTime tgl_dibuka, string kota)
        {
            Id = id;
            Nama_cabang = nama_cabang;
            Alamat = alamat;
            Tgl_dibuka = tgl_dibuka;
            Kota = kota;
        }
        public Cinema()
        {
            Id = id;
            Nama_cabang = nama_cabang;
            Alamat = alamat;
            Tgl_dibuka = tgl_dibuka;
            Kota = kota;
        }
        #endregion

        #region properties
        public int Id { get => id; set => id = value; }
        public string Nama_cabang { get => nama_cabang; set => nama_cabang = value; }
        public string Alamat { get => alamat; set => alamat = value; }
        public DateTime Tgl_dibuka { get => tgl_dibuka; set => tgl_dibuka = value; }
        public string Kota { get => kota; set => kota = value; }
        #endregion

        #region methods
        //select = query
        //crud = non query
        public static void TambahData(Cinema c)
        {
            string sql = "insert into cinemas(Id, Nama_cabang, Alamat, Tgl_dibuka, Kota) values ('" + c.Id +
                         "','" + c.Nama_cabang.Replace("'", "\\'") + "','" + c.Alamat + "','" + c.Tgl_dibuka.ToString("yyyy-MM-dd") + "','" +
                         c.Kota + "')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static Boolean HapusData(Cinema c)
        {
            string perintah = "delete from cinemas where Id = '" + c.Id + "'";
            int jumlahDataBerubah = Koneksi.JalankanPerintahNonQuery(perintah);
            Boolean status;
            if (jumlahDataBerubah == 0)
            {
                status = false;
            }
            else
            {
                status = true;
            }
            return status;
        }

        public static List<Cinema> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from cinemas";
            }
            else
            {
                sql = "select * from cinemas where " + kriteria + "='" + nilaiKriteria + "'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Cinema> listCinema = new List<Cinema>();
            while (hasil.Read() == true)
            {
                Cinema c = new Cinema(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                     hasil.GetValue(2).ToString(),
                     DateTime.Parse(hasil.GetValue(3).ToString()),
                     hasil.GetValue(4).ToString());
                listCinema.Add(c);
            }
            return listCinema;
        }

        public static Cinema AmbilDataByCode(string id)
        {
            string sql = "select * from cinemas where id = '" + id + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                //masukkan hasil pengambilan data ke objek kategori
                Cinema c = new Cinema(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                                      hasil.GetValue(2).ToString(), DateTime.Parse(hasil.GetValue(3).ToString()),
                                      hasil.GetValue(4).ToString()); ;
                return c;
            }
            else
            {
                return null;
            }
        }

        public static List<Cinema> BacaDataUntukTiket(string kriteria, string nilaiKriteria, string nilaiKriteria2)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select* from cinemas";
            }
            else if (kriteria == "1")
            {
                sql = "select c.* from cinemas c inner join studios s on c.id = s.cinemas_id inner join film_studio fs " +
                      "on s.id = fs.studios_id inner join sesi_films sf on fs.studios_id = s.id inner join films f " +
                      "on sf.films_id = f.id inner join jadwal_films jf on sf.jadwal_film_id = jf.id where f.id = fs.films_id " +
                      "and fs.films_id = sf.films_id and fs.studios_id = sf.studios_id and sf.jadwal_film_id = jf.id and " +
                      "f.id = '" + nilaiKriteria + "' and jf.tanggal = '" + nilaiKriteria2 + "'; ";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Cinema> listCinema = new List<Cinema>();
            while (hasil.Read() == true)
            {
                Cinema c = new Cinema(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                     hasil.GetValue(2).ToString(),
                     DateTime.Parse(hasil.GetValue(3).ToString()),
                     hasil.GetValue(4).ToString());
                listCinema.Add(c);
            }
            return listCinema;
        }
        public override string ToString()
        {
            return nama_cabang;
        }
        #endregion
    }
}
