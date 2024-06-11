using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MedMinder_Api.Data;
using MedMinder_Api.Controllers;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddNewtonsoftJson(s => {
    s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// var sqlConBuilder = new SqlConnectionStringBuilder();
// sqlConBuilder.ConnectionString = builder.Configuration.GetConnectionString("SQLDbConnection");
// builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(sqlConBuilder.ConnectionString));


builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase(databaseName: "MedMinderDb"));

builder.Services.AddScoped<IPatientRepo, PatientRepo>();
builder.Services.AddScoped<TestAsyncActionFilter>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();