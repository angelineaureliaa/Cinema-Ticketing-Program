using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Celikoor_LIB
{
    public class Koneksi
    {
        private MySqlConnection koneksiDB;

        public Koneksi()
        {
            //string strCon = "server=" + pServer + ";database=" + pDatabase + ";uid=" + pUsername + 
            //                ";password=" + pPassword;

            //KoneksiDB = new MySqlConnection();

            //KoneksiDB.ConnectionString = strCon;

            //Connect();

            Configuration myConf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSectionGroup userSettings = myConf.SectionGroups["userSettings"];
            var settingsSection = userSettings.Sections["Celikoor_BersamaKitaBisaDatabase.dbcelikoor"] as  1;
            string DbServer = settingsSection.Settings.Get("DbServer").Value.ValueXml.InnerText;
            string DbName = settingsSection.Settings.Get("DbName").Value.ValueXml.InnerText;
            string DbUsername = settingsSection.Settings.Get("DbUsername").Value.ValueXml.InnerText;
            string DbPassword = settingsSection.Settings.Get("DbPassword").Value.ValueXml.InnerText;

            string strCon = "server =" + DbServer + "; database = " + DbName + "; uid = " + DbUsername
                            + "; password = " + DbPassword;
            ;
            KoneksiDB = new MySqlConnection();
            KoneksiDB.ConnectionString = strCon;
            Connect();
        }

        public MySqlConnection KoneksiDB { get => koneksiDB; private set => koneksiDB = value; }

        public void Connect()
        {
            //jika connection sedang terbuka, maka tutup dahulu
            //kalau koneksi 'k'nya huruf besar brati manggil properties
            //ga akan menjalankan apa-apa kalau belum diconnection string

            if (KoneksiDB.State == System.Data.ConnectionState.Open)
            {
                KoneksiDB.Close();
            }
            KoneksiDB.Open();
        }

        public static MySqlDataReader JalankanPerintahQuery(string sql)
        {
            Koneksi k = new Koneksi();
            MySqlCommand c = new MySqlCommand(sql, k.KoneksiDB);

            MySqlDataReader hasil = c.ExecuteReader();

            return hasil;
        }

        public static int JalankanPerintahNonQuery(string sql)
        {
            Koneksi k = new Koneksi();
            MySqlCommand c = new MySqlCommand(sql, k.KoneksiDB);

            int hasil = c.ExecuteNonQuery();

            return hasil;
        }
        
    }
}
