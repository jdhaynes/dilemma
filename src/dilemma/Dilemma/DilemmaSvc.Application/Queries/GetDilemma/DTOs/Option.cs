using System;

namespace DilemmaSvc.Application.Queries.GetDilemma.DTOs
{
    public class Option
    {
        public Guid OptionId { get; set; }
        public string Description { get; set; }
        public string ImageObjectId { get; set; }
        public string ImageUrl { get; set; }
    }
}