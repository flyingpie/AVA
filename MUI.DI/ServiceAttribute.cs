using System;

namespace MUI.DI
{
    public class ServiceAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; private set; }

        public ServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            Lifetime = lifetime;
        }
    }
}