using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Backend.Courses.Domain.Entities;

public class Enrollment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdEnrollment { get; set; } = string.Empty;

    [BsonElement("enrolledAt")]
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    [BsonElement("status")]
    public string Status { get; set; } = "active"; // valores: active | cancelled

    [BsonElement("idUser")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdUser { get; set; } = string.Empty; // referencia a User

    [BsonElement("idCourse")]
    [BsonRepresentation(BsonType.ObjectId)]
    public string IdCourse { get; set; } = string.Empty; // referencia a Course
}
