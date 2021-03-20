using System;

namespace Dilemma.Domain
{
    public class Poster
    {
        public Guid Id { get; }
        public int Age { get; set; }
        public string Location { get; }
        public string Gender { get; }
    }
}