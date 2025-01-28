global using AlzaTechTask.Data;
using System.Reflection;
using AlzaTechTask.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
// api explorer for versioning
  builder.Services.AddVersionedApiExplorer(options =>
  {
      options.GroupNameFormat = "'v'VVV";
      options.SubstituteApiVersionInUrl = true;
  });
builder.Services.AddDbContext<ProductsContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("Database")!);
  
});

//api versioning
  builder.Services.AddApiVersioning(options =>
  { 
      options.AssumeDefaultVersionWhenUnspecified = true;
      options.ApiVersionReader = new UrlSegmentApiVersionReader();
      options.DefaultApiVersion = new ApiVersion(1, 0);
      options.ReportApiVersions = true;

  });

 
//swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API v1", Version = "v1", Description = "Simple API to manage products" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "Product API v2", Version = "v2", Description = "Simple API to manage products with pagination" });
  
    
   
    options.CustomSchemaIds(type=>type.FullName);
});

//DI
builder.Services.AddScoped<IProductService, ProductService>();
var app = builder.Build();
//middleware debugging




// app.Use(async (context, next) =>
// {
//     Console.WriteLine($"Request Path: {context.Request.Path}");
//     await next();
// });



  //  app.UseDeveloperExceptionPage();
   
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API V1");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "Products API V2");

        });
    }

app.UseHttpsRedirection();
 
app.UseAuthorization();
app.MapControllers();

var apiExplorer = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
foreach (var description in apiExplorer.ApiVersionDescriptions)
{
    Console.WriteLine($"Discovered API Version: {description.GroupName}");
}

var endpointDataSource = app.Services.GetRequiredService<EndpointDataSource>();
foreach (var endpoint in endpointDataSource.Endpoints)
{
    if (endpoint is RouteEndpoint routeEndpoint)
    {
        Console.WriteLine($"Endpoint: {routeEndpoint.DisplayName}");
        Console.WriteLine($"Pattern: {routeEndpoint.RoutePattern.RawText}");
        Console.WriteLine("Metadata:");
        foreach (var metadata in endpoint.Metadata)
        {
            Console.WriteLine($"- {metadata.GetType().Name}");
        }
    }
}

app.Run();

