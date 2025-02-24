//using System.Reflection;
//using CleanArchitectureExample.Application.Features.Commands;
//using CleanArchitectureExample.Infrastructure;
//using static CleanArchitectureExample.Application.Features.Commands.ProductCommand;

//var builder = WebApplication.CreateBuilder(args);

////builder.AddServiceDefaults();

//// Add services to the container.
//// Đăng ký các tầng vào DI
//builder.Services.AddInfrastructureServices(builder.Configuration);
//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));



//builder.Services.AddControllers();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

////app.MapDefaultEndpoints();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

//app.MapControllers();

//app.Run();


using CleanArchitectureExample.API;

public class Program
{
    public static void Main(string[] args)
    {
        //var builder = WebApplication.CreateBuilder(args);

        //// Rest of your Startup.cs code goes here...

        //builder.Build().Run();
        CreateHostBuilder(args).Build().Run();
        //var builder = WebApplication.CreateBuilder(args);
        //var app = builder.Build();

        //app.MapIdentityApi<IdentityUser>();

    }


    //EF Core uses this method at design time to access the DbContext
    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<StartUp>());



}