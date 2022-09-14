using BankaFisiExcelAktarim.Data.Engine;
using BankaFisiExcelAktarim.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaFisiExcelAktarim.Data.Base
{
    public class LogoData
    {
        EfContext efContext = new EfContext();
        public List<LogoBnfLine> GetLogoBnfLineList(string DbName, string CompanyNo,string PeriodNo,string query)
        {
            CompanyNo = CompanyNo.ToString().PadLeft(3, '0');
            PeriodNo = PeriodNo.ToString().PadLeft(2, '0');
            string command = string.Format("SELECT LOGICALREF,SOURCEFREF from {0}.dbo.LG_{1}_{2}_BNFLINE {3}", DbName, CompanyNo, PeriodNo, query);
            return efContext.Database.SqlQuery<LogoBnfLine>(command).ToList();
        }
    }
}
