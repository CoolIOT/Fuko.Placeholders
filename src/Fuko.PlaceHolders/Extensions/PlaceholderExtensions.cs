using Fuko.PlaceHolders.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Fuko.PlaceHolders.Extensions
{
    public static class PlaceholderExtensions
    {

        public static IServiceCollection AddFukoPlaceholders(this IServiceCollection services)
        {
            services.AddSingleton<IPlaceholders>(sp => {
                /*
                * Load Configuration
                */
                var options = new PlaceHolderOptions();
                var configuration = sp.GetService<IConfiguration>();
                var p = configuration.GetSection("PlaceHolders");
                
                // check if config has section
                if (p.Exists())
                {
                    p.Bind(options);
                }
                
                return new PlaceHolders(options);
            });

            return services;
        }
        
    }
}