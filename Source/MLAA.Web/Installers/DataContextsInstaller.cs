using System.Configuration;
using System.Data.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MLAA.Data.Linq2Sql;

namespace MLAA.Web.Installers
{
    public class DataContextsInstaller: IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var connectionString = ConfigurationManager
                .ConnectionStrings["DerpUniversityConnectionString"]
                .ConnectionString;
            container.Register(
                Classes.FromAssemblyContaining<DerpUniversityDataContext>()
                       .BasedOn<DataContext>()
                       .LifestylePerWebRequest()
                       .Configure(x => x.DependsOn(Dependency
                           .OnValue<string>(connectionString)))
                );
        }
    }
}