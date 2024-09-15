using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Studio
    {
        #region data member
        private int id;
        private string nama;
        private int kapasitas;
        private Jenis_Studio jenisStudio;
        private Cinema cinema;
        private int hargaWeekday;
        private int hargaWeekend;
        #endregion

        #region constructor
        public Studio(int id, string nama, int kapasitas, Jenis_Studio jenisStudio,
            Cinema cinema, int hargaWeekday, int hargaWeekend)
        {
            Id = id;
            Nama = nama;
            Kapasitas = kapasitas;
            JenisStudio = jenisStudio;
            Cinema = cinema;
            HargaWeekday = hargaWeekday;
            HargaWeekend = hargaWeekend;
        }

        public Studio()
        {
        }
        #endregion

        #region properties
        public int Id { get => id; set => id = value; }
        public string Nama { get => nama; set => nama = value; }
        public int Kapasitas { get => kapasitas; set => kapasitas = value; }
        public Jenis_Studio JenisStudio { get => jenisStudio; set => jenisStudio = value; }
        public Cinema Cinema { get => cinema; set => cinema = value; }
        public int HargaWeekday { get => hargaWeekday; set => hargaWeekday = value; }
        public int HargaWeekend { get => hargaWeekend; set => hargaWeekend = value; }
        #endregion

        #region methods
        public static List<Studio> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select s.id,s.nama, s.kapasitas, j.id, c.id, s.harga_weekday," +
               "s.harga_weekend,j.nama, j.deskripsi, c.nama_cabang, c.alamat, c.tgl_dibuka," +
                "c.kota from studios s inner join cinemas c on s.cinemas_id = c.id inner join " +
                "jenis_studios j on s.jenis_studios_id = j.id";
            }
            else if (kriteria == "1")
            {
                sql = "select s.id,s.nama, s.kapasitas, j.id, c.id, s.harga_weekday, s.harga_weekend,j.nama, j.deskripsi, c.nama_cabang, c.alamat, c.tgl_dibuka, " +
                      "c.kota " +
                      "from studios s inner join cinemas c on s.cinemas_id = c.id inner join jenis_studios j on s.jenis_studios_id = j.id " +
                      "where " + nilaiKriteria;
            }
            else if (kriteria == "s.nama")
            {
                sql = "select s.id,s.nama, s.kapasitas, j.id, c.id, s.harga_weekday, s.harga_weekend,j.nama, j.deskripsi, c.nama_cabang, c.alamat, c.tgl_dibuka, " +
                      "c.kota " +
                      "from studios s inner join cinemas c on s.cinemas_id = c.id inner join jenis_studios j on s.jenis_studios_id = j.id " +
                      "where " + kriteria + " = " + nilaiKriteria;
            }
            else
            {
                sql = "select s.id,s.nama, s.kapasitas, j.id, c.id, s.harga_weekday, s.harga_weekend,j.nama, j.deskripsi, c.nama_cabang, c.alamat, c.tgl_dibuka, " +
                    "c.kota " +
                    "from studios s inner join cinemas c on s.cinemas_id = c.id inner join jenis_studios j on s.jenis_studios_id = j.id " +
                    "where s." + kriteria + " like '%" + nilaiKriteria + "%'; ";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Studio> list = new List<Studio>();
            while (hasil.Read() == true)
            {
                Cinema c = new Cinema(int.Parse(hasil.GetValue(4).ToString()), hasil.GetValue(9).ToString(), hasil.GetValue(10).ToString(),
                    DateTime.Parse(hasil.GetValue(11).ToString()), hasil.GetValue(12).ToString());
                Jenis_Studio j = new Jenis_Studio(int.Parse(hasil.GetValue(3).ToString()), hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString());
                Studio s = new Studio(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), int.Parse(hasil.GetValue(2).ToString()),
                    j, c, int.Parse(hasil.GetValue(5).ToString()), int.Parse(hasil.GetValue(6).ToString()));
                list.Add(s);
            }
            return list;
        }

        public static Studio BacaDataSatu(string nilai, string nilaiS)
        {
            string sql = "select s.id,s.nama, s.kapasitas, j.id, c.id, s.harga_weekday, s.harga_weekend,j.nama, j.deskripsi, c.nama_cabang, c.alamat, c.tgl_dibuka, " +
                    "c.kota " +
                    "from studios s inner join cinemas c on s.cinemas_id = c.id inner join jenis_studios j on s.jenis_studios_id = j.id " +
                    "where s." + nilai + "='" + nilaiS + "'";

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            if (hasil.Read() == true)
            {
                Cinema c = new Cinema(int.Parse(hasil.GetValue(4).ToString()), hasil.GetValue(9).ToString(), hasil.GetValue(10).ToString(),
                    DateTime.Parse(hasil.GetValue(11).ToString()), hasil.GetValue(12).ToString());
                Jenis_Studio j = new Jenis_Studio(int.Parse(hasil.GetValue(3).ToString()), hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString());

                Studio studio = new Studio(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), int.Parse(hasil.GetValue(2).ToString()),
                    j, c, int.Parse(hasil.GetValue(5).ToString()), int.Parse(hasil.GetValue(6).ToString()));
                studio.Id = (int)hasil.GetValue(0);
                studio.Nama = hasil.GetValue(1).ToString();

                return studio;
            }
            else throw new Exception("Data tidak ditemukan");
        }
        public static void TambahData(Studio s)
        {
            string sql = "insert into studios (nama, kapasitas, jenis_studios_id, cinemas_id, harga_weekday, harga_weekend) " +
                "value ('" + s.Nama + "', '" + s.Kapasitas + "','" + s.JenisStudio.Id + "', '" + s.Cinema.Id + "','" + s.HargaWeekday + "', '" + s.HargaWeekend + "');";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static bool HapusData(string kode)
        {
            string sql = "delete from studios where id = '" + kode + "'";

            int jumlahDataBerubah = Koneksi.JalankanPerintahNonQuery(sql); //yang direturn kyk how many rows affected
            if (jumlahDataBerubah == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void UbahData(Studio s, string kode)
        {
            string sql = "update studios set `nama`='" + s.Nama + "', `kapasitas`='" + s.Kapasitas + "', `jenis_studios_id`='" + s.JenisStudio.Id + "', `cinemas_id`='" + s.Cinema.Id + "', " +
                "`harga_weekday`='" + s.HargaWeekday + "', `harga_weekend`='" + s.HargaWeekend + "' where `id`='" + kode + "';";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public override string ToString()
        {
            return nama;
        }
        #endregion
    }
}
