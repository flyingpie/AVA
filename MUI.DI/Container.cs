using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MUI.DI
{
    public interface IContainer
    {
        IContainer Register<TInterface, TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TImplementation : TInterface;

        IContainer Register<TInterface, TImplementation>(Func<IContainer, TImplementation> factory, ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TImplementation : TInterface;

        IContainer Register(Type type, ServiceLifetime lifetime = ServiceLifetime.Singleton);

        IContainer Register(Type type, Func<IContainer, object> factory, ServiceLifetime lifetime = ServiceLifetime.Singleton);

        IContainer Register<TInterface, TImplementation>(TImplementation implementation)
            where TImplementation : TInterface;

        IContainer Register<TService>(TService service);

        object Resolve(Type type);

        T Resolve<T>();

        IEnumerable<object> ResolveAll(Type type);

        IEnumerable<T> ResolveAll<T>();

        void Wireup(object instance);
    }

    public class Container : IContainer
    {
        private Dictionary<Type, ServiceRegistration> _registrations;

        public Container()
        {
            _registrations = new Dictionary<Type, ServiceRegistration>();
        }

        public IContainer Register<TInterface, TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TImplementation : TInterface
        {
            return Register<TInterface, TImplementation>(c =>
            {
                Console.WriteLine($"Creating instance of type '{typeof(TInterface).FullName}', using implementation of type '{typeof(TImplementation).FullName}'...");

                return Activator.CreateInstance<TImplementation>();
            }, lifetime);
        }

        public IContainer Register<TInterface, TImplementation>(Func<IContainer, TImplementation> factory, ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TImplementation : TInterface
        {
            return Register(typeof(TInterface), c => factory(c), lifetime);
        }

        public IContainer Register<TInterface, TImplementation>(TImplementation implementation)
            where TImplementation : TInterface
        {
            return Register<TInterface, TImplementation>(c => implementation, ServiceLifetime.Singleton);
        }

        public IContainer Register(Type type, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            Console.WriteLine($"Registering service of type '{type.FullName}' with lifetime '{lifetime}'");

            return Register(type, c => Activator.CreateInstance(type), lifetime);
        }

        public IContainer Register(Type type, Func<IContainer, object> factory, ServiceLifetime lifetime = ServiceLifetime.Singleton)
        {
            _registrations.Add(type, new ServiceRegistration()
            {
                Type = type,
                Lifetime = lifetime,
                Resolver = c => factory(c)
            });

            return this;
        }

        public IContainer Register<TService>(TService service)
        {
            return Register<TService, TService>(service);
        }

        public object Resolve(Type type)
        {
            if (!_registrations.ContainsKey(type))
                throw new InvalidOperationException($"No service found of type '{type.FullName}'");

            return _registrations[type].Resolve(this);
        }

        public T Resolve<T>() => (T)Resolve(typeof(T));

        public IEnumerable<object> ResolveAll(Type type)
        {
            return _registrations
                .Where(r => type.IsAssignableFrom(r.Key))
                .Select(r => r.Value.Resolve(this))
                .ToList();
        }

        public IEnumerable<T> ResolveAll<T>() => ResolveAll(typeof(T)).Select(s => (T)s).ToArray();

        public void Wireup(object instance)
        {
            Console.WriteLine($"Wiring up instance of type '{instance}'...");

            // TODO: Test - Circular reference

            var members = instance.GetType()
                //.GetMembers(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .GetAllMembers()
                .Select(m => new
                {
                    Member = m,
                    DependencyAttribute = m.GetCustomAttribute<DependencyAttribute>(),
                    RunAfterInjectAttribute = m.GetCustomAttribute<RunAfterInjectAttribute>()
                })
                .OrderBy(m => m.Member.Name)
                .ToList();

            var mm = members.Where(m => m.Member.Name.Contains("_context")).ToList();

            // Inject dependencies
            foreach (var member in members.Where(m => m.DependencyAttribute != null))
            {
                if (member.Member.MemberType == MemberTypes.Field)
                {
                    var field = (FieldInfo)member.Member;
                    var fieldType = field.FieldType;

                    if (typeof(object[]).IsAssignableFrom(fieldType))
                    {
                        var serviceType = fieldType.GetElementType();
                        var services = ResolveAll(serviceType).ToArray();

                        var serviceArray = Array.CreateInstance(serviceType, services.Length);
                        services.CopyTo(serviceArray, 0);

                        field.SetValue(instance, serviceArray);
                    }
                    else
                    {
                        var dependency = Resolve(fieldType);

                        Console.WriteLine($"Injecting dependency of type '{dependency.GetType().FullName}' into instance field ('{instance.GetType().FullName}')'{field.Name}'");

                        field.SetValue(instance, dependency);
                    }

                    continue;
                }

                // TODO: properties
            }

            // Call post-injection methods
            foreach (var member in members.Where(m => m.RunAfterInjectAttribute != null))
            {
                if (member.Member.MemberType == MemberTypes.Method)
                {
                    var method = (MethodInfo)member.Member;

                    method.Invoke(instance, new object[0]);
                }
            }
        }
    }

    public static class SystemExtensions
    {
        public static IEnumerable<MemberInfo> GetAllMembers(this Type type)
        {
            foreach (var member in type.GetMembers(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                yield return member;

                //.Select(m => new
                //{
                //    Member = m,
                //    DependencyAttribute = m.GetCustomAttribute<DependencyAttribute>(),
                //    RunAfterInjectAttribute = m.GetCustomAttribute<RunAfterInjectAttribute>()
                //});
            }
            //.OrderBy(m => m.Member.Name)
            //.ToList();

            if(type.BaseType != null)
            {
                foreach(var member in type.BaseType.GetAllMembers())
                {
                    yield return member;
                }
            }
        }
    }

    public class ServiceRegistration
    {
        public Type Type { get; set; }

        public ServiceLifetime Lifetime { get; set; }

        public Func<Container, object> Resolver { get; set; }

        private object _instance;

        public object Resolve(Container container)
        {
            object instance = null;

            switch (Lifetime)
            {
                case ServiceLifetime.Singleton:
                    {
                        if (_instance == null)
                        {
                            _instance = Resolver(container);
                            container.Wireup(_instance);
                        }

                        instance = _instance;
                        break;
                    }
                case ServiceLifetime.Transient:
                    {
                        instance = Resolver(container);
                        container.Wireup(instance);

                        break;
                    }
            }

            if (instance == null)
            {
                throw new InvalidOperationException($"Resolver for type '{Type.FullName}' resulted in a null-reference");
            }

            return instance;
        }
    }
}