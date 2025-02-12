namespace CQRS.Resolution
{
    public static class DependencyRegistrationBoostrapper
    {
        public delegate IService1 ReminderServiceResolver(string identifier);
        public static void RegisterDependencies(this IServiceCollection services)
        {
            // -----------------------------------------------------------------------------------
            // Register all implementations as given below
            // -----------------------------------------------------------------------------------
            services.AddTransient<Service11>();
            services.AddTransient<Service12>();
            services.AddTransient<Service21>();
            services.AddTransient<Service22>();
            // -----------------------------------------------------------------------------------
            // Resolve the implementation via GetService as shown below
            // NOTE: If you have enabled NRT, then you may have to adjust return type of the resolver
            // -----------------------------------------------------------------------------------
            services.AddTransient<ReminderServiceResolver>(serviceProvider => token =>
            {
                // hardcoded strings can be extracted as constants
                return token switch
                {
                    "service1" => serviceProvider.GetService<Service11>()!,
                    "service2" => serviceProvider.GetService<Service12>()!,
                    
                    _ => serviceProvider.GetService<Service11>()!
                };
            });
            services.AddTransient<Func<string, IService2>>(serviceProvider => key =>
            {
                switch (key)
                {
                    case "service21":
                        return serviceProvider.GetService<Service21>()!;
                    case "service22":
                        return serviceProvider.GetService<Service22>()!;
                    default:
                        return serviceProvider.GetService<Service21>()!;
                }
            });
        }
    }
}
