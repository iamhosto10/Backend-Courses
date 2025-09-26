using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Courses.Domain.Entities;

public class Course
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdCourse { get; set; } = string.Empty;

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("idProfessor")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdProfessor { get; set; } = string.Empty; // referencia a User (profesor)
}
