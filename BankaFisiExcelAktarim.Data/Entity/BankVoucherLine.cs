using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaFisiExcelAktarim.Data.Entity
{
    public class BankVoucherLine
    {
        [Key]
        public int ID { get; set; }
        public int BANKVOUCHERID { get; set; }
        public int NO { get; set; }
        public string DEFINITION_ { get; set; }
        public string OHP_CODE1 { get; set; }
        public string BANKACC_CODE { get; set; }
        public string ARP_CODE { get; set; }
        public string ACCOUNT_NO { get; set; }
        public decimal? AMOUNT { get; set; }
        public string DESCRIPTION1 { get; set; }
        public string DESCRIPTION2 { get; set; }
        

    }
}
