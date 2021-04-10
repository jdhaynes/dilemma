namespace DilemmaSvc.Domain.Model.Dilemma
{
    public class Location
    {
        public string Country { get; }
        
        public Location(string country)
        {
            Country = country;
        }
    }
}