using Castle.Windsor;
using Castle.Windsor.Installer;
using MLAA.Web.Installers;
using NUnit.Framework;

namespace MLAA.UnitTests
{
    public class WindsorDemystification
    {
        [Test]
        public void ThereShouldBeNoComponentsWithBorkedRegistrations()
        {
            var container = new WindsorContainer();
            container.Install(
                FromAssembly
                    .Containing<ViewModelsInstaller>());
        }
    }
}