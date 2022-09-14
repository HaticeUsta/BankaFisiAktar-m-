using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaFisiExcelAktarim
{
    public class ResponseLogo
    {
        public int Logicalref { get; set; }
        public string No { get; set; }
        public bool Status { get; set; }
        public Int16 ErrorType { get; set; }
        public string Message { get; set; }

    }
}
