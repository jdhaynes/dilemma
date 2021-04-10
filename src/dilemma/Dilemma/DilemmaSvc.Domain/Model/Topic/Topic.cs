﻿using System;

namespace DilemmaSvc.Domain.Model.Topic
{
    public class Topic
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public Topic(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
