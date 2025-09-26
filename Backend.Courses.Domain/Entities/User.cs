using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Courses.Domain.Entities;


public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdUser { get; set; } = string.Empty;

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("password")]
    public string Password { get; set; } = string.Empty;

    [BsonElement("role")]
    public string Role { get; set; } = "student"; // valores: student | teacher
}