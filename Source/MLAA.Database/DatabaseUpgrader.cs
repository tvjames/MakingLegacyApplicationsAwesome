using System.Reflection;
using DbUp;

namespace MLAA.Database
{
    public static class DatabaseUpgrader
    {
        public static void UpgradeTheWorld(string connectionString)
        {
            var upgrader = DeployChanges.To
                                        .SqlDatabase(connectionString)
                                        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                                        .LogToTrace()
                                        .Build();

            var result = upgrader.PerformUpgrade();
            if (!result.Successful) throw result.Error;
        }
    }
}