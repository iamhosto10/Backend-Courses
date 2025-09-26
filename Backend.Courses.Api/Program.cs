using Backend.Courses.Infrastructure.Persistence;
using Backend.Courses.Application.Common;
using Backend.Courses.Domain.Entities;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// =====================
// MongoDB Configuration
// =====================
builder.Services.Configure<MongoSettings>(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING") ?? "";
    options.DatabaseName = builder.Configuration["MongoDbSettings:DatabaseName"]!;
});


// Registramos MongoSettings para inyección directa
builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MongoSettings>>().Value);

// Registramos MongoDbContext
builder.Services.AddSingleton<MongoDbContext>();

// =====================
// Repositories
// =====================
builder.Services.AddScoped<IRepository<User>>(sp =>
    new MongoRepository<User>(
        sp.GetRequiredService<MongoDbContext>().GetDatabase(),
        "Users"));

builder.Services.AddScoped<IRepository<Course>>(sp =>
    new MongoRepository<Course>(
        sp.GetRequiredService<MongoDbContext>().GetDatabase(),
        "Courses"));

builder.Services.AddScoped<IRepository<Lesson>>(sp =>
    new MongoRepository<Lesson>(
        sp.GetRequiredService<MongoDbContext>().GetDatabase(),
        "Lessons"));

builder.Services.AddScoped<IRepository<Enrollment>>(sp =>
    new MongoRepository<Enrollment>(
        sp.GetRequiredService<MongoDbContext>().GetDatabase(),
        "Enrollments"));

// =====================
// Controllers & Swagger
// =====================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// =====================
// Middleware
// =====================

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
