using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Student.DataLayer;
using Student.DataLayer.Repositories;

namespace StudentApi
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();
            return services;
        }
    }
}
