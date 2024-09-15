using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Celikoor_LIB;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using CheckBox = System.Windows.Forms.CheckBox;
using GroupBox = System.Windows.Forms.GroupBox;

namespace Celikoor_BersamaKitaBisaDatabase
{
    public partial class FormPemesananTiket : Form
    {
        List<Film> listFilm = new List<Film>();
        List<PenjadwalanFilm> listPenjadwalanFilm = new List<PenjadwalanFilm>();
        List<Cinema> listCinema = new List<Cinema>();
        List<SesiFilm> listStudio = new List<SesiFilm>();
        List<AktorFilm> listAktor = new List<AktorFilm>();
        List<GenreFilm> listGenre = new List<GenreFilm>();
        List<Tiket> listTiket = new List<Tiket>();
        //Membuat list seatGroupBox buat menyimpan groupbox kursi
        private List<GroupBox> seatGroupBoxes;
        public List<string> selectedSeats;
        //List<Tiket> kursiSudahTerisi = new List<Tiket>();
        List<Tiket> listKursiDiisiA = new List<Tiket>();
        List<Tiket> listKursiDiisiB = new List<Tiket>();
        List<Tiket> listKursiDiisiC = new List<Tiket>();
        Tiket tiket;

        public FormPemesananTiket()
        {
            InitializeComponent();
        }

