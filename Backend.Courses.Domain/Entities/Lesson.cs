using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Courses.Domain.Entities;

public class Lesson
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdLesson { get; set; } = string.Empty;

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("content")]
    public string Content { get; set; } = string.Empty;

    [BsonElement("order")]
    public int Order { get; set; }

    [BsonElement("idCourse")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdCourse { get; set; } = string.Empty; // referencia a Course
}
