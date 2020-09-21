using Microsoft.EntityFrameworkCore;
using Municipality.Tax.Calculator.Models;
using System;

namespace Municipality.Tax.Calculator.API
{
    public class MunicipaltyTaxCalculatorDBContext : DbContext
    {
        public DbSet<TaxSchedule> TaxSchedules { get; set; }
        public DbSet<MunicipaltyTax> MunicipalTaxes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipalty>().HasData(new Municipalty() { Name = "Copenhagen" });

            modelBuilder.Entity<TaxType>().HasData(new TaxType() { Type = "daily" }, new TaxType() { Type = "monthly" }, new TaxType() { Type = "yearly" });

            modelBuilder.Entity<TaxSchedule>()
                .HasData(
                    new TaxSchedule() { MunicipaltyId = 1, TaxTypeId = 1, FromDate = DateTime.Parse(""), ToDate = DateTime.Parse("2016/01/01") },
                    new TaxSchedule() { MunicipaltyId = 1, TaxTypeId = 1, FromDate = DateTime.Parse(""), ToDate = DateTime.Parse("2016/12/25") },
                    new TaxSchedule() { MunicipaltyId = 1, TaxTypeId = 2, FromDate = DateTime.Parse("2016/05/01"), ToDate = DateTime.Parse("2016/05/31") },
                    new TaxSchedule() { MunicipaltyId = 1, TaxTypeId = 3, FromDate = DateTime.Parse("2016/01/01"), ToDate = DateTime.Parse("2016/12/31") }
                );

            modelBuilder.Entity<MunicipaltyTax>()
                .HasData(
                    new MunicipaltyTax() { MunicipaltyId = 1, TaxTypeId = 1, TaxRate = 0.1M },
                    new MunicipaltyTax() { MunicipaltyId = 1, TaxTypeId = 2, TaxRate = 0.4M },
                    new MunicipaltyTax() { MunicipaltyId = 1, TaxTypeId = 3, TaxRate = 0.6M }
                );
        }
    }
}
