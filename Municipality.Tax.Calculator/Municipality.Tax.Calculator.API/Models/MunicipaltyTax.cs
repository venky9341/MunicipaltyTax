using System.ComponentModel.DataAnnotations.Schema;

namespace Municipality.Tax.Calculator.Models
{
    public class MunicipaltyTax
    {
        public int MunicipaltyId { get; set; }
        public int TaxTypeId { get; set; }
        public decimal TaxRate { get; set; }
        
        [ForeignKey("MunicipaltyId")]
        public Municipalty Municipalty { get; set; }

        [ForeignKey("TaxTypeId")]
        public TaxType TaxType { get; set; }
    }
}
