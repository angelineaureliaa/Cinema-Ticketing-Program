using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Jenis_Studio
    {
        #region data member
        private int id;
        private string nama;
        private string deskripsi;
        #endregion

        #region constructor
        public Jenis_Studio(int id, string nama, string deskripsi)
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

        #region methods
        public static void TambahData(Jenis_Studio js)
        {
            string sql = "insert into jenis_studios(id, nama, deskripsi) values " +
                "('" + js.Id + "','" + js.Nama + "','" + js.deskripsi + "')";

            Koneksi.JalankanPerintahNonQuery(sql);
        }
        public static Boolean HapusData(Jenis_Studio js)
        {
            string perintah = "delete from jenis_studios where id = '" + js.Id + "'";
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
        public static List<Jenis_Studio> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from jenis_studios";
            }
            else
            {
                sql = "select * from jenis_studios where " + kriteria + "='" + nilaiKriteria + "'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Jenis_Studio> listJenisStudio = new List<Jenis_Studio>();
            while (hasil.Read() == true)
            {
                Jenis_Studio js = new Jenis_Studio(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    hasil.GetValue(2).ToString());
                listJenisStudio.Add(js);

            }
            return listJenisStudio;
        }
        public override string ToString()
        {
            return Nama;
        }
        #endregion
    }
}
