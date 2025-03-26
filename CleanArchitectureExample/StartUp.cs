using CleanArchitectureExample.Application;
using CleanArchitectureExample.Infrastructure;
using CleanArchitectureExample.Infrastructure.Persistence.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Data;
using System.Text.Json.Serialization;
using static CleanArchitectureExample.Application.Features.Commands.ProductCommand;

namespace CleanArchitectureExample.API
{
    public class StartUp
    {
        public IConfiguration Configuration { get; }
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Add Infrastructure
            services.AddInfrastructureServices(Configuration);
            services.AddApplication();



            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));

            services.AddHttpContextAccessor();
            services.AddControllers();

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                //options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                //options.JsonSerializerOptions.MaxDepth = 5; // Tùy chỉnh độ sâu tối đa
            });
            


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);
            });

        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production.
                app.UseHsts();
            }

            SeedData.Initialize(services);

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();




            //app.UseCors(MyAllowSpecificOrigins);


            app.UseEndpoints(endpoints =>
            {

                //endpoints.MapIdentityApi<IdentityUser>();
                endpoints.MapControllers();
                //endpoints.MapHub<EmployeeHub>("/employeeHub");
            });

            CreateRoles(services).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            //IdentityResult adminRoleResult; 
            var roles = RoleManager.Roles;
            List<IdentityRole> roleLists = roles.ToList<IdentityRole>();
            if (roleLists.Count == 0)
            {
                await RoleManager.CreateAsync(new IdentityRole("Admin"));
                await RoleManager.CreateAsync(new IdentityRole("Manager"));
                await RoleManager.CreateAsync(new IdentityRole("Staff"));
                await RoleManager.CreateAsync(new IdentityRole("Unknows"));
            }

            IdentityUser? identityUser = await UserManager.FindByEmailAsync("caffegg998@gmail.com");

            if (identityUser != null)
            {
                bool adminRoleExists = await RoleManager.RoleExistsAsync("Admin");
                if (adminRoleExists)
                {
                    //IdentityUser userToMakeAdmin = await UserManager.FindByEmailAsync("pcb-pro87@local.canon-vn.com.vn");
                    await UserManager.AddToRoleAsync(identityUser, "Admin");
                }
            }
        }
    }

}
