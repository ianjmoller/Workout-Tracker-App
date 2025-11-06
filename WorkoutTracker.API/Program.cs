// We need to tell our app about the new folder
using WorkoutTracker.Data.Repositories; 

var builder = WebApplication.CreateBuilder(args);

// --- This is the ONLY line we need to register our module ---
// This "registers" our new Repository so the controller can use it.
builder.Services.AddScoped<MuscleGroupRepository>();
// --- End of change ---


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