﻿
namespace Celikoor_BersamaKitaBisaDatabase
{
    partial class FormDaftarInvoicesValidasi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.buttonKeluar = new System.Windows.Forms.Button();
            this.dataGridViewDaftarInvoices = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDaftarInvoices)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Tan;
            this.label1.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(665, 61);
            this.label1.TabIndex = 35;
            this.label1.Text = "DAFTAR INVOICES VALIDASI";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonKeluar
            // 
            this.buttonKeluar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.buttonKeluar.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonKeluar.ForeColor = System.Drawing.Color.White;
            this.buttonKeluar.Location = new System.Drawing.Point(541, 428);
            this.buttonKeluar.Margin = new System.Windows.Forms.Padding(2);
            this.buttonKeluar.Name = "buttonKeluar";
            this.buttonKeluar.Size = new System.Drawing.Size(135, 51);
            this.buttonKeluar.TabIndex = 37;
            this.buttonKeluar.Text = "&KELUAR";
            this.buttonKeluar.UseVisualStyleBackColor = false;
            // 
            // dataGridViewDaftarInvoices
            // 
            this.dataGridViewDaftarInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDaftarInvoices.Location = new System.Drawing.Point(11, 82);
            this.dataGridViewDaftarInvoices.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridViewDaftarInvoices.Name = "dataGridViewDaftarInvoices";
            this.dataGridViewDaftarInvoices.RowHeadersWidth = 82;
            this.dataGridViewDaftarInvoices.RowTemplate.Height = 33;
            this.dataGridViewDaftarInvoices.Size = new System.Drawing.Size(665, 330);
            this.dataGridViewDaftarInvoices.TabIndex = 36;
            this.dataGridViewDaftarInvoices.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewDaftarInvoices_CellContentClick);
            // 
            // FormDaftarInvoicesValidasi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 492);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonKeluar);
            this.Controls.Add(this.dataGridViewDaftarInvoices);
            this.Name = "FormDaftarInvoicesValidasi";
            this.Text = "FormDaftarInvoicesValidasi";
            this.Load += new System.EventHandler(this.FormDaftarInvoicesValidasi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDaftarInvoices)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonKeluar;
        private System.Windows.Forms.DataGridView dataGridViewDaftarInvoices;
    }
}