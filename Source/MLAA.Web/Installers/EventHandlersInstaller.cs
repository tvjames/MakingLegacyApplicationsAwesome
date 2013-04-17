using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MLAA.Core;
using MLAA.Core.Domain.EventHandlers.WhenAStudentEnrolsInASubject;

namespace MLAA.Web.Installers
{
    public class EventHandlersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<SendThemAConfirmationEmail>()
                       .BasedOn(typeof (IHandle<>))
                       .WithServiceBase()
                       .LifestyleTransient());
        }
    }
}