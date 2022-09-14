namespace BankaFisiExcelAktarim
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDosyaSec = new System.Windows.Forms.Button();
            this.txtxlsldosyayolu = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnAktar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dtTarih = new System.Windows.Forms.DateTimePicker();
            this.btnLogoAktar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDosyaSec
            // 
            this.btnDosyaSec.Location = new System.Drawing.Point(248, 33);
            this.btnDosyaSec.Name = "btnDosyaSec";
            this.btnDosyaSec.Size = new System.Drawing.Size(110, 23);
            this.btnDosyaSec.TabIndex = 0;
            this.btnDosyaSec.Text = "Dosya Yolu Seç";
            this.btnDosyaSec.UseVisualStyleBackColor = true;
            this.btnDosyaSec.Click += new System.EventHandler(this.btnDosyaSec_Click);
            // 
            // txtxlsldosyayolu
            // 
            this.txtxlsldosyayolu.Location = new System.Drawing.Point(12, 33);
            this.txtxlsldosyayolu.Name = "txtxlsldosyayolu";
            this.txtxlsldosyayolu.Size = new System.Drawing.Size(214, 20);
            this.txtxlsldosyayolu.TabIndex = 1;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // btnAktar
            // 
            this.btnAktar.Location = new System.Drawing.Point(492, 30);
            this.btnAktar.Name = "btnAktar";
            this.btnAktar.Size = new System.Drawing.Size(110, 23);
            this.btnAktar.TabIndex = 2;
            this.btnAktar.Text = "Sql Aktar";
            this.btnAktar.UseVisualStyleBackColor = true;
            this.btnAktar.Click += new System.EventHandler(this.btnAktar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 78);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(704, 369);
            this.dataGridView1.TabIndex = 26;
            // 
            // dtTarih
            // 
            this.dtTarih.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtTarih.Location = new System.Drawing.Point(379, 34);
            this.dtTarih.Name = "dtTarih";
            this.dtTarih.Size = new System.Drawing.Size(88, 20);
            this.dtTarih.TabIndex = 27;
            // 
            // btnLogoAktar
            // 
            this.btnLogoAktar.Location = new System.Drawing.Point(617, 30);
            this.btnLogoAktar.Name = "btnLogoAktar";
            this.btnLogoAktar.Size = new System.Drawing.Size(100, 23);
            this.btnLogoAktar.TabIndex = 28;
            this.btnLogoAktar.Text = "Logo Aktar";
            this.btnLogoAktar.UseVisualStyleBackColor = true;
            this.btnLogoAktar.Click += new System.EventHandler(this.btnLogoAktar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 469);
            this.Controls.Add(this.btnLogoAktar);
            this.Controls.Add(this.dtTarih);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnAktar);
            this.Controls.Add(this.txtxlsldosyayolu);
            this.Controls.Add(this.btnDosyaSec);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDosyaSec;
        private System.Windows.Forms.TextBox txtxlsldosyayolu;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button btnAktar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dtTarih;
        private System.Windows.Forms.Button btnLogoAktar;
    }
}

