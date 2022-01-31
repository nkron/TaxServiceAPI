namespace TaxServiceAPI.Models
{
    public class GetOrderTaxModel
    {
        public string from_country { get; set; }
        public string from_zip { get; set; }
        public string from_state { get; set; }
        public string from_city { get; set; }
        public string from_street { get; set; }
        public string to_country { get; set; }
        public string to_zip { get; set; }
        public string to_state { get; set; }
        public string to_city { get; set; }
        public string to_street { get; set; }
        public decimal amount { get; set; }
        public decimal shipping { get; set; }

        public GetOrderTaxModel() { }

        public GetOrderTaxModel(string fromCountry,
            string fromZip,
            string fromState,
            string fromCity,
            string fromStreet,
            string toCountry,
            string toZip,
            string toState,
            string toCity,
            string toStreet,
            decimal amount,
            decimal shipping)
        {
            from_country = fromCountry;
            from_zip = fromZip;
            from_state = fromState;
            from_city = fromCity;
            from_street = fromStreet;
            to_country = toCountry;
            to_zip = toZip;
            to_state = toState;
            to_city = toCity;
            to_street = toStreet;
            this.amount = amount;
            this.shipping = shipping;
        }
    }
}
