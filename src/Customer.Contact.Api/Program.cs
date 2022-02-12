using Customer.Contact.Core.Entities;
using Customer.Contact.Core.Interfaces;
using Customer.Contact.Core.Services;
using Customer.Contact.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConection");
builder.Services.AddSqlite<AppDbContext>(connectionString);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddLogging();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<IContactReository, ContactRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
