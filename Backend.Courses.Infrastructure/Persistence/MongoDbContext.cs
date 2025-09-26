using MongoDB.Driver;
using Backend.Courses.Domain.Entities;

namespace Backend.Courses.Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(MongoSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    public IMongoCollection<Course> Courses => _database.GetCollection<Course>("Courses");
    public IMongoCollection<Lesson> Lessons => _database.GetCollection<Lesson>("Lessons");
    public IMongoCollection<Enrollment> Enrollments => _database.GetCollection<Enrollment>("Enrollments");

    public IMongoDatabase GetDatabase() => _database;
}
