using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Aktor
    {
        #region data members
        private int idAktor;
        private string namaAktor;
        private DateTime tanggalLahirAktor;
        private string genderAktor;
        private string negaraAsalAktor;
        #endregion

        #region constructors
        public Aktor(int idAktor, string namaAktor, DateTime tanggalLahirAktor, string genderAktor, string negaraAsalAktor)
        {
            IdAktor = idAktor;
            NamaAktor = namaAktor;
            TanggalLahirAktor = tanggalLahirAktor;
            GenderAktor = genderAktor;
            NegaraAsalAktor = negaraAsalAktor;
        }
        #endregion

        #region properties
        public int IdAktor { get => idAktor; set => idAktor = value; }
        public string NamaAktor { get => namaAktor; set => namaAktor = value; }
        public DateTime TanggalLahirAktor { get => tanggalLahirAktor; set => tanggalLahirAktor = value; }
        public string GenderAktor { get => genderAktor; set => genderAktor = value; }
        public string NegaraAsalAktor { get => negaraAsalAktor; set => negaraAsalAktor = value; }
        #endregion

        #region methods
        //baca data
        public static List<Aktor> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "") //jika tidak ada kriteria yang diisikan
            {
                sql = "select * from aktors";
            }
            else if (kriteria == "1")
            {
                sql = "select * from aktors where id!='" + nilaiKriteria + "'";
            }
            else
            {
                sql = "select * from aktors" + " where " + kriteria + "='" + nilaiKriteria + "'";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Aktor> listAktor = new List<Aktor>(); //hasil list untuk menampung data

            while (hasil.Read() == true) //selama masih ada data atau masih bisa membaca data
            {
                Aktor a = new Aktor(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), DateTime.Parse(hasil.GetValue(2).ToString()),
                          hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString());

                listAktor.Add(a);
            }
            return listAktor;
        }

        public static List<Aktor> BacaDataSatu(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "") //jika tidak ada kriteria yang diisikan
            {
                sql = "select * from aktors";
            }
            else
            {
                sql = "select * from aktors" + " where " + kriteria + " LIKE '%" + nilaiKriteria + "%'";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Aktor> listAktor = new List<Aktor>(); //hasil list untuk menampung data

            while (hasil.Read() == true) //selama masih ada data atau masih bisa membaca data
            {
                Aktor a = new Aktor(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), DateTime.Parse(hasil.GetValue(2).ToString()),
                          hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString());

                listAktor.Add(a);
            }
            return listAktor;
        }

        //tambah
        public static Boolean TambahData(Aktor a)
        {
            string sql = "INSERT INTO aktors (id, nama, tgl_lahir, gender, negara_asal) values ('" + a.IdAktor + "','" + a.NamaAktor.Replace("'", "\\'") + "','" + a.tanggalLahirAktor.ToString("yyyy-MM-dd") + "','" + a.genderAktor +
                         "','" + a.negaraAsalAktor + "')";

            int JumlahDitambahkan = Koneksi.JalankanPerintahNonQuery(sql);
            Boolean Status;

            if (JumlahDitambahkan == 0)
            {
                Status = false;
            }
            else
            {
                Status = true;
            }

            return Status;
        }

        //ubah
        public static void UbahData(Aktor a)
        {
            string sql = "";
            sql = "update aktors set nama ='" + a.NamaAktor.Replace("'", "\\'") + "', tgl_lahir='" +
                  a.TanggalLahirAktor.ToString("yyyy-MM-dd") + "', gender='" + a.GenderAktor + "', negara_asal = '" + a.NegaraAsalAktor + "'" +
                  " where id='" + a.IdAktor + "'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        //hapus
        public static Boolean HapusData(Aktor a)
        {
            string perintah = "delete from aktors where id = '" + a.IdAktor + "'";
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

        public static Aktor AmbilDataByKode(string id)
        {
            string sql = "select * from aktors where id = '" + id + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                //masukkan hasil pengambilan data ke objek kategori
                Aktor a = new Aktor(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), DateTime.Parse(hasil.GetValue(2).ToString()),
                          hasil.GetValue(3).ToString(), hasil.GetValue(4).ToString());
                return a;
            }
            else
            {
                return null;
            }
        }

        public override string ToString()
        {
            return namaAktor;
        }
        #endregion
    }
}
