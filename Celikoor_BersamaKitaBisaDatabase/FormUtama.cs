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

namespace Celikoor_BersamaKitaBisaDatabase
{
    public partial class FormUtama : Form
    {
        public Konsumen konsumenLogin;
        public Pegawai pegawaiLogin;
        public FormUtama()
        {
            InitializeComponent();
        }

        private void FormUtama_Load(object sender, EventArgs e)
        {
            try
            {
                //ubah form ini (FormUtama) menjadi fullscreen (maximized)
                this.WindowState = FormWindowState.Maximized;

                //ubah FormUtama menjadi MdiParent (MdiContainer)
                this.IsMdiContainer = true;

                Koneksi koneksi = new Koneksi();

                //tampilkan form login
                FormLogin frmLogin = new FormLogin();
                frmLogin.Owner = this;
                if (frmLogin.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Welcome to Celikoor" );
                    if(konsumenLogin!=null && pegawaiLogin == null)
                    {
                        masterToolStripMenuItem.Visible=false;
                        jadwalToolStripMenuItem.Visible = false;
                        
                        laporanToolStripMenuItem.Visible = false;
                        invoicesToolStripMenuItem.Visible = false;
                        scanTiketToolStripMenuItem.Visible = false;
                        cetakToolStripMenuItem.Visible = false;
                        MessageBox.Show(konsumenLogin.Saldo.ToString());
                    }
                    else if(konsumenLogin == null && pegawaiLogin != null)
                    {
                        if (pegawaiLogin.Roles == "ADMIN")
                        {
                            pembelianTiketToolStripMenuItem.Visible = false;
                        }
                        else if (pegawaiLogin.Roles == "KASIR")
                        {
                            masterToolStripMenuItem.Visible = false;
                            jadwalToolStripMenuItem.Visible = false;
                            
                            laporanToolStripMenuItem.Visible = false;
                            pembelianTiketToolStripMenuItem.Visible = false;
                            scanTiketToolStripMenuItem.Visible = false;
                        }
                        else if (pegawaiLogin.Roles == "OPERATOR")
                        {
                            masterToolStripMenuItem.Visible = false;
                            jadwalToolStripMenuItem.Visible = false;
                            invoicesToolStripMenuItem.Visible = false;
                            laporanToolStripMenuItem.Visible = false;
                            pembelianTiketToolStripMenuItem.Visible = false;
                            scanTiketToolStripMenuItem.Visible = true;
                        }
                    }
                }
                else
                {
                    //jika login gagal
                    MessageBox.Show("Gagal login");
                    Application.Exit();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Pesan kesalahan : " + ex.Message, "Informasi");
            }
        }

        private void keluarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Terima kasih telah menggunakan Celikoor", "Informasi");
            Application.Exit();
        }

        private void profileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (konsumenLogin != null && pegawaiLogin == null)
            {
                FormHalamanKonsumen form = new FormHalamanKonsumen();
                form.MdiParent = this;
                form.Show();
            }
            else if(pegawaiLogin!=null && konsumenLogin == null)
            {
                if (pegawaiLogin.Roles == "ADMIN")
                {
                    FormHalamanAdmin form = new FormHalamanAdmin();
                    form.MdiParent = this;
                    form.Show();
                }
                else if (pegawaiLogin.Roles == "KASIR")
                {
                    FormHalamanKasir form = new FormHalamanKasir();
                    form.MdiParent = this;
                    form.Show();
                }
                else if (pegawaiLogin.Roles == "OPERATOR")
                {
                    FormHalamanOperator form = new FormHalamanOperator();
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }

        private void cinemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarCinema"];
            if (form == null)
            {
                FormDaftarCinema frm = new FormDaftarCinema();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void studioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarStudio"];
            if (form == null)
            {
                FormDaftarStudio frm = new FormDaftarStudio();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void jenisStudioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarJenisStudio"];
            if (form == null)
            {
                FormDaftarJenisStudio frm = new FormDaftarJenisStudio();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void genreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarGenre"];
            if (form == null)
            {
                FormDaftarGenre frm = new FormDaftarGenre();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void kelompokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarKelompok"];
            if (form == null)
            {
                FormDaftarKelompok frm = new FormDaftarKelompok();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void aktorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarAktor"];
            if (form == null)
            {
                FormDaftarAktor frm = new FormDaftarAktor();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void filmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarFilm"];
            if (form == null)
            {
                FormDaftarFilm frm = new FormDaftarFilm();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void pegawaiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarPegawai"];
            if (form == null)
            {
                FormDaftarPegawai frm = new FormDaftarPegawai();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void konsumenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarKonsumen"];
            if (form == null)
            {
                FormDaftarKonsumen frm = new FormDaftarKonsumen();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void jadwalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormJadwalFilm"];
            if (form == null)
            {
                FormJadwalFilm frm = new FormJadwalFilm();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void pembelianTiketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormPemesananTiket"];
            if (form == null)
            {
                FormPemesananTiket frm = new FormPemesananTiket();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void invoicesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void scanTiketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormScanTiket"];
            if (form == null)
            {
                FormScanTiket frm = new FormScanTiket();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void pendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarInvoicesPending"];
            if (form == null)
            {
                FormDaftarInvoicesPending frm = new FormDaftarInvoicesPending();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void laporanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormLaporan"];
            if (form == null)
            {
                FormLaporan frm = new FormLaporan();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void validasiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormDaftarInvoicesValidasi"];
            if (form == null)
            {
                FormDaftarInvoicesValidasi frm = new FormDaftarInvoicesValidasi();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }

        private void cetakToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms["FormCetakInvoice"];
            if (form == null)
            {
                FormCetakInvoice frm = new FormCetakInvoice();
                frm.MdiParent = this;
                frm.Show();
            }
            else
            {
                form.Show();
                form.BringToFront();
            }
        }
    }
}
