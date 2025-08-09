var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Bookstore_Web_Api>("bookstore-web-api");

builder.Build().Run();