var builder = DistributedApplication.CreateBuilder(args);

//var postgres = builder.AddPostgres("cleanarchitectureexample-db").WithPgAdmin();

builder.AddProject<Projects.CleanArchitectureExample_API>("cleanarchitectureexample-api").WithExternalHttpEndpoints();

builder.Build().Run();
