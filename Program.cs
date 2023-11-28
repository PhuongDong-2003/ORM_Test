using Microsoft.EntityFrameworkCore;
using ORMTest.DB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
DatabaseSetting databaseSetting = new DatabaseSetting();
builder.Configuration.GetSection("DatabaseSetting").Bind(databaseSetting);
builder.Services.AddSingleton(databaseSetting);
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(databaseSetting.Connection));
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

app.MapControllers();

app.Run();
