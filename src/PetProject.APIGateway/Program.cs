using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional : false, reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();
await app.UseOcelot();


//var builder = new WebHostBuilder()
//            .UseKestrel()
//            .UseContentRoot(Directory.GetCurrentDirectory())
//            .ConfigureAppConfiguration((hostingContext, config) =>
//            {
//                config
//                    .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
//                    .AddJsonFile("appsettings.json", true, true)
//                    .AddJsonFile($"appsettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
//                    .AddJsonFile("ocelot.json")
//                    .AddEnvironmentVariables();
//            })
//            .ConfigureServices(s =>
//            {
//                s.AddOcelot();
//            })
//            .ConfigureLogging((hostingContext, logging) =>
//            {
//                //add your logging
//            })
//            .UseIISIntegration()
//            .Configure(app =>
//            {
//                app.UseOcelot().Wait();
//            });

//var app = builder.Build();

app.Run();

