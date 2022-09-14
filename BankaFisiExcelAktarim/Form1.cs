
using ExcelDataReader;
using Microsoft.Office.Interop.Excel;
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

using Excel1 = Microsoft.Office.Interop.Excel;
using Excel;
using System.Data.SqlClient;

using System.Text.RegularExpressions;
using System.Reflection;
using System.Configuration;
using BankaFisiExcelAktarim.Data.Entity;
using BankaFisiExcelAktarim.Data.Base;
using DataTable = System.Data.DataTable;

namespace BankaFisiExcelAktarim
{
    public partial class Form1 : Form
    {
        //  string Baglanti = ConfigurationManager.ConnectionStrings["Baglan"].ConnectionString;

        public Form1()
        {
            InitializeComponent();
        }
        string query = string.Empty;
        private void btnDosyaSec_Click(object sender, EventArgs e)
        {

            //MessageBox.Show("Sql tablosu temizlendi");
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

            }
        }

        private void btnAktar_Click(object sender, EventArgs e)
        {
            if (txtxlsldosyayolu.Text != "")
            {
                DialogResult dr = MessageBox.Show("Sqle aktarım yapmak istediğinize emin misiniz...?", "UYARI", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    //SqlDelete();
                    ExceldenSqleAktarim();
                    MessageBox.Show("Sqle aktarım tamamlandı");
                }
                else if (dr == DialogResult.Cancel)
                {

                    MessageBox.Show("Sqle aktarım yapılamadı");
                }

            }
            else
            {
                MessageBox.Show("Aktarılacak excel dosyası seçiniz");
                btnDosyaSec.Focus();
            }
        }




