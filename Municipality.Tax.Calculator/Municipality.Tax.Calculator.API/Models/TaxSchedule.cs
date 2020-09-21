using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Municipality.Tax.Calculator.Models
{
    public class TaxSchedule
    {
        public int MunicipaltyId { get; set; }
        public int TaxTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        [ForeignKey("MunicipaltyId")]
        public Municipalty Municipalty { get; set; }

        [ForeignKey("TaxTypeId")]
        public TaxType TaxType { get; set; }
    }
}
