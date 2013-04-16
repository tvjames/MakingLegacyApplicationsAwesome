using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace MLAA.Web.Installers
{
    public class EnrolmentManagerInstaller:IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<EnrolmentManager>().LifestylePerWebRequest()
                );
        }
    }
}