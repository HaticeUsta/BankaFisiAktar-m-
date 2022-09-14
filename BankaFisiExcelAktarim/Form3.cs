using BankaFisiExcelAktarim.Data.Base;
using BankaFisiExcelAktarim.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Excel1 = Microsoft.Office.Interop.Excel;

namespace BankaFisiExcelAktarim
{
    public partial class Form3 : Form
    {
        public enum IslemTipleri { Bos = 0, SecinizVar = 1, AyniAlanKullanimi = 233 };
        public Form3()
        {
            InitializeComponent();
        }
        private void xmlokuma()
        {

        }
        private void xmltoexcel()
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


                //for (int i = 1; i < dtExcelSchema.Columns.Count + 1; i++) // Creating Header Column In Excel
                //{
                //   string deneme = dtExcelSchema.Columns[i - 1].ColumnName;
                //    MessageBox.Show(deneme);
                //}



                oda.Fill(tbContainer);

                dataGridView1.DataSource = tbContainer;









                //oda.Fill(tbContainer);

                //dataGridView1.DataSource = tbContainer;
            }
        }

        private void btnDosyaSec_Click(object sender, EventArgs e)
        {

            DialogResult drResult = OFD.ShowDialog();
            if (drResult == System.Windows.Forms.DialogResult.OK)
                txtDosyaYolu.Text = OFD.FileName;



        }

        private void xmltodatagridview()
        {
            XmlReader xmlFile = XmlReader.Create(txtDosyaYolu.Text, new XmlReaderSettings());
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(xmlFile);

            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void btnYukle_Click(object sender, EventArgs e)
        {
            //xmltodatagridview();
            xmlLoad();
        }
        public void excelLoad(DataGridView dgvMethodParam)
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
                txtDosyaYolu.Text = pathName;
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
                string[] starr = new string[tbContainer.Columns.Count];
                for (int i = 1; i < tbContainer.Columns.Count; i++)
                {
                    starr[i] = ConvertExcelColumnNumberToName(i);
                    MessageBox.Show(starr[i]);
                    (dataGridView1.Columns[1] as DataGridViewComboBoxColumn).Items.AddRange(starr[i]);
                }
                


                dataGridView1.AllowUserToAddRows = false;



                //*************************************************
                //string[] starr = new string[tbContainer.Columns.Count + 1];
                //starr[0] = "Seçiniz";


                //    for (int i = 0; i < tbContainer.Columns.Count; i++)
                //    {
                //        starr[i + 1] = tbContainer.Columns[i].ToString();


                //    }
                //    for (int s = 0; s < dataGridView1.RowCount; s++)
                //    {
                //        (dataGridView1.Rows[s].Cells[1] as DataGridViewComboBoxCell).Value = "Seçiniz";

                //    }

                //    (dataGridView1.Columns[1] as DataGridViewComboBoxColumn).Items.AddRange(starr);
                //**************************************************************
                //string[] starr = new string[tbContainer.Columns.Count];
                //for (int i = 0; i < tbContainer.Columns.Count; i++)
                //{
                //    starr[i] = tbContainer.Columns[i].ToString();
                //}
                //(dataGridView1.Columns[1] as DataGridViewComboBoxColumn).Items.AddRange(starr);
                //dataGridView1.AllowUserToAddRows = false;
                //****************************************************************

            }
        }
        
      
            static string ConvertExcelColumnNumberToName(int columnNumber)
            {
               // if (columnNumber == null) throw new ArgumentNullException("columnNumber");

                string setColumnName = String.Empty;
                int tempRemainder = 0;

                while (columnNumber > 0)
                {
                    tempRemainder = (columnNumber - 1) % 26;
                    setColumnName = Convert.ToChar(65 + tempRemainder).ToString() + setColumnName;
                    columnNumber = (int)((columnNumber - tempRemainder) / 26);
                }

                return setColumnName;
            }

        
        public IslemTipleri KontrolEt()
        {
            IslemTipleri tip = IslemTipleri.Bos;

            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[1].Value.ToString() == "Seçiniz")
                {
                    tip = IslemTipleri.SecinizVar;
                    break;
                }
            }

            if (tip == IslemTipleri.Bos)
            {
                for (int i = 1; i < dataGridView1.Rows.Count - 1; i++)
                {
                    for (int j = i + 1; j < dataGridView1.Rows.Count; j++)
                    {
                        if (dataGridView1.Rows[i].Cells[1].Value.ToString() == dataGridView1.Rows[j].Cells[1].Value.ToString())
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
        //private void DatagridSqlAktar()
        //{
        // string str = "";
        // int satir = 1;
        //     try
        //     {
        //         for (satir = 2; satir <= dataGridView1.Rows.Count; satir++)
        //         {
        //             BankVoucher bv = new BankVoucher();

        //             str = dataGridView1.Rows[satir].Cells[1].Value.ToString();
        //             bv.OHP_CODE1 = str;
        //             str = dataGridView1.Rows[satir].Cells[1].Value.ToString();
        //             bv.BANKACC_CODE = str;
        //             str = dataGridView1.Rows[satir].Cells[1].Value.ToString();
        //             bv.ARP_CODE = str;
        //             str = dataGridView1.Rows[satir].Cells[1].Value.ToString();
        //             bv.AMOUNT = Convert.ToDecimal(str);
                    
        //             str = dataGridView1.Rows[satir].Cells[1].Value.ToString();
        //             bv.DESCRIPTION1 = str;
        //             bv.LOGOSTATUS = false;

        //             int ID = new BankVoucherData().Add(bv);
        //         }
        //     }
        //     catch (Exception ex)
        //     {
        //         //MessageBox.Show(ex.Message);
        //         MessageBox.Show("Excel dokümanı alanlar uyuşmuyor.");

        //         //throw;
        //     }
         
        //}
          
        


        private void sqlAktar()
        {
            //IslemTipleri tip = KontrolEt();
            //if (tip == IslemTipleri.Bos)
            //{
            //    string str = "";
            //    textBox1.Text = "";
            //    string targetAlanAdi = "";
            //    string sourceAlanAdi = "";
            //    string alanlar = "";
            //    string degerler = "";
            //    for (int i = 0; i < dtSource.Rows.Count; i++)
            //    {
            //        alanlar = "";
            //        degerler = "";

            //        for (int j = 0; j < dataGridView2.Rows.Count; j++) //rows ama insert-teki column-lara denk geliyor.
            //        {
            //            targetAlanAdi = dataGridView2.Rows[j].Cells[0].Value.ToString();
            //            sourceAlanAdi = dataGridView2.Rows[j].Cells[1].Value.ToString();
            //            if (j > 0) alanlar += ",";
            //            alanlar += "[" + targetAlanAdi + "]";
            //            if (j > 0) degerler += ",";
            //            degerler += ByDataType(dtSource.Columns[j].DataType.Name, dtSource.Rows[i][sourceAlanAdi].ToString());
            //        }
            //        str += String.Format(@" Insert into BankVoucher ({0}) values ({1}); ", alanlar, degerler) + "\r\n";
            //    }
            //    textBox1.Text = str; // strInsert; //"aabb\n\r<br/>" // + + + +  // "aabb\r\n" + + + 
            //    textBox1.SelectAll();
            //    textBox1.Focus();
            //    txtMessage.Text = "Eşleştirme başarılı";
            //}
            //else if (tip == IslemTipleri.SecinizVar)
            //{
            //    txtMessage.Text = "IslemTipleri.SecinizVar";
            //    textBox1.Text = "";
            //    return;
            //}
            //else if (tip == IslemTipleri.AyniAlanKullanimi)
            //{
            //    txtMessage.Text = "IslemTipleri.AyniAlanKullanimi";
            //    textBox1.Text = "";
            //}
        }



        public void xmlLoad()
        {
            XmlReader xmlFile = XmlReader.Create(txtDosyaYolu.Text, new XmlReaderSettings());
            XDocument xDoc = XDocument.Load(txtDosyaYolu.Text);
            XElement rootElement = xDoc.Root;
           // XElement attributesElement = XElement.Parse(xmlFile.ReadOuterXml());

            
            XmlDocument document = new XmlDocument();



          
            while (xmlFile.Read())
            {
                switch (xmlFile.NodeType)
                {
                    case XmlNodeType.Element: 
                       string str = xmlFile.Name;
                       // MessageBox.Show(str);


                        dataGridView1.Rows.Add(str);




                        break;
                   
                }
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            excelLoad(dataGridView1);
        }

        //   while (reader.Read()) {
        //  switch (reader.NodeType) 
        //  {
        //      case XmlNodeType.Element: // The node is an element.
        //          Console.Write("<" + reader.Name);

        //          while (reader.MoveToNextAttribute()) // Read the attributes.
        //              Console.Write(" " + reader.Name + "='" + reader.Value + "'");
        // Console.WriteLine(">");
        //          break;
        //case XmlNodeType.Text: //Display the text in each element.
        //          Console.WriteLine(reader.Value);
        //          break;
        //case XmlNodeType.EndElement: //Display the end of the element.
        //          Console.Write("</" + reader.Name);
        // Console.WriteLine(">");
        //          break;
        //  }
        //}


    }



}
