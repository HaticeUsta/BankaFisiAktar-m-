using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaFisiExcelAktarim.Data.Entity
{
    public class BankVoucher
    {
        [Key]
        public int ID { get; set; }
        public Int16 TYPE { get; set; }
        public string NUMBER { get; set; }                               
        public DateTime DATE { get; set; }
        public int LOGOREFID { get; set; }
        public bool LOGOSTATUS { get; set; }
        public string LOGOMESSAGE { get; set; }

    }
}
