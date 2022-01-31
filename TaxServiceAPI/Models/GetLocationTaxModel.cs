namespace TaxServiceAPI.Models
{
    public class GetLocationTaxModel
    {
        public string Country { get; set; }
        public string Zip { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public GetLocationTaxModel() {}

        public GetLocationTaxModel(string country, string zip, string state, string city, string street)
        {
            Country = country;
            Zip = zip;
            State = state;
            City = city;
            Street = street;
        }
    }
}
