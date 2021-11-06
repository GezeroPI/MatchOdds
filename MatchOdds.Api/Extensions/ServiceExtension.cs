using MatchOdds.Data.DataProviders;
using MatchOdds.Data.DataProviders.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchOdds.Api.Extensions
{
    /// <summary>
    /// Service Extension Class
    /// </summary>
    public class ServiceExtension
    {
        private readonly IServiceCollection _services;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceExtension"/> class.
        /// </summary>
        /// <param name="services">The services.</param>
        public ServiceExtension(IServiceCollection services)
        {
            _services = services;
        }
        /// <summary>
        /// Registers data providers.
        /// </summary>
        public void RegisterProviders()
        {
            _services.AddScoped<IMatchDataProvider, MatchDataProvider>();
        }
    }
}