        private void buttonKonfirmasiPembayaran_Click(object sender, EventArgs e)
        {
            try
            {
                if (labelTotal.Text != null && labelDiskon.Text != null)
                {
                    FormUtama formUtama = (FormUtama)this.MdiParent;
                    Invoices i = new Invoices(0, DateTime.Now, double.Parse(labelTotal.Text),
                                              double.Parse(labelDiskon.Text), formUtama.konsumenLogin);
                    Invoices.TambahData(i);
                    MessageBox.Show("Data telah ditambahkan ke kelas invoices");

                    PenjadwalanFilm selectedTglID = ((PenjadwalanFilm)comboBoxTgl.SelectedItem);
                    string tanggal = selectedTglID.Tanggal.ToShortDateString();
                    //bacadata pake syarat selectedTglID buat nyari ID e tgl
                    List<SesiFilm> listSesi = new List<SesiFilm>();
                    listSesi = SesiFilm.BacaData("1", "");

                    List<Invoices> inv = new List<Invoices>();
                    inv = Invoices.BacaData("1");

                    //checkbox dicentang masuk ke dalam listSementara

                    for (int n = 0; n < selectedSeats.Count; n++)
                    {
                        tiket = new Tiket(inv[0], selectedSeats[n], 0, double.Parse(labelHarga.Text), 
                                          listSesi[0].JadwalFilm, listSesi[0].Studio, listSesi[0].Film);
                        Tiket.TambahData(tiket);
                        MessageBox.Show("Data telah ditambahkan ke kelas tikets");
                    }

                    selectedSeats.Clear();
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
            //FormInvoices form = new FormInvoices();
            //form.Owner = this;
            //form.Show();
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabelKursi();
        }

        private void UpdateLabelKursi()
        {
            selectedSeats = new List<string>();
            // Loop melalui setiap GroupBox
            foreach (GroupBox groupBox in seatGroupBoxes)
            {
                // Dapatkan identifikasi GroupBox (misal, A, B, C)
                string groupBoxIdentifier = groupBox.Text;

                // Dapatkan kursi yang dicentang dalam GroupBox
                List<string> selectedSeatsInGroupBox = groupBox.Controls
                    .OfType<CheckBox>()
                    .Where(checkBox => checkBox.Checked && checkBox.Enabled == true)
                    .Select(checkBox => groupBoxIdentifier + checkBox.Text)
                    .ToList();

                // Gabungkan dengan daftar kursi yang dipilih dari semua GroupBox
                selectedSeats.AddRange(selectedSeatsInGroupBox);
            }

            try
            {
                if (selectedSeats.Any())
                {
                    // Update label dengan kursi yang dipilih
                    labelKursi.Text = string.Join(", ", selectedSeats);
                    // Update label dengan harga x total kursi
                    string totalKursi = selectedSeats.Count().ToString();
                    int totalTiketxKursi = int.Parse(totalKursi) * int.Parse(labelHarga.Text);
                    labelTotal.Text = totalTiketxKursi.ToString();

                    string selectedDiskon = ((Film)comboBoxJudul.SelectedItem).DiskonNominal.ToString();
                    double diskonNominal = double.Parse(selectedDiskon) * totalTiketxKursi;
                    labelDiskon.Text = diskonNominal.ToString();

                    double totalAkhir = (double)totalTiketxKursi - diskonNominal;
                    labelTotalAkhir.Text = totalAkhir.ToString();
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message);
            }
        }

        private void FormPemesananTiket_Load(object sender, EventArgs e)
        {
            //string s = "10";
            //foreach (Control cc in this.Controls)
            //{
            //    if (cc is CheckBox)
            //    {
            //        CheckBox b = (CheckBox)cc;
            //        if (b.Checked)
            //        {
            //            s = b.Text;
            //            MessageBox.Show(s);
            //        }
            //    }
            //}
            ////baca data konsumen yang login
            FormUtama formUtama = (FormUtama)this.MdiParent;
            labelSaldo.Text = formUtama.konsumenLogin.Saldo.ToString();//gatau kenapa masi 0

            comboBoxJudul.DataSource = null;
            comboBoxJudul.Items.Clear();
            listFilm = Film.BacaDataDua("1", "");
            comboBoxJudul.DataSource = listFilm;
            comboBoxJudul.DisplayMember = "Judul";
            comboBoxJudul.SelectedIndex = -1;

            seatGroupBoxes = new List<GroupBox> { groupBoxA, groupBoxB, groupBoxC }; // Sesuaikan dengan nama GroupBox di proyek

            foreach (GroupBox groupBox in seatGroupBoxes)
            {
                foreach (Control control in groupBox.Controls)
                {
                    if (control is CheckBox checkBox)
                    {
                        checkBox.CheckedChanged += CheckBox_CheckedChanged;
                    }
                }
            }
        }

        private void comboBoxJudul_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UDH BENER
            comboBoxTgl.DataSource = null;
            comboBoxTgl.Items.Clear();
            labelDetailSinopsis.Text = null;
            labelDurasi.Text = null;
            labelAktor.Text = null;
            labelGenre.Text = null;
            labelKelompok.Text = null;

            Film selectedFilm = (Film)comboBoxJudul.SelectedItem;
            if (selectedFilm != null)
            {
                //ngisi data comboboxTgl sesuai dengan selectedFilm
                //bacadata where f.id = selectedFilm.id
                listPenjadwalanFilm = PenjadwalanFilm.BacaData("3", selectedFilm.Id.ToString());
                comboBoxTgl.DataSource = listPenjadwalanFilm;
                comboBoxTgl.DisplayMember = "Tanggal.ToString(\"yyyy - MM - dd\")";//omg thank u lop
                comboBoxTgl.SelectedIndex = -1;

                //ngisi labelSinopsis dengan Sinopsis sesuai yang diselect dari comboboxJudul
                string selectedSinopsis = ((Film)comboBoxJudul.SelectedItem).Sinopsis.ToString();
                labelDetailSinopsis.Text = selectedSinopsis;

                //ngisi labelSinopsis dengan Durasi sesuai yang diselect dari comboboxJudul
                string selectedDurasi = ((Film)comboBoxJudul.SelectedItem).Durasi.ToString();
                labelDurasi.Text = selectedDurasi;

                pictureBoxMovie.Image = Image.FromFile(selectedFilm.CoverImage);

                ////ngisi labelAktor sesuai dengan selectedFilm
                ////baca data Aktor Film buat tau sapa aja aktornya
                //listAktor = AktorFilm.BacaData("f.id", selectedFilm.Id.ToString());
                //string teksAktor = string.Join(", ", listAktor);
                //labelAktor.Text = teksAktor;
                int idFilm = selectedFilm.Id;
                List<AktorFilm> listPemeran = new List<AktorFilm>();
                listPemeran = AktorFilm.AktorPemeranFilm("1", idFilm.ToString());
                if (listPemeran.Count <= 2)
                {
                    if (listPemeran.Count == 1)
                    {
                        labelAktor.Text = listPemeran[0].Aktor.NamaAktor;
                    }
                    else if (listPemeran.Count == 2)
                    {
                        labelAktor.Text = listPemeran[0].Aktor.NamaAktor + "," + listPemeran[1].Aktor.NamaAktor.ToString();
                    }
                }
                else
                {
                    labelAktor.Text = listPemeran[0].Aktor.NamaAktor + "," + listPemeran[1].Aktor.NamaAktor.ToString() + ",...";
                }

                //ngisi labelGenre sesuai dengan yang diselect dari comboboxJudul
                //baca data dari class GenreFilm
                listGenre = GenreFilm.BacaData("f.id", selectedFilm.Id.ToString());
                string teksGenre = string.Join(", ", listGenre);
                labelGenre.Text = teksGenre;

                //ngisi labelKelompok sesuai dengan yang diselect dari comboboxJudul
                string selectedKelompok = ((Film)comboBoxJudul.SelectedItem).Kelompok.Nama.ToString();
                labelKelompok.Text = selectedKelompok;
            }
        }

        private void comboBoxTgl_SelectedIndexChanged(object sender, EventArgs e)
        {
            //UDH BENER
            comboBoxCinema.DataSource = null;
            comboBoxCinema.Items.Clear();
            //menampilkan list cinema yang menampilkan film(comboboxJudul) di tanggal(comboboxTgl)
            Film selectedFilm = (Film)comboBoxJudul.SelectedItem;
            PenjadwalanFilm selectedTanggal = (PenjadwalanFilm)comboBoxTgl.SelectedItem;

            if (selectedFilm != null && selectedTanggal != null)
            {
                //menampilkan nama" cinema sesuai dengan judul + tanggal tayang 
                listCinema = Cinema.BacaDataUntukTiket("1", selectedFilm.Id.ToString(), selectedTanggal.ToString());
                comboBoxCinema.DataSource = listCinema;
                comboBoxCinema.DisplayMember = "Nama_cabang";
                comboBoxCinema.SelectedIndex = -1;
            }
        }

        private void comboBoxCinema_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxStudio.DataSource = null;
            comboBoxStudio.Items.Clear();
            ////nampilin studio sesuai ama selected film, tgl, and cinema
            ////goodluck
            Film selectedFilm = (Film)comboBoxJudul.SelectedItem;
            PenjadwalanFilm selectedTanggal = (PenjadwalanFilm)comboBoxTgl.SelectedItem;
            Cinema selectedCinema = (Cinema)comboBoxCinema.SelectedItem;
            if (selectedFilm != null && selectedTanggal != null && selectedCinema != null)
            {
                //menampilkan studio sesuai dengan judul + tanggal + cinema yang dipilih (class studio)
                listStudio = SesiFilm.BacaDataUntukTiket("2", selectedFilm.Id.ToString(), selectedTanggal.ToString(), selectedCinema.Nama_cabang.ToString());
                comboBoxStudio.DataSource = listStudio;
                comboBoxStudio.DisplayMember = "Studio.Nama";
                comboBoxStudio.SelectedIndex = -1;
            }
        }

