using Asp.Versioning.Routing;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiVersioning(options => { options.ReportApiVersions = true; });
builder.Services.Configure<RouteOptions>(options => { options.ConstraintMap["apiVersion"] = typeof(ApiVersionRouteConstraint); });
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" }); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1"); });
}

app.UseRouting();
app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.UseHttpsRedirection();
app.Run();