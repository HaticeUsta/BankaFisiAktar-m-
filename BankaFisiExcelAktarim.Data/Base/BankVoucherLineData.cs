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
    public class BankVoucherLineData
    {
        private EfContext efContext;
        public BankVoucherLineData()
        {
            efContext = new EfContext();
        }
        public int Add(BankVoucherLine entity)
        {
            try
            {
                efContext.bankvoucherline.Add(entity);
                efContext.SaveChanges();
                return entity.ID;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("BankVoucherLine tablosuna kayıt eklenemedi : " + ex.Message);
            }
        }

        public bool Edit(BankVoucherLine entity)
        {
            try
            {
                BankVoucherLine toEdit = Find(x => x.ID == entity.ID).FirstOrDefault();

                if (toEdit == null)
                    return false;

                toEdit.BANKVOUCHERID = entity.BANKVOUCHERID;
                toEdit.NO = entity.NO;
                toEdit.DEFINITION_ = entity.DEFINITION_;
                toEdit.OHP_CODE1 = entity.OHP_CODE1;
                toEdit.BANKACC_CODE = entity.BANKACC_CODE;
                toEdit.ARP_CODE = entity.ARP_CODE;
                toEdit.ACCOUNT_NO = entity.ACCOUNT_NO;
                toEdit.AMOUNT = entity.AMOUNT; 
                toEdit.DESCRIPTION1 = entity.DESCRIPTION1;
                toEdit.DESCRIPTION2 = entity.DESCRIPTION2;
         
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
                BankVoucherLine toDelete = Find(x => x.ID == ID).FirstOrDefault();

                if (toDelete == null)
                    return false;

                efContext.bankvoucherline.Remove(toDelete);
                efContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw new NotImplementedException("Kayıt silinemedi : " + ex.Message);
            }
        }

        public IEnumerable<BankVoucherLine> GetAll()
        {
            return efContext.bankvoucherline.ToList().OrderBy(x => x.ID);
        }

        public IEnumerable<BankVoucherLine> Find(Expression<System.Func<BankVoucherLine, bool>> predicate)
        {
            return efContext.bankvoucherline.Where(predicate);
        }
    }
}