using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Konsumen
    {
        #region data members
        private int id;
        private string nama;
        private string email;
        private string no_hp;
        private string gender;
        private DateTime tgl_lahir;
        private double saldo;
        private string username;
        private string password;
        #endregion

        #region constructor
        public Konsumen(int id, string nama, string email, string no_hp, string gender,
            DateTime tgl_lahir, string username, string password)
        {
            Id = id;
            Nama = nama;
            Email = email;
            No_hp = no_hp;
            Gender = gender;
            Tgl_lahir = tgl_lahir;
            Saldo = saldo;
            Username = username;
            Password = password;
        }

        public Konsumen()
        {
        }
        #endregion

        #region properties
        public int Id { get => id; set => id = value; }
        public string Nama { get => nama; set => nama = value; }
        public string Email { get => email; set => email = value; }
        public string No_hp { get => no_hp; set => no_hp = value; }
        public string Gender { get => gender; set => gender = value; }
        public DateTime Tgl_lahir { get => tgl_lahir; set => tgl_lahir = value; }
        public double Saldo { get => saldo; private set => saldo = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        #endregion

        #region methods
        public static void TambahData(Konsumen k)
        {
            string sql = "";
            sql = "insert into konsumens (nama, email, no_hp, gender, tgl_lahir, username, password, saldo)" +
                "values('"+k.Nama+"', '"+k.Email+"', '"+k.No_hp+"', '"+k.Gender+"', '"+k.Tgl_lahir+"', '"+k.Username+"', '"+k.Password+"', '2000000');";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static Konsumen CekLogin(string username, string password)
        {
            string sql = "";
            sql = "select id, nama, email, no_hp, gender, tgl_lahir, saldo, username, password from konsumens where username='" +
                username + "' AND password = SHA2('" + password + "', 512)";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            while (hasil.Read() == true)
            {
                Konsumen k = new Konsumen(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                    hasil.GetValue(2).ToString(), hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(),
                    DateTime.Parse(hasil.GetValue(5).ToString()),
                    hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString());
                return k;
            }
            return null;
        }

        public static List<Konsumen> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from konsumens";
            }
            else
            {
                sql = "select * from konsumens  where " + kriteria + " like '%" + nilaiKriteria + "%'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Konsumen> listKonsumen = new List<Konsumen>();
            while (hasil.Read() == true)
            {
                Konsumen k = new Konsumen(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                     hasil.GetValue(2).ToString(), hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(),
                     DateTime.Parse(hasil.GetValue(5).ToString()),
                     hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString());
                listKonsumen.Add(k);
            }
            return listKonsumen;
        }

        public static void UbahData(Konsumen k)
        {
            string sql = "";
            sql = "update konsumens set nama ='" + k.Nama.Replace("'", "\\'") + "', email='" +
                k.Email + "', no_hp='" + k.No_hp + "', username = '" + k.Username + "', " +
                "password=SHA2('" + k.Password + "', 512)" + " where id = '" + k.Id + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static Boolean HapusData(Konsumen pK)
        {
            string sql = "delete from konsumens where id = '" + pK.Id + "'";

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

        public static Konsumen AmbilDataByKode(string id)
        {
            string sql = "select * from konsumens where id = '" + id + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                //masukkan hasil pengambilan data ke objek kategori
                Konsumen k = new Konsumen(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(),
                     hasil.GetValue(2).ToString(), hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString(),
                     DateTime.Parse(hasil.GetValue(5).ToString()),
                     hasil.GetValue(7).ToString(), hasil.GetValue(8).ToString());
                return k;
            }
            else
            {
                return null;
            }
        }

        public static Boolean CekKonsumen(string kriteria, string nilaiKriteria)
        {
            string sql = "select * from konsumens where " + kriteria + "='" + nilaiKriteria + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            bool akhir = true;
            while (hasil.Read() == true)
            {
                akhir = false;
            }
            return akhir;
        }

        public static void UpdateSaldo (Konsumen k)
        {
            string sql = "";
            sql = "update konsumens set saldo ='" + k.Saldo
               + "' where id = '" + k.Id + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public override string ToString()
        {
            return Saldo.ToString();
        }
        #endregion
    }
}
