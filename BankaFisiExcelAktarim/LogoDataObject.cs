using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BankaFisiExcelAktarim.LogoCore;

namespace BankaFisiExcelAktarim
{
    public class LogoDataObject
    {
        BankaFisiExcelAktarim.ServiceReference1.SvcClient client = new BankaFisiExcelAktarim.ServiceReference1.SvcClient();

        public string XMLData_Read(int Type, int DataRef, string ParamXML, int FirmNr, string SecurityCode)
        {
            try
            {
                int dataType = Type;
                int dataReference = DataRef;
                string dataXML = "";
                string errorString = "";
                byte status = 32;
                //bool r = true;
                client.ReadDataObject(dataType, ref dataReference, ref dataXML, ref ParamXML, ref errorString, ref status, "", 0, SecurityCode);
                if (status == 4) //Status 4 olması durumunda logodan hata dönmektedir.
                    throw new Exception("Kayıt okunamadı : " + errorString);
                else
                    return dataXML;
            }
            catch (Exception ex)
            {
                throw new Exception("Kayıt okunamadı : " + ex.Message);
            }
        }

        public ResponseLogo XMLData_Post(int DataType, string DataXML, string ParameterXML, int FirmNr, string SecurityCode)
        {

            ResponseLogo rs = new ResponseLogo();
            try
            {
                int dataReference = 0; //Kaydın Unique ID'si appendDataObject methodu buraya set etmektedir.
                byte Status = 0; //İşlem sonucu 4 ise hatalı, değil ise başarılı sonucu dönmektedir.
                string ErrorString = "";

                client.AppendDataObject(DataType, ref dataReference, ref DataXML, ref ParameterXML, ref ErrorString, ref Status, "", FirmNr, SecurityCode);
                if (Status == 4) //başarısız
                {
                    rs.Logicalref = 0;
                    rs.ErrorType = 1;
                    rs.Message = ErrorString;
                    rs.Status = false;
                    return rs;
                }
                else //başarılı
                {
                    //string data = new LogoDataObject().XMLData_Read(DataType, dataReference, ParameterXML, FirmNr, SecurityCode);
                    //if (!string.IsNullOrEmpty(data))
                    //{

                    //XmlDocument xml = new XmlDocument();
                    //xml.LoadXml(data);
                    if (DataType == 19 || DataType == 3 || DataType == 18 || DataType == 31 || DataType == 24 || DataType == 4)
                    {
                        string Number = StringCompressor.UnzipBase64(DataXML);
                        int From = Number.IndexOf("<NUMBER>") + "<NUMBER>".Length;
                        int To = Number.LastIndexOf("</NUMBER>");
                        String No = Number.Substring(From, To - From);
                        rs.No = No;//Fiş Numarası
                    }
                    else if (DataType == 30 || DataType == 90 || DataType == 25)
                    {
                        string Code = StringCompressor.UnzipBase64(DataXML);
                        int From = Code.IndexOf("<CODE>") + "<CODE>".Length;
                        int To = Code.LastIndexOf("</CODE>");
                        String No = Code.Substring(From, To - From);
                        rs.No = No;//Kodu
                    }
                    //}
                    rs.Logicalref = dataReference;
                    rs.Message = dataReference + " referans IDli kayıt başarıyla kaydedildi";
                    rs.Status = true;
                    return rs;
                }
            }
            catch (Exception ex)
            {
                rs.ErrorType = 1;
                rs.Logicalref = 0;
                rs.Message = ex.Message;
                rs.Status = false;
                return rs;
            }
        }

        //xmldata_add'i neden eklediniz daha önce postta çalıştım hata vardır diye deneme yapıyordum
        //yeni bir method eklemenize gerek yok burada. post methodu doğru çalışıyor biz de bütün aktarımları bu method ile yapıyoruz.
        //Bu şekilde kullanabilirsiniz insert ve update bu methodla çalışıyorm
        //dimi
      
        //bu method silinmiş. bunu silmeyin, bununla da silme işlemi yapabilirsiniz.
        public ResponseLogo Data_Delete(int DataType, int DataRef, string ParameterXML, int FirmNr, string SecurityCode)
        {
            ResponseLogo rs = new ResponseLogo();
            try
            {
                byte Status = 0; //İşlem sonucu 4 ise hatalı değil ise başarılı sonucu dönmektedir.
                string ErrorString = "";
                client.DeleteDataObject(DataType, DataRef, ref ErrorString, ref Status, "", FirmNr, SecurityCode);
                if (Status == 4) //başarısız
                {
                    rs.ErrorType = 2;
                    rs.Logicalref = 0;
                    rs.Message = ErrorString;
                    rs.Status = false;
                    return rs;
                }
                else //başarılı
                {
                    rs.Logicalref = DataRef;
                    rs.Message = DataRef + " referans IDli kayıt başarıyla silindi";
                    rs.Status = true;
                    return rs;
                }
            }
            catch (Exception ex)
            {
                rs.Logicalref = 0;
                rs.Message = ex.Message;
                rs.Status = false;
                return rs;
            }
        }
    }
}




                
             
