using System;
using Microsoft.Extensions.DependencyInjection;

namespace AperionQB.Application
{
	public static class DependencyInjection
	{
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services;
        }
    }
}

