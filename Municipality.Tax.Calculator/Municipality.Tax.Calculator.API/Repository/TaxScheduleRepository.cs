using Microsoft.EntityFrameworkCore;
using Municipality.Tax.Calculator.API;
using Municipality.Tax.Calculator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Tax.Calculator.Repository
{
    public class TaxScheduleRepository<T> : ITaxScheduleRepository<T> where T : TaxSchedule
    {
        private MunicipaltyTaxCalculatorDBContext _context;

        public TaxScheduleRepository(MunicipaltyTaxCalculatorDBContext context)
        {
            _context = context;
        }

        public async Task<List<T>> GetById(int municipaltyId, int taxTypeId)
        {
            return await _context.Set<T>().Where(x => x.MunicipaltyId == municipaltyId && x.TaxTypeId == taxTypeId).ToListAsync();
        }

        public async Task<bool> Insert(T newSchedule)
        {
            await _context.TaxSchedules.AddAsync(newSchedule);

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<bool> Update(T newSchedule)
        {
            var schedules = await GetById(newSchedule.MunicipaltyId, newSchedule.TaxTypeId);

            foreach (var schedule in schedules)
            {
                schedule.FromDate = newSchedule.FromDate;
                schedule.ToDate = newSchedule.ToDate;
            }

            var rowsAffected = await _context.SaveChangesAsync();

            return rowsAffected > 0;
        }
    }

    public interface ITaxScheduleRepository<T> : IGenericRepository<T> where T : class
    {
        Task<bool> Update(T newSchedule);
    }
}
