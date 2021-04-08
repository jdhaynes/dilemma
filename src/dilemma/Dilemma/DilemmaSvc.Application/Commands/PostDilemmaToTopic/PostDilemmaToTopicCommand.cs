using System;
using System.Collections.Generic;

namespace DilemmaSvc.Application.Commands.PostDilemmaToTopic
{
    public class PostDilemmaToTopicCommand 
    {
        public Guid TopicId { get; set; }
        public Guid PosterId { get; set; }
        public string Question { get; set; }
        public List<Option> Options { get; set; }

        public class Option
        {
            public string Description { get; set; }
        }
    }
}