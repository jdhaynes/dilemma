using System;

namespace DilemmaSvc.Domain.Model.Dilemma
{
    public class Poster
    {
        public Guid Id { get; }
        public int Age { get; set; }
        public Location Location { get; }
        public string Gender { get; }
    }
}