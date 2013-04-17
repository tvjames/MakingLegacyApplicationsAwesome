namespace MLAA.Core
{
    public interface IEventBroker
    {
        void Raise<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent;
    }
}