using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankaFisiExcelAktarim
{
    public partial class Form2 : Form
    {
        public enum IslemTipleri { Bos = 0, SecinizVar = 1, AyniAlanKullanimi = 233 };
        public bool goster = false;
        public DataTable dtTarget = new DataTable();
        public DataTable dtSource = new DataTable();
        public string connectionString;
        public string selectedCellRowCol = "";
        public string TabloAdi = "BankVoucher";
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            GetTargetColumnsFromDBTableANDSetDgv(dataGridView2);
            GetColumnsFromExcel(dataGridView2);
        }
        public void GetTargetColumnsFromDBTableANDSetDgv(DataGridView dgvMethodParam)
        {

            SqlConnection conn = OpenSqlConnection();
            string cmd = string.Format(@"Select * from [{0}]", TabloAdi); //ExceldenAdAdetT

            SqlDataAdapter adap = new SqlDataAdapter(cmd, conn);
            adap.Fill(dtTarget);

            for (int i = 0; i < dtTarget.Columns.Count; i++)
            {
                dgvMethodParam.Rows.Add();
                dgvMethodParam.Rows[i].Cells[0].Value = dtTarget.Columns[i].ColumnName;
            }
        }
        public SqlConnection OpenSqlConnection()
        {
            SqlConnection conn = new SqlConnection(GetConnectionString("EfContext")); //webConfig deki connection-in adi 

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
                conn.Open();
            }
            try
            {
                conn.Open();
                if (goster)
                {
                    MessageBox.Show(conn.State.ToString());
                }
            }
            catch (Exception ex)
            {
                if (goster)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            return conn;
        }
        public OleDbConnection OpenOleDbConnection(string connName) //("Excel2007")
        {
            OleDbConnection conn = new OleDbConnection(GetExcelConnectionString(connName)); //connName //Excel2007

            if (conn.State != ConnectionState.Closed)
            {
                conn.Close();
                conn.Open();
            }
            try
            {
                conn.Open();
                if (goster)
                {
                    MessageBox.Show(conn.State.ToString() + " .. Microsoft.Jet.OLEDB.4.0 .. Microsoft.ACE.OLEDB.12.0 ");
                    //textBox1.Text = conn.State.ToString() + " .. Microsoft.Jet.OLEDB.4.0 .. Microsoft.ACE.OLEDB.12.0 ";
                }
            }
            catch (Exception ex)
            {
                if (goster)
                {
                    MessageBox.Show(ex.ToString());
                    //textBox1.Text = ex.ToString();
                }
            }
            return conn;
        }
        public string GetExcelConnectionString(string param)
        {
            string exConnection = "";
            switch (param)
            {
                case "Excel2003":
                    exConnection = String.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}; Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';", @"C:\Users\Pinar.Karaman\Desktop\Excele Aktarım\bankafisi.xlsx");
                    break;
                case "Excel2007":
                    exConnection = String.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties='Excel 12.0 Xml; HDR = YES';", @"C:\Users\Pinar.Karaman\Desktop\Excele Aktarım\bankafisi.xlsx");
                    break;
                default:
                    break;
            }
            return exConnection;
        }
        public string GetConnectionString(string connName)
        {
            //Readme: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.6.1\System.Configuration.dll
            //References-a eklemek gerekiyor WinForms uygulamasında.
            return ConfigurationManager.ConnectionStrings[connName].ConnectionString;
        }
        public void GetColumnsFromExcel(DataGridView dgvMethodParam)
        {
            OleDbConnection conn = OpenOleDbConnection("Excel2007");
            string cmd = "Select * from [Sayfa1$]";

            OleDbDataAdapter adap = new OleDbDataAdapter(cmd, conn);
            adap.Fill(dtSource);

            string[] starr = new string[dtSource.Columns.Count + 1];
            starr[0] = "Seçiniz";
            for (int i = 1; i < dtSource.Columns.Count; i++)
            {
                starr[i + 1] = dtSource.Columns[i].ToString();
                (dgvMethodParam.Rows[i].Cells[1] as DataGridViewComboBoxCell).Value = "Seçiniz";
            }

            (dgvMethodParam.Columns[1] as DataGridViewComboBoxColumn).Items.AddRange(starr);
            DGVdoldur(dataGridView1);
        }
        private void btnDosyaSec_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();  //create openfileDialog Object
            openFileDialog1.Filter = "XML Files (*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb) |*.xml; *.xls; *.xlsx; *.xlsm; *.xlsb";//open file format define Excel Files(.xls)|*.xls| Excel Files(.xlsx)|*.xlsx| 
            openFileDialog1.FilterIndex = 3;

            openFileDialog1.Multiselect = false;        //not allow multiline selection at the file selection level
            openFileDialog1.Title = "Open Text File-R13";   //define the name of openfileDialog
            openFileDialog1.InitialDirectory = @"Desktop"; //define the initial directory

            if (openFileDialog1.ShowDialog() == DialogResult.OK)        //executing when file open
            {
                string pathName = openFileDialog1.FileName;
                string fileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);

                DataTable tbContainer = new DataTable();
                string strConn = string.Empty;
                string sheetName = fileName;

                FileInfo file = new FileInfo(pathName);
                if (!file.Exists) { throw new Exception("Error, file doesn't exists!"); }
                string extension = file.Extension;
                switch (extension)
                {
                    case ".xls":
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                        break;
                    case ".xlsx":
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1;'";
                        break;
                    default:
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                        break;
                }


                OleDbConnection cnnxls = new OleDbConnection(strConn);
                string SheetName = string.Empty;
                cnnxls.Open();
                DataTable dtExcelSchema = cnnxls.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow row in dtExcelSchema.Rows)
                {
                    if (row["TABLE_NAME"].ToString().Contains(""))
                    {
                        SheetName = row["TABLE_NAME"].ToString();
                        break;
                    }
                }
                OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [{0}]", SheetName), cnnxls);
                oda.Fill(tbContainer);
                txtxlsldosyayolu.Text = pathName;
                dataGridView1.DataSource = tbContainer;
                //DgvPropertiesSet(dataGridView1);
                
                GetTargetColumnsFromDBTableANDSetDgv(dataGridView2);
                GetColumnsFromExcel(dataGridView2);
            }
        }

        public void DGVdoldur(DataGridView dgvMethodParam)
        {
            dgvMethodParam.DataSource = dtSource.DefaultView; //Additional information: DataGridView denetiminin SelectionMode özelliği FullColumnSelect olarak ayarlanmışken sütunun SortMode özelliği Automatic olarak ayarlanamaz.
            //--DgvPropertiesSet(dataGridView2); // + + + +  
        }
        public void DgvPropertiesSet(DataGridView dgv1)
        {
            dataGridView1.AllowUserToAddRows = false;

            dgv1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable; //++
            dgv1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv1.Columns[3].SortMode = DataGridViewColumnSortMode.NotSortable;
            dgv1.Columns[4].SortMode = DataGridViewColumnSortMode.NotSortable;

            dgv1.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;

            //dgv1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv1.SelectionMode = DataGridViewSelectionMode.FullColumnSelect;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (selectedCellRowCol != "")
            {
                int rr = int.Parse(selectedCellRowCol.Split(',')[0]); //secilenCellRowCol
                int cc = int.Parse(selectedCellRowCol.Split(',')[1]); //secilenCellRowCol
                txtMessage.Text = selectedCellRowCol;
                dataGridView2.Rows[rr].Cells[cc].Value = dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText;
            }
        }
        public IslemTipleri KontrolEt()
        {
            IslemTipleri tip = IslemTipleri.Bos;

            for (int i = 0; i < dataGridView2.Rows.Count; i++)
            {
                if (dataGridView2.Rows[i].Cells[1].Value.ToString() == "Seçiniz")
                {
                    tip = IslemTipleri.SecinizVar;
                    break;
                }
            }

            if (tip == IslemTipleri.Bos)
            {
                for (int i = 1; i < dataGridView2.Rows.Count - 1; i++)
                {
                    for (int j = i + 1; j < dataGridView2.Rows.Count; j++)
                    {
                        if (dataGridView2.Rows[i].Cells[1].Value.ToString() == dataGridView2.Rows[j].Cells[1].Value.ToString())
                        {
                            tip = IslemTipleri.AyniAlanKullanimi;
                            break;
                        }
                    }
                    if (tip != IslemTipleri.Bos)
                    {

                    }
                }
            }
            return tip;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selectedCellRowCol = e.RowIndex.ToString() + "," + "1";
            txtMessage.Text = selectedCellRowCol;
        }

        private void btnEslestir_Click(object sender, EventArgs e)
        {
            var aa = "";
            textBox1.Text = "";
            for (int i = 1; i < dataGridView2.Rows.Count; i++)
            {
                for (int j = 1; j < (dataGridView2.Rows[i].Cells[1] as DataGridViewComboBoxCell).Items.Count; j++)
                {
                    aa = "";
                    //if (dataGridView2.Rows[i].Cells[0].Value.ToString().Substring(0, 1) == (dataGridView2.Rows[i].Cells[1] as DataGridViewComboBoxCell).Items[j].ToString().Substring(0, 1))
                    //{
                        (dataGridView2.Rows[i].Cells[1] as DataGridViewComboBoxCell).Value = (dataGridView2.Rows[i].Cells[1] as DataGridViewComboBoxCell).Items[j].ToString();

                    //}
                }
            }
        }

        private void btnMatchingKod_Click(object sender, EventArgs e)
        {
            IslemTipleri tip = KontrolEt();
            if (tip == IslemTipleri.Bos)
            {
                string str = "";
                textBox1.Text = "";
                string targetAlanAdi = "";
                string sourceAlanAdi = "";
                string alanlar = "";
                string degerler = "";
                for (int i = 0; i < dtSource.Rows.Count; i++)
                {
                    alanlar = "";
                    degerler = "";

                    for (int j = 0; j < dataGridView2.Rows.Count; j++) //rows ama insert-teki column-lara denk geliyor.
                    {
                        targetAlanAdi = dataGridView2.Rows[j].Cells[0].Value.ToString();
                        sourceAlanAdi = dataGridView2.Rows[j].Cells[1].Value.ToString();
                        if (j > 0) alanlar += ",";
                        alanlar += "[" + targetAlanAdi + "]";
                        if (j > 0) degerler += ",";
                        degerler += ByDataType(dtSource.Columns[j].DataType.Name, dtSource.Rows[i][sourceAlanAdi].ToString());
                    }
                    str += String.Format(@" Insert into BankVoucher ({0}) values ({1}); ", alanlar, degerler) + "\r\n";
                }
                textBox1.Text = str; // strInsert; //"aabb\n\r<br/>" // + + + +  // "aabb\r\n" + + + 
                textBox1.SelectAll();
                textBox1.Focus();
                txtMessage.Text = "Eşleştirme başarılı";
            }
            else if (tip == IslemTipleri.SecinizVar)
            {
                txtMessage.Text = "IslemTipleri.SecinizVar";
                textBox1.Text = "";
                return;
            }
            else if (tip == IslemTipleri.AyniAlanKullanimi)
            {
                txtMessage.Text = "IslemTipleri.AyniAlanKullanimi";
                textBox1.Text = "";
            }
        }
        public string ByDataType(string dc, string st)
        {
            //dtSource.Columns[0].DataType.Name
            if (dc == "String")
            {
                st = string.Format("'{0}'", st);
            }
            else if (dc == "Double")
            {
                st = string.Format("{0}", st);
            }
            else
            {
                st = string.Format("{0}", st);
            }
            return st;
        }
        private void btnAktar_Click(object sender, EventArgs e)
        {

            txtMessage.Text = "Excel to Custom DB table : " + DBExecuteNonQueryBySqlCmd(textBox1.Text);
        }
        public bool DBExecuteNonQueryBySqlCmd(string query)
        {
            SqlConnection conn = new SqlConnection();
            SqlCommand cmd = null;
            string SqlCommand = query;

            try
            {
                cmd = new SqlCommand(SqlCommand, conn);
                //cmd = SqlHelper.GetSqlConnectionCMD(SqlCommand, baglanti);
                //cmd.Connection.Open();
                cmd.CommandText = SqlCommand;
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (cmd != null && cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                if (cmd != null)
                    cmd.Dispose();
            }
        }
    }

}

