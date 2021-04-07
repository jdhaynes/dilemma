using System;

namespace DilemmaSvc.Application.Queries.GetTopics.DTOs
{
    public class Topic
    {
        public Guid TopicId { get; set; }
        public string Name { get; set; }
    }
}