using Microsoft.EntityFrameworkCore;
using Municipality.Tax.Calculator.API;
using Municipality.Tax.Calculator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Tax.Calculator.Repository
{
    public class MunicipaltyTaxRepository<T> : IGenericRepository<T> where T : MunicipaltyTax
    {
        private readonly MunicipaltyTaxCalculatorDBContext _context;

        public MunicipaltyTaxRepository(MunicipaltyTaxCalculatorDBContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetById(int municipaltyId, int taxTypeId)
        {
            return await _context.Set<T>().Where(x => x.MunicipaltyId == municipaltyId && x.TaxTypeId == taxTypeId).ToListAsync();
        }

        public async Task<bool> Insert(T municipaltyTax)
        {
            await _context.AddAsync(municipaltyTax);
            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }
    }
}
