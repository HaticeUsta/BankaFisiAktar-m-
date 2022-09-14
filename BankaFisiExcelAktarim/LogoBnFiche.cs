using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaFisiExcelAktarim
{
    public class LogoBnFiche
    {
        public int? Division { get; set; }
        public int? Department { get; set; }
        public DateTime? Date_ { get; set; }
        public string Number { get; set; }
        public int? Type { get; set; }
        public byte? Accounted { get; set; }
        public decimal? Total_Debit { get; set; }
        public decimal? Total_Credit { get; set; }
        public string Notes1 { get; set; }
        public string Notes2 { get; set; }
        public string Notes3 { get; set; }
        public string Notes4 { get; set; }
        public string Authcode { get; set; }
        public string Specode { get; set; }
        public LogoBnfLine[] Detaylar { get; set; }
    }
    public class LogoBnfLine
    {
        public int? Type { get; set; }
        public string Ohp_Code1 { get; set; }
        public string Arp_Code { get; set; }
        public string Bankacc_Code { get; set; }
        public string Doc_Number { get; set; }
        public string Description { get; set; }
        public int? Sign { get; set; }
        public decimal Amount { get; set; }
        public decimal? Tc_XRate { get; set; }
        public decimal? Tc_Amount { get; set; }
        public string Specode { get; set; }
        public string ProjectCode { get; set; }
        public int? LineNr { get; set; }
        public string IBAN { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
    }
}
