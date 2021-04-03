using System;

namespace DilemmaSvc.Application.Queries
{
    public class DilemmaDto
    {
        public Guid Id { get; set; }
        public string Question { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime WithdrawnDate { get; set; }
    }
}