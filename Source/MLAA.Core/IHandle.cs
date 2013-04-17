namespace MLAA.Core
{
    public interface IHandle<TDomainEvent> where TDomainEvent: IDomainEvent
    {
        void Handle(TDomainEvent domainEvent);
    }
}