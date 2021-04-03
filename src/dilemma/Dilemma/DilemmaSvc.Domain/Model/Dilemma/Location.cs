namespace Dilemma.Domain
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