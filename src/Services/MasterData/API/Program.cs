using Core.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("SQLConnection") ?? "";
builder.Services.AddControllers();
builder.Services.AddDbContext<MasterDataContext>(opt => opt.UseMySQL(connectionString));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1, 0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
    x.ApiVersionReader = ApiVersionReader.Combine(
        // new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("x-version")//,
                                               // new MediaTypeApiVersionReader("ver")
    );
});

builder.Services.AddVersionedApiExplorer(opt =>
{
    opt.GroupNameFormat = "'v'VVV";
    opt.SubstituteApiVersionInUrl = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseApiVersioning();

app.MapControllers();

app.Run();
