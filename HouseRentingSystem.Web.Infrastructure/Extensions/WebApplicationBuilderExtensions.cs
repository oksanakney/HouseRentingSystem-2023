namespace Microsoft.Extensions.DependencyInjection.Extensions
{
    using DependencyInjection;
    using HouseRentingSystem.Services.Data;
    using HouseRentingSystem.Services.Data.Interfaces;
    using System.Reflection;

    public static class WebApplicationBuilderExtensions
    {
        /// <summary>
        /// This method register all services with their interfaces and implementation of the Assembly
        /// The assembly istaken from the type of random service interface or implementation provided.
        /// </summary>
        /// <param name="serviceType"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static void AddApplicationServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            if(serviceAssembly == null) 
            {
                throw new InvalidOperationException("Invalid service type provided!");
            }

            Type[] implementationTypes = serviceAssembly
                .GetTypes()
                .Where(t => t.Name.EndsWith("Service") && !t.IsInterface)
                .ToArray();
            foreach(Type implementationType in implementationTypes) 
            {
                Type? interfaceType = implementationType
                    .GetInterface($"I{implementationType.Name}");
                if(interfaceType == null)
                {
                    throw new InvalidOperationException($"No interface is provided for he service with name: {implementationType.Name}");
                }

                services.AddScoped(interfaceType, implementationType);
            }
            services.AddScoped<IHouseService, HouseService>();
        }

    }
}
