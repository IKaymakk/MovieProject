using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using MovieProject.Application.Behaviors;

namespace MovieProject.Application.Registrations;

public static class ApplicationRegistration
{
    public static void ApplicationRegistrationServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationRegistration).Assembly));
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}