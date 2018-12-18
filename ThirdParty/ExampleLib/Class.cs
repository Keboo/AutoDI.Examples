using System;
using AutoDI;

[assembly:Settings(GenerateRegistrations = false)]

namespace ExampleLib
{
    public class Class
    {
        public IService Service { get; }

        public Class([Dependency]IService service = null)
        {
            Service = service ?? throw new ArgumentNullException(nameof(service));
        }
    }
}
