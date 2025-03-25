using CleanArchitectureExample.Application.MapperProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Đăng ký AutoMapper và chỉ định assembly chứa các Profile
            services.AddAutoMapper(typeof(ApplicationServiceRegistration).Assembly);
            // Đăng ký các dịch vụ khác của Application (nếu có)
            // services.AddScoped<IMyService, MyService>();

            return services;
        }
    }
}
