using BankaFisiExcelAktarim.Data.Engine;
using BankaFisiExcelAktarim.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankaFisiExcelAktarim.Data.Base
{
    public class BankVoucherData
    {
        private EfContext efContext;
        public BankVoucherData()
        {
            efContext = new EfContext();
        }
        public int Add(BankVoucher entity)
        {
            try
            {
                efContext.bankvoucher.Add(entity);
                efContext.SaveChanges();
                return entity.ID;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("BankVoucher tablosuna kayıt eklenemedi : " + ex.Message);
            }
        }

        public bool Edit(BankVoucher entity)
        {
            try
            {
                BankVoucher toEdit = Find(x => x.ID == entity.ID).FirstOrDefault();

                if (toEdit == null)
                    return false;
                toEdit.TYPE = entity.TYPE;
                toEdit.NUMBER = entity.NUMBER;
                toEdit.DATE = entity.DATE;
                toEdit.LOGOREFID = entity.LOGOREFID;              
                toEdit.LOGOSTATUS = entity.LOGOSTATUS;
                toEdit.LOGOMESSAGE = entity.LOGOMESSAGE;
               
                efContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Kayıt güncellenemedi : " + ex.Message);
            }
        }

        public bool Delete(int ID)
        {
            try
            {
                BankVoucher toDelete = Find(x => x.ID == ID).FirstOrDefault();

                if (toDelete == null)
                    return false;

                efContext.bankvoucher.Remove(toDelete);
                efContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Kayıt silinemedi : " + ex.Message);
            }
        }

        public IEnumerable<BankVoucher> GetAll()
        {
            return efContext.bankvoucher.ToList().OrderBy(x => x.ID);
        }

        public IEnumerable<BankVoucher> Find(Expression<System.Func<BankVoucher, bool>> predicate)
        {
            return efContext.bankvoucher.Where(predicate);
        }
    }
}