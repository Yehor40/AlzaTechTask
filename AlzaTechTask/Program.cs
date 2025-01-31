global using AlzaTechTask.Data;
using AlzaTechTask.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;


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
    options.UseNpgsql(builder.Configuration.GetConnectionString("Database")!);
  
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
    options.IncludeXmlComments(Path.Combine(System.AppContext.BaseDirectory,"AlzaTechTask.xml"));
    options.EnableAnnotations();
   
    options.CustomSchemaIds(type=>type.FullName);
});

//DI
builder.Services.AddScoped<IProductService, ProductService>();
var app = builder.Build();

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Products API V1");
        options.SwaggerEndpoint("/swagger/v2/swagger.json", "Products API V2");

    });

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

