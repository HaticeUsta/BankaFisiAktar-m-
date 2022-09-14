using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaFisiExcelAktarim.Data.Base
{
   public class CompanyConfig
    {
        [Key]
        public int ConfigID { get; set; }
        public int CompanyID { get; set; }
        public string ServerIP { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string LogoDbName { get; set; }
        public int LogoCompanyID { get; set; }
        public int LogoCompanyPeriodID { get; set; }

    }
}
