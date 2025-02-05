﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using NotesVaultApp.Data.Repository;
using NotesVaultApp.Data.Repository.Interface;

namespace NotesVaultApp.Web.Infrastucture.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services, Assembly modelsAssembly)
        {
            Type repositoryInterfaceType = typeof(IRepository<>);
            Type repositoryImplementationType = typeof(BaseRepository<>);

            Type[] modelTypes = modelsAssembly
                .GetTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface &&
                            !t.IsEnum && t.IsClass)
                .ToArray();

            foreach (Type modelType in modelTypes)
            {
                Type constructedInterface = repositoryInterfaceType.MakeGenericType(modelType);
                Type constructedImplementation = repositoryImplementationType.MakeGenericType(modelType);

                services.AddScoped(constructedInterface, constructedImplementation);
            }
        }
        public static void RegisterUserDefinedServices(this IServiceCollection services, Assembly serviceAssembly)
        {
            Type[] serviceInterfaceTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.IsInterface)
                .ToArray();
            Type[] serviceTypes = serviceAssembly
                .GetTypes()
                .Where(t => !t.IsInterface && !t.IsAbstract &&
                                t.Name.ToLower().EndsWith("service"))
                .ToArray();

            foreach (Type serviceInterfaceType in serviceInterfaceTypes)
            {
                Type? serviceType = serviceTypes
                    .SingleOrDefault(t => "i" + t.Name.ToLower() == serviceInterfaceType.Name.ToLower());
                if (serviceType == null)
                {
                    throw new NullReferenceException($"Service type could not be obtained for the service {serviceInterfaceType.Name}");
                }

                services.AddScoped(serviceInterfaceType, serviceType);
            }
        }
    }
}
