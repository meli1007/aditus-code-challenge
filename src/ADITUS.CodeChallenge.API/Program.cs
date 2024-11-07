using System.Text.Json.Serialization;
using ADITUS.CodeChallenge.API.Domain;
using ADITUS.CodeChallenge.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowAllOrigins", builder =>
  {
    builder.AllowAnyOrigin()   // Permite cualquier origen
           .AllowAnyMethod()   // Permite cualquier método (GET, POST, etc.)
           .AllowAnyHeader();  // Permite cualquier cabecera
  });
}); 

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddMemoryCache();

builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IEventService, EventService>();
builder.Services.AddSingleton<IHardwareService, HardwareService>();
builder.Services.AddSingleton<IEventHarwdareService, EventHardwareService>();
builder.Services.AddSingleton<IReservationService, ReservationService>();

builder.Services.AddHttpClient();

var app = builder.Build();


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("AllowAllOrigins");
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
