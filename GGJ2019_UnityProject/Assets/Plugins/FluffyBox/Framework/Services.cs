using System;
using System.Collections.Generic;

namespace FluffyBox
{
    public static class Services
    {
        private static Dictionary<Type, IService> services;

        static Services()
        {
            Services.services = new Dictionary<Type, IService>();
        }

        public static void AddService(Type type, IService service)
        {
            Services.services.Add(type, service);
        }

        public static void AddService<T>(T service) where T : IService
        {
            Services.services.Add(typeof(T), service);
        }

        public static IService GetService(Type type)
        {
            IService service = null;
            if (true == Services.services.TryGetValue(type, out service))
            {
                return service;
            }

            return null;
        }

        public static T GetService<T>() where T : class, IService
        {
            IService service = null;
            if (true == Services.services.TryGetValue(typeof(T), out service))
            {
                return service as T;
            }

            return default(T);
        }

        public static void ClearServices()
        {
            Services.services.Clear();
        }
    }
}