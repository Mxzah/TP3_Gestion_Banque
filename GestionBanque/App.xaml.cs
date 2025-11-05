using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System.Windows;

namespace GestionBanque
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var config = new ConfigurationBuilder();
            // https://autofac.readthedocs.io/en/latest/configuration/xml.html
            config.AddJsonFile("di.json");

            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);

            FournisseurDI.Container = builder.Build();
        }
    }
}
