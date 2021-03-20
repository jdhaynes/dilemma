using System;
using System.Collections.Generic;

namespace Common.Domain
{
    public class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        protected void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent == null)
            {
                throw new ArgumentNullException(nameof(domainEvent));
            }

            _domainEvents.Add(domainEvent);
        }

        protected void CheckRule(IDomainRule rule)
        {
            if (!rule.Valid())
            {
                throw new DomainRuleException(rule.ErrorCode);
            }
        }
    }
}