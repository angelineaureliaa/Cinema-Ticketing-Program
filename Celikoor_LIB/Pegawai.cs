using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Pegawai
    {
        #region data members
        private int id;
        private string nama;
        private string email;
        private string username;
        private string password;
        private string roles;
        #endregion

        #region constructors
        public Pegawai(int id, string nama, string email, string username, string password, string roles)
        {
            Id = id;
            Nama = nama;
            Email = email;
            Username = username;
            Password = password;
            Roles = roles;
        }

        public Pegawai()
        {
        }

        #endregion

        #region properties
        public int Id { get => id; set => id = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Email { get => email; set => email = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Roles { get => roles; set => roles = value; }
        #endregion

        #region methods
        public static List<Pegawai> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from pegawais";
            }
            else
            {
                sql = "select * from pegawais" + " where " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
                ;

            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            //buat list untuk menampung data
            List<Pegawai> listPegawai = new List<Pegawai>();
            while (hasil.Read() == true)
            {
                //baca data dari MySqlReader dan simpan di objek
                Pegawai p = new Pegawai(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                            hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), hasil.GetValue(5).ToString());
                listPegawai.Add(p);

            }
            return listPegawai;
        }
        public static void TambahData(Pegawai p)
        {
            string sql = "insert into pegawais(nama, email,username, password, roles) values('" +
                  p.Nama + "','" + p.Email + "','" + p.Username + "',SHA2('" + p.Password +
                  "',512)" + ",'" + p.Roles + "')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static Pegawai CekLogin(string username, string password)
        {
            string sql = "";
            sql = "select * from Pegawais" + " where Username ='" + username + "' AND Password = SHA2('" + password + "', 512)";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            while (hasil.Read() == true) // selama masih ada data yang bisa dibaca
            {
                Pegawai p = new Pegawai(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                            hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), hasil.GetValue(5).ToString());
                return p;
            }
            return null;

        }

        public static bool CekPegawai(string kriteria, string nilaiKriteria)
        {
            string sql = "select * from pegawais where " + kriteria + "='" + nilaiKriteria + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            bool akhir = true;
            while (hasil.Read() == true)
            {
                akhir = false;
            }
            return akhir;
        }

        public static bool HapusData(Pegawai p)
        {
            string sql = "delete from pegawais where id = '" + p.Id + "'";

            int jumlahDataBerubah = Koneksi.JalankanPerintahNonQuery(sql);
            if (jumlahDataBerubah == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static Pegawai AmbilDataByKode(string kode)
        {
            string sql = "select * from pegawais where id = '" + kode + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                //masukkan hasil pengambilan data ke objek kategori
                Pegawai p = new Pegawai(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                            hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(), hasil.GetValue(5).ToString());
                return p;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
