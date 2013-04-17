using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MLAA.Web.Infrastructure;

namespace MLAA.Web.Installers
{
    public class InfrastructureInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromAssemblyContaining<EmailSender>()
                       .Where(Component.IsInSameNamespaceAs<EmailSender>())
                       .WithServiceDefaultInterfaces()
                       .LifestyleTransient());
        }
    }
}