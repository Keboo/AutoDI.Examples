using System;
using AutoDI;
using Autofac.Extensions.DependencyInjection;
using ExampleLib;
using Microsoft.Extensions.DependencyInjection;

namespace Autofac.Console.Core
{
    class Program
    {
        static void Main(string[] args)
        {
            //Get the provider from the entry assembly
            //Alternatively you can use GlobalDI.GetService<IServiceProvider>(new object[0]);
            IServiceProvider provider = DI.GetGlobalServiceProvider(typeof(Program).Assembly);

            //Do stuff with nested scopes - if you want to
            IServiceScopeFactory scopeFactory = ServiceProviderMixins.GetService<IServiceScopeFactory>(provider);
            using (IServiceScope scope1 = scopeFactory.CreateScope())
            using (IServiceScope scope2 = scopeFactory.CreateScope())
            {
                IService service1 = scope1.ServiceProvider.GetService<IService>();
                IService service2 = scope2.ServiceProvider.GetService<IService>();
                System.Console.WriteLine($"Are scoped instances different? {(ReferenceEquals(service1, service2) ? "no" : "yes")}");
            }

            //Create program instance and start running
            Program program = ServiceProviderMixins.GetService<Program>(provider);
            program.DoStuff();
        }

        private readonly IService _service;

        public Program(IService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public void DoStuff()
        {
            System.Console.WriteLine($"Started with service? {(_service != null ? "yes" : "no")}");
            System.Console.ReadLine();
        }

        [SetupMethod]
        public static void SetupDI(IApplicationBuilder builder)
        {
            builder.ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                //Do any specific registration on the ContainerBuilder
            });
            builder.ConfigureServices(services =>
            {
                //Add in Autofac using Autofac.Extensions.DependencyInjection
                services.AddAutofac();
                //Configure any additional services
            });
        }
    }

}
