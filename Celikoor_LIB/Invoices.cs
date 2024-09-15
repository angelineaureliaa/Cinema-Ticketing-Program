using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Invoices
    {
        #region data members
        private int id;
        private DateTime tanggal;
        private double grand_total;
        private double diskon_nominal;
        private Konsumen konsumen_id;
        private Pegawai kasir_id;
        private string status;
        private List<Tiket> listTiket;
        #endregion

        #region constructors
        public Invoices(int id, DateTime tanggal, double grand_total,
                        double diskon_nominal, Konsumen konsumen_id)
        {
            Id = id;
            Tanggal = tanggal;
            Grand_total = grand_total;
            Diskon_nominal = diskon_nominal;
            Konsumen_id = konsumen_id;
            Kasir_id = null;
            ListTiket = listTiket;
        }

        public Invoices()
        {
            Id = id;
            Tanggal = tanggal;
            Grand_total = grand_total;
            Diskon_nominal = diskon_nominal;
            Konsumen_id = konsumen_id;
            Kasir_id = kasir_id;
            Status = status;
        }
        #endregion

        #region properties
        public int Id { get => id; set => id = value; }
        public DateTime Tanggal { get => tanggal; set => tanggal = value; }
        public double Grand_total { get => grand_total; set => grand_total = value; }
        public double Diskon_nominal { get => diskon_nominal; set => diskon_nominal = value; }
        public Konsumen Konsumen_id { get => konsumen_id; set => konsumen_id = value; }
        public Pegawai Kasir_id { get => kasir_id; set => kasir_id = value; }
        public string Status { get => status; set => status = value; }
        public List<Tiket> ListTiket { get => listTiket; set => listTiket = value; }
        #endregion

        #region methods
        //method tambah data
        public static List<Invoices> BacaData(string kriteria)
        {
            string sql = "";
            if (kriteria == "") //jika tidak ada kriteria yang diisikan
            {

                sql = "select * from invoices";
            }
            else if (kriteria == "1")
            {
                sql = "select * from invoices where id = (select max(id) from invoices)";
            }

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Invoices> listInvoices = new List<Invoices>(); //hasil list untuk menampung data

            while (hasil.Read() == true) //selama masih ada data atau masih bisa membaca data
            {
                Konsumen k = new Konsumen();
                k.Id = int.Parse(hasil.GetValue(4).ToString());
                Pegawai p = new Pegawai();
                p.Id = int.Parse(hasil.GetValue(5).ToString());
                Invoices i = new Invoices();
                i.Id = int.Parse(hasil.GetValue(0).ToString());
                i.Tanggal = DateTime.Parse(hasil.GetValue(1).ToString());
                i.Grand_total = double.Parse(hasil.GetValue(2).ToString());
                i.Diskon_nominal = double.Parse(hasil.GetValue(3).ToString());
                i.Konsumen_id = k;
                i.Kasir_id = p;
                i.Status = hasil.GetValue(6).ToString();

                listInvoices.Add(i);
            }
            return listInvoices;
        }
       
        public static void TambahData(Invoices i)
        {
            string sql1 = "insert into invoices(id, tanggal, grand_total, diskon_nominal, konsumens_id, kasir_id, status) values ('" +
                         i.Id + "','" + i.Tanggal.ToString("yy-MM-dd") + "','" + i.Grand_total + "','" + i.Diskon_nominal + "','" +
                         i.Konsumen_id.Id + "','" + i.Kasir_id.Id + "','" + i.Status + "')";
            Koneksi.JalankanPerintahNonQuery(sql1);
        }

        public static List<Invoices> BacaDataPending()
        {

            string sql = "select i.* from invoices i inner join konsumens k on " +
            "i.konsumens_id = k.id inner join pegawais p on p.id = i.kasir_id" +
            " where i.status ='PENDING'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Invoices> listInvoices = new List<Invoices>();
            while (hasil.Read() == true)
            {
                Invoices i = new Invoices();
                i.Id = int.Parse(hasil.GetValue(0).ToString());
                i.Tanggal = DateTime.Parse(hasil.GetValue(1).ToString());
                i.Grand_total = double.Parse(hasil.GetValue(2).ToString());
                i.Diskon_nominal = double.Parse(hasil.GetValue(3).ToString());
                Konsumen k = new Konsumen();
                k.Id = int.Parse(hasil.GetValue(4).ToString());
                i.konsumen_id = k;
                Pegawai p = new Pegawai();
                p.Id = int.Parse(hasil.GetValue(5).ToString());
                i.Kasir_id = p;
                i.Status = hasil.GetValue(6).ToString();
                listInvoices.Add(i);
            }
            return listInvoices;
        }

        public static void UpdateDataPending(Invoices i)
        {
            string sql = "update invoices set kasir_id = '" + i.Kasir_id + "'where id ='" + i.Id + " and ";
        }

        public static void UpdateDataValid(Invoices i)
        {
            string sql = "update invoices set kasir_id = '" + i.Kasir_id + "'where id ='" + i.Id + "';";
        }
        public static List<Invoices> BacaDataValid()
        {

            string sql = "select i.* from invoices i inner join konsumens k on " +
            "i.konsumens_id = k.id inner join pegawais p on p.id = i.kasir_id" +
            " where i.status ='VALIDASI'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Invoices> listInvoices = new List<Invoices>();
            while (hasil.Read() == true)
            {
                Invoices i = new Invoices();
                i.Id = int.Parse(hasil.GetValue(0).ToString());
                i.Tanggal = DateTime.Parse(hasil.GetValue(1).ToString());
                i.Grand_total = double.Parse(hasil.GetValue(2).ToString());
                i.Diskon_nominal = double.Parse(hasil.GetValue(3).ToString());
                Konsumen k = new Konsumen();
                k.Id = int.Parse(hasil.GetValue(4).ToString());
                i.konsumen_id = k;
                Pegawai p = new Pegawai();
                p.Id = int.Parse(hasil.GetValue(5).ToString());
                i.Kasir_id = p;
                i.Status = hasil.GetValue(6).ToString();
                listInvoices.Add(i);
            }
            return listInvoices;
        }

        public static Invoices AmbilDataByKode(string id)
        {
            string sql = "select i.* from invoices i inner join konsumens k on " +
            "i.konsumens_id = k.id inner join pegawais p on p.id = i.kasir_id" +
            " where i.id ='" + id + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            if (hasil.Read() == true)
            {
                //masukkan hasil pengambilan data ke objek kategori
                Invoices i = new Invoices();
                i.Id = int.Parse(hasil.GetValue(0).ToString());
                i.Tanggal = DateTime.Parse(hasil.GetValue(1).ToString());
                i.Grand_total = double.Parse(hasil.GetValue(2).ToString());
                i.Diskon_nominal = double.Parse(hasil.GetValue(3).ToString());
                Konsumen k = new Konsumen();
                k.Id = int.Parse(hasil.GetValue(4).ToString());
                i.konsumen_id = k;
                Pegawai p = new Pegawai();
                p.Id = int.Parse(hasil.GetValue(5).ToString());
                i.Kasir_id = p;
                i.Status = hasil.GetValue(6).ToString();
                return i;
            }
            else
            {
                return null;
            }
        }
        public static void ValidasiInvoice(Invoices i)
        {
            string sql = "update invoices set status = 'VALIDASI' and kasir_id = '" + i.Kasir_id + "' where id='" + i.Id +
                "' and status= 'PENDING'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }
        public static void TerbayarInvoice(Invoices i)
        {
            string sql = "update invoices set status = 'TERBAYAR' and kasir_id = '" + i.Kasir_id + "' where id='" + i.Id +
                "' and status= 'VALIDASI'";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static List<Invoices> BacaDataTerbayar()
        {

            string sql = "select i.* from invoices i inner join konsumens k on " +
            "i.konsumens_id = k.id inner join pegawais p on p.id = i.kasir_id" +
            " where i.status ='TERBAYAR'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Invoices> listInvoices = new List<Invoices>();
            while (hasil.Read() == true)
            {
                Invoices i = new Invoices();
                i.Id = int.Parse(hasil.GetValue(0).ToString());
                i.Tanggal = DateTime.Parse(hasil.GetValue(1).ToString());
                i.Grand_total = double.Parse(hasil.GetValue(2).ToString());
                i.Diskon_nominal = double.Parse(hasil.GetValue(3).ToString());
                Konsumen k = new Konsumen();
                k.Id = int.Parse(hasil.GetValue(4).ToString());
                i.konsumen_id = k;
                Pegawai p = new Pegawai();
                p.Id = int.Parse(hasil.GetValue(5).ToString());
                i.Kasir_id = p;
                i.Status = hasil.GetValue(6).ToString();
                listInvoices.Add(i);
            }
            return listInvoices;
        }

        public static List<Invoices> BacaDataId(string kriteria)
        {

            string sql = "select p.nama, k.nama, i.grand_total, i.diskon_nominal from invoices i" +
                " inner join konsumens k on k.id = i.konsumens_id inner join pegawais p on " +
                "p.id = i.kasir_id where i.id ='" + kriteria + "'";
            

            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);

            List<Invoices> listInvoices = new List<Invoices>(); //hasil list untuk menampung data

            while (hasil.Read() == true) //selama masih ada data atau masih bisa membaca data
            {
                Invoices i = new Invoices();
                i.Kasir_id = new Pegawai();
                i.Kasir_id.Nama = hasil.GetValue(0).ToString();
                i.Konsumen_id = new Konsumen();
                i.Konsumen_id.Nama = hasil.GetValue(1).ToString();
                i.Grand_total = double.Parse(hasil.GetValue(2).ToString());
                i.Diskon_nominal = double.Parse(hasil.GetValue(3).ToString());
                listInvoices.Add(i);
            }
            return listInvoices;
        }
        #endregion
    }
}
