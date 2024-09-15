using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class PenjadwalanFilm
    {
        #region data members
        private int id;
        private DateTime tanggal;
        private string jamPemutaran;
        #endregion

        #region constructor
        public PenjadwalanFilm(int id, DateTime tanggal, string jamPemutaran)
        {
            Id = id;
            Tanggal = tanggal;
            JamPemutaran = jamPemutaran;
        }

        public PenjadwalanFilm()
        {
        }

        #endregion


        #region properties
        public int Id { get => id; set => id = value; }
        public DateTime Tanggal { get => tanggal; set => tanggal = value; }
        public string JamPemutaran { get => jamPemutaran; set => jamPemutaran = value; }
        #endregion


        #region methods
        public static void TambahData(PenjadwalanFilm j)
        {
            string sql = "";
            sql = "insert into jadwal_films (tanggal, jam_pemutaran) values ('" + j.Tanggal.ToString("yyyy-MM-dd hh:mm:ss") +
                "','" + j.JamPemutaran + "')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static void UpdateData(PenjadwalanFilm j)
        {
            string sql = "";
            sql = "update jadwal_films set tanggal='" + j.Tanggal.ToString("yyyy-MM-dd") +
                "','jam_pemutaran='" + j.JamPemutaran + "' where id='" + j.Id + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static Boolean HapusData(PenjadwalanFilm pK)
        {
            string sql = "delete from jadwal_films where id = '" + pK.Id + "'";

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
        public static List<PenjadwalanFilm> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from jadwal_films";
            }
            else if (kriteria == "1")
            {
                sql = "select * from jadwal_films where " + nilaiKriteria;
            }
            else if (kriteria == "tanggal")
            {
                sql = "select * from jadwal_films where tanggal='" +  nilaiKriteria + "'";
            }
            else if (kriteria == "2")
            {
                sql = "select * from jadwal_films where id=(select max(id) from jadwal_films);";
            }
            else if (kriteria == "3")
            {
                //buat form pemesanan tiket, menampilkan tanggal tertentu sesuai film apa yang dipilih
                sql = " select jf.* from jadwal_films jf inner join sesi_films sf on jf.id = sf.jadwal_film_id inner join " +
                      "film_studio fs on sf.films_id = fs.films_id and sf.studios_id = fs.studios_id inner join films f on fs.films_id = f.id " +
                      "and f.id = '" + nilaiKriteria + "';";
            }
            else
            {
                sql = "select * from jadwal_films where " + kriteria  + "='" +nilaiKriteria + "'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<PenjadwalanFilm> listJadwalFilm = new List<PenjadwalanFilm>();
            while (hasil.Read() == true)
            {
                PenjadwalanFilm jf = new PenjadwalanFilm(int.Parse(hasil.GetValue(0).ToString()),
                    DateTime.Parse(hasil.GetValue(1).ToString()), hasil.GetValue(2).ToString());
                listJadwalFilm.Add(jf);
            }
            return listJadwalFilm;
        }


        #endregion


        public override string ToString()
        {
            return Tanggal.ToString("yyyy-MM-dd");
        }

        //public override string ToString()
        //{
        //    return Id.ToString();
        //}
    }
}
