using System;
using DilemmaApp.Services.Common.Domain;

namespace DilemmaApp.Services.Dilemma.Domain.Model.OptionImage
{
    public class OptionImage : Entity
    {
        public Guid Id { get; }
        public byte[] ImageData { get; set; }
    }
}