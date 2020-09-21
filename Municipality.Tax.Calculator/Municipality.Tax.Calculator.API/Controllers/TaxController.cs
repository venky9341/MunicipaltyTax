using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Municipality.Tax.Calculator.Repository;
using Municipality.Tax.Calculator.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Municipality.Tax.Calculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxScheduleRepository<TaxSchedule> _taxScheduleRepository;
        private readonly IGenericRepository<MunicipaltyTax> _municipaltyTaxRepository;

        public TaxController(IGenericRepository<MunicipaltyTax> municipaltyTaxRepository, ITaxScheduleRepository<TaxSchedule> taxScheduleRepository)
        {
            _taxScheduleRepository = taxScheduleRepository;
            _municipaltyTaxRepository = municipaltyTaxRepository;
        }

        public async Task<IActionResult> InsertSchedule(TaxSchedule newSchedule)
        {
            return Ok(await _taxScheduleRepository.Insert(newSchedule));
        }

        public async Task<IActionResult> UpdateSchedule(TaxSchedule newSchedule)
        {
            return Ok(await _taxScheduleRepository.Update(newSchedule));
        }

        public async Task<IActionResult> InsertTax(MunicipaltyTax municipaltyTax)
        {
            return Ok(await _municipaltyTaxRepository.Insert(municipaltyTax));
        }

        public async Task<IActionResult> GetMunicipaltyTax(int municipaltyId, DateTime date)
        {
            var dailySchedules = await _taxScheduleRepository.GetById(municipaltyId, (int)TaxTypes.daily);
            var monthlySchedules = await _taxScheduleRepository.GetById(municipaltyId, (int)TaxTypes.monthly);
            var yearlySchedules = await _taxScheduleRepository.GetById(municipaltyId, (int)TaxTypes.yearly);

            if (dailySchedules.Any(x => x.ToDate == date))
            {
                return Ok(await _municipaltyTaxRepository.GetById(municipaltyId, (int)TaxTypes.daily));
            }
            else if (monthlySchedules.Any(x => x.FromDate >= date && x.ToDate <= date))
            {
                return Ok(await _municipaltyTaxRepository.GetById(municipaltyId, (int)TaxTypes.monthly));
            }
            else if (yearlySchedules.Any(x => x.FromDate >= date && x.ToDate <= date))
            {
                return Ok(await _municipaltyTaxRepository.GetById(municipaltyId, (int)TaxTypes.yearly));
            }

            return BadRequest();
        }
    }
}