namespace BankaFisiExcelAktarim
{
    partial class Form2
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Target = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Source = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.btnDosyaSec = new System.Windows.Forms.Button();
            this.txtxlsldosyayolu = new System.Windows.Forms.TextBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnEslestir = new System.Windows.Forms.Button();
            this.btnMatchingKod = new System.Windows.Forms.Button();
            this.btnAktar = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Seçili = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Location = new System.Drawing.Point(12, 126);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(557, 367);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Target,
            this.Source,
            this.Seçili});
            this.dataGridView2.Location = new System.Drawing.Point(594, 126);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(464, 367);
            this.dataGridView2.TabIndex = 1;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // Target
            // 
            this.Target.HeaderText = "Target";
            this.Target.Name = "Target";
            // 
            // Source
            // 
            this.Source.HeaderText = "Source";
            this.Source.Name = "Source";
            // 
            // btnDosyaSec
            // 
            this.btnDosyaSec.Location = new System.Drawing.Point(321, 7);
            this.btnDosyaSec.Name = "btnDosyaSec";
            this.btnDosyaSec.Size = new System.Drawing.Size(75, 29);
            this.btnDosyaSec.TabIndex = 2;
            this.btnDosyaSec.Text = "Dosya Seç";
            this.btnDosyaSec.UseVisualStyleBackColor = true;
            this.btnDosyaSec.Click += new System.EventHandler(this.btnDosyaSec_Click);
            // 
            // txtxlsldosyayolu
            // 
            this.txtxlsldosyayolu.Location = new System.Drawing.Point(12, 12);
            this.txtxlsldosyayolu.Name = "txtxlsldosyayolu";
            this.txtxlsldosyayolu.Size = new System.Drawing.Size(303, 20);
            this.txtxlsldosyayolu.TabIndex = 3;
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(424, 12);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(223, 43);
            this.txtMessage.TabIndex = 4;
            // 
            // btnEslestir
            // 
            this.btnEslestir.Location = new System.Drawing.Point(667, 13);
            this.btnEslestir.Name = "btnEslestir";
            this.btnEslestir.Size = new System.Drawing.Size(94, 42);
            this.btnEslestir.TabIndex = 5;
            this.btnEslestir.Text = "Eşleştir";
            this.btnEslestir.UseVisualStyleBackColor = true;
            this.btnEslestir.Click += new System.EventHandler(this.btnEslestir_Click);
            // 
            // btnMatchingKod
            // 
            this.btnMatchingKod.Location = new System.Drawing.Point(767, 13);
            this.btnMatchingKod.Name = "btnMatchingKod";
            this.btnMatchingKod.Size = new System.Drawing.Size(94, 42);
            this.btnMatchingKod.TabIndex = 6;
            this.btnMatchingKod.Text = "Matching Kod";
            this.btnMatchingKod.UseVisualStyleBackColor = true;
            this.btnMatchingKod.Click += new System.EventHandler(this.btnMatchingKod_Click);
            // 
            // btnAktar
            // 
            this.btnAktar.Location = new System.Drawing.Point(867, 13);
            this.btnAktar.Name = "btnAktar";
            this.btnAktar.Size = new System.Drawing.Size(94, 42);
            this.btnAktar.TabIndex = 7;
            this.btnAktar.Text = "Aktar";
            this.btnAktar.UseVisualStyleBackColor = true;
            this.btnAktar.Click += new System.EventHandler(this.btnAktar_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 38);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(406, 82);
            this.textBox1.TabIndex = 8;
            // 
            // Seçili
            // 
            this.Seçili.HeaderText = "Seçili";
            this.Seçili.Name = "Seçili";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 505);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnAktar);
            this.Controls.Add(this.btnMatchingKod);
            this.Controls.Add(this.btnEslestir);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.txtxlsldosyayolu);
            this.Controls.Add(this.btnDosyaSec);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnDosyaSec;
        private System.Windows.Forms.TextBox txtxlsldosyayolu;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnEslestir;
        private System.Windows.Forms.Button btnMatchingKod;
        private System.Windows.Forms.Button btnAktar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Target;
        private System.Windows.Forms.DataGridViewComboBoxColumn Source;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Seçili;
    }
}