        public void LogoAktar()
        {
            try
            {
                string Message = "";
                #region Logo Object Parametreleri
                LogoObjectParameter objectparameter = new LogoObjectParameter();
                objectparameter.ApplyCampaign = 0;
                objectparameter.ApplyCondition = 0;
                objectparameter.CheckApproveDate = 0;
                objectparameter.CheckParams = 1;
                objectparameter.CheckRight = 1;
                objectparameter.ExportAllData = 1;
                objectparameter.FillAccCodes = 1;
                objectparameter.GetStockLinePrice = 0;
                objectparameter.Period = 0;
                objectparameter.Validation = 1;
                #endregion

                int Type = Convert.ToInt32(DataType.doBankVoucher); //Hangi modüldeki kayıtla ilgili işlem yapılacak ise onun Type'ı seçilmeli. DataType enumunda türkçe karşılıkları bulunuyor.
                int DataRef = Type; //hangi fiş okunacak ise onun logicalrefi veritabanından okunarak yazılmalı
                int FirmNr = 0;
                string securitycode = "";
                string ParamXML = new LogoCore().XML_LogoObjectParameters(objectparameter);

                List<BankVoucher> bankvList = new BankVoucherData().GetAll().ToList();

                List<BankVoucherLine> bvl = new BankVoucherLineData().GetAll().ToList();
                List<LogoBnfLine> bnfLineList = new List<LogoBnfLine>();

                foreach (var bankv in bankvList)
                {
                    LogoBnFiche bnFiche = new LogoBnFiche();

                    bnFiche.Number = "~"; //Logoda fiş numarası üretilmesiyle ilgili tanımlama yapıldıysa tilde işareti kullanılabilir
                    bnFiche.Date_ = bankv.DATE;
                    bnFiche.Type = bankv.TYPE;

                    foreach (var bvlitem in bvl)
                    {

                        LogoBnfLine bnfLine = new LogoBnfLine();
                        bnfLine.Type = 1;
                        bnfLine.Sign = 0; //Sign : 0 Borç -- Sign : 1 Alacak
                        bnfLine.Ohp_Code1 = bvlitem.OHP_CODE1;
                        bnfLine.Bankacc_Code = bvlitem.BANKACC_CODE;
                        bnfLine.Arp_Code = bvlitem.ARP_CODE;
                        bnfLine.Amount = Convert.ToDecimal(bvlitem.AMOUNT);
                        bnfLine.Description = bvlitem.DESCRIPTION1;
                        bnfLineList.Add(bnfLine);

                    }
                    bnFiche.Detaylar = bnfLineList.ToArray();

                    string DataXML = new LogoCore().XmlBankVoucherFormat(ActionType.Insert, 0, ParamXML, 0, securitycode, bnFiche);

                    ResponseLogo response = new LogoDataObject().XMLData_Post(Type, DataXML, ParamXML, 0, securitycode);

                    bankv.LOGOREFID = response.Logicalref;
                    bankv.LOGOSTATUS = response.Status;
                    bankv.LOGOMESSAGE = response.Message;
                    bool res = new BankVoucherData().Edit(bankv);

                    Message += bankv.DATE + " tarihli banka fişinin işlem sonucu : " + response.Message + "\n";
                }
                MessageBox.Show(Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Logoya aktarım işleminde hata oluştu : " + ex.Message);

            }
        }

        public void SqldenLogoyaAktar()
        {
            try
            {
                string Message = "";
                #region Logo Object Parametreleri
                LogoObjectParameter objectparameter = new LogoObjectParameter();
                objectparameter.ApplyCampaign = 0;
                objectparameter.ApplyCondition = 0;
                objectparameter.CheckApproveDate = 0;
                objectparameter.CheckParams = 1;
                objectparameter.CheckRight = 1;
                objectparameter.ExportAllData = 1;
                objectparameter.FillAccCodes = 1;
                objectparameter.GetStockLinePrice = 0;
                objectparameter.Period = 0;
                objectparameter.Validation = 1;
                #endregion

                int Type = Convert.ToInt32(DataType.doBankVoucher); //Hangi modüldeki kayıtla ilgili işlem yapılacak ise onun Type'ı seçilmeli. DataType enumunda türkçe karşılıkları bulunuyor.
                int DataRef = Type; //hangi fiş okunacak ise onun logicalrefi veritabanından okunarak yazılmalı
                int FirmNr = 0;
                string securitycode = "";
                string ParamXML = new LogoCore().XML_LogoObjectParameters(objectparameter);

                List<BankVoucher> bankvList = new BankVoucherData().GetAll().Where(x => x.LOGOSTATUS == false).ToList();
                List<LogoBnfLine> bnfLineList = new List<LogoBnfLine>();

                foreach (var bankv in bankvList)
                {
                    LogoBnFiche bnFiche = new LogoBnFiche();

                    bnFiche.Number = "~"; //Logoda fiş numarası üretilmesiyle ilgili tanımlama yapıldıysa tilde işareti kullanılabilir
                    bnFiche.Date_ = bankv.DATE;
                    bnFiche.Type = bankv.TYPE;
                    List<BankVoucherLine> bvl = new BankVoucherLineData().GetAll().Where(x => x.BANKVOUCHERID == bankv.ID).ToList();
                    foreach (var bvlitem in bvl)
                    {

                        LogoBnfLine bnfLine = new LogoBnfLine();
                        bnfLine.Type = 1;
                        bnfLine.Sign = 0; //Sign : 0 Borç -- Sign : 1 Alacak
                        bnfLine.Ohp_Code1 = bvlitem.OHP_CODE1;
                        bnfLine.Bankacc_Code = bvlitem.BANKACC_CODE;
                        bnfLine.Arp_Code = bvlitem.ARP_CODE;
                        bnfLine.Amount = Convert.ToDecimal(bvlitem.AMOUNT);
                        bnfLine.Description = bvlitem.DESCRIPTION1;
                        bnfLineList.Add(bnfLine);

                    }
                    bnFiche.Detaylar = bnfLineList.ToArray();

                    string DataXML = new LogoCore().XmlBankVoucherFormat(ActionType.Insert, 0, ParamXML, 0, securitycode, bnFiche);

                    ResponseLogo response = new LogoDataObject().XMLData_Post(Type, DataXML, ParamXML, 0, securitycode);

                    bankv.LOGOREFID = response.Logicalref;
                    bankv.LOGOSTATUS = response.Status;

                    bankv.LOGOMESSAGE = response.Message;
                    bool res = new BankVoucherData().Edit(bankv);

                    List<BankVoucherLine> bvline = new BankVoucherLineData().Find(x => x.BANKVOUCHERID == bankv.ID).ToList();
                    CompanyConfig config = new CompanyConfigData().Find(x => x.CompanyID == 1).FirstOrDefault();
                    string BvLineQuery = "WHERE SOURCEFREF=" + bankv.LOGOREFID;
                    int LogoBnfLineCount = new LogoData().GetLogoBnfLineList(config.LogoDbName, config.LogoCompanyID.ToString(), config.LogoCompanyPeriodID.ToString(), BvLineQuery).Count();

                    if (bvline.Count() == LogoBnfLineCount)
                        Message += bankv.DATE.ToShortDateString() + " tarihli banka fişinin işlem sonucu : " + response.Message + "\n";
                    else
                    {

                        ResponseLogo responsedel = new LogoDataObject().Data_Delete(Type, bankv.LOGOREFID, ParamXML, 0, securitycode);
                        Message += bankv.LOGOREFID + "   idli fişin işlem sonucu : " + responsedel.Message;

                        foreach (var bvitem in bvline)
                        {
                            BankVoucherLineData bankline = new BankVoucherLineData();
                            bankline.Delete(bvitem.ID);
                        }
                        foreach (var bvitem in bankvList)
                        {
                            BankVoucherData bankvouc = new BankVoucherData();
                            bankvouc.Delete(bvitem.ID);
                        }
                    }
                }

                MessageBox.Show(Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Logo aktarım işleminde hata oluştu : " + ex.Message);
            }
        }


        public void ExceldenSqleAktarim()
        {


            Excel1.Application xlUygulama;//uygulama değişkeni tanımlandı
            Excel1.Workbook xlKitap;//kitap değişkeni tanımlandı
            Excel1.Worksheet xlSayfa;//sayfa değişkeni tanımlandı
            Excel1.Range range;//aralık değişkeni tanımlandı

            var str = "";
            int satir = 0;
            int sutun = 0;

            xlUygulama = new Excel1.Application();//excelin bir örneği oluşturuldu

            //belirtilen yoldaki excel dosyası açıldı ve ilk sayfası okunmaya başlandı
            xlKitap = xlUygulama.Workbooks.Open(txtxlsldosyayolu.Text, 0, true, 5, "", "", true, Excel1.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlSayfa = (Excel1.Worksheet)xlKitap.Worksheets.get_Item(1);
            //okunan sayfadaki son bölümün index değeri alındı.
            //satır ve sütun olacak şekilde iki boyutlu bir yapı üzerinde hareket edilecektir.
            range = xlSayfa.UsedRange;//alınacak aralık belirtilmediği için sayfa içindeki tüm alanları alacaktır.

            // satır ve sütunlar üzerinde dönerek ekrana okuduğumuz bilgileri yazdırıyoruz.

            try
            {

                BankVoucher bnFiche = new BankVoucher();

                bnFiche.NUMBER = "~"; //Logoda fiş numarası üretilmesiyle ilgili tanımlama yapıldıysa tilde işareti kullanılabilir
                bnFiche.DATE = Convert.ToDateTime(dtTarih.Text);
                bnFiche.TYPE = 4;

                int BankVoucherID = new BankVoucherData().Add(bnFiche);

                for (satir = 2; satir <= range.Rows.Count; satir++)
                {
                    BankVoucherLine bv = new BankVoucherLine();
                    bv.BANKVOUCHERID = BankVoucherID;
                    str = Convert.ToString((range.Cells[satir, 1] as Excel1.Range).Value2);
                    bv.NO = Convert.ToInt32(str);
                    str = Convert.ToString((range.Cells[satir, 2] as Excel1.Range).Value2);
                    bv.DEFINITION_ = str;
                    str = Convert.ToString((range.Cells[satir, 3] as Excel1.Range).Value2);
                    bv.OHP_CODE1 = str;
                    str = Convert.ToString((range.Cells[satir, 4] as Excel1.Range).Value2);
                    bv.BANKACC_CODE = str;
                    str = Convert.ToString((range.Cells[satir, 5] as Excel1.Range).Value2);
                    bv.ARP_CODE = str;
                    str = Convert.ToString((range.Cells[satir, 6] as Excel1.Range).Value2);
                    bv.ACCOUNT_NO = str;
                    str = Convert.ToString((range.Cells[satir, 7] as Excel1.Range).Value2);
                    bv.AMOUNT = Convert.ToDecimal(str);
                    str = Convert.ToString((range.Cells[satir, 8] as Excel1.Range).Value2);
                    bv.DESCRIPTION1 = str;
                    str = Convert.ToString((range.Cells[satir, 9] as Excel1.Range).Value2);
                    bv.DESCRIPTION2 = str;


                    int ID = new BankVoucherLineData().Add(bv);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("Excel dokümanı alanlar uyuşmuyor.");

                //throw;
            }





            xlKitap.Close(true, null, null);
            xlUygulama.Quit();

            /*Ram üzeründen de excelin bağlantısını kaldırıyoruz.*/
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlUygulama);
                xlUygulama = null;
            }
            catch (Exception ex)
            {
                xlUygulama = null;
                MessageBox.Show("Hata " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
            //MessageBox.Show("Excel tablosu başaryla Sqle aktarıldı");



        }
        private void Form1_Load(object sender, EventArgs e)
        {


            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName == "EXCEL")
                {
                    p.Kill();
                }
            }
        }





        private void btnLogoAktar_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Logoya aktarım yapmak istediğinize emin misiniz...?", "UYARI", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {

                SqldenLogoyaAktar();

            }
            else if (dr == DialogResult.Cancel)
            {

                MessageBox.Show("Logoya Aktarım yapılamadı");
            }

        }



    }
}
