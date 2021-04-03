using System;

namespace DilemmaSvc.Application.Queries.GetDilemma
{
    public class OptionDto
    {
        public Guid OptionId { get; set; }
        public string Description { get; set; }
        public Guid ImageObjectId { get; set; }
    }
}