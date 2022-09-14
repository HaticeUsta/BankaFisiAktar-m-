
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace BankaFisiExcelAktarim 
{
    public class LogoCore
    {
        LogoBnFiche bnFiche = new LogoBnFiche();
        LogoDataObject LogoDataObject = new LogoDataObject();
        public string XML_LogoObjectParameters(LogoObjectParameter objectparameter)
        {
            try
            {
                string paramXML = "<?xml version=\"1.0\" encoding=\"utf-16\"?>"
                                + "<Parameters>"
                                + "<ReplicMode>" + objectparameter.ReplicMode + "</ReplicMode>"
                                + "<CheckParams>" + objectparameter.CheckParams + "</CheckParams>"
                                + "<CheckRight>" + objectparameter.CheckRight + "</CheckRight>"
                                + "<ApplyCampaign>" + objectparameter.ApplyCampaign + "</ApplyCampaign>"
                                + "<ApplyCondition>" + objectparameter.ApplyCondition + "</ApplyCondition>"
                                + "<FillAccCodes>" + objectparameter.FillAccCodes + "</FillAccCodes>"
                                + "<FormSeriLotLines> " + objectparameter.FormSeriLotLines + "</FormSeriLotLines>"
                                + "<GetStockLinePrice> " + objectparameter.GetStockLinePrice + "</GetStockLinePrice>"
                                + "<ExportAllData>" + objectparameter.ExportAllData + "</ExportAllData>"
                                + "<Validation>" + objectparameter.Validation + "</Validation>"
                                + "</Parameters>";

                return paramXML;
            }
            catch (Exception ex)
            {
                throw new Exception("Logo Object Parametre XML'i Oluşturulamadı : " + ex.Message);

            }
        }

        internal string XML_LogoObjectParameters(object objectParameter)
        {
            throw new NotImplementedException();
        }

        public string XmlBankVoucherFormat(ActionType ActionType, int DataRef, string ParamXML, int FirmNr, string SecurityCode, LogoBnFiche bnfiche)
        {
            //ActionType = 1 - Insert - INS
            //ActionType = 2 - Update - UPD
            string xmlFormat = string.Empty;
            xmlFormat = "<BANK_VOUCHERS><BANK_VOUCHER DBOP=\"" + (ActionType == ActionType.Insert ? "INS" : "UPD") + "\">";

            if (ActionType == ActionType.Insert) //Insert ise;
            {
                // xmlFormat += "<DATA_REFERENCE>" + clfiche.LOGICALREF + "</DATA_REFERENCE>";
                if (bnfiche.Division.HasValue)
                    xmlFormat += "<DIVISION>" + bnfiche.Division + "</DIVISION>"; //Bölüm
                if (bnfiche.Department.HasValue)
                    xmlFormat += "<DEPARTMENT>" + bnfiche.Department + "</DEPARTMENT>"; //Departman
                if (!string.IsNullOrEmpty(bnfiche.Number))
                    xmlFormat += "<NUMBER>" + bnfiche.Number + "</NUMBER>"; //Banka Fiş Numarası
                if (bnfiche.Date_.HasValue)
                    xmlFormat += "<DATE>" + bnfiche.Date_.Value.ToString("dd.MM.yyyy") + "</DATE>"; //Banka Fiş Tarihi
                if (bnfiche.Type.HasValue)
                    xmlFormat += "<TYPE>" + bnfiche.Type + "</TYPE>"; //Banka Fiş Tipi
                if (bnfiche.Accounted.HasValue)
                    xmlFormat += "<GL_POSTED>" + bnfiche.Accounted + "</GL_POSTED>"; //Banka Fiş Tipi

                if (bnfiche.Total_Debit.HasValue)
                    xmlFormat += "<TOTAL_DEBIT>" + bnfiche.Total_Debit.ToString().Replace(",", ".") + "</TOTAL_DEBIT>"; //Toplam Borç
                if (bnfiche.Total_Credit.HasValue)
                    xmlFormat += "<TOTAL_CREDIT>" + bnfiche.Total_Credit.ToString().Replace(",", ".") + "</TOTAL_CREDIT>"; //Toplam Alacak
                if (!string.IsNullOrEmpty(bnfiche.Notes1))
                    xmlFormat += "<NOTES1>" + bnfiche.Notes1 + "</NOTES1>"; //Açıklama1
                if (!string.IsNullOrEmpty(bnfiche.Notes2))
                    xmlFormat += "<NOTES2>" + bnfiche.Notes2 + "</NOTES2>"; //Açıklama2
                if (!string.IsNullOrEmpty(bnfiche.Notes3))
                    xmlFormat += "<NOTES3>" + bnfiche.Notes3 + "</NOTES3>"; //Açıklama3
                if (!string.IsNullOrEmpty(bnfiche.Notes4))
                    xmlFormat += "<NOTES4>" + bnfiche.Notes4 + "</NOTES4>"; //Açıklama4
                if (!string.IsNullOrEmpty(bnfiche.Authcode))
                    xmlFormat += "<AUTH_CODE>" + bnfiche.Authcode + "</AUTH_CODE>"; //Yetki Kodu
                if (!string.IsNullOrEmpty(bnfiche.Specode))
                    xmlFormat += "<AUXIL_CODE>" + bnfiche.Specode + "</AUXIL_CODE>"; //Özel Kod

                //Cari Fiş Satırları
                xmlFormat += "<TRANSACTIONS>";
                if (bnfiche.Detaylar != null)
                {
                    foreach (LogoBnfLine ficheDetail in bnfiche.Detaylar)
                    {
                        xmlFormat += "<TRANSACTION>";
                        if (ficheDetail.Type.HasValue)
                            xmlFormat += "<TYPE>" + ficheDetail.Type + "</TYPE>"; //Cari Hesap Kodu
                        if (!string.IsNullOrEmpty(ficheDetail.Arp_Code))
                            xmlFormat += "<OHP_CODE1>" + ficheDetail.Ohp_Code1 + "</OHP_CODE1>"; //Cari Hesap Kodu
                        if (!string.IsNullOrEmpty(ficheDetail.Arp_Code))
                            xmlFormat += "<ARP_CODE>" + ficheDetail.Arp_Code + "</ARP_CODE>"; //Cari Hesap Kodu
                        if (!string.IsNullOrEmpty(ficheDetail.Bankacc_Code))
                            xmlFormat += "<BANKACC_CODE>" + ficheDetail.Bankacc_Code + "</BANKACC_CODE>"; //Banka Hesap Kodu
                        if (!string.IsNullOrEmpty(ficheDetail.Doc_Number))
                            xmlFormat += "<DOC_NUMBER>" + ficheDetail.Doc_Number + "</DOC_NUMBER>"; //Belge Numarası

                        if (!string.IsNullOrEmpty(ficheDetail.Description))
                            xmlFormat += "<DESCRIPTION>" + ficheDetail.Description + "</DESCRIPTION>"; //Satır Açıklaması
                        if (ficheDetail.Sign.HasValue)
                            xmlFormat += "<SIGN>" + ficheDetail.Sign + "</SIGN>";
                        if (ficheDetail.Sign == 1)
                        {
                            xmlFormat += "<CREDIT>" + ficheDetail.Amount.ToString().Replace(",", ".") + "</CREDIT>"; //Alacak
                            xmlFormat += "<AMOUNT>" + ficheDetail.Amount.ToString().Replace(",", ".") + "</AMOUNT>"; //Alacak
                        }
                        if (ficheDetail.Sign == 0)
                        {
                            xmlFormat += "<DEBIT>" + ficheDetail.Amount.ToString().Replace(",", ".") + "</DEBIT>"; //Borç
                            xmlFormat += "<AMOUNT>" + ficheDetail.Amount.ToString().Replace(",", ".") + "</AMOUNT>"; //Borç
                        }
                        if (ficheDetail.Tc_XRate.HasValue)
                            xmlFormat += "<TC_XRATE>" + ficheDetail.Tc_XRate + "</TC_XRATE>";
                        if (ficheDetail.Tc_Amount.HasValue)
                            xmlFormat += "<TC_AMOUNT>" + ficheDetail.Tc_Amount.ToString().Replace(",", ".") + "</TC_AMOUNT>";
                        if (!string.IsNullOrEmpty(ficheDetail.Specode))
                            xmlFormat += "<AUXIL_CODE>" + ficheDetail.Specode + "</AUXIL_CODE>"; //Özel Kod
                        if (!string.IsNullOrEmpty(ficheDetail.ProjectCode))
                            xmlFormat += "<PROJECT_CODE>" + ficheDetail.ProjectCode + "</PROJECT_CODE>"; //Özel Kod
                        xmlFormat += "</TRANSACTION>";
                    }
                }
                xmlFormat += "</TRANSACTIONS></BANK_VOUCHER></BANK_VOUCHERS>";
                return StringCompressor.ZipBase64(xmlFormat);
            }
            else //Update ise;
            {
                int Type = Convert.ToInt32(DataType.doBankVoucher);
                string data = new LogoDataObject().XMLData_Read(Type, DataRef, ParamXML, FirmNr, SecurityCode).Replace("DBOP=\"INS\"", "DBOP=\"UPD\"");
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(data);

                if (bnfiche.Division.HasValue)
                    xml.GetElementsByTagName("DIVISION")[0].InnerXml = bnfiche.Division.ToString();
                if (bnfiche.Department.HasValue)
                    xml.GetElementsByTagName("DEPARTMENT")[0].InnerXml = bnfiche.Department.ToString();
                if (bnfiche.Date_.HasValue)
                    xml.GetElementsByTagName("DATE")[0].InnerXml = bnfiche.Date_.Value.ToString("dd.MM.yyyy");
                if (bnfiche.Type.HasValue)
                    xml.GetElementsByTagName("TYPE")[0].InnerXml = bnfiche.Type.ToString();
                if (bnfiche.Total_Debit.HasValue)
                    xml.GetElementsByTagName("TOTAL_DEBIT")[0].InnerXml = bnfiche.Total_Debit.ToString().Replace(",", ".");
                if (bnfiche.Total_Credit.HasValue)
                    xml.GetElementsByTagName("TOTAL_CREDIT")[0].InnerXml = bnfiche.Total_Credit.ToString().Replace(",", ".");
                if (!string.IsNullOrEmpty(bnfiche.Notes1))
                    xml.GetElementsByTagName("NOTES1")[0].InnerXml = bnfiche.Notes1.ToString();
                if (!string.IsNullOrEmpty(bnfiche.Notes2))
                    xml.GetElementsByTagName("NOTES2")[0].InnerXml = bnfiche.Notes2.ToString();
                if (!string.IsNullOrEmpty(bnfiche.Notes3))
                    xml.GetElementsByTagName("NOTES3")[0].InnerXml = bnfiche.Notes3.ToString();
                if (!string.IsNullOrEmpty(bnfiche.Notes4))
                    xml.GetElementsByTagName("NOTES4")[0].InnerXml = bnfiche.Notes4.ToString();
                if (!string.IsNullOrEmpty(bnfiche.Authcode))
                    xml.GetElementsByTagName("AUTH_CODE")[0].InnerXml = bnfiche.Authcode.ToString();
                if (!string.IsNullOrEmpty(bnfiche.Specode))
                    xml.GetElementsByTagName("AUXIL_CODE")[0].InnerXml = bnfiche.Specode.ToString();

                foreach (var detail in bnfiche.Detaylar)
                {
                    if (detail.LineNr.HasValue)
                    {
                        XmlNode LineNode = xml.SelectNodes("/BANK_VOUCHERS/BANK_VOUCHER/TRANSACTIONS/TRANSACTION")[detail.LineNr.Value - 1];
                        if (LineNode == null) //Eğer gönderilen sipariş satır numarasına ait satır yok ise hata dönecek..
                            throw new NotImplementedException("Banka Fiş XML'i oluşturulamadı : Girilen satır numarasına ait satır bulunmamaktadır");

                        if (!string.IsNullOrEmpty(detail.IBAN))
                            LineNode["IBAN"].InnerText = detail.IBAN;
                        if (!string.IsNullOrEmpty(detail.Arp_Code))
                            LineNode["ARP_CODE"].InnerText = detail.Arp_Code;
                        if (!string.IsNullOrEmpty(detail.Bankacc_Code))
                            LineNode["BANKACC_CODE"].InnerText = detail.Bankacc_Code;
                        if (!string.IsNullOrEmpty(detail.Doc_Number))
                            LineNode["DOC_NUMBER"].InnerText = detail.Doc_Number;
                        if (!string.IsNullOrEmpty(detail.Description))
                            LineNode["DESCRIPTION"].InnerText = detail.Description;
                        if (detail.Sign == 1)
                            LineNode["CREDIT"].InnerText = detail.Amount.ToString().Replace(",", ".");
                        if (detail.Sign == 0)
                            LineNode["DEBIT"].InnerText = detail.Amount.ToString().Replace(",", ".");
                        if (detail.Tc_XRate.HasValue)
                            LineNode["TC_XRATE"].InnerText = detail.Tc_XRate.ToString();
                        if (detail.Tc_Amount.HasValue)
                            LineNode["TC_AMOUNT"].InnerText = detail.Tc_Amount.ToString();
                        if (!string.IsNullOrEmpty(detail.Specode))
                            LineNode["AUXIL_CODE"].InnerText = detail.Specode;
                    }
                    else
                    {
                        string AddLine = "";
                        AddLine += "<TRANSACTION>";
                        if (detail.Type.HasValue)
                            AddLine += "<TYPE>" + detail.Type + "</TYPE>";
                        if (!string.IsNullOrEmpty(detail.IBAN))
                            AddLine += "<IBAN>" + detail.IBAN + "</IBAN>";
                        if (!string.IsNullOrEmpty(detail.Arp_Code))
                            AddLine += "<ARP_CODE>" + detail.Arp_Code + "</ARP_CODE>"; //Cari Hesap Kodu
                        if (!string.IsNullOrEmpty(detail.Bankacc_Code))
                            AddLine += "<BANKACC_CODE>" + detail.Bankacc_Code + "</BANKACC_CODE>"; //Banka Hesap Kodu

                        if (!string.IsNullOrEmpty(detail.Doc_Number))
                            AddLine += "<DOC_NUMBER>" + detail.Doc_Number + "</DOC_NUMBER>"; //Belge Numarası
                        if (!string.IsNullOrEmpty(detail.Description))
                            AddLine += "<DESCRIPTION>" + detail.Description + "</DESCRIPTION>"; //Satır Açıklaması
                        if (detail.Sign == 1)
                            AddLine += "<CREDIT>" + detail.Amount.ToString().Replace(",", ".") + "</CREDIT>"; //Alacak
                        if (detail.Sign == 0)
                            AddLine += "<DEBIT>" + detail.Amount.ToString().Replace(",", ".") + "</DEBIT>"; //Borç
                        if (detail.Tc_XRate.HasValue)
                            AddLine += "<TC_XRATE>" + detail.Tc_XRate + "</TC_XRATE>";
                        if (detail.Tc_Amount.HasValue)
                            AddLine += "<TC_AMOUNT>" + detail.Tc_Amount.ToString().Replace(",", ".") + "</TC_AMOUNT>";
                        if (!string.IsNullOrEmpty(detail.Specode))
                            AddLine += "<AUXIL_CODE>" + detail.Specode + "</AUXIL_CODE>"; //Özel Kod
                        AddLine += "</TRANSACTION>";
                        xml.InnerXml = xml.InnerXml.Insert(xml.InnerXml.IndexOf("</TRANSACTIONS>", 0), AddLine);
                    }
                }
                return StringCompressor.ZipBase64(xml.InnerXml);
            }
        }
























        internal static class StringCompressor
        {
            private static void CopyTo(Stream src, Stream dest)
            {
                byte[] bytes = new byte[4096];
                int cnt;
                while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
                {
                    dest.Write(bytes, 0, cnt);
                }
            }
            public static byte[] Zip(string str)
            {
                var bytes = Encoding.UTF8.GetBytes(str);
                using (var msi = new MemoryStream(bytes))
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(mso, CompressionMode.Compress))
                    {
                        CopyTo(msi, gs);
                    }
                    return mso.ToArray();
                }
            }
            public static string Unzip(byte[] bytes)
            {
                using (var msi = new MemoryStream(bytes))
                using (var mso = new MemoryStream())
                {
                    using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                    {
                        CopyTo(gs, mso);
                    }
                    return Encoding.UTF8.GetString(mso.ToArray());
                }
            }
            // Base64
            public static string ZipBase64(string compress)
            {
                var bytes = Zip(compress);
                var encoded = Convert.ToBase64String(bytes, Base64FormattingOptions.None);
                return encoded;
            }
            public static string UnzipBase64(string compressRequest)
            {
                var bytes = Convert.FromBase64String(compressRequest);
                var unziped = Unzip(bytes);
                return unziped;
            }
        }
    }

    public enum DataType
        {
            doMaterial = 0, //Malzeme
            doMaterialSlip = 1, //Malzeme Fişi
            doPurchService = 2, //Alınan Hizmet Kartı
            doSalesOrderSlip = 3, //Satış Sipariş
            doPurchOrderSlip = 4, //Alım Sipariş
            doPurchDisc = 5, //Alım İndirim Kartı
            doPurchExpn = 6, //Alım Masraf Kartı
            doSalesDisc = 7, //Satış İndirim Kartı
            doSalesExpn = 8, //Satış Masraf Kartı
            doPurchProm = 9, //Alım Promosyon
            doSalesProm = 10, //Satış Promosyon
            doPurchPriceItem = 11, //Mal Alım Fiyat Kartı
            doPurchPriceService = 12, //Hizmet ALım Fiyat Kartı
            doSalesPriceItem = 13, //Malzeme Satış Fiyatı
            doSalesPriceService = 14, //Hizmet Satış Fiyatı
            doSalesmanCl = 15, //Satıcı-Cari Bağlantısı
            doPurchDispatch = 16, //Alım İrsaliye
            doSalesDispatch = 17, //Satış İrsaliye
            doPurchInvoice = 18, //Alım Fatura
            doSalesInvoice = 19, //Satış Fatura
            doTransCqPn = 20, //Çek/Senet Devir
            doCQPnRoll = 21, //Çek/Senet Bordorsu
            doBank = 22, //Banka Kartı
            doBankAccount = 23, //Banka Hesap Kartı
            doBankVoucher = 24, //Banka Fişi
            doGLAccount = 25, //Muhasebe Hesap Kartı
            doGLVoucher = 26, //Muhasebe Fişi
            doOverheadPoolAcc = 27, //Masraf Merkezi
            doSafeDeposit = 28, //Kasa Kartları
            doSafeDepositTrans = 29, //Kasa İşlemleri
            doAccountsRP = 30, //Cari Kart
            doARAPVoucher = 31, //Cari Fiş
            doPayPlan = 32, //Ödeme Planı
            doUnitSet = 33, //Birim Seti
            doArpShipLic = 34, //Cari Sevkiyat Adresleri
            doFARegistry = 35, //Sabit Kıymet Kaydı
            doItemLangExt = 36, //Malzeme Farklı Dilde Açıklamalar
            doARAPLangExt = 37, //Cari Kart Farklı Dil Açıklamaları
            doBankLangExt = 38, //Banka Kartı Farklı Dilde Açıklamalar
            doGLAcLangExt = 39, //Hesap Kartı Farklı Dil Açıklamalar
            doCustLangExt = 40, //Müşteri Yabancı Dil Açıklamaları
            doItemAlters = 41, //Alternatifler
            doItemBOM = 42, //Malzeme Reçetesi
            doSerialLot = 43, //Seri Lot Tablosu
            doItChCodes = 44, //Malzeme Özellikleri
            doWstChars = 45, //İş İstasyonu Özellikleri
            doWorkStat = 46, //İş İstasyonu
            doWstGroup = 47, //İş İstasyonu Grubu
            doEmployee = 48, //Çalışan
            doEmpGroup = 49, //Çalışan Grupları
            doWrStCost = 50, //İş İstasyonu Maliyetleri
            doEmplCost = 51, //Çalışan Maliyetleri
            doShifts = 52, //Vardiya
            doShiftAsg = 53, //Vardiya Ataması
            doBOM = 54, //Ürün Reçetesi
            doOperation = 55, //Operasyonlar
            doRouting = 56, //Rota Tanımı
            doPrdParams = 57, //Reçete Sabitleri
            doQCCSet = 58, //Kalite Kontrol Seti
            doDelCodes = 59, //Teslimat Kodu
            doGrpCodes = 60, //Grup Kodları
            doPosCodes = 61, //Satıcı Pozisyon Kodları
            doPPGCodes = 62, //Ödeme Planı Grup Kodu
            doSpeCodes = 63, //Özel Kodlar
            doCypCodes = 64, //Yetki Kodları
            doSalesmanCust = 65, //Satıcı-Cari Bağlantısı(Satış Yönetimi Flag’i Açıkken)
            doSlsRoute = 66, //Satıcı Rota Baglantısı
            doSlsTarget = 67, //Satıcı Hedef Bağlantısı
            doPrCampaign = 68, //Alım Kampanyası
            doSlCampaign = 69, //Satış Kampanya
            doDistVehicle = 70, // Dağıtım Aracı
            doDistRouting = 71, //Dağıtım Rotası
            doDistOrder = 72, //Dağıtım Emri
            doCountry = 73, //Ülke Bilgileri
            doCity = 74, //Şehir Bilgileri
            doPostCode = 75, //Posta Kodu Bilgisi
            doTown = 76, //İlçe Bilgileriı
            doDistrict = 77, //Semt
            doItemClsAsgn = 78, //Malzeme Sınıfı Ataması
            doStdCostPrd = 79, //Standart Maliyet Periodları
            doItmStdCosts = 80, //Malzeme Standart Maliyeti
            doWStdCosts = 81, //İş İstasyonu Standart Maliyeti
            doEmpStdCosts = 82, // Çalışan Standart Maliyeti
            doStdBOMCosts = 83, //Standart Reçete Maliyeti
            doExceptns = 84, //İş istasyonu İstisnaları
            doSalesService = 85, //Verilen Hizmet Kartı
            doAddTax = 86, //Ek Vergi Kartı
            doPrdLine = 87, //Ürün Hattı
            doDemindPegging = 88, //Talep Karşılama
            doDateDiffInvoice = 89, //Fiyat Farkı Faturası
            doProject = 90, //Proje Kartı
            doRePayPlan = 91, //Geri Ödeme Planları
            doDistTemp = 92, //Dağıtım Şablonları
            doLocCodes = 93, //Stok Yeri Kodları
            doSalesConditionLine = 94, //Satış Koşulları(Fiş Satırları)
            doSalesConditionGn = 95, //Satış Koşulları(Fiş Geneli)
            doPurchConditionLine = 96, //Alış Koşulları(Fiş Satırları)
            doPurchConditionGn = 97, //Alış Koşulları(Fiş Geneli)
            doDemand = 98, //Talep
            doEximCredit = 99, //İhracat Kredisi(Döviz/Exim)
            doFreeZone = 100, //Serbest Bölge Tanımı
            doCustom = 101, //Gümrük Tanımı
            doImportOpFiche = 102, //İthalat Operasyon Fişi
            doExportOpFiche = 103, //İhracat Operasyon Fişi
            doExTypedPurchInv = 104, //İhracat / İhraç Kayıtlı Alım Faturaları
            doExTypedSalesInv = 105, //İhracat / İhraç Kayıtlı Satış Faturaları
            doDIIB = 106, //İhracat / Dahilde İşleme İzin Belgesi
            doMovementFiche = 107, //İthalat / Malzeme Dolaşım Fişi
            doNationalizeFiche = 108, //İthalat / Millileştirme Fişleri
            doEximDist = 109, //İthalat / Dağıtım Fişleri
            doMark = 110, //Marka Tanımları
            doDefField = 111, //Tanımlı Alanlar
            doDefFieldDef = 112, //Tanımlı Alan Tanımları
            doMandField = 113, //Zorunlu Alanlar
            doCategList = 114, //Tanımlı Alan Kategori Listeleri
            doRoleDef = 115, //İş Akış Yönetimi  / AnaKayıtlar / İş Akış Rol Tanımları
            doWFlowCard = 116, // İş Akış Yönetimi  / AnaKayıtlar / İş Akış Kartları
            doOccupation = 117, // Planlanan/Gerçekleşen Kaynak Kullanımı Girişi
            doGrpArp = 118, //Grup Şirketi
            doCollatrlRoll = 119, //Teminat Bordroları
            doPurchOfOrder = 120, //Satınalma Teklif Yönetimi – Emir
            doPurchOffer = 121, // Satınalma Teklif Yönetimi – Teklif
            doPurchOfCont = 122, //Satınalma Teklif Yönetimi – Sözleşme
            doQProduction = 123, //Hızlı Üretim Fişi
            doCustomer = 124, //Müşteriler(Teklif Yönetimi Kategorileri)
            doSaleOfCateg = 125, //Satış Kategorileri
            doContact = 126, //İlgili Kişiler(Teklif Yönetimi Kategorileri)
            doBankCredit = 127, //Banka Kredileri
            doCostDistFiche = 128, //Maliyet Dağıtım Fişi
            doCharSet = 129, //Malzeme Özellik Seti
            doVariant = 130, //Malzeme Variantları
            doEntegCode = 131, //Muhasebe Bağlantı Kodları
            doEngChange = 132, //Mühendislik değişiklikleri
            doQCCAsgn = 133, // Malzeme Kartı Kalite Kontrol Kriteri atamaları
            doFAZFiche = 134, //Zimmet formu(Sabit Kıymet İşlemleri)
            doSalesOffer = 135, //Satış Teklif Formu
            doSalesCont = 136, //Satış Sözleşmesi
            doSalesProvDistFc = 137, // Satış Provizyon Dağıtım Fişleri
            doStopCause = 138, //Durma nedenleri
            doSalesOpportunity = 139, //Satış Fırsatları
            doSalesActivity = 140, //Satış Faaliyetleri
        } //Veri tipleri

        public enum ActionType //Yapılacak işlem
        {
            Insert = 1,
            Update = 2
        }



    

}

