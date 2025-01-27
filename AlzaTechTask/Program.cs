global using AlzaTechTask.Data;
using AlzaTechTask.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ProductsContext>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("Database"));
  
});

builder.Services.AddControllers();

 builder.Services.AddApiVersioning(options =>
 { 
     options.AssumeDefaultVersionWhenUnspecified = true;
     options.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
     options.DefaultApiVersion = new ApiVersion(1, 0);
     options.ReportApiVersions = true;

 });


 builder.Services.AddVersionedApiExplorer(options =>
 {
     options.GroupNameFormat = "'v'VVV";
     options.SubstituteApiVersionInUrl = true;
 });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Product API v1", Version = "v1", Description = "Simple API to manage products" });
    options.SwaggerDoc("v2", new OpenApiInfo { Title = "Product API v2", Version = "v2", Description = "Simple API to manage products with pagination" });

    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        var versions = apiDesc.ActionDescriptor.EndpointMetadata
            .OfType<ApiVersionAttribute>()
            .SelectMany(attr => attr.Versions);
           
            
        return versions.Any(v=> $"v{v}"==docName); 
    });
    options.CustomSchemaIds(type=>type.FullName);
});
builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
});
    

builder.Services.AddScoped<IProductService, ProductService>();


var app = builder.Build();
//middleware debugging
var controllerTypes = app.Services.GetServices<ControllerBase>().Select(c => c.GetType()).Distinct();
foreach (var type in controllerTypes)
{
    Console.WriteLine($"Registered Controller: {type.Name}");
}
app.Use(async (context, next) =>
{
    Console.WriteLine($"Request Path: {context.Request.Path}");
    await next();
});

var endpointDataSource = app.Services.GetRequiredService<EndpointDataSource>();
foreach (var endpoint in endpointDataSource.Endpoints)
{
    Console.WriteLine($"Registered Endpoint: {endpoint.DisplayName}");
}

  //  app.UseDeveloperExceptionPage();
    app.UseRouting();  
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json","Products API V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json","Products API V2");

    });

app.UseAuthorization();
app.MapControllers();
app.UseHttpsRedirection();



app.Run();