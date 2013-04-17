using MLAA.Core;

namespace MLAA.Web
{
    public class WindsorEventBroker : IEventBroker  //FIXME this will move to web in a minute
    {
        public void Raise<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IDomainEvent
        {
            //FIXME this is icky. We shouldn't know about our container.
            var handlers = Global.Container.ResolveAll<IHandle<TDomainEvent>>();
            foreach (var handler in handlers)
            {

                try
                {
                    handler.Handle(domainEvent);
                }
                finally
                {
                    Global.Container.Release(handler);
                }
            }
        }
    }
}