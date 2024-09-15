using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class FilmStudio
    {
        private Studio studio;
        private Film film;

        public FilmStudio(Studio studio, Film film)
        {
            this.Studio = studio;
            this.Film = film;
        }

        public Studio Studio { get => studio; set => studio = value; }
        public Film Film { get => film; set => film = value; }

        public static List<FilmStudio> BacaData(string kriteria, string nilaiKriteria)
        {
            string sql = "";
            if (kriteria == "")
            {
                sql = "select * from film_studio fs inner join films " +
                "f on fs.films_id=f.id inner join studios s on fs.studios_id=s.id";
            }
            else
            {
                sql = "select * from film_studio fs inner join films " +
                "f on fs.films_id=f.id inner join studios s on fs.studios_id=s.id where " +
                kriteria + "='" + nilaiKriteria + "'";
            }
            MySqlDataReader hasil = Koneksi.JalankanPerintahQuery(sql);
            List<FilmStudio> list = new List<FilmStudio>();
            while (hasil.Read() == true)
            {
                Studio studio = Studio.BacaDataSatu("id", hasil.GetValue(0).ToString());
                Film f = Film.BacaDataSatu("id", hasil.GetValue(1).ToString());
                FilmStudio fs = new FilmStudio(studio, f);
                list.Add(fs);
            }
            return list;
        }
        public static bool HapusData(string nilaiKriteria)
        {
            string sql = "delete from film_studio where films_id='" + nilaiKriteria + "'";

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
        public static void TambahData(FilmStudio fs)
        {
            string sql = "insert into film_studio (studios_id, films_id) values ('" + fs.Studio.Id + "','" + fs.Film.Id + "')";
            Koneksi.JalankanPerintahNonQuery(sql);
        }
    }
}
