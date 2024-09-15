using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Tiket
    {
        #region data members
        private Invoices invoices;
        private string nomorKursi;
        private int statusHadir;
        private Pegawai operators;
        private double harga;
        private PenjadwalanFilm jadwalFilm;
        private Studio studio;
        private Film film;
        #endregion

        #region properties
        public Invoices Invoices { get => invoices; set => invoices = value; }
        public string NomorKursi { get => nomorKursi; set => nomorKursi = value; }
        public int StatusHadir { get => statusHadir; set => statusHadir = value; }
        public Pegawai Operators { get => operators; set => operators = value; }
        public double Harga { get => harga; set => harga = value; }
        public PenjadwalanFilm JadwalFilm { get => jadwalFilm; set => jadwalFilm = value; }
        public Studio Studio { get => studio; set => studio = value; }
        public Film Film { get => film; set => film = value; }
        #endregion

        #region constructors
        public Tiket()
        {
        }

        public Tiket(Invoices invoices, string nomorKursi, int statusHadir, double harga,
                     PenjadwalanFilm jadwalFilm, Studio studio, Film film)
        {
            Invoices = invoices;
            NomorKursi = nomorKursi;
            StatusHadir = statusHadir;
            Harga = harga;
            JadwalFilm = jadwalFilm;
            Studio = studio;
            Film = film;
        }
        #endregion

        #region methods
        public static void UbahStatusTiket(int idInvoice, string kodeKursi)
        {
            string sql = "UPDATE tikets SET `status_hadir`='1' " +
                "WHERE `invoices_id`='" + idInvoice + "' and`nomor_kursi`='" + kodeKursi + "';";
            Koneksi.JalankanPerintahNonQuery(sql);
        }
        //method tambah
        public static void TambahData(Tiket t)
        {
            string sql = "INSERT INTO tikets (invoices_id, nomor_kursi, status_hadir, harga, jadwal_film_id, studios_id, films_id,operator_id) values('" +
                        t.Invoices.Id + "', '" + t.NomorKursi + "', '" + t.StatusHadir + "','" + t.Harga + "','" +
                        t.JadwalFilm.Id + "','" + t.Studio.Id + "','" + t.Film.Id + "','3')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }
        //baca data 
        public static List<Tiket> BacaData(string kriteria, string nilaiKriteria, string nilaiKriteria2, string nilaiKriteria3)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from tikets";
            }
            if (kriteria == "totalKursi")
            {
                sql = "select t.* from tikets t inner join films f on f.id = t.films_id = '" + nilaiKriteria +
                    "'inner join jadwal_films jf on jf.id = t.jadwal_film_id and jf.tanggal = '" + nilaiKriteria2 +
                    "'inner join studios s on s.id = t.studios_id = '" + nilaiKriteria3 + "'";
            }
            else
            {
                sql = "select * from tikets" +
                      "where s." + kriteria + " like '%" + nilaiKriteria + "%'; ";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Tiket> listTiket = new List<Tiket>();
            while (hasil.Read() == true)
            {
                Tiket t = new Tiket();

                t.Invoices = new Invoices();
                t.Invoices.Id = int.Parse(hasil.GetValue(0).ToString());

                t.NomorKursi = hasil.GetValue(1).ToString();
                t.StatusHadir = int.Parse(hasil.GetValue(2).ToString());

                t.Operators = new Pegawai();
                t.Operators.Id = int.Parse(hasil.GetValue(3).ToString());

                t.Harga = double.Parse(hasil.GetValue(4).ToString());

                t.JadwalFilm = new PenjadwalanFilm();
                t.JadwalFilm.Id = int.Parse(hasil.GetValue(5).ToString());

                t.Studio = new Studio();
                t.Studio.Id = int.Parse(hasil.GetValue(6).ToString());

                t.Film = new Film();
                t.Film.Id = int.Parse(hasil.GetValue(7).ToString());

                listTiket.Add(t);
            }
            return listTiket;
        }

        public static List<Tiket> BacaTiketDibeli(string kriteria1, string kriteria2, string kriteria3,string kriteria4)
        {
            string kursi = "SELECT nomor_kursi from tikets where nomor_kursi LIKE '%"+kriteria4+"%' and jadwal_film_id = '" + kriteria1+"' and studios_id='"+kriteria2+"' and films_id='"+kriteria3+"';";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(kursi);
            List<Tiket> kursiDiisi = new List<Tiket>();
            while (hasil.Read() == true)
            {
                Tiket tampung = new Tiket();
                tampung.NomorKursi = hasil.GetValue(0).ToString();
                kursiDiisi.Add(tampung);
            }
            return kursiDiisi;
        }
        public override string ToString()
        {
            return NomorKursi;
        }

        public static List<Tiket> BacaDataCetakTiket(string kriteria)
        {
           string sql = "select t.nomor_kursi, f.judul, jf.tanggal, jf.jam_pemutaran,s.nama, c.nama_cabang from " +
                "tikets t inner join invoices i " +
                "on t.invoices_id=i.id inner join jadwal_films jf on jf.id=t.jadwal_film_id inner join studios s on s.id =" +
                " t.studios_id inner join cinemas c on s.cinemas_id = c.id inner join films f" +
                " on f.id = t.films_id where t.status_hadir = '1' and i.id='" + kriteria + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<Tiket> listTiket = new List<Tiket>();
            while (hasil.Read() == true)
            {
                Tiket t = new Tiket();
                t.NomorKursi = hasil.GetValue(0).ToString();
                t.Film = new Film();
                t.Film.Judul = hasil.GetValue(1).ToString();
                t.JadwalFilm = new PenjadwalanFilm();
                t.JadwalFilm.Tanggal = DateTime.Parse(hasil.GetValue(2).ToString());
                t.JadwalFilm.JamPemutaran = hasil.GetValue(3).ToString();
                t.Studio = new Studio();
                t.Studio.Nama = hasil.GetValue(4).ToString();
                t.Studio.Cinema = new Cinema();
                t.Studio.Cinema.Nama_cabang = hasil.GetValue(5).ToString();
                listTiket.Add(t);
            }
            return listTiket;
        }

        public static void CetakTiket(int id)
        {
            StreamWriter tempFile = new StreamWriter("ani");
            List<Tiket> listTiket = Tiket.BacaDataCetakTiket(id.ToString());
            List<Invoices> listInvoice = Invoices.BacaDataId(id.ToString());
            string namaKonsumen = listInvoice[0].Konsumen_id.Nama;
            tempFile.WriteLine("Nama Konsumen : " + namaKonsumen);
            tempFile.WriteLine("=".PadRight(50, '='));
            for(int i=0; i < listTiket.Count; i++)
            {
                tempFile.WriteLine("Judul Film     : " + listTiket[i].Film.Judul + "\n" +
                                   "Cinema         : " + listTiket[i].Studio.Cinema.Nama_cabang + "\n" +
                                   "Studio         : " + listTiket[i].Studio.Nama + "\n" +
                                   "Tanggal        : " + listTiket[i].JadwalFilm.Tanggal.ToShortDateString() + "\n" +
                                   "Jam Pemutaran  : " + listTiket[i].JadwalFilm.JamPemutaran + "\n"+
                                   "Nomor Kursi    : " + listTiket[i].NomorKursi + "\n");
                tempFile.WriteLine("-".PadRight(50, '-'));
            }
            tempFile.WriteLine("");
            tempFile.WriteLine("Nama Kasir  : " + listInvoice[0].Kasir_id.Nama);
            tempFile.WriteLine("Total Harga : " + listInvoice[0].Grand_total.ToString());
            tempFile.WriteLine("Diskon      : " + listInvoice[0].Diskon_nominal.ToString());
            tempFile.WriteLine("Biaya Akhir : " + (listInvoice[0].Grand_total - listInvoice[0].Diskon_nominal).ToString());
            tempFile.Close();
            CustomPrint p = new CustomPrint(new System.Drawing.Font("courier new", 12), "ani");
            p.KirimkePrinter();
        }
        #endregion
    }
}
