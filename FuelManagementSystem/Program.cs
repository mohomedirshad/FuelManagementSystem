using FuelManagementSystem.Application.Interfaces;
using FuelManagementSystem.Application.Services;
using FuelManagementSystem.BL.Interfaces;
using FuelManagementSystem.BL.Services;
using FuelManagementSystem.Data.Repository;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Cors configuration
builder.Services.AddCors(context =>
{
    context.AddPolicy("AllowCors", opt => opt.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Json
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options => 
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddNewtonsoftJson(op=>op.SerializerSettings.ContractResolver = new DefaultContractResolver());


// DependencyInjection
#region User
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
#endregion

#region FuelStation
builder.Services.AddTransient<IFuelStationService, FuelStationService>();
builder.Services.AddTransient<IFuelStationRepository, FuelStationRepository>();
#endregion

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowCors");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*app.UseHttpsRedirection();*/

app.UseAuthorization();

app.MapControllers();

app.Run();
