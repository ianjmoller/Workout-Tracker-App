using  Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Get the connection string from appsettings.Development.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// This makes the database available to all our controllers
builder.Services.AddTransient<NpgsqlConnection>(_ => new NpgsqlConnection(connectionString));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
