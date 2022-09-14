using BankaFisiExcelAktarim.Data.Base;
using BankaFisiExcelAktarim.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankaFisiExcelAktarim.Data.Engine
{
    public class EfContext : DbContext
    {
        public DbSet<BankVoucher> bankvoucher { get; set; }
        public DbSet<BankVoucherLine> bankvoucherline { get; set; }
        public DbSet<CompanyConfig> companyconfig { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BankVoucher>().ToTable("BankVoucher");
            modelBuilder.Entity<BankVoucherLine>().ToTable("BankVoucherLine");
            modelBuilder.Entity<CompanyConfig>().ToTable("CompanyConfig");
        }
    }
}
