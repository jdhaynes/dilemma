using System;
using Common.Domain;

namespace DilemmaSvc.Domain.Model.OptionImage
{
    public class OptionImage : Entity
    {
        public Guid Id { get; }
        public byte[] ImageData { get; set; }
    }
}