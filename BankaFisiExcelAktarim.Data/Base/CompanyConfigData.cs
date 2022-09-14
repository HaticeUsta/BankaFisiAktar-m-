using BankaFisiExcelAktarim.Data.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankaFisiExcelAktarim.Data.Base
{
    public class CompanyConfigData
    {
        private EfContext efContext;
        public CompanyConfigData()
        {
            efContext = new EfContext();
        }
        public IEnumerable<CompanyConfig> Find(Expression<System.Func<CompanyConfig, bool>> predicate)
        {
            return efContext.companyconfig.Where(predicate);
        }
        public IEnumerable<CompanyConfig> GetAll()
        {
            return efContext.companyconfig.ToList();
        }

    }
}
