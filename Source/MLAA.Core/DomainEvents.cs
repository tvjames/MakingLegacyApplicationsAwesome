using System;

namespace MLAA.Core
{
    public static class DomainEvents
    {
        private static IEventBroker _eventBroker;

        public static void SetEventBrokerStrategy(IEventBroker eventBroker)
        {
            _eventBroker = eventBroker;
        }

        public static void Raise<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            var eventBroker = _eventBroker;
            if (eventBroker == null) throw new InvalidOperationException("You must provide an event broker before you can call this!");

            eventBroker.Raise(domainEvent);
        }
    }
}