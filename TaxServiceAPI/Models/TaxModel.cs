namespace TaxServiceAPI.Models
{
    public class TaxModel
    {
        public Tax tax { get; set; }
    }

    public class Tax
    {
        public decimal amount_to_collect { get; set; }
        public bool freight_taxable { get; set; }
        public bool has_nexus { get; set; }
        public Jurisdictions jurisdictions { get; set; }
        public decimal order_total_amount { get; set; }
        public decimal rate { get; set; }
        public decimal shipping { get; set; }
        public string tax_source { get; set; }
        public decimal taxable_amount { get; set; }
    }

    public class Jurisdictions
    {
        public string city { get; set; }
        public string country { get; set; }
        public string county { get; set; }
        public string state { get; set; }
    }

}