        private void comboBoxStudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelJenisStudio.Text = null;
            labelHarga.Text = null;
            labelSisa.Text = null;
            labelTerisi.Text = null;

            Film selectedFilm = (Film)comboBoxJudul.SelectedItem;
            PenjadwalanFilm selectedTanggal = (PenjadwalanFilm)comboBoxTgl.SelectedItem;
            Cinema selectedCinema = (Cinema)comboBoxCinema.SelectedItem;
            SesiFilm selectedStudio = (SesiFilm)comboBoxStudio.SelectedItem;

            if (selectedFilm != null && selectedTanggal != null && selectedCinema != null && selectedStudio != null)
            {
                listStudio = SesiFilm.BacaDataUntukTiket("2", selectedFilm.Id.ToString(), selectedTanggal.ToString(), selectedCinema.Nama_cabang.ToString());
                string selectedType = ((SesiFilm)comboBoxStudio.SelectedItem).Studio.JenisStudio.ToString();
                labelJenisStudio.Text = selectedType;

                //baca data jumlah kapasitas studio
                int jumlahKursiAwal = int.Parse(selectedStudio.Studio.Kapasitas.ToString());

                int num = jumlahKursiAwal / 3;
                int x ;
                foreach (Control cc in groupBoxA.Controls)
                {
                    if (cc is CheckBox)
                    {
                        x = int.Parse(cc.Text);
                        if (x > num)
                        { cc.Visible = false; }
                    }
                }
                foreach (Control cc in groupBoxB.Controls)
                {
                    if (cc is CheckBox)
                    {
                        x = int.Parse(cc.Text);
                        if (x > num)
                        { cc.Visible = false; }
                    }
                }
                foreach (Control cc in groupBoxC.Controls)
                {
                    if (cc is CheckBox)
                    {
                        x = int.Parse(cc.Text);
                        if (x > num)
                        { cc.Visible = false; }
                    }
                }

                ////baca data jumlah tickets
                //listTiket = Tiket.BacaData("totalKursi", selectedFilm.Id.ToString(), comboBoxTgl.SelectedItem.ToString(), comboBoxStudio.SelectedItem.ToString());
                //int jumlahKursiSkrng = jumlahKursiAwal - listTiket.Count();
                //labelSisa.Text = "sisa (" + jumlahKursiSkrng.ToString() + " kursi)";

                //labelTerisi.Text = listTiket.Count.ToString() + " kursi";

                //jika tanggalnya weekday maka menampilkan harga weekday, begitu pula jika weekend
                if (selectedTanggal.Tanggal.DayOfWeek == DayOfWeek.Saturday && selectedTanggal.Tanggal.DayOfWeek == DayOfWeek.Sunday)
                {
                    string selectedPrice = ((SesiFilm)comboBoxStudio.SelectedItem).Studio.HargaWeekend.ToString();
                    labelHarga.Text = selectedPrice;
                }
                else if (selectedTanggal.Tanggal.DayOfWeek != DayOfWeek.Saturday && selectedTanggal.Tanggal.DayOfWeek != DayOfWeek.Sunday)
                {
                    string selectedPrice = ((SesiFilm)comboBoxStudio.SelectedItem).Studio.HargaWeekday.ToString();
                    labelHarga.Text = selectedPrice;
                }

                //menampilkan yang sudah terisi
                //kursiSudahTerisi = Tiket.BacaTiketDibeli("1", "1", "1");
                listKursiDiisiA= Tiket.BacaTiketDibeli("1", "1", "1", "A");
                listKursiDiisiB= Tiket.BacaTiketDibeli("1", "1", "1", "B");
                listKursiDiisiC= Tiket.BacaTiketDibeli("1", "1", "1", "C");

                listTiket = Tiket.BacaData("totalKursi", selectedFilm.Id.ToString(), comboBoxTgl.SelectedItem.ToString(), comboBoxStudio.SelectedItem.ToString());
                int jumlahKursiSkrng = jumlahKursiAwal - listKursiDiisiA.Count() - listKursiDiisiB.Count() - listKursiDiisiC.Count();
                labelSisa.Text = "sisa " + jumlahKursiSkrng.ToString() + "  listKursiDiisiA.Count() - listKursiDiisiB.Count() - listKursiDiisiC.Count();kursi";

                int jumlahKursiTerisi = listKursiDiisiA.Count() + listKursiDiisiB.Count() + listKursiDiisiC.Count();
                labelTerisi.Text = jumlahKursiTerisi.ToString() + " kursi";
                foreach (Control cc in groupBoxA.Controls)
                    
                {
                    if(cc is CheckBox)
                    {
                        for (int i=0; i < listKursiDiisiA.Count; i++)
                        {
                            if("A"+ cc.Text == listKursiDiisiA[i].NomorKursi)
                            {
                                CheckBox b = (CheckBox)cc;
                                cc.Enabled = false;
                                b.Checked = true;
                            }
                        }
                    }
                }
                foreach (Control cc in groupBoxB.Controls)
                {
                    if (cc is CheckBox)
                    {
                        for (int i = 0; i < listKursiDiisiB.Count; i++)
                        {
                            if ("B" + cc.Text == listKursiDiisiB[i].NomorKursi)
                            {
                                CheckBox b = (CheckBox)cc;
                                cc.Enabled = false;
                                b.Checked = true;
                            }
                        }
                    }
                }
                foreach (Control cc in groupBoxC.Controls)
                {
                    if (cc is CheckBox)
                    {
                        for (int i = 0; i < listKursiDiisiC.Count; i++)
                        {
                            if ("C" + cc.Text == listKursiDiisiC[i].NomorKursi)
                            {
                                CheckBox b = (CheckBox)cc;
                                cc.Enabled = false;
                                b.Checked = true;
                            }
                        }
                    }
                }
                labelKursi.Text = "";
            }
        }

        private void buttonKeluar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
