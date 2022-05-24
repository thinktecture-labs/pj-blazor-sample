using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RuntimePerformance.WebApi.Models;
using RuntimePerformance.WebApi.Services;
using RuntimePerformance.WebApi.Utils;
using ProtoBuf.Grpc.Server;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddScoped<ContributionsService>();
builder.Services.AddScoped<ConferencesService>();
builder.Services.AddScoped<SpeakersService>();

builder.Services.AddDbContext<SampleDatabaseContext>(
                options => options.UseInMemoryDatabase(databaseName: "NET6FeaturesDb"));

builder.Services.AddControllers();

builder.Services.AddCodeFirstGrpc(config => { config.ResponseCompressionLevel = CompressionLevel.Optimal; });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Net6Features.Api", Version = "v1" });
});
var app = builder.Build();
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blazor.WASM.Api v1"));
}

// ONLY FOR DEMO
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DataGenerator.InitializeAsync(services);
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseGrpcWeb();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<ContributionsService>().EnableGrpcWeb();
    endpoints.MapGrpcService<ConferencesService>().EnableGrpcWeb();
    endpoints.MapGrpcService<SpeakersService>().EnableGrpcWeb();
    endpoints.MapControllers();
    endpoints.MapFallbackToFile("index.html");
});

app.Run();