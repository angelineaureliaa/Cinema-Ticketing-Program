using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Drawing;
using System.Drawing.Printing;

namespace Celikoor_LIB
{
    public class CustomPrint
    {
        private Font tipeFont;
        private StreamReader fileCetak;
        private float marginAtas, marginBawah, marginKanan, marginKiri;

        public Font TipeFont { get => tipeFont; set => tipeFont = value; }
        public StreamReader FileCetak { get => fileCetak; set => fileCetak = value; }
        public float MarginAtas { get => marginAtas; set => marginAtas = value; }
        public float MarginBawah { get => marginBawah; set => marginBawah = value; }
        public float MarginKanan { get => marginKanan; set => marginKanan = value; }
        public float MarginKiri { get => marginKiri; set => marginKiri = value; }

        public CustomPrint(Font vFont, string vNamaFile)
        {
            TipeFont = vFont;
            FileCetak = new StreamReader(vNamaFile);
            MarginAtas = 10;
            MarginBawah = 10;
            MarginKanan = 10;
            MarginKiri = 10;
        }

        private void Cetak(object sender, PrintPageEventArgs e)
        {
            float tinggiFont = TipeFont.GetHeight(e.Graphics);
            float y;
            float x = MarginKiri;
            int jumBarisSaatIni = 0;
            int maxBarisDalamHalaman = (int)((e.MarginBounds.Height - MarginAtas - MarginBawah) / tinggiFont); //e.MarginBounds.Height --> tinggi kertas             
            String textCetak = FileCetak.ReadLine(); //mengambil 1 baris isi filetext
            while (jumBarisSaatIni < maxBarisDalamHalaman && textCetak != null)
            {
                y = MarginAtas + (jumBarisSaatIni * tinggiFont);
                e.Graphics.DrawString(textCetak, TipeFont, Brushes.DarkBlue, x, y); //menulis ke memory
                jumBarisSaatIni++;
                textCetak = FileCetak.ReadLine();
            }
            if (textCetak != null) //jika isi filetext belum habis namun halaman sdh penuh, pindah ke halaman berikutnya
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        public void KirimkePrinter()
        {
            //proses mencetak ke printer
            PrintDocument p = new PrintDocument();
            //p.PrinterSettings.PrinterName = "POS-80C";
            p.PrinterSettings.PrinterName = "Microsoft Print to PDF";

            //menulis ke memory untuk tiap halaman
            p.PrintPage += new PrintPageEventHandler(Cetak);
            p.Print();

            FileCetak.Close();
        }
    }
}
