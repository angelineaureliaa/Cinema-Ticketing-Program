using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class SesiFilm
    {
        #region data members
        private PenjadwalanFilm jadwalFilm;
        private Studio studio;
        private Film film;
        #endregion

        #region constructor
        public SesiFilm(PenjadwalanFilm jadwalFilm, Studio studio, Film film)
        {
            JadwalFilm = jadwalFilm;
            Studio = studio;
            Film = film;
        }
        #endregion

        #region properties
        public PenjadwalanFilm JadwalFilm { get => jadwalFilm; set => jadwalFilm = value; }
        public Studio Studio { get => studio; set => studio = value; }
        public Film Film { get => film; set => film = value; }
        #endregion

        public static void TambahData(SesiFilm sf)
        {
            string sql = "";
            sql = "insert into sesi_films(jadwal_film_id, studios_id, films_id) values('" + sf.JadwalFilm.Id + "','" + sf.Studio.Id + "','" + sf.Film.Id + "')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }

        public static List<SesiFilm> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            //untuk ditampilkan pada datagridview
            if (kriteria == "")
            {
                sql = "select c.id, c.nama_cabang, c.alamat, c.tgl_dibuka, c.kota, js.id,  " +
                     "js.nama, js.deskripsi, s.id, s.nama, s.harga_weekday, s.harga_weekend, " +
                     "j.id, j.tanggal, j.jam_pemutaran, f.id, f.judul as judulFilm, f.sinopsis, f.tahun, f.durasi, " +
                     "k.id, f.bahasa, f.is_sub_indo, f.cover_image, f.diskon_nominal, k.nama, s.kapasitas from sesi_films" +
                     " sf inner join films f on sf.films_id = f.id inner join studios s on" +
                     " sf.studios_id = s.id inner join jadwal_films j on j.id = sf.jadwal_film_id" +
                     " inner join kelompoks k on k.id = f.kelompoks_id inner join jenis_studios js " +
                     "on js.id = s.jenis_studios_id inner join cinemas c on s.cinemas_id = c.id";
            }
            if (kriteria == "1")
            {
                sql = "select c.id, c.nama_cabang, c.alamat, c.tgl_dibuka, c.kota, js.id, " + 
                      "js.nama, js.deskripsi, s.id, s.nama, s.harga_weekday, s.harga_weekend, " + 
                      "j.id, j.tanggal, j.jam_pemutaran, f.id, f.judul as judulFilm, f.sinopsis, f.tahun, f.durasi, " + 
                      "k.id, f.bahasa, f.is_sub_indo, f.cover_image, f.diskon_nominal, k.nama, s.kapasitas from sesi_films " +
                      "sf inner join films f on sf.films_id = f.id inner join studios s on " +
                      "sf.studios_id = s.id inner join jadwal_films j on j.id = sf.jadwal_film_id " +
                      "inner join kelompoks k on k.id = f.kelompoks_id inner join jenis_studios js " +
                      "on js.id = s.jenis_studios_id inner join cinemas c on s.cinemas_id = c.id " +
                      "where f.id = '1' and s.id = '1' and j.id = '1'";
            }
            else
            {
                sql += " where " + kriteria + "='" + nilaiKriteria + "'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<SesiFilm> listSesiFilm = new List<SesiFilm>();
            while (hasil.Read() == true)
            {
                Cinema c = new Cinema(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                    DateTime.Parse(hasil.GetValue(3).ToString()), hasil.GetValue(4).ToString());
                Jenis_Studio js = new Jenis_Studio(int.Parse(hasil.GetValue(5).ToString()), hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString());
                Studio s = new Studio(int.Parse(hasil.GetValue(8).ToString()), hasil.GetValue(9).ToString(), int.Parse(hasil.GetValue(26).ToString()),
                    js, c, int.Parse(hasil.GetValue(10).ToString()),
                    int.Parse(hasil.GetValue(11).ToString()));
                Kelompok k = new Kelompok(int.Parse(hasil.GetValue(20).ToString()), hasil.GetValue(25).ToString());
                Film f = new Film(int.Parse(hasil.GetValue(15).ToString()), hasil.GetValue(16).ToString(), hasil.GetValue(17).ToString(), int.Parse(hasil.GetValue(18).ToString()),
                    int.Parse(hasil.GetValue(19).ToString()),
                    k, hasil.GetValue(21).ToString(), int.Parse(hasil.GetValue(22).ToString()), hasil.GetValue(23).ToString(), double.Parse(hasil.GetValue(24).ToString()));
                PenjadwalanFilm jf = new PenjadwalanFilm(int.Parse(hasil.GetValue(12).ToString()), DateTime.Parse(hasil.GetValue(13).ToString()), hasil.GetValue(14).ToString());
                SesiFilm sf = new SesiFilm(jf, s, f);
                listSesiFilm.Add(sf);
            }
            return listSesiFilm;
        }

        public static bool HapusData(string nilaiKriteria)
        {
            string sql = "delete from sesi_films where films_id='" + nilaiKriteria + "'";

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

        public static List<SesiFilm> BacaDataUntukTiket(string kriteria, string nilaiKriteria, string nilaiKriteria2, string nilaiKriteria3)
        {
            string sql = "select c.id, c.nama_cabang, c.alamat, c.tgl_dibuka, c.kota, js.id, " +
                         "js.nama, js.deskripsi, s.id, s.nama, s.harga_weekday, s.harga_weekend, j.id, " +
                         "j.tanggal, j.jam_pemutaran, f.id, f.judul as judulFilm, f.sinopsis, f.tahun, f.durasi, k.id, " +
                         "f.bahasa, f.is_sub_indo, f.cover_image, f.diskon_nominal, k.nama, s.kapasitas from sesi_films " +
                         "sf inner join films f on sf.films_id = f.id inner join studios s on sf.studios_id = s.id " +
                         "inner join jadwal_films j on j.id = sf.jadwal_film_id inner join kelompoks k on k.id = f.kelompoks_id " +
                         "inner join jenis_studios js on js.id = s.jenis_studios_id inner join cinemas c on s.cinemas_id = c.id " +
                         "where sf.films_id = f.id and sf.jadwal_film_id = j.id and sf.studios_id = s.id and s.cinemas_id = c.id " +
                         "and f.id = '" + nilaiKriteria + "' and j.tanggal = ' " + nilaiKriteria2 + "' and c.nama_cabang = '" + nilaiKriteria3 + "'";
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<SesiFilm> listSesiFilm = new List<SesiFilm>();
            while (hasil.Read() == true)
            {
                Cinema c = new Cinema(int.Parse(hasil.GetValue(0).ToString()), hasil.GetValue(1).ToString(), hasil.GetValue(2).ToString(),
                                      DateTime.Parse(hasil.GetValue(3).ToString()), hasil.GetValue(4).ToString());
                Jenis_Studio js = new Jenis_Studio(int.Parse(hasil.GetValue(5).ToString()), hasil.GetValue(6).ToString(), hasil.GetValue(7).ToString());
                Studio s = new Studio(int.Parse(hasil.GetValue(8).ToString()), hasil.GetValue(9).ToString(), int.Parse(hasil.GetValue(26).ToString()),
                                      js, c, int.Parse(hasil.GetValue(10).ToString()), int.Parse(hasil.GetValue(11).ToString()));
                Kelompok k = new Kelompok(int.Parse(hasil.GetValue(20).ToString()), hasil.GetValue(25).ToString());
                Film f = new Film(int.Parse(hasil.GetValue(15).ToString()), hasil.GetValue(16).ToString(), hasil.GetValue(17).ToString(),
                                  int.Parse(hasil.GetValue(18).ToString()), int.Parse(hasil.GetValue(19).ToString()), k, hasil.GetValue(21).ToString(),
                                  int.Parse(hasil.GetValue(22).ToString()), hasil.GetValue(23).ToString(), double.Parse(hasil.GetValue(24).ToString()));
                PenjadwalanFilm jf = new PenjadwalanFilm(int.Parse(hasil.GetValue(12).ToString()), DateTime.Parse(hasil.GetValue(13).ToString()),
                                                        hasil.GetValue(14).ToString());
                SesiFilm sf = new SesiFilm(jf, s, f);
                listSesiFilm.Add(sf);
            }
            return listSesiFilm;

        }
        public override string ToString()
        {
            return Studio.Nama;
        }
    }
}
