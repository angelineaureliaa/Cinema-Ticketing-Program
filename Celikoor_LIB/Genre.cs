using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Genre
    {
        #region data members
        private int id;
        private string nama;
        private string deskripsi;
        #endregion

        #region constructor
        public Genre(int id, string nama, string deskripsi)
        {
            Id = id;
            Nama = nama;
            Deskripsi = deskripsi;
        }
        public Genre()
        {
            Id = id;
            Nama = nama;
            Deskripsi = deskripsi;
        }
        #endregion

        #region properties
        public int Id { get => id; set => id = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Deskripsi { get => deskripsi; set => deskripsi = value; }
        #endregion

        #region method
        //tambah
        public static void TambahData(Genre g)
        {
            string sql = "";
            sql = "insert into genres (nama, deskripsi) values('" + g.Nama + "', '" + g.Deskripsi + "')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }
        //baca data
        public static List<Genre> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from genres";
            }
            else
            {
                sql = "select * from genres where " + kriteria + "='" + nilaiKriteria + "'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Genre> listGenre = new List<Genre>();
            while (hasil.Read() == true)
            {
                Genre g = new Genre(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString());
                listGenre.Add(g);
            }
            return listGenre;
        }

        public static Boolean HapusData(Genre g)
        {
            string perintah = "delete from genres where id = '" + g.Id + "'";
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

        public static Genre AmbilDataByCode(string id)
        {
            string sql = "select * from genres where id = '" + id + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                //masukkan hasil pengambilan data ke objek kategori
                Genre g = new Genre(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    hasil.GetValue(2).ToString());
                return g;
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            return nama;
        }
        #endregion
    }
}
