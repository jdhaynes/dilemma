using System;
using System.Collections.Generic;

namespace DilemmaSvc.Application.Queries.GetDilemma.DTOs
{
    public class Dilemma
    {
        public Guid DilemmaId { get; set; }
        public string Question { get; set; }
        public DateTime PostedDate { get; set; }
        public Nullable<DateTime> WithdrawnDate { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}