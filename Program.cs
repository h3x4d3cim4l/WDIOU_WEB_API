using Microsoft.EntityFrameworkCore;
using WDIOU_WEB_API.Models;
using WDIOU_WEB_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<WDIOUDatabaseSettings>(builder.Configuration.GetSection("WDIOUDatabase"));

builder.Services.AddSingleton<UsersService>();
builder.Services.AddSingleton<usedEmailsService>();
builder.Services.AddSingleton<PersonService>();
builder.Services.AddSingleton<DebtService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(c=> c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
