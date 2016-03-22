using RxDemo.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RxDemo.API.DI;
using RxDemo.API.Interfaces;

namespace RxDemo
{
    public static class RxDemoEntryPoint
    {
        [STAThread]
        static int Main(string[] args)
        {
            var containerBuilder = new ContainerBuilder();

            var allAssemblies = new DirectoryInfo(Environment.CurrentDirectory).GetFiles("*.dll").Select(x => Assembly.LoadFile(x.FullName));
            var assemblies = allAssemblies.Where(x => x.GetTypes().Any(t => t.IsAssignableTo<RxDemoModule>())).ToArray();

            containerBuilder.RegisterAssemblyModules(assemblies);

            var container = containerBuilder.Build();


            new MainWindow(container.Resolve<IList<IDemo>>()).ShowDialog();
            return 0;
        }
    }
}
