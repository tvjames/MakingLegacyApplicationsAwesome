using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MLAA.Web.Installers
{
    public class ViewModelsInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                      .Where(c => c.Name.EndsWith("ViewModel"))
                                      .LifestyleTransient());
        }
    }
}