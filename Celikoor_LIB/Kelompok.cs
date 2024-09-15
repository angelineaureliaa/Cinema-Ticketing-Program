using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Kelompok
    {
        #region data members
        private int id;
        private string nama;
        #endregion

        #region Constructor
        public Kelompok(int id, string nama)
        {
            Id = id;
            Nama = nama;
        }
        #endregion

        #region Properties
        public int Id { get => id; set => id = value; }
        public string Nama { get => nama; set => nama = value; }
        #endregion

        #region methods
        public static Kelompok AmbilDataByCode(string id)
        {
            string sql = "select * from kelompoks where id = '" + id + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                //masukkan hasil pengambilan data ke objek kategori
                Kelompok k = new Kelompok(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString());
                return k;
            }
            else
            {
                return null;
            }
        }
        public static void TambahData(Kelompok k)
        {
            string sql = "insert into kelompoks(id, nama) values ('" + k.Id + "','" + k.Nama.Replace("'", "\\'") + "')";

            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static void UbahData(Kelompok k)
        {
            throw new NotImplementedException();
        }

        public static List<Kelompok> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from kelompoks";
            }
            else
            {
                sql = "select * from kelompoks where " + kriteria + "='" + nilaiKriteria + "'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Kelompok> listKelompok = new List<Kelompok>();
            while (hasil.Read() == true)
            {
                Kelompok kel = new Kelompok(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString());
                listKelompok.Add(kel);
            }
            return listKelompok;
        }
        public static Boolean HapusData(Kelompok k)
        {
            string perintah = "delete from kelompoks where id = '" + k.Id + "'";
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
        #endregion
    }
}